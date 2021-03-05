using MesheshaBooks.DataAccess.Data;
using MesheshaBooks.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MesheshaBooks.DataAccess.Repository
{
    /// <summary>
    /// Repository class which contains methods from interface IRepository for common CRUD operations.
    /// </summary>
    /// <typeparam name="T">T is a generic to ensure type safety for our class.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        //Whenever we need to modify the database we need our DbContext object.
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        //We will use 
        /// <summary>
        /// Constructor method for our Repository which uses dependency injection via constructor injection to 
        /// get the applicationDbContext object.
        /// </summary>
        /// <param name="db">A ApplicationDbContext object which will give us access to our database to preform CRUD operations.</param>
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        /// <summary>
        /// This method lets us add a row to a database table.
        /// </summary>
        /// <param name="entity">This is an object which is equivalent to a table row to be added to our database table.</param>
        void IRepository<T>.Add(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Retrieves a single row from a database table.
        /// </summary>
        /// <param name="id">The primary key value for a specific row in a database table.</param>
        /// <returns>A class object which represents a table row.</returns>
        T IRepository<T>.Get(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Returns a collection of objects/ database table rows.
        /// </summary>
        /// <param name="filter">The conditions for our where clause in our query.</param>
        /// <param name="orderBy">The condition by which our collection of objects will be ordered.</param>
        /// <param name="includeProperties">Related entities(tables) which can be loaded along with the main table being
        /// queried.</param>
        /// <returns>Returns a collection of objects.</returns>
        IEnumerable<T> IRepository<T>.GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                /*Eager loading is the process whereby a query for one type of entity also loads related entities 
                 as part of the query. Eager loading is achieved by use of the Include method. For example, the 
                 queries below will load blogs and all the posts related to each blog in a single load*/
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        /// <summary>
        /// Returns the first object/row from a database table .
        /// </summary>
        /// <param name="filter">The conditions for our where clause in our query.</param>
        /// <param name="includeProperties">Related entities(tables) which can be loaded along with the main table being
        /// queried.</param>
        /// <returns>Returns the first object/row from a query.</returns>
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Removes/Deletes a row from a database table based on the id/PK passed.
        /// </summary>
        /// <param name="id">Unique identifier aka Primary key to identify a row from a database table.</param>
        void IRepository<T>.Remove(int id)
        {
            T entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Removes/Deletes a row from a database table based on the object/row passed.
        /// </summary>
        /// <param name="entity">A object which represents a row from a database table.</param>
        void IRepository<T>.Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// This will remove/delete multiple rows from a database table.
        /// </summary>
        /// <param name="entity">A collection of objects which represents a collection of rows from a database table.</param>
        void IRepository<T>.RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
