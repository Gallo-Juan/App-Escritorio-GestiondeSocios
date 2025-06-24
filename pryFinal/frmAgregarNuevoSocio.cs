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
    public partial class frmAgregarNuevoSocio : Form
    {
        public frmAgregarNuevoSocio()
        {
            InitializeComponent();
        }

        private void frmAgregarNuevoSocio_Load(object sender, EventArgs e)
        {
            clsBarrio objBarrio=new clsBarrio();
            clsActividad objActividad=new clsActividad();

            objBarrio.Listar(cmbBarrio);
            objActividad.Listar(cmbActividad);
            btnCargar.Enabled = false;
        }

        private void VaciarTxt()
        {
            txtDNI.Text = "";
            txtDireccion.Text = "";
            txtNombre.Text = "";
            cmbActividad.SelectedIndex = 0;
            cmbBarrio.SelectedIndex = 0;
        }

        private void ControlarTxt()
        {
            if(txtDNI.Text!="" && txtNombre.Text!="" && txtDireccion.Text != "")
            {
                btnCargar.Enabled = true;
            }
            else
            {
                btnCargar.Enabled=false;
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio = new clsSocio();
            if (objSocio.VerificarDni(Convert.ToInt32(txtDNI.Text)))
            {
                MessageBox.Show("¡¡El DNI ya existe!!");
            }
            else
            {
                objSocio.IdSocio = Convert.ToInt32(txtDNI.Text);
                objSocio.Nombre = txtNombre.Text;
                objSocio.Direccion = txtDireccion.Text;
                objSocio.idBarrio = Convert.ToInt32(cmbBarrio.SelectedValue);
                objSocio.idActividad = Convert.ToInt32(cmbActividad.SelectedValue);
                objSocio.Agregar();
                MessageBox.Show("Dato Grabado");
                VaciarTxt();
            }
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            ControlarTxt();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ControlarTxt();
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            ControlarTxt();
        }
    }
}
