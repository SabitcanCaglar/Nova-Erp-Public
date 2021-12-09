namespace Nova.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();

    }
}
