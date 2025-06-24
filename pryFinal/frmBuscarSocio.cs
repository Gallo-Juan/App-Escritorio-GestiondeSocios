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
    public partial class frmBuscarSocio : Form
    {
        public frmBuscarSocio()
        {
            InitializeComponent();
        }
        private void Inicializar()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtDeuda.Text = "";
            cmbBarrio.SelectedIndex = 0;
            cmbActividad.SelectedIndex = 0;
        }
        private void Deshabilitar()
        {
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnBorrar.Enabled=false;
            txtNombre.Enabled = false;
            txtDireccion.Enabled = false;
            txtDeuda.Enabled = false;
            cmbActividad.Enabled = false;
            cmbBarrio.Enabled = false;
        }
        private void Habilitar()
        {
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnBorrar.Enabled = true;
            txtNombre.Enabled = true;
            txtDireccion.Enabled = true;
            txtDeuda.Enabled = true;
            cmbActividad.Enabled = true;
            cmbBarrio.Enabled = true;
        }
        private void frmBuscarSocio_Load(object sender, EventArgs e)
        {
            clsBarrio objBarrio=new clsBarrio();
            clsActividad objActividad=new clsActividad();

            objBarrio.Listar(cmbBarrio);
            objActividad.Listar(cmbActividad);
            Deshabilitar();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio=new clsSocio();
            Int32 dni = Convert.ToInt32(txtDNI.Text);
            objSocio.Buscar(dni);
            if (objSocio.IdSocio == 0)
            {
                MessageBox.Show("No se ha encontrado el cliente");
                Inicializar();
                btnModificar.Enabled = false;
                
            }
            else
            {
                txtNombre.Text = objSocio.Nombre;
                txtDireccion.Text = objSocio.Direccion;
                cmbBarrio.SelectedValue = objSocio.idBarrio;
                cmbActividad.SelectedValue = objSocio.idActividad;
                txtDeuda.Text = objSocio.Deuda.ToString("0.00");
                btnModificar.Enabled = true;
            }
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio=new clsSocio();
            objSocio.IdSocio = Convert.ToInt32(txtDNI.Text);
            objSocio.Eliminar();
            Inicializar();
            MessageBox.Show("Socio Eliminado!!");
            txtDNI.Enabled = true;
            Deshabilitar();
            btnModificar.Enabled = false;
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Habilitar();
            txtDNI.Enabled = false;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio = new clsSocio();            
            objSocio.IdSocio=Convert.ToInt32(txtDNI.Text);
            objSocio.Nombre = txtNombre.Text;
            objSocio.Direccion = txtDireccion.Text;
            objSocio.idBarrio = Convert.ToInt32(cmbBarrio.SelectedValue);
            objSocio.idActividad = Convert.ToInt32(cmbActividad.SelectedValue);
            objSocio.Deuda = Convert.ToDecimal(txtDeuda.Text);
            objSocio.Modificar();
            MessageBox.Show("Datos Guardados");
            Inicializar();
            Deshabilitar();
            txtDNI.Enabled = true;
            btnModificar.Enabled = false;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio = new clsSocio();
            Int32 dni = Convert.ToInt32(txtDNI.Text);
            objSocio.Buscar(dni);
            txtNombre.Text = objSocio.Nombre;
            txtDireccion.Text = objSocio.Direccion;
            cmbBarrio.SelectedValue = objSocio.idBarrio;
            cmbActividad.SelectedValue = objSocio.idActividad;
            txtDeuda.Text = objSocio.Deuda.ToString("0.00");
            Deshabilitar();
            txtDNI.Enabled = true;
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            if (txtDNI.Text != "")
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }
    }
}
