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
        private readonly ConfiguracaoSistema _ConfiguracoesSistema;

        public frmMenu()
        {
            InitializeComponent();
            _ConfiguracoesSistema = new ConfiguracaoSistema();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ConfiguracoesSistema.CriarConfigSeNaoExistir();
            _ConfiguracoesSistema.CarregarDadosPeloConfig();

            if (!string.IsNullOrEmpty(_ConfiguracoesSistema.CaminhoRepositorio))
                txtCaminhoRepositorio.Text = _ConfiguracoesSistema.CaminhoRepositorio;
        }

        private void btnBuscarRepositorio_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtCaminhoRepositorio.Text = dialog.SelectedPath;
                    AtualizarConfiguracoesSistema();
                }
            }
        }

        private void txtCaminhoRepositorio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Apagar caminho", "Confirma?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                txtCaminhoRepositorio.Text = string.Empty;
                AtualizarConfiguracoesSistema();
            }
        }

        private void AtualizarConfiguracoesSistema()
        {
            _ConfiguracoesSistema.CaminhoRepositorio = txtCaminhoRepositorio.Text;
            _ConfiguracoesSistema.SalvarConfig();
        }
    }
}
