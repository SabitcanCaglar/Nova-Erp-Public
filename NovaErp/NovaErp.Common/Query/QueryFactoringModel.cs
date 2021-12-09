using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Common.Query
{
    public class QueryFactoringModel
    {
        public QueryFactoringModel()
        {
            Parameters = new DynamicParameters();
        }
        public string Query { get; set; }
        public int? TimeOut { get; set; }
        public DynamicParameters Parameters { get; set; }
    }
}
