using DataAccess.Data;
using DataAccess.Repositery.IRepositery;
using Microsoft.EntityFrameworkCore;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositery
{
    public class Repositery<T> : IRepositery<T> where T: class
    {
        private readonly ExamsPaltFormDbContext _db;
        DbSet<T> dbset;
        public Repositery(ExamsPaltFormDbContext db) 
        {
            _db = db;
            this.dbset = _db.Set<T>();
             
        }

        public void Create(T entity)
        {
            dbset.Add(entity);

        }

        public void Delete(T entity)
        {
            dbset.Remove(entity);
             
        }

        public void DeleteAll(List<T> entity)
        {
            dbset.RemoveRange(entity);
          
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>>? filter,string? include = null)
        {
           IQueryable<T> query =  dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(include != null)
            {
                foreach (var includeproperty in include.Split(new char[] {','} ,StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
            }
        
         
            return query.FirstOrDefault();
        }

        public List<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, string? include = null)
        {
            IQueryable<T> query = dbset;
            
            if(filter != null) { query = query.Where(filter); }
            if (include != null)
            {
                foreach (var includeproperty in include.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeproperty);
                }
               
            }

            return query.ToList();
        }
    }
}









