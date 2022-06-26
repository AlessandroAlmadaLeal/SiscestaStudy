using Siscesta.Model;
using System;
using System.Linq;
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

        public string valida()
        {
            string valida = "";

            if (String.IsNullOrWhiteSpace(txtNome.Text))
            {
                valida += " - Preencher o nome do indivíduo.\n";
            }
            
            if (!txtMskCPF.MaskCompleted)
            {
                valida += " - Preenchimento do campo CPF.\n"; 
            }

            /* TODO: Implementar verificação de CPF.
              
            string chkDbCPF = _context.Pessoas.Where(x => x.Cpf == "Cpf").FirstOrDefault();

            if (!String.IsNullOrEmpty(chkDbCPF))
            {
                valida += " - Esse CPF já existe na base de dados, verifique os registros.\n";
            }

            */

            if (cmbNascimento.Value > DateTime.Now.AddYears(-18) || cmbNascimento.Value < DateTime.Now.AddYears(-90))
            {
                valida += " - Preenchimento da data de nascimento entre 18 anos e 90 anos.\n";
            }
            
            if (!rdbMasculino.Checked && !rdbFeminino.Checked)
            {
                valida += " - Deve haver somente um sexo marcado ou pelo menos um.\n";
            }

            return valida;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            Pessoa pessoa = new Pessoa();
            pessoa.Nome = txtNome.Text;
            pessoa.Cpf = txtMskCPF.Text;
            pessoa.DataNascimento = cmbNascimento.Value;
            pessoa.CreateTime = DateTime.Now;
            pessoa.UpdateTime = DateTime.Now;

            if (rdbMasculino.Checked)
            {
                pessoa.Sexo = 'M';
            }
            else if (rdbFeminino.Checked)
            {
                pessoa.Sexo = 'F';
            }

            var isValid = valida();

            if (String.IsNullOrEmpty(isValid))
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
                MessageBox.Show("Um ou mais erros ocorreram no processo, por gentileza: \n\n" +
                                $"{isValid}");
            }
        }

    }
}
