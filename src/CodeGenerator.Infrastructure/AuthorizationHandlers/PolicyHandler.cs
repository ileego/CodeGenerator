using CodeGenerator.Infrastructure.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeGenerator.Infrastructure.Options;


namespace CodeGenerator.Infrastructure.AuthorizationHandlers
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        public IAuthenticationSchemeProvider SchemeProvider { get; set; }
        private readonly IRedis _redis;
        private readonly IJwtHelper _jwtHelper;
        readonly ILogger _logger = Log.ForContext<PolicyHandler>();
        private readonly double _expireMinutes;

        private readonly IHttpContextAccessor _httpContextAccessor;

        //private readonly ISystemActionService _systemAction;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemeProvider"></param>
        /// <param name="redis"></param>
        /// <param name="jwtHelper"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="jwtOptions"></param>
        public PolicyHandler(
            IAuthenticationSchemeProvider schemeProvider,
            IRedis redis,
            IJwtHelper jwtHelper,
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtOptions> jwtOptions
            //ISystemActionService systemActionService
            )
        {
            SchemeProvider = schemeProvider;
            _redis = redis;
            _jwtHelper = jwtHelper;
            var jwtOptions1 = jwtOptions.Value;
            _httpContextAccessor = httpContextAccessor;
            _expireMinutes = jwtOptions1.ExpireMinutes;
            //_systemAction = systemActionService;
        }

        /// <summary>
        /// 请求处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PolicyRequirement requirement)
        {
            //Todo：获取角色、Url 对应关系
            List<Menu> list = new List<Menu> {
                new Menu
                {
                    Url = "/logout"
                },
                new Menu
                {
                    Url="/userInfo"
                },
                new Menu
                {
                    Url="/api/v1.0/secret/refresh"
                }
            };

            try
            {
                //var httpContext = (context.Resource as AuthorizationFilterContext)?.HttpContext;
                var httpContext = _httpContextAccessor.HttpContext;
                //获取授权方式
                var defaultAuthenticate = await SchemeProvider.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    //验证签发的用户信息
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    if (result.Succeeded)
                    {
                        if (httpContext != null)
                        {
                            //判断是否为已停用的 Token
                            if (!await _jwtHelper.IsCurrentActiveTokenAsync())
                            {
                                context.Fail();
                            }

                            httpContext.User = result.Principal;

                            //获取 Token
                            var token = result.Ticket.Properties.Items[".Token.access_token"];
                            //缓存超时
                            var cached = await _redis.KeyExistsAsync(token);
                            if (!cached)
                            {
                                context.Fail();
                            }
                            else //缓存延时
                            {
                                //var expiry = TimeSpan.FromMinutes(_expireMinutes);
                                //await _redis.KeyExpireAsync(token, expiry, db: 1);
                            }

                            var user = await (new CurrentUserHelper(_redis, _jwtHelper)).GetUser(token);

                            //判断角色与 Url 是否对应
                            //
                            var url = httpContext.Request.Path.Value.ToLower();
                            // var role = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                            var menu = list.FirstOrDefault(x => x.Url.ToLower().Equals(url));

                            //if (menu == null)
                            //{
                            //    context.Fail();
                            //    return;
                            //}

                            _logger.Warning("登录信息： {login} ,访问地址：{url}", $"【{user.Account}】{user.UserName}", url);

                            //判断是否过期
                            if (DateTime.Parse(httpContext.User.Claims
                                .SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value!) >= DateTime.Now)
                            {
                                context.Succeed(requirement);
                            }
                            else
                            {
                                context.Fail();
                            }
                        }
                        else
                        {
                            context.Fail();
                        }

                        return;
                    }
                }
                context.Fail();
            }
            catch (Exception)
            {
                context.Fail();
            }

        }

        /// <summary>
        /// 测试菜单类
        /// </summary>
        public class Menu
        {
            public string Role { get; set; }

            public string Url { get; set; }
        }
    }
}
