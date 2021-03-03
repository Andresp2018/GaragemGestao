﻿using GaragemGestao.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    public class GenericRepositoryEmployee<T> : IGenericRepository<T> where T : Employee //Since it cannot inherit Struct and class
    {
        private DataContext _context;

        //Constructor with the Attribute
        public GenericRepositoryEmployee(DataContext context)
        {
            _context = context;
        }
        //Generic Implementation - AsNoTracking will pass the data and detaches
        //from the database closing the connection
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveAllAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        public async Task<bool> ExistAsync(int Id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == Id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
