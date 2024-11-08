using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ItRootsTask_Core.Interfaces
{
    public interface IDapperManager<T> /*: IDisposable */ where T : class
    {
        DbConnection GetConnection();

        Task<T> GetByIdAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetAllAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        //Task<IReadOnlyList<T>> GetPagedReponseAsync(string sp,int pageNumber, int pageSize,DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<SqlMapper.GridReader> MultipleQuery(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, int timeout = 0);
        //Task MultipleQuery<f, s, t>(string sp, Func<f, s, t> map, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);


        Task<T> AddAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<int> UpdateAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<int> DeleteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);


        Task<T> ExecuteScalarAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<int> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);


        Task<DataSet> FillDs(string sp, Dictionary<string, object> parameters, CommandType commandType = CommandType.StoredProcedure);


    }
}
