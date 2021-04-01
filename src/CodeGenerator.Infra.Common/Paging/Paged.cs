using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Infra.Common.Paging
{
    public class Paged
    {
        /// <summary>
        /// 需要注入Automapper configuration provider
        /// </summary>
        private readonly IConfigurationProvider _mapperConfigurationProvider;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configurationProvider">MapperConfigurationProvider</param>
        public Paged(IConfigurationProvider configurationProvider)
        {
            _mapperConfigurationProvider = configurationProvider;
        }

        public PagedResult<TValue> GetPaged<TValue>(IQueryable<TValue> query,
                                         int page, int pageSize) where TValue : class
        {
            var result = new PagedResult<TValue>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public PagedResult<TU> GetPaged<TValue, TU>(IQueryable<TValue> query,
                                            int page, int pageSize) where TU : class
        {
            var result = new PagedResult<TU>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip)
                                  .Take(pageSize)
                                  .ProjectTo<TU>(_mapperConfigurationProvider)
                                  .ToList();

            return result;
        }
    }
}
