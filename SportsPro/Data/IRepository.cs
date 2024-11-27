public interface IRepository<T>
    where T : class
{
    IQueryable<T> GetAll();
    T Get(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);
    void SaveChanges();
}
