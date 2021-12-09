
using Nova.Core.UnitOfWork;
using NovaErp.Common.Context;
using Npgsql;
using System.Data.SqlClient;

namespace NovaErp.Repository.RepositoryContext
{

    public sealed class RepositoryContext
    {
        public static DbContext New()
        {
            return new DbContext(new UnitOfWorkFactory<NpgsqlConnection>(ConnnectionString.Get));
        }
       
    }
    
}
