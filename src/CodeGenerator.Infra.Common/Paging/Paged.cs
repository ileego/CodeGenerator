using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Infra.Common.Paging
{
    public class Paged
    {
        /// <summary>
        /// AutoMapper IConfigurationProvider
        /// </summary>
        private readonly IConfigurationProvider _mapperProvider;

        /// <summary>
        /// 临时添加Mapper配置
        /// </summary>
        /// <typeparam name="TSourceType"></typeparam>
        /// <typeparam name="TDestinationType"></typeparam>
        public void AddMap<TSourceType, TDestinationType>()
            => _mapperProvider.ResolveTypeMap(typeof(TSourceType), typeof(TDestinationType));

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configurationProvider">MapperConfiguration</param>
        public Paged(IConfigurationProvider configurationProvider)
        {
            _mapperProvider = configurationProvider;
        }

        public async Task<PagedResult<TValue>> GetPagedAsync<TValue>(IQueryable<TValue> query,
                                         int page, int pageSize) where TValue : class
        {
            var result = new PagedResult<TValue>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.CountAsync()
            };


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        public async Task<PagedResult<TU>> GetPagedAsync<TValue, TU>(IQueryable<TValue> query,
                                            int page, int pageSize) where TU : class
        {
            var result = new PagedResult<TU>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await query.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip)
                                  .Take(pageSize)
                                  .ProjectTo<TU>(_mapperProvider)
                                  .ToListAsync();

            return result;
        }
    }
}
