using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryFinal
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void acercaDelDesarrolladorDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcercaDe form=new frmAcercaDe();
            form.ShowDialog();
        }

        private void agregarNuevosSociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAgregarNuevoSocio form=new frmAgregarNuevoSocio();
            form.ShowDialog();
        }

        private void buscarSocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBuscarSocio form=new frmBuscarSocio();
            form.ShowDialog();
        }

        private void consultaDeUnSocioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaDeUnSocio form = new frmConsultaDeUnSocio();
            form.ShowDialog();
        }

        private void listadoDeTodosLosSociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListadoClientes form=new frmListadoClientes();
            form.ShowDialog();
        }

        private void listadoDeSociosDeudoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListadoDeudores form=new frmListadoDeudores();
            form.ShowDialog();
        }

        private void listadoDeSociosDeUnaActividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListadoPorActividad form=new frmListadoPorActividad();
            form.ShowDialog();
        }

        private void listadoDeSociosDeUnBarrioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListadoPorBarrio form=new frmListadoPorBarrio();
            form.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
