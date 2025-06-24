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
    public partial class frmConsultaDeUnSocio : Form
    {
        public frmConsultaDeUnSocio()
        {
            InitializeComponent();
        }

        private void frmConsultaDeUnSocio_Load(object sender, EventArgs e)
        {
            clsSocio objSocio=new clsSocio();
            objSocio.ListarNombres(cmbNombre);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio = new clsSocio();
            clsActividad objActividad=new clsActividad();
            clsBarrio objBarrio=new clsBarrio();
            Int32 dni = Convert.ToInt32(cmbNombre.SelectedValue);

            objSocio.Buscar(dni);

            lblDNI.Text = objSocio.IdSocio.ToString();
            lblDomicilio.Text = objSocio.Direccion;
            lblBarrio.Text = objBarrio.DevolverNombre(objSocio.idBarrio);         
            lblActividad.Text = objActividad.DevolverNombre(objSocio.idActividad);
            lblDeuda.Text=objSocio.Deuda.ToString();
        }
    }
}
