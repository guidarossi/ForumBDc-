using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Aula_Banco_Locadora
{
    public partial class Locadora : Form
    {
        public Locadora()
        {
            InitializeComponent();
        }
        private MySqlConnectionStringBuilder conexaoBanco()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "veiculoslocadora";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            return conexaoBD;
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();

                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "INSERT INTO carros (idCarro,ModeloCarro, MarcaCarro, AnoCarro, CorCarro) VALUES ('" + txtID.Text + "','" + txtModelo.Text + "', '" + txtMarca.Text + "', " + txtAno.Text + ", '" + txtCor.Text + "')";
                comandoMySql.ExecuteNonQuery();

                realizaConexaoBD.Close();
                MessageBox.Show("Inserido com sucesso!");
                atualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir a conexão!");
                Console.WriteLine(ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDados.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridViewDados.CurrentRow.Selected = true;

                txtID.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnID"].FormattedValue.ToString();
                txtModelo.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnNome"].FormattedValue.ToString();
                txtMarca.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnMarca"].FormattedValue.ToString();
                txtAno.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnAno"].FormattedValue.ToString();
                txtCor.Text = dataGridViewDados.Rows[e.RowIndex].Cells["ColumnCor"].FormattedValue.ToString();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            atualizarGrid();
        }
        private void atualizarGrid()
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                realizaConexaoBD.Open();
                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM carros";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dataGridViewDados.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridViewDados.Rows[0].Clone();
                    row.Cells[0].Value = reader.GetInt32(0);
                    row.Cells[1].Value = reader.GetString(1);
                    row.Cells[2].Value = reader.GetString(2);
                    row.Cells[3].Value = reader.GetInt32(3);
                    row.Cells[4].Value = reader.GetString(4);
                    dataGridViewDados.Rows.Add(row);
                }
                realizaConexaoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível realizar a conexão!");
                Console.WriteLine(ex.Message);
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexaoBD.Open();

                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "UPDATE carros SET  ModeloCarro = '" + txtModelo.Text + "', MarcaCarro = '" + txtMarca.Text + "', AnoCarro = '" + txtAno.Text + "', CorCarro = '" + txtCor.Text + "' WHERE idCarro = " + txtID.Text + "";
                comandoMySql.ExecuteNonQuery();

                realizaConexaoBD.Close();
                MessageBox.Show("Alterado com sucesso!");
                atualizarGrid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void btnDeletar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexaoBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                realizaConexaoBD.Open();
                MySqlCommand comandoMySql = realizaConexaoBD.CreateCommand();
                comandoMySql.CommandText = "DELETE FROM carros WHERE idCarro = '" + txtID.Text + "'";
                comandoMySql.ExecuteNonQuery();

                realizaConexaoBD.Close();
                MessageBox.Show("Removido com sucesso");
                atualizarGrid();
                limparCampos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void limparCampos()
        {
            txtID.Clear();
            txtModelo.Clear();
            txtMarca.Clear();
            txtAno.Clear();
            txtCor.Clear();
        }
    }
}