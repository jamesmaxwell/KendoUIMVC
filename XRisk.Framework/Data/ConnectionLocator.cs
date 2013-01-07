using System;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client;
using XRisk.Logging;
using ServiceStack.OrmLite;

namespace XRisk.Data
{
    public class OracleConnectionLocator : IConnectionLocator
    {
        private readonly ILogger _logger;
        private IDbConnection _connection;
        private readonly IDbConnectionFactory _dbConnectionFactory;

        private const string DatasourceStr =
            "(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1})))(CONNECT_DATA = (SERVER = DEDICATED)(SID = {2})))";

        private const string ConnStr = "Data Source = {0};User ID={1};Password={2}";
        private const string Pwd = "xrisk";

        public OracleConnectionLocator(ILogger logger)
        {
            _logger = logger;
            _dbConnectionFactory = new DbConnectionFactory(() =>
                {
                    // 获取数据库连接字符串
                    var host = ConfigurationManager.AppSettings["Host"] ?? "127.0.0.1";
                    var port = ConfigurationManager.AppSettings["Port"] ?? "1521";
                    var serviceName = ConfigurationManager.AppSettings["ServiceName"];
                    var user = ConfigurationManager.AppSettings["User"];
                    var dataSource = ConfigurationManager.AppSettings["DataSource"];
                    if (string.IsNullOrWhiteSpace(dataSource))
                    {
                        dataSource = string.Format(DatasourceStr, host, port, serviceName);
                    }

                    return new OracleConnection(string.Format(ConnStr, dataSource, user, Pwd));
                });
        }

        public ILogger Logger { get { return _logger; } }

        public IDbConnection For(Type entityType)
        {
            Logger.Debug("Acquiring connection for {0}", entityType);

            if (_connection == null)
            {
                Logger.Information("Openning database connection");
                _connection = _dbConnectionFactory.OpenDbConnection();
            }
            return _connection;
        }
    }
}
