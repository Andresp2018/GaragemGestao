using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    //T will substitute any class that shows and will be defined as a class
    //Works wih any application
    public interface IGenericRepository<T> where T:class 
    {
        //Will go to any table and gather all data
        IQueryable<T> GetAll();
        //Create the layer access data
        Task<T> GetByIdAsync(int Id);

        //Will create/Update/Delete a registy where the T will be an entity
        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        //Will check if the entity exists
        Task <bool>ExistAsync(int Id);

        //The save action will be inside the GenericRepository class, so there is no need to add it here


    }
}
