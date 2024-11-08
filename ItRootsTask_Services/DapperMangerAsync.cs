using Dapper;
using ItRootsTask_Core.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItRootsTask_Services
{
    public class DapperManagerAsync<T> : IDapperManager<T> where T : class
    {
        private readonly string connectionstring;

        public DapperManagerAsync(IConfiguration configuration)
        {
            this.connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<T> AddAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result = default(T);

            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                try
                {
                    if (db.State == ConnectionState.Closed) { db.Open(); }
                    using (var trans = db.BeginTransaction())
                    {
                        try
                        {
                            result = await db.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType, transaction: trans);
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (db.State == ConnectionState.Open)
                        db.Close();
                }

            }

            return result;
        }

        public async Task<int> DeleteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return await db.ExecuteAsync(sp, parms, commandType: commandType);
            }
        }

        public async Task<T> ExecuteScalarAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                try
                {
                    return await db.ExecuteScalarAsync<T>(sp, parms, commandType: commandType);

                }
                catch (Exception ex)
                { throw ex; }
            }
        }
        public async Task<int> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return await db.ExecuteAsync(sp, parms, commandType: commandType);

            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return await db.QueryAsync<T>(sp, parms, commandType: commandType);

            }
        }

        public async Task<T> GetByIdAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return await db.QueryFirstAsync<T>(sp, parms, commandType: commandType);
            }
        }

        public DbConnection GetConnection()
        {
            return new SqlConnection(connectionstring);
        }

        public async Task<SqlMapper.GridReader> MultipleQuery(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, int timeout = 0)
        {

            //using (IDbConnection db = new SqlConnection(connectionstring))
            //{
            SqlMapper.GridReader reader = null;

            IDbConnection db = new SqlConnection(connectionstring);
            //{
            if (timeout != 0)
            {

                reader = await db.QueryMultipleAsync(sp, parms, commandType: commandType, commandTimeout: timeout);

            }
            else
            {
                reader = await db.QueryMultipleAsync(sp, parms, commandType: commandType);
            }
            // }
            return reader;

            //var data = db.QueryAsync<f,s,t>(sp, map, parms,commandType: commandType);
            //}

        }

        public async Task<int> UpdateAsync(string sp, DynamicParameters parms, CommandType commandType)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return await db.ExecuteAsync(sp, parms, commandType: commandType);
            }
        }

        public async Task<DataSet> FillDs(string sp, Dictionary<string, object> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DataSet Ds = new DataSet();
            using (SqlConnection db = new SqlConnection(connectionstring))
            {
                if (db.State == ConnectionState.Closed) { await db.OpenAsync(); }
                using (SqlDataAdapter DA = new SqlDataAdapter(sp, db))
                {
                    var cmd = DA.SelectCommand;
                    cmd.Parameters.Clear();
                    cmd.CommandType = commandType;
                    foreach (var p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }

                    DA.Fill(Ds);
                }
            }
            return Ds;
        }
    }
}

