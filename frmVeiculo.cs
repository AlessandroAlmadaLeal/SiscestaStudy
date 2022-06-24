using System;
using Siscesta.Model;
using System.Linq;
using System.Windows.Forms;

namespace Siscesta
{
    public partial class frmVeiculo : Form
    {
        public SiscestaDbContext _context { get; set; }
        public frmVeiculo()
        {
            InitializeComponent();
            _context = new SiscestaDbContext();
            Inicio();
        }

        private void Inicio()
        {

            var marca = _context.Marcas.ToList();
            cmbMarca.DataSource = marca;
            cmbMarca.ValueMember = "Id";
            cmbMarca.DisplayMember = "Descricao";

            var pessoa = _context.Pessoas.ToList();
            cmbPessoa.DataSource = pessoa;
            cmbPessoa.ValueMember = "Id";
            cmbPessoa.DisplayMember = "Nome";

        }
        private void btnCadastro_Click(object sender, EventArgs e)
        {

            bool formCheck = false;

            Veiculo veiculo = new Veiculo();
            veiculo.Placa = txtPlaca.Text;
            veiculo.Cor = txtCor.Text;
            veiculo.ModeloId = int.Parse(cmbModelo.SelectedValue.ToString());
            veiculo.MarcaId = int.Parse(cmbMarca.SelectedValue.ToString());
            veiculo.PessoaId = int.Parse(cmbPessoa.SelectedValue.ToString());
            veiculo.CreateTime = DateTime.Now;
            veiculo.UpdateTime = DateTime.Now;

            //Validação de formulário -- Falar com o Fabinho sobre este método.

            if (String.IsNullOrEmpty(txtPlaca.Text))
            {
                MessageBox.Show("Por gentileza, preencher a placa do veículo.");
                formCheck = false;
            }
            else if (String.IsNullOrEmpty(txtCor.Text))
            {
                MessageBox.Show("Por gentileza, preencher a cor do veiculo.");
                formCheck = false;
            }
            else if (String.IsNullOrEmpty(cmbMarca.SelectedValue.ToString()))
            {
                MessageBox.Show("Por gentileza, selecionar uma marca para o veículo.");
                formCheck = false;
            }
            else if (String.IsNullOrEmpty(cmbModelo.SelectedValue.ToString()))
            {
                MessageBox.Show("Por gentileza, selecionar um modelo para o veículo.");
                formCheck = false;
            }
            else if (String.IsNullOrEmpty(cmbPessoa.SelectedValue.ToString()))
            {
                MessageBox.Show("Por gentileza, selecionar definir um dono para o veículo.");
                formCheck = false;
            }
            else
            {
                formCheck = true;
            }

            if (formCheck)
            {
                try
                {

                    _context.Veiculos.Add(veiculo);
                    _context.SaveChanges();
                    MessageBox.Show("Cadastro realizado com sucesso");

                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Ocorreu o seguinte erro: {ex.Message}");

                }
            }
            else
            {
                MessageBox.Show("Falta alguma informação para prosseguir com o cadastro. \n" +
                                "Por gentileza, corrija o formulário e submeta o cadastro novamente.");
            }
        }

        private void cmbMarca_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var modelo = _context.Modelos.
                        Where(a => a.MarcaId == int.Parse(cmbMarca.SelectedValue.ToString())).ToList();
            cmbModelo.DataSource = modelo;
            cmbModelo.ValueMember = "Id";
            cmbModelo.DisplayMember = "Descricao";
        }
    }
}
