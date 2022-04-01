using AlmedalGameStore.DataAccess.GenericRepository.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlmedalGameStore.DataAccess.GenericRepository
    //Repository är de sista stället där man kan interacta med databasen
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        //alla transaktioner sker genom ApplicationDbContext genom en DbSet 
        //Så är gör vi en DbSet som är till alla classer
        internal DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            
            //sätter dbSet till den klassen som anropar vårt repository "Basic setup"
            this.dbSet = _db.Set<T>();

        }
        public void Add(T entity)
        {
            //ett exempel här är _db.Genre.Add(entity) är samma som det nedan men mer enkelt
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            //komplett dbset!, Assigns to a class so if it directly return the db set it will be sufficent
            // includeProp - "Genre,Fler klasser" 
            IQueryable<T> query = dbSet;
            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] { ','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            //komplett dbset!, Assigns to a class so if it directly return the db set it will be sufficent
            IQueryable<T> query = dbSet;

            
            
            if (includeProperties != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {      
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
