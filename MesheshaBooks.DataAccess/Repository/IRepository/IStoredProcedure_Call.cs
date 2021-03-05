using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MesheshaBooks.DataAccess.Repository.IRepository
{
    public interface IStoredProcedure_Call : IDisposable
    {
        //This returns an integer value or boolean value
        T Single<T>(string procedureName, DynamicParameters param = null);

        void Execute(string procedureName, DynamicParameters param = null);

        //Retrieves the complete row/object
        T OneRecord<T>(string procedureName, DynamicParameters param = null);

        //This returns all of the rows from a table
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);

        //This returns data from 2 tables
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null);
    }
}
