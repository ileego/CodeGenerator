﻿namespace CodeGenerator.Infra.Common.Paging
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PagedModel
    {
        public PagedModel()
        {
            _page = 1;
            _pageSize = 10;
        }

        private int _page;
        private int _pageSize;

        /// <summary>
        /// 第几页
        /// </summary>
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? 10 : value;
        }
    }
}
