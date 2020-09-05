using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BIshop.Data.Conectores
{
    public abstract class AbstractConnector
    {
        protected readonly string _Ip;
        protected readonly string _Database;
        protected readonly string _Username;
        protected readonly string _Password;

        public AbstractConnector(string ip, string database, string username, string password)
        {
            _Ip = ip;
            _Database = database;
            _Username = username;
            _Password = password;
        }

        protected abstract string GetConnectionString();
        public abstract Task<(bool Success, string Message)> TryConnection();
    }
}
