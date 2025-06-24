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
    public partial class frmListadoPorActividad : Form
    {
        public frmListadoPorActividad()
        {
            InitializeComponent();
        }

        private void frmListadoPorActividad_Load(object sender, EventArgs e)
        {
            clsActividad objActividad =new clsActividad();
            objActividad.Listar(cmbActividad);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio=new clsSocio();
            Int32 id = Convert.ToInt32(cmbActividad.SelectedValue);
            objSocio.ListarPorActividad(dgvListado, id);

            lblMayorDeuda.Text = objSocio.DeudaMayor.ToString("0.00");
            lblMenorDeuda.Text = objSocio.DeudaMenor.ToString("0.00");
            lblPromedio.Text = objSocio.Promedio.ToString("0.00");
            lblTotal.Text = objSocio.TotalDeuda.ToString("0.00");
            objSocio.ListarNombresDeudas(lstMayorDeuda, objSocio.DeudaMayor);
            objSocio.ListarNombresDeudas(lstMenorDeuda, objSocio.DeudaMenor);
            btnExportar.Enabled = true;
            btnImprimir.Enabled = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog objArchivo = new SaveFileDialog();
            objArchivo.Title = "Selecciones carpeta y asigne un nombre al archivo";
            objArchivo.RestoreDirectory = true;
            objArchivo.Filter = "Archivo separado por coma (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";
            objArchivo.ShowDialog();
            Int32 act = Convert.ToInt32(cmbActividad.SelectedValue);
            clsSocio socio = new clsSocio();
            socio.ReporteSociosPorActividad(act, objArchivo.FileName);
            MessageBox.Show("Reporte Generado!");
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            prtVentana.ShowDialog();
            prtDocumento.PrinterSettings = prtVentana.PrinterSettings;
            prtDocumento.Print();
            MessageBox.Show("¡Reporte impreso exitosamente!");
        }

        private void prtDocumento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Int32 idAct = Convert.ToInt32(cmbActividad.SelectedValue);
           
            clsActividad objActividad=new clsActividad();
            string act=objActividad.DevolverNombre(idAct);
           
            clsSocio objSocio = new clsSocio();
            objSocio.ImprimirPorActividad(idAct,e,act);
        }
    }
}
