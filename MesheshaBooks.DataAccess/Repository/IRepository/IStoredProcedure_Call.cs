using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MesheshaBooks.DataAccess.Repository.IRepository
{
    public interface IStoredProcedure_Call : IDisposable
    {
        T Single<T>(string procedureName, DynamicParameters)
    }
}
