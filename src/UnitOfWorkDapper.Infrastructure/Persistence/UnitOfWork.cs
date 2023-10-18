using UnitOfWorkDapper.Core.Interfaces.UnitOfWork;

namespace UnitOfWorkDapper.Infrastructure.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DapperDbSession _session;

        public UnitOfWork(DapperDbSession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_session.Transaction != null)
            {
                _session.Transaction.Commit();
                Dispose();
            }
        }

        public void Rollback()
        {
            if (_session.Transaction != null)
            {
                _session.Transaction.Rollback();
                Dispose();
            }
        }

        public void Dispose() => _session.Transaction?.Dispose();


    }
}
