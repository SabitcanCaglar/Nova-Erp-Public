using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Common.Query
{
    public interface ISQLQuery :IQuery
    {
        IQuery SQLQuery(string queryString);

    }
}
