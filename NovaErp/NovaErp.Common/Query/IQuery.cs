using NovaErp.Common.Paged;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NovaErp.Common.Query
{
    public interface IQuery
    {
        T SingleResultModel<T>();
        T SingleResult<T>();
        long ExecuteUpdateOrInsertOrDelete();
        IQuery SetParameter(string name, object val, DbType type);
        IQuery SetParameter(string name, object val);
        long ExecuteInsertId();
        List<T> ToList<T>();
        PagedList<T> ToPagedList<T>(int pageIndex, int pageSize);
    }

}
