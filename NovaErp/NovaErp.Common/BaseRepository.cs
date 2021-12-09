using Nova.Core.UnitOfWork;
using NovaErp.Common.Query;
using System;
using System.Text;

namespace NovaErp.Common
{
    public abstract class BaseRepository
    {
        protected System.Data.IDbTransaction Transaction { get; set; }

        protected BaseRepository(UnitOfWork unitOfWork)
        {
            Transaction = unitOfWork.Transaction;
        }
        protected ISQLQuery DbAction => new QueryFactoring(Transaction);
     
    }
}
