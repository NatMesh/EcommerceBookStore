using System;
using System.Collections.Generic;
using System.Text;

namespace MesheshaBooks.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IStoredProcedure_Call StoredProcedure_Call { get; }
    }
}
