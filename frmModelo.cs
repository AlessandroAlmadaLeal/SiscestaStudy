using System;
using Siscesta.Model;
using System.Linq;
using System.Windows.Forms;

namespace Siscesta
{
    public partial class frmModelo : Form
    {
        public SiscestaDbContext _context { get; set; }
        public frmModelo()
        {
            _context = new SiscestaDbContext();
            InitializeComponent();
            Inicio();
        }

        private void Inicio()
        {

            var marca = _context.Marcas.ToList();
            cmbMarca.DataSource = marca;
            cmbMarca.ValueMember = "Id";
            cmbMarca.DisplayMember = "Descricao";

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Modelo modelo = new Modelo();
            modelo.Descricao = txtDesc.Text;
            modelo.CreateTime = DateTime.Now;
            modelo.UpdateTime = DateTime.Now;
            modelo.MarcaId = int.Parse(cmbMarca.SelectedValue.ToString());

            try
            {
                _context.Modelos.Add(modelo);
                _context.SaveChanges();
                MessageBox.Show("O cadastro ocorreu com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu o seguinte erro: {ex.Message}");
            }
        }
    }
}
