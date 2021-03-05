using MesheshaBooks.DataAccess.Data;
using MesheshaBooks.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MesheshaBooks.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            StoredProcedure_Call = new StoredProcedure_Call(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IStoredProcedure_Call StoredProcedure_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
