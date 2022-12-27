using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T">Kiểu model truyền vào</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(dynamic id, T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(dynamic id);
        
        Task<PagingData<T>> GetPagingAsync(int pageIndex, int pageSize, string keywords);
        /// <summary>
        /// Lấy danh sách phân trang theo điều kiện. Column là tên cột trong table database, condition là biểu thức điều kiện.
        /// Ví dụ column = "AccountType", condition = "LIKE 'MENTOR'".
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="column"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        Task<PagingData<T>> GetPagingWithConditionAsync(int pageIndex, int pageSize, string column, string condition);
        Task<T> GetSingleAsync(List<KeyValuePair<string, dynamic>> list);

        Task<int> DeleteByIdAsync(dynamic id);

        Task<IEnumerable<T>> GetMultiByIdAsync(List<dynamic> listId);

        Task<IEnumerable<T>> GetMultiByConditionAsync(string column, dynamic value);

        Task<int> RemoveByIdAsync(dynamic id);

    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly string _connectionString = "";
        protected readonly string _table = "";

        public RepositoryBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
            _table = typeof(T).Name; // Lấy tên của đối tượng => tên bảng
        }


        public async Task<IEnumerable<T>> GetMultiByConditionAsync(string column, dynamic value)
        {
            var columns = new List<KeyValuePair<string, dynamic>>();
            columns.Add(new KeyValuePair<string, dynamic>(column, value));
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listColumn", JsonConvert.SerializeObject(columns));
                    return await conn.QueryAsync<T>("GET_MULTI_DATA", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public virtual async Task<IEnumerable<T>> GetMultiByIdAsync(List<dynamic> listId)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listId", JsonConvert.SerializeObject(listId));
                    return await conn.QueryAsync<T>("GET_DATA_BY_ID", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            var query = GenerateInsertQuery(entity);
            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.QuerySingleOrDefaultAsync<T>(query, null, null, null, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public virtual async Task<T> UpdateAsync(dynamic id, T entity)
        {
            string query = GenerateUpdateQuery(id, entity);
            using (var conn = CreateConnection())
            {
                try
                {
                    await conn.ExecuteAsync(query, null, null, null, CommandType.Text);
                    return entity;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {_table} ";
            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.QueryAsync<T>(query, null, null, null, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public virtual async Task<T> GetByIdAsync(dynamic id)
        {
            var query = $"SELECT * FROM {_table} WHERE Id = '{id}' ";
            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.QueryFirstOrDefaultAsync<T>(query, null, null, null, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public async Task<PagingData<T>> GetPagingAsync(int pageIndex, int pageSize, string keywords)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@pageIndex", pageIndex);
                    parameters.Add("@pageSize", pageSize);
                    parameters.Add("@keywords", keywords);
                    parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await conn.QueryAsync<T>("GET_PAGING_DATA", parameters, null, null, CommandType.StoredProcedure);
                    int totalRow = parameters.Get<int>("@totalRow");
                    return new PagingData<T>()
                    {
                        Data = result,
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        Keywords = keywords,
                        TotalRow = totalRow
                    };
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<PagingData<T>> GetPagingWithConditionAsync(int pageIndex, int pageSize, string column, string condition)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@pageIndex", pageIndex);
                    parameters.Add("@pageSize", pageSize);
                    parameters.Add("@columns", column);
                    parameters.Add("@condition", condition);
                    parameters.Add("@totalRow", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    var result = await conn.QueryAsync<T>("GET_PAGING_DATA_WITH_CONDITION", parameters, null, null, CommandType.StoredProcedure);
                    int totalRow = parameters.Get<int>("@totalRow");
                    return new PagingData<T>()
                    {
                        Data = result,
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        Keywords = "",
                        TotalRow = totalRow
                    };
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public virtual async Task<int> DeleteByIdAsync(dynamic id)
        {
            var query = $"DELETE FROM {_table} WHERE Id = '{id}'";
            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.ExecuteAsync(query, null, null, null, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public virtual async Task<int> RemoveByIdAsync(dynamic id)
        {
            var query = $"DELETE FROM {_table} WHERE Id = '{id}'";
            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.ExecuteAsync(query, null, null, null, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public async Task<T> GetSingleAsync(List<KeyValuePair<string, dynamic>> list)
        {
            var query = GenerateSelectSingleQuery(list);
            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.QueryFirstOrDefaultAsync<T>(query, null, null, null, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        protected SqlConnection CreateConnection()
        {
            var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        protected string GenerateInsertQuery(T entity)
        {
            try
            {
                var props = entity.GetType().GetProperties();
                var columns = "";
                var values = "";

                var index = props[0].PropertyType.Name == "String" ? 0 : 1;
                for (var i = index; i < props.Length; i++)
                {
                    if (!props[i].PropertyType.IsPrimitive
                        && props[i].PropertyType.Name != "String"
                        && props[i].PropertyType.Name != "DateTime") continue;
                    columns += $"{props[i].Name}, ";
                    values += $"N'{props[i].GetValue(entity)}', ";
                }

                columns = columns.Remove(columns.Length - 2, 2);
                values = values.Remove(values.Length - 2, 2);

                var query = $"INSERT INTO {_table} ({columns}) VALUES ({values});";
                query += $"SELECT TOP 1 * FROM {_table} ORDER BY Id DESC;";
                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        protected string GenerateUpdateQuery(dynamic id, T entity)
        {
            try
            {
                var props = entity.GetType().GetProperties();
                var query = $"UPDATE {_table} SET ";
                for (var i = 1; i < props.Length; i++)
                {
                    if(props[i].GetValue(entity) != null)
                    {
                        query += $"{props[i].Name} = N'{props[i].GetValue(entity)}', ";
                    }
                }
                query = query.Remove(query.Length - 2, 2);
                query += $" WHERE Id = '{id}'";
                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        protected string GenerateSelectSingleQuery(List<KeyValuePair<string, dynamic>> list)
        {
            var query = $"SELECT TOP 1 * FROM {_table} WHERE ";
            foreach (var element in list) query += $"{element.Key} = N'{element.Value}' AND ";

            query = query.Substring(0, query.Length - 4);

            return query;
        }
    }
}