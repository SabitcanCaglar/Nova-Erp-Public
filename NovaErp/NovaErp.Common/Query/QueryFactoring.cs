using Dapper;
using NovaErp.Common.Paged;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NovaErp.Common.Query
{
    public class QueryFactoring : ISQLQuery
    {
        private IDbTransaction _transaction;
        private QueryFactoringModel _queryFactoringModel;
        public QueryFactoring(IDbTransaction transaction)
        {
            _transaction = transaction;
            _queryFactoringModel = new QueryFactoringModel();
            _queryFactoringModel.TimeOut = 60;
        }

        public long ExecuteUpdateOrInsertOrDelete()
        {
            return _transaction.Connection.Execute(_queryFactoringModel.Query, _queryFactoringModel.Parameters, _transaction, _queryFactoringModel.TimeOut, CommandType.Text);
        }

        public long ExecuteInsertId()
        {
            //var newQuery = string.Concat(_queryFactoringModel.Query, "; SELECT CAST(SCOPE_IDENTITY() as int)");
            var newQuery = string.Concat(_queryFactoringModel.Query, " RETURNING id;");
            
            return _transaction.Connection.QuerySingle<int>(newQuery, _queryFactoringModel.Parameters, _transaction, _queryFactoringModel.TimeOut, CommandType.Text);
        }

        public IQuery SetParameter(string name, object val, DbType type)
        {
            _queryFactoringModel.Parameters.Add(name, val,type);
            return this;
        }

        public IQuery SetParameter(string name, object val)
        {
            _queryFactoringModel.Parameters.Add(name, val);
            return this;
        }

        public IQuery SQLQuery(string queryString)
        {
            _queryFactoringModel.Query = queryString;
            return this;
        }
       
        public List<T> ToList<T>()
        {
            var result = _transaction?.Connection.Query<T>(_queryFactoringModel.Query, _queryFactoringModel.Parameters, _transaction, false, _queryFactoringModel.TimeOut, CommandType.Text).ToList();
            return result;
        }
       
        public T SingleResultModel<T>()
        {
            return _transaction.Connection.QuerySingle<T>(_queryFactoringModel.Query, _queryFactoringModel.Parameters, _transaction, _queryFactoringModel.TimeOut, CommandType.Text);

        }
        public T SingleResult<T>()
        {
          return  _transaction.Connection.ExecuteScalar<T>(_queryFactoringModel.Query, _queryFactoringModel.Parameters, _transaction, _queryFactoringModel.TimeOut, CommandType.Text);

        }

        PagedList<T> IQuery.ToPagedList<T>(int pageIndex, int pageSize)
        {
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (_queryFactoringModel.Query.TrimEnd().EndsWith(";"))
            {
                _queryFactoringModel.Query = _queryFactoringModel.Query.TrimEnd().TrimEnd(';');
            }

            if (string.IsNullOrEmpty(_queryFactoringModel.Query))
            {
                throw new Exception("T-Sql Sorgusu bulunamadı");
            }
            var rowCountSql = $"SELECT COUNT(*) AS TotalCount FROM ({_queryFactoringModel.Query}) AS originalQueryRowCount;";
            var monitorsql = "";
            if (pageSize == int.MaxValue)
            {
                monitorsql = _queryFactoringModel.Query + ";";
            }
            else
            {
                var startRow = (pageIndex - 1) * pageSize;
                monitorsql = string.Format(_queryFactoringModel.Query + " OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY;", startRow, pageSize);
            }
            string sql = rowCountSql + monitorsql;
            var multiresult = _transaction?.Connection.QueryMultiple(sql, _queryFactoringModel.Parameters, _transaction);
            var resulttotal = multiresult.ReadSingle<int>();
            var resultset = multiresult.Read<T>(false).ToList();
            var totalPage = Math.Ceiling(Convert.ToDouble(resulttotal.ToString()) / Convert.ToDouble(pageSize.ToString()));

            return new PagedList<T>(resultset, pageSize, pageIndex, resulttotal,
                Convert.ToInt32(totalPage));
        }
    }
}
