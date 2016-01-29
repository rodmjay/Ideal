#region credits
// ***********************************************************************
// Assembly	: Ideal.Core
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Collections.Generic;
using Ideal.Core.Interfaces.Paging;
using Ideal.Core.Model;

namespace Ideal.Core.Common.Paging
{
    #region

    

    #endregion

    public partial class Page<T> : IPage<T> where T : DomainObject
    {
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Entities { get; set; }

        public Page(IEnumerable<T> entities, int count, int pageSize, int currentPage)
        {
            Entities = entities;
            Count = count;
            CurrentPage = currentPage;
            PageSize = pageSize;
            PagesCount = count <= pageSize ? 1 : (count / pageSize) + 1;
        }

        public Page()
        {
        }
    } 
}