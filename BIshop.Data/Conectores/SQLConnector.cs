﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BIshop.Data.Conectores
{
    public class SQLConnector : AbstractConnector
    {
        public SQLConnector(string ip, string database, string username, string password)
            : base(ip, database, username, password) { }

        public override async Task<(bool Success, string Message)> TryConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync();
                    connection.Close();
                }

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        protected override string GetConnectionString()
        {
            return $"Server={_Ip};Database={_Database};User Id={_Username};Password={_Password};Connection Timeout=5";
        }
    }
}
