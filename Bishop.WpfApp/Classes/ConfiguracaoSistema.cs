using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Bishop.WpfApp.Classes
{
    public class ConfiguracaoSistema
    {
        public string CaminhoBase => AppDomain.CurrentDomain.BaseDirectory;
        public string CaminhoRepositorio { get; set; }
        public string CaminhoPastaConfiguracao => $"{CaminhoBase}Config";
        public string CaminhoArquivoConfiguracao => $"{CaminhoPastaConfiguracao}{Path.DirectorySeparatorChar}config.json";

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
            ConfiguracaoSistema configuracaoSistemaArquivo = LerConfig();
            CaminhoRepositorio = configuracaoSistemaArquivo.CaminhoRepositorio;
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
