

namespace RepositoryLayer.Repostories.Interface
{
    public interface IRepository<T>
    {
        void Create(T data);
        void Uptade(T data);
        void Delete(T data);
        T GetById(int id);
        
    }
}
