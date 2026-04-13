using CP2_Crud;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
// IMPORTANTE: A biblioteca do SQL Server do PDF
using System.Data.SqlClient;
using System.Windows;

namespace CP2_Crud
{
    public partial class MainWindow : Window
    {
        // Substitua "sua_senha_aqui" pela senha que você usa para entrar no MySQL Workbench (geralmente é root, admin ou vazia)
        private string stringConexao = "Server=localhost;Database=Escola;Uid=root;Pwd=Mysql@fr4nk13;";

        public MainWindow()
        {
            InitializeComponent();
        }

        // ==========================================
        // 1. INSERIR (INSERT)
        // ==========================================
        private void BtnInserir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string query = "INSERT INTO Alunos (Nome, Idade) VALUES (@Nome, @Idade)";
                    MySqlCommand comando = new MySqlCommand(query, conexao);

                    comando.Parameters.AddWithValue("@Nome", txtNome.Text);
                    comando.Parameters.AddWithValue("@Idade", int.Parse(txtIdade.Text));

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Aluno inserido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==========================================
        // 2. LISTAR (SELECT)
        // ==========================================
        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Aluno> lista = new List<Aluno>();
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string query = "SELECT * FROM Alunos";
                    MySqlCommand comando = new MySqlCommand(query, conexao);

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Aluno
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nome = reader["Nome"].ToString(),
                                Idade = Convert.ToInt32(reader["Idade"])
                            });
                        }
                    }
                }
                dgAlunos.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==========================================
        // 3. ATUALIZAR (UPDATE)
        // ==========================================
        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string query = "UPDATE Alunos SET Nome = @Nome, Idade = @Idade WHERE Id = @Id";
                    MySqlCommand comando = new MySqlCommand(query, conexao);

                    comando.Parameters.AddWithValue("@Nome", txtNome.Text);
                    comando.Parameters.AddWithValue("@Idade", int.Parse(txtIdade.Text));
                    comando.Parameters.AddWithValue("@Id", int.Parse(txtId.Text));

                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                        MessageBox.Show("Aluno atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("ID não encontrado para atualização.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);

                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==========================================
        // 4. REMOVER (DELETE)
        // ==========================================
        private void BtnRemover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string query = "DELETE FROM Alunos WHERE Id = @Id";
                    MySqlCommand comando = new MySqlCommand(query, conexao);

                    comando.Parameters.AddWithValue("@Id", int.Parse(txtId.Text));

                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                        MessageBox.Show("Aluno removido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("ID não encontrado para remoção.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);

                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao remover: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==========================================
        // ⭐ BÔNUS: BUSCAR POR ID
        // ==========================================
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(stringConexao))
                {
                    conexao.Open();
                    string query = "SELECT * FROM Alunos WHERE Id = @Id";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@Id", int.Parse(txtId.Text));

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNome.Text = reader["Nome"].ToString();
                            txtIdade.Text = reader["Idade"].ToString();
                            MessageBox.Show("Aluno encontrado e carregado nos campos!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Nenhum aluno encontrado com este ID.", "Não Encontrado", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtNome.Clear();
                            txtIdade.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==========================================
        // 5. SAIR E AUXILIARES
        // ==========================================
        private void BtnSair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtIdade.Clear();
        }
    }
}