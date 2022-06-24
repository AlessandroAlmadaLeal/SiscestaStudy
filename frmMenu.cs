
using Siscesta.Model;
using System;
using System.Windows.Forms;

namespace Siscesta
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmList frm = new frmList("Marca");
            frm.ShowDialog();
        }

        private void modeloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmList frm = new frmList("Modelo");
            frm.ShowDialog();
        }

        private void pessoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmList frm = new frmList("Pessoa");
            frm.ShowDialog();
        }

        private void veículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmList frm = new frmList("Veiculo");
            frm.ShowDialog();
        }
    }
}


