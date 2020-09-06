using FoodBlog.Common;
using FoodBlog.Entities;
using FoodBlog.Main.DbAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodBlog.DAL.EntityFramework
{
    public class Repository<T> : IDbAccess<T> where T: class
    {
        private DatabaseContext db;
        private DbSet<T> _objectSet;

        public Repository()
        {
            db = RepositoryBase.CreateContext();
            _objectSet = db.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            if(obj is EntityBase)
            {
                EntityBase en = obj as EntityBase;
                DateTime time = DateTime.Now;

                en.Created = time;
                en.Modified = time;
                en.ModifiedUser = App.common.GetUsername();
            }
            return Save();
        }

        public int Update(T obj)
        {
            if (obj is EntityBase)
            {
                EntityBase en = obj as EntityBase;

                en.Modified = DateTime.Now;
                en.ModifiedUser = App.common.GetUsername();
            }
            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return db.SaveChanges();
        }

    }
}
