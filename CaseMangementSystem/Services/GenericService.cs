using CaseMangementSystem.Contexts;
using CaseMangementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Services
{
    internal abstract class GenericService<TEntity> where TEntity : class
    {
        private readonly DataContext _context = new DataContext();


        // Generic GetAllData using Virutal Async to be able to change override the function
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        //Get Specific userdata or casedata
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var item = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, CancellationToken.None);
            if (item != null)
            {
                return item;
            }
            return null!;
        }

        // Save new data or send back the data if it already exists
        public virtual async Task<TEntity> SaveAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var item = await GetAsync (predicate);
            if (item == null)
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return item;
        }

        //Remove a specific Entitydata or return null if nothing is removed
        public virtual async Task<TEntity> DeleteAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var item = await GetAsync (predicate);
            if (item != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null!;
        }

  
    }
}
