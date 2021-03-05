using MesheshaBooks.DataAccess.Data;
using MesheshaBooks.DataAccess.Repository.IRepository;
using MesheshaBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesheshaBooks.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// Update an existing row/object in our database
        /// </summary>
        /// <param name="category">The Category object we want to update.</param>
        public void Update(Category category)
        {
            //We are using LINQ to find the specific row I want to update
                //s represents a generic entity from the Categories table
                //For that entity s we are grabbing the Id via it's GET property
                //Then comparing it to the Category object's Id that we are passing as a parameter.
                //For all those records where the Id matches the Category object's Id retrieve the first one
                    //Note: we should only have one object returned since the Id is a UNIQUE value aka only one
                //If the id doesnt exist it will return null.
            var objFromDb = _db.Categories.FirstOrDefault(s => s.Id == category.Id);
            //confirms we found an object in our Categories table with matching Id
            if(objFromDb != null)
            {
                //Once we have the Category object we want to update we can then change the name value on that row/object.
                objFromDb.Name = category.Name;

                //Lastly we must save the changes so they persist to our database
                _db.SaveChanges();
            }
        }
    }
}
