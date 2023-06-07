#region Assembly Copernicus.NetCore.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// C:\Users\sergio.hernandez\.nuget\packages\copernicus.netcore.common\1.0.0\lib\net6.0\Copernicus.NetCore.Common.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Domain.Configuration
{
    public class Sql
    {
        public class DatabaseConnection : IDatabaseConnection, IDisposable
        {
            private ISqlConfig _config;

            private bool disposedValue;

            public IDbConnection Connection { get; private set; }

            public IDbTransaction Transaction { get; set; }

            public DatabaseConnection(ISqlConfig config)
            {
                _config = config;
            }

            public IDatabaseConnection GetInstance()
            {
                return new DatabaseConnection(_config);
            }

            public IDatabaseConnection CreateConnection()
            {
                try
                {
                    SqlConnection sqlConnection = new SqlConnection(_config.CnnString);
                    sqlConnection.Open();
                    Connection = sqlConnection;
                    return this;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void CloseConnection()
            {
                Connection.Close();
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue && disposing && Connection != null)
                {
                    Connection.Close();
                }

                disposedValue = true;
            }

            public void Dispose()
            {
                Dispose(disposing: true);
            }
        }

        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            private IDatabaseConnection DbConnection { get; }

            public string QueryString { get; set; }

            public object? Param { get; set; }

            public GenericRepository(IDatabaseConnection DbConnection)
            {
                this.DbConnection = DbConnection;
            }

            public async Task<IEnumerable<T>> FindAsync()
            {
                try
                {
                    IEnumerable<T> result;
                    using (IDatabaseConnection dbConn = DbConnection.GetInstance())
                    {
                        using IDatabaseConnection conn = dbConn.CreateConnection();
                        IDbConnection connection = conn.Connection;
                        string queryString = QueryString;
                        object? param = Param;
                        CommandType? commandType = CommandType.Text;
                        result = await connection.QueryAsync<T>(queryString, param, null, null, commandType);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task<T> FindFirstOrDefaultAsync()
            {
                try
                {
                    T result;
                    using (IDatabaseConnection dbConn = DbConnection.GetInstance())
                    {
                        using IDatabaseConnection conn = dbConn.CreateConnection();
                        IDbConnection connection = conn.Connection;
                        string queryString = QueryString;
                        object? param = Param;
                        CommandType? commandType = CommandType.Text;
                        result = await connection.QueryFirstOrDefaultAsync<T>(queryString, param, null, null, commandType);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task<int> ExecuteAsync()
            {
                try
                {
                    return await DbConnection.Connection.ExecuteAsync(QueryString, Param, DbConnection.Transaction);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task<int> ExecuteAsync(T entity)
            {
                try
                {
                    return await DbConnection.Connection.ExecuteAsync(QueryString, entity, DbConnection.Transaction);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task<int> ExecuteScalarAsync(T entity)
            {
                try
                {
                    return await DbConnection.Connection.ExecuteScalarAsync<int>(QueryString, entity, DbConnection.Transaction);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task<int> ExecuteScalarAsync()
            {
                try
                {
                    return await DbConnection.Connection.ExecuteScalarAsync<int>(QueryString, Param, DbConnection.Transaction);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public async Task<int> ExecuteAsync(IEnumerable<T> entityList)
            {
                try
                {
                    return await DbConnection.Connection.ExecuteAsync(QueryString, entityList, DbConnection.Transaction);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public interface IDatabaseConnection : IDisposable
        {
            IDbConnection Connection { get; }

            IDbTransaction Transaction { get; set; }

            IDatabaseConnection GetInstance();

            IDatabaseConnection CreateConnection();

            void CloseConnection();
        }

        public interface IGenericRepository<T> where T : class
        {
            string QueryString { get; set; }

            Task<IEnumerable<T>> FindAsync();

            Task<T> FindFirstOrDefaultAsync();

            Task<int> ExecuteAsync();

            Task<int> ExecuteAsync(T entity);

            Task<int> ExecuteScalarAsync(T entity);

            Task<int> ExecuteScalarAsync();

            Task<int> ExecuteAsync(IEnumerable<T> entityList);
        }

        public interface ISqlConfig
        {
            string CnnString { get; set; }
        }

        public interface IUnitOfWork : IDisposable
        {
            void SaveChanges();

            void UndoChanges();
        }

        public interface IUnitOfWorkFactory
        {
            T GetRepository<T>();

            IUnitOfWorkFactory Create();

            void SaveChanges();

            void UndoChanges();
        }

        public interface IUnitOfWorkRepository
        {
            T GetRepository<T>(IDatabaseConnection connection);
        }

        public class SqlConfig : ISqlConfig
        {
            public string CnnString { get; set; }
        }

        public class UnitOfWork : IUnitOfWork, IDisposable
        {
            private bool disposedValue;

            private IDatabaseConnection DatabaseConnection { get; set; }

            public UnitOfWork(IDatabaseConnection dbConn)
            {
                DatabaseConnection = dbConn;
            }

            public virtual void SaveChanges()
            {
                try
                {
                    DatabaseConnection.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public virtual void UndoChanges()
            {
                try
                {
                    DatabaseConnection.Transaction.Rollback();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing && DatabaseConnection.Connection != null)
                    {
                        DatabaseConnection.Connection.Close();
                    }

                    disposedValue = true;
                }
            }

            public void Dispose()
            {
                Dispose(disposing: true);
            }
        }

        public class UnitOfWorkFactory : IUnitOfWorkFactory
        {
            private IUnitOfWork unitOfWork;

            private IDatabaseConnection dbConnection;

            private IUnitOfWorkRepository unitOfWorkRepository;

            public UnitOfWorkFactory(IDatabaseConnection dbConnection, IUnitOfWorkRepository unitOfWorkRepository)
            {
                this.dbConnection = dbConnection;
                this.unitOfWorkRepository = unitOfWorkRepository;
            }

            public IUnitOfWorkFactory Create()
            {
                dbConnection = dbConnection.CreateConnection();
                dbConnection.Transaction = dbConnection.Connection.BeginTransaction();
                unitOfWork = new UnitOfWork(dbConnection);
                return this;
            }

            public T GetRepository<T>()
            {
                return unitOfWorkRepository.GetRepository<T>(dbConnection);
            }

            public void SaveChanges()
            {
                unitOfWork.SaveChanges();
                Dispose();
            }

            public void UndoChanges()
            {
                unitOfWork.UndoChanges();
                Dispose();
            }

            public void Dispose()
            {
                unitOfWork.Dispose();
            }
        }

        public class UnitOfWorkRepository : IUnitOfWorkRepository
        {
            private IServiceProvider serviceProvider;

            public UnitOfWorkRepository(IServiceProvider serviceProvider)
            {
                this.serviceProvider = serviceProvider;
            }

            public T GetRepository<T>(IDatabaseConnection databaseConnection)
            {
                try
                {
                    return (T)Activator.CreateInstance(((T)serviceProvider.GetService(typeof(T))).GetType(), databaseConnection);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}