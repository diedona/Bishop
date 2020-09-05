using Bishop.WpfApp.Classes;
using System;
using System.Collections.Generic;
using System.Text;
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

        private void AtualizarConfig()
        {
            _ConfiguracoesSistema.SalvarConfig();
        }
    }
}
