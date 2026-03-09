using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CP1_Cadastro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Cadastro<Pessoa> cadastro = new Cadastro<Pessoa>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(EntradaID.Text, out int id))
            {
                if (string.IsNullOrWhiteSpace(EntradaNome.Text))
                {
                    MessageBox.Show("O Nome não pode estar vazio!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Pessoa novaPessoa = new Pessoa { Nome = EntradaNome.Text };

                if (cadastro.Adicionar(id, novaPessoa))
                {
                    MessageBox.Show("Cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCampos();
                    AtualizarGrid(); // Atualiza a tabela automaticamente
                }
                else
                {
                    MessageBox.Show("Este ID já está cadastrado!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Digite um ID numérico válido.", "Erro de Validação", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- AÇÃO: LISTAR ---
        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {
            AtualizarGrid();
        }

        // --- AÇÃO: BUSCAR (Botão Laranja do Topo) ---
        private void BtnBuscarTopo_Click(object sender, RoutedEventArgs e)
        {
            RealizarBusca(EntradaID.Text);
        }

        // --- AÇÃO: BUSCAR (Botão Azul ao lado da Tabela) ---
        private void BtnBuscarFiltro_Click(object sender, RoutedEventArgs e)
        {
            RealizarBusca(EntradaBuscaID.Text);
        }

        // Lógica centralizada para buscar, já que temos 2 botões de busca
        private void RealizarBusca(string idTexto)
        {
            if (int.TryParse(idTexto, out int id))
            {
                Pessoa pessoaEncontrada = cadastro.Buscar(id);
                if (pessoaEncontrada != null)
                {
                    MessageBox.Show($"Registro encontrado!\n\nID: {id}\nNome: {pessoaEncontrada.Nome}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado com este ID.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Digite um ID numérico para buscar.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- AÇÃO: REMOVER ---
        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(EntradaID.Text, out int id))
            {
                if (cadastro.Remover(id))
                {
                    MessageBox.Show("Registro removido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCampos();
                    AtualizarGrid();
                }
                else
                {
                    MessageBox.Show("ID não encontrado para remoção.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Digite um ID numérico para remover.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- AÇÃO: LIMPAR ---
        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            LimparCampos();
        }

        // Métodos Auxiliares
        private void LimparCampos()
        {
            EntradaID.Clear();
            EntradaNome.Clear();
            EntradaBuscaID.Clear();
        }

        private void AtualizarGrid()
        {
            // O WPF precisa que você limpe a fonte (null) e reatribua para enxergar as mudanças do Dictionary
            GridDados.ItemsSource = null;
            GridDados.ItemsSource = cadastro.ListarTodos();
        }
    }
}