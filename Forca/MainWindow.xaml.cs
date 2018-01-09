using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forca
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        #region "PROPRIEDADES"
        private GerenciadorDePalavras _gerenciador;
        private string _palavraEscolhida;
        private int _numeroTentativas = 5;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            PreparacaoInicial();
        }

        #region "ACOES DE TELA"

        private void iniciar_Click(object sender, RoutedEventArgs e)
        {
            conteudo_jogo.Visibility = Visibility.Visible;
            definicao_jogo.Visibility = Visibility.Hidden;
            AtulizeNumeroDeTentativas();
            nome_categoria.Text = categorias.SelectedValue.ToString();
            _palavraEscolhida = _gerenciador.SelecionePalavra(nome_categoria.Text);
            _palavraEscolhida.ToList().ForEach(x => { palavra_escolhida.Text += "_ "; });
        }

        private void categorias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            iniciar.IsEnabled = true;
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DigitoNaoVeioVazio())
            {
                var indices = ObtenhaOsIndicesOndeTemOcorrenciaDaLetra(digito.Text);

                if (indices.Count > 0)
                {
                    indices.ForEach(indice => EfetueTrocaDeCaractere(indice));
                    if (!palavra_escolhida.Text.Contains("_") && _numeroTentativas > 0)
                    {
                        EmitaMensagemEReinicieOJogo("Parabéns você ganhou !!!");
                    }
                }
                else
                {
                    if (_numeroTentativas > 1)
                    {
                        _numeroTentativas--;
                        AtulizeNumeroDeTentativas();
                    }
                    else
                    {
                        EmitaMensagemEReinicieOJogo("Ops que pena você perdeu, tente novamente !!!");
                    }
                }

                digito.Text = string.Empty;
            }
        }

        private void EmitaMensagemEReinicieOJogo(string mensagem)
        {
            MessageBox.Show(mensagem);
            ReinicieJogo();
        }

        private void ReinicieJogo()
        {
            conteudo_jogo.Visibility = Visibility.Hidden;
            definicao_jogo.Visibility = Visibility.Visible;
            iniciar.IsEnabled = false;
            categorias.SelectedValue = string.Empty;
            palavra_escolhida.Text = string.Empty;
            _numeroTentativas = 5;
        }

        #endregion

        #region "METODOS AUXILIARES"

        private void PreparacaoInicial()
        {
            _gerenciador = new GerenciadorDePalavras();
            conteudo_jogo.Visibility = Visibility.Hidden;
            categorias.ItemsSource = _gerenciador.ObtenhaCategorias();
            iniciar.IsEnabled = false;
        }

        private List<int> ObtenhaOsIndicesOndeTemOcorrenciaDaLetra(string digito)
        {
            List<int> indices = new List<int>();

            for (int indice = 0; indice < _palavraEscolhida.Length; indice++)
            {
                if (_palavraEscolhida[indice].Equals(Convert.ToChar(digito)))
                {
                    indices.Add(indice);
                }
            }

            return indices;
        }

        private void EfetueTrocaDeCaractere(int indice)
        {
            indice = indice > 0 && indice == 1 ? indice + 1 : indice > 1 ? indice + indice : indice;
            palavra_escolhida.Text = palavra_escolhida.Text.Remove(indice, digito.Text.Length).Insert(indice, digito.Text);
        }

        private void AtulizeNumeroDeTentativas()
        {
            numero_tentativas.Text = Convert.ToString(_numeroTentativas);
        }

        private bool DigitoNaoVeioVazio()
        {
            return !string.IsNullOrEmpty(digito.Text);
        }

        #endregion
    }
}
