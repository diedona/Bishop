using BIshop.Data.Enums;
using BIshop.Data.Interfaces;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Bishop.WpfApp.Classes
{
    public class ConfiguracaoSistema : INotifyPropertyChanged, ICanConnectToDabase
    {
        private readonly string[] _NomesDeArquivos = { "appsettings.json", "connections.config" };

        public event PropertyChangedEventHandler PropertyChanged;

        [DoNotNotify]
        public string CaminhoBase => AppDomain.CurrentDomain.BaseDirectory;
        [DoNotNotify]
        public string CaminhoPastaConfiguracao => $"{CaminhoBase}Config";
        [DoNotNotify]
        public string CaminhoArquivoConfiguracao => $"{CaminhoPastaConfiguracao}{Path.DirectorySeparatorChar}config.json";

        public string CaminhoRepositorio { get; set; }
        public string IpDoBanco { get; set; }
        public string NomeDoBanco { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public EDatabase TipoDeBase { get; set; } = EDatabase.SQLServer;

        public void CriarConfigSeNaoExistir()
        {
            if (!File.Exists(CaminhoArquivoConfiguracao))
            {
                SalvarConfig();
            }
        }

        public void SalvarConfig()
        {
            string content = JsonSerializer.Serialize(this);
            Directory.CreateDirectory(CaminhoPastaConfiguracao);
            File.WriteAllText(CaminhoArquivoConfiguracao, content);
        }

        public void CarregarDadosPeloConfig()
        {
            ConfiguracaoSistema configuracao = LerConfig();
            CaminhoRepositorio = configuracao.CaminhoRepositorio;
            IpDoBanco = configuracao.IpDoBanco;
            NomeDoBanco = configuracao.NomeDoBanco;
            Usuario = configuracao.Usuario;
            Senha = configuracao.Senha;
        }

        public IEnumerable<string> CarregarCaminhosArquivosDeConexao()
        {
            List<string> caminhos = new List<string>();
            foreach (string arquivo in _NomesDeArquivos)
            {
                caminhos.AddRange(Directory.GetFiles(CaminhoRepositorio, arquivo, SearchOption.AllDirectories));
            }
            return caminhos;
        }

        private ConfiguracaoSistema LerConfig()
        {
            if (!File.Exists(CaminhoArquivoConfiguracao))
            {
                throw new ArgumentException($"{CaminhoArquivoConfiguracao} não existe");
            }

            string json = File.ReadAllText(CaminhoArquivoConfiguracao);
            return JsonSerializer.Deserialize<ConfiguracaoSistema>(json);
        }
    }
}
