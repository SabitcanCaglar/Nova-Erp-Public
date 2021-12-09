using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Common.Paged
{
    internal interface IPagedList<T>
    {
        List<T> Items { get; }
        int PageIndex { set; }
        int PageSize { set; }
        int TotalPage { set; }
        int TotalRecord { set; }
    }
}
