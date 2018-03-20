using PlaylistManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistManager.DataAccess
{
    public class BaseRepository<T> where T : class, new()
    {
        private DbContext context;
        private DbSet<T> dbSet;
        
        public BaseRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).FirstOrDefault();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).ToList();
        }

        public void Add(T item)
        {
            context.Entry(item).State = EntityState.Added;
            context.SaveChanges();
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(T item)
        {
            context.Entry(item).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }
    }
}
