using Bishop.WpfApp.Classes;
using BIshop.Data.Conectores;
using BIshop.Data.Factories;
using BIshop.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms; // necessário dar unload no projeto e adicionar <UseWindowsForms>true</UseWindowsForms>
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bishop.WpfApp.Formularios
{
    /// <summary>
    /// Interaction logic for frmMenu.xaml
    /// </summary>
    public partial class frmMenu : Window
    {
        private ConfiguracaoSistema _ConfiguracoesSistema { get; set; }

        public frmMenu()
        {
            InitializeComponent();
            _ConfiguracoesSistema = new ConfiguracaoSistema();
            stpDadosSistema.DataContext = _ConfiguracoesSistema;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EsconderAguarde(progressTestarConexao);
            _ConfiguracoesSistema.CriarConfigSeNaoExistir();
            _ConfiguracoesSistema.CarregarDadosPeloConfig();
        }

        private void btnBuscarRepositorio_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _ConfiguracoesSistema.CaminhoRepositorio = dialog.SelectedPath;
                    AtualizarConfig();
                }
            }
        }

        private void txtCaminhoRepositorio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Apagar caminho", "Confirma?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                _ConfiguracoesSistema.CaminhoRepositorio = string.Empty;
                AtualizarConfig();
            }
        }

        private void btnSalvarDadosConexao_Click(object sender, RoutedEventArgs e)
        {
            AtualizarConfig();
        }

        private void btnBuscarArquivos_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> jsonFiles = _ConfiguracoesSistema.CarregarCaminhosArquivosDeConexao();
        }

        private async void btnTestarConexao_Click(object sender, RoutedEventArgs e)
        {
            AbstractConnector connector = ConnectorFactory.CreateConnector(_ConfiguracoesSistema.TipoDeBase, _ConfiguracoesSistema);
            MostrarAguarde(progressTestarConexao);
            btnTestarConexao.Visibility = Visibility.Collapsed;
            (bool Success, string Message) resposta;

            try
            {
                resposta = await connector.TryConnection();
            }
            finally
            {
                EsconderAguarde(progressTestarConexao);
                btnTestarConexao.Visibility = Visibility.Visible;
            }
            
            if (!resposta.Success)
            {
                System.Windows.MessageBox.Show(resposta.Message, "Ocorreu um erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EsconderAguarde(System.Windows.Controls.ProgressBar progressBar)
        {
            progressBar.Visibility = Visibility.Collapsed;
        }

        private void MostrarAguarde(System.Windows.Controls.ProgressBar progressBar)
        {
            progressBar.Visibility = Visibility.Visible;
        }

        private void AtualizarConfig()
        {
            _ConfiguracoesSistema.SalvarConfig();
        }
    }
}
