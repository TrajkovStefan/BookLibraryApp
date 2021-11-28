using System.Collections.Generic;

namespace Seavus.BookLibrary.DataAccess.Interfaces
{
    public interface IRepository<T>
    {
        //CRUD METHODS
        List<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}