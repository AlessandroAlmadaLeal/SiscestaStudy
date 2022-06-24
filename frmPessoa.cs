using Siscesta.Model;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Siscesta
{
    public partial class frmPessoa : Form
    {
        public SiscestaDbContext _context { get; set; }

        public frmPessoa()
        {
            _context = new SiscestaDbContext();
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            bool formCheck = false;

            Pessoa pessoa = new Pessoa();
            pessoa.Nome = txtNome.Text;
            pessoa.Cpf = txtMskCPF.Text;
            pessoa.DataNascimento = cmbNascimento.Value;
            pessoa.CreateTime = DateTime.Now;
            pessoa.UpdateTime = DateTime.Now;

            if (chkMasculino.Checked && !chkFeminino.Checked)
            {
                pessoa.Sexo = 'M';
            }
            else if (chkFeminino.Checked && !chkMasculino.Checked)
            {
                pessoa.Sexo = 'F';
            }

            //Aqui faço uma serie de validações no formulário - Verificar o que pode fazer para melhorar!!!

            if (String.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por gentileza, preencher o nome.");
                formCheck = false;
            }
            else if (String.IsNullOrWhiteSpace((Regex.Replace(txtMskCPF.Text, @"[^0-9a-zA-Z:,]+", ""))))
            {
                MessageBox.Show("Por gentileza, preencher o CPF.");
                formCheck = false;
            }
            else if (String.IsNullOrWhiteSpace(cmbNascimento.Text) ||
                     cmbNascimento.Value > DateTime.Now.AddYears(-18) ||
                     cmbNascimento.Value < DateTime.Now.AddYears(-90))
            {
                MessageBox.Show("Verificar a data de nascimento dessa pessoa (Maior de 18 anos e no máximo 90 anos).");
                formCheck = false;
            }
            else if (chkMasculino.Checked && chkFeminino.Checked ||
                    !chkMasculino.Checked && !chkFeminino.Checked)
            {
                MessageBox.Show("Deve haver somente um sexo marcado ou pelo menos um.");
                formCheck = false;
            }
            else
            {
                formCheck = true;
            }

            //Uma vez que a validação seguiu com sucesso, vamos realizar o cadastro no banco.

            if (formCheck)
            {
                try
                {

                    _context.Pessoas.Add(pessoa);
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

    }
}
