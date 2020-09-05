using BIshop.Data.Conectores;
using BIshop.Data.Enums;
using BIshop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIshop.Data.Factories
{
    public static class ConnectorFactory
    {
        public static AbstractConnector CreateConnector(EDatabase type, ICanConnectToDabase connection)
        {
            switch (type)
            {
                case EDatabase.SQLServer:
                    return new SQLConnector(connection.IpDoBanco, connection.NomeDoBanco, connection.Usuario, connection.Senha);
                default:
                    throw new ArgumentOutOfRangeException($"Tipo {type} não programado!");
            }
        }
    }
}
