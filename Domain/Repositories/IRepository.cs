namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetItem(ulong id);
        bool Create(T item);
        bool Update(T item);
        bool Delete(ulong id);
        void Save();
    }
}