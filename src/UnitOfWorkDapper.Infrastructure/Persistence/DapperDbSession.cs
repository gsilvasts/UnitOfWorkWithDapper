using System.Data;
using System.Data.SqlClient;

namespace UnitOfWorkDapper.Infrastructure.Persistence
{
    public sealed class DapperDbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        public DapperDbSession(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();

    }
}
