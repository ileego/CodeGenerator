using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeGenerator.Infra.Common.Extensions
{
    public static class InjectAssemblyExtension
    {
        /// <summary>
        /// Inject Assembly
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="lifetime"></param>
        /// <param name="startWith"></param>
        /// <param name="endWith"></param>
        public static void AddAssembly(this IServiceCollection services,
            Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped, string startWith = null, string endWith = null)
        {
            var interfaces = assembly.GetTypes().Where(t => t.IsInterface);
            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract).ToList();
            if (!string.IsNullOrEmpty(startWith))
            {
                interfaces = interfaces.Where(t => t.Name.StartsWith(startWith));
            }
            if (!string.IsNullOrEmpty(endWith))
            {
                interfaces = interfaces.Where(t => t.Name.EndsWith(endWith));
            }

            foreach (var item in interfaces)
            {
                var name = item.Name.Substring(1);
                var impl = types.FirstOrDefault(t => t.Name.Equals(name));
                if (impl != null)
                {
                    ServiceDescriptor sd = new ServiceDescriptor(item, impl, lifetime);
                    services.Add(sd);
                }
            }
        }

        /// <summary>
        /// Inject Scope Assembly
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="startWith"></param>
        /// <param name="endWith"></param>
        public static void AddScopeAssembly(this IServiceCollection services, Assembly assembly, string startWith = null, string endWith = null)
        {
            var interfaces = assembly.GetTypes().Where(t => t.IsInterface);
            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract).ToList();
            if (!string.IsNullOrEmpty(startWith))
            {
                interfaces = interfaces.Where(t => t.Name.StartsWith(startWith));
            }
            if (!string.IsNullOrEmpty(endWith))
            {
                interfaces = interfaces.Where(t => t.Name.EndsWith(endWith));
            }

            foreach (var item in interfaces)
            {
                var name = item.Name.Substring(1);
                var impl = types.FirstOrDefault(t => t.Name.Equals(name));
                if (impl != null)
                {
                    services.AddScoped(item, impl);
                }
            }
        }

        /// <summary>
        /// Inject Transient Assembly
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="startWith"></param>
        /// <param name="endWith"></param>
        public static void AddTransientAssembly(this IServiceCollection services, Assembly assembly, string startWith = null, string endWith = null)
        {
            var interfaces = assembly.GetTypes().Where(t => t.IsInterface);
            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract).ToList();
            if (!string.IsNullOrEmpty(startWith))
            {
                interfaces = interfaces.Where(t => t.Name.StartsWith(startWith));
            }
            if (!string.IsNullOrEmpty(endWith))
            {
                interfaces = interfaces.Where(t => t.Name.EndsWith(endWith));
            }

            foreach (var item in interfaces)
            {
                var name = item.Name.Substring(1);
                var impl = types.FirstOrDefault(t => t.Name.Equals(name));
                if (impl != null)
                {
                    services.AddTransient(item, impl);
                }
            }
        }
    }
}
