using System;
using System.Collections.Generic;
using FoodBlog.Main.DbAccess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FoodBlog.DAL.EntityFramework;

namespace FoodBlog.BusinessLayer
{
    public abstract class MainManager<T> : IDbAccess<T> where T: class
    {
        private Repository<T> _repo = new Repository<T>();

        public virtual int Delete(T obj)
        {
            return _repo.Delete(obj);
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            return _repo.Find(where);
        }

        public virtual int Insert(T obj)
        {
            return _repo.Insert(obj);
        }

        public virtual List<T> List()
        {
            return _repo.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> where)
        {
            return _repo.List(where);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return _repo.ListQueryable();
        }

        public virtual int Save()
        {
            return _repo.Save();
        }

        public virtual int Update(T obj)
        {
            return _repo.Update(obj);
        }
    }
}
