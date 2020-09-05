using System;
using System.Collections.Generic;
using System.Text;

namespace BIshop.Data.Interfaces
{
    public interface ICanConnectToDabase
    {
        string IpDoBanco { get; }
        string NomeDoBanco { get; }
        string Usuario { get; }
        string Senha { get; }
    }
}
