using Microsoft.EntityFrameworkCore;
using Siscesta.Model;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

namespace Siscesta
{
    public partial class frmList : Form
    {
        public SiscestaDbContext _context { get; set; }
        public string _menu { get; set; }

        public frmList(string menu)
        {
            _context = new SiscestaDbContext();
            InitializeComponent();

            _menu = menu;

            switch (_menu)
            {
                case "Marca":

                    ICollection data = _context.Marcas.ToList();

                    var filtros = typeof(Marca).GetProperties().
                        Where(p => p.Name == "Descricao" || p.Name == "Id")
                        .ToList();

                    Inicio(filtros, data);

                    break;

                case "Modelo":

                    data = _context.Modelos.
                        Include(m => m.Marca).
                        Select(m => new { m.Id, m.Descricao, m.MarcaId, Marca = m.Marca.Descricao }).ToList();
                    
                    filtros = typeof(Modelo).GetProperties().
                        Where(p => p.Name == "Descricao" || p.Name == "Id")
                        .ToList();

                    Inicio(filtros, data);

                    break;

                case "Pessoa":

                    data = _context.Pessoas.ToList();

                    filtros = typeof(Pessoa).GetProperties().
                        Where(p => p.Name == "Nome" || p.Name == "Cpf")
                        .ToList();

                    Inicio(filtros, data);

                    break;

                case "Veiculo":

                    data = _context.Veiculos.ToList();

                    filtros = typeof(Veiculo).GetProperties().
                        Where(p => p.Name == "Placa" || p.Name == "Cor")
                        .ToList();

                    Inicio(filtros, data);

                    break;
            }
        }

        public void Inicio(ICollection dataFilter, ICollection data)
        {
            grdList.DataSource = data;

            cmbFiltro.DataSource = dataFilter;
            cmbFiltro.DisplayMember = "Name";
            cmbFiltro.ValueMember = "Name";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
           
            switch(_menu)
            { 
                case "Modelo":
            
                    frmModelo frm = new frmModelo();
                    frm.ShowDialog();

                break;

                case "Marca":
            
                    frmMarca frm1 = new frmMarca();
                    frm1.ShowDialog();
                
                break;

                case "Pessoa":

                    frmPessoa frm2 = new frmPessoa();
                    frm2.ShowDialog();

                    break;

                case "Veiculo":

                    frmVeiculo frm3 = new frmVeiculo();
                    frm3.ShowDialog();

                    break;

            }
        }

        private void txtDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && cmbFiltro.SelectedValue.ToString() == "Id" &&
                e.KeyChar != Convert.ToChar(Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {
            Atualiza();
        }

        public void Atualiza()
        {
            ICollection search;

            switch (_menu)
            {
                case "Marca":
                    if (cmbFiltro.SelectedValue.ToString() == "Id")
                    {
                        if (!String.IsNullOrWhiteSpace(txtDesc.Text))
                        {
                            search = _context.Marcas.Where(m => m.Id == int.Parse(txtDesc.Text)).
                            ToList();
                        }
                        else
                        {
                            search = _context.Marcas.ToList();
                        }

                    }
                    else
                    {
                        search = _context.Marcas.Where(m => m.Descricao.
                        Contains(txtDesc.Text)).ToList();
                    }
                        grdList.DataSource = search;
                    break;

                case "Modelo":
                    if (cmbFiltro.SelectedValue.ToString() == "Id")
                    {
                        if (!String.IsNullOrWhiteSpace(txtDesc.Text))
                        {
                            search = _context.Modelos.Include(m => m.Marca).
                        Select(m => new { m.Id, m.Descricao, m.MarcaId, Marca = m.Marca.Descricao })
                        .Where(m => m.Id == int.Parse(txtDesc.Text)).
                            ToList();
                        }
                        else
                        {
                            search = _context.Modelos.Include(m => m.Marca).
                        Select(m => new { m.Id, m.Descricao, m.MarcaId, Marca = m.Marca.Descricao }).ToList();
                        }
                    }
                    else
                    {
                        search = _context.Modelos.
                            Include(m => m.Marca).
                            Select(m => new { m.Id, m.Descricao, m.MarcaId, Marca = m.Marca.Descricao })
                            .Where(m => m.Descricao.
                            Contains(txtDesc.Text)).
                            ToList();
                    }
                        grdList.DataSource = search;
                    break;

                case "Pessoa":

                    if (cmbFiltro.SelectedValue.ToString() == "Cpf")
                    {
                        if (!String.IsNullOrWhiteSpace(txtDesc.Text))
                        {
                            search = _context.Pessoas.Where(o => o.Cpf.Contains(txtDesc.Text)).ToList();
                        }
                        else
                        {
                            search = _context.Pessoas.ToList();
                        }

                    }
                    else
                    {
                        search = _context.Pessoas.Where(o => o.Nome.Contains(txtDesc.Text)).ToList();
                    }
                    
                    grdList.DataSource = search;

                    break;

                case "Veiculo":

                    if (cmbFiltro.SelectedValue.ToString() == "Placa")
                    {
                        if (!String.IsNullOrWhiteSpace(txtDesc.Text))
                        {
                            search = _context.Veiculos.Where(u => u.Placa.Contains(txtDesc.Text)).
                                     OrderBy(u => u.Placa).ToList();
                        }
                        else
                        {
                            search = _context.Veiculos.ToList();
                        }

                    }
                    else
                    {
                        search = _context.Veiculos.Where(u => u.Cor.Contains(txtDesc.Text)).ToList();
                    }

                    grdList.DataSource = search;

                    break;
            }
        }
    }
}