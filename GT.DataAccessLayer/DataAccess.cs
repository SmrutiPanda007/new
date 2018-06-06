using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace GT.DataAccessLayer
{
    public abstract class DataAccess : IDisposable
    {
        private SqlConnection _connection;
        protected DataAccess(string sConnString)
        {
            _connection = new SqlConnection(sConnString);
        }

        protected SqlConnection Connection
        {
            get { return _connection; }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
