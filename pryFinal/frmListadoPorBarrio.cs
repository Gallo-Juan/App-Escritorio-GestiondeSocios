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
    public partial class frmListadoPorBarrio : Form
    {
        public frmListadoPorBarrio()
        {
            InitializeComponent();
        }

        private void frmListadoPorBarrio_Load(object sender, EventArgs e)
        {
            clsBarrio objBarrio=new clsBarrio();
            objBarrio.Listar(cmbBarrio);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio=new clsSocio();
            Int32 id = Convert.ToInt32(cmbBarrio.SelectedValue);

            objSocio.ListarPorBarrio(dgvListado, id);

            lblCantidad.Text = objSocio.Cantidad.ToString();
            lblTotal.Text = objSocio.TotalDeuda.ToString("0.00");
            btnImprimir.Enabled = true;
            btnExportar.Enabled = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog objArchivo = new SaveFileDialog();
            objArchivo.Title = "Selecciones carpeta y asigne un nombre al archivo";
            objArchivo.RestoreDirectory = true;
            objArchivo.Filter = "Archivo separado por coma (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";
            objArchivo.ShowDialog();
            Int32 bar = Convert.ToInt32(cmbBarrio.SelectedValue);
            clsSocio socio = new clsSocio();
            socio.ReporteSociosPorBarrio(bar, objArchivo.FileName);
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
            Int32 idBar = Convert.ToInt32(cmbBarrio.SelectedValue);

            clsBarrio objBarrio=new clsBarrio();
            string barrio=objBarrio.DevolverNombre(idBar);
            
            clsSocio objSocio = new clsSocio();
            objSocio.ImprimirPorBarrio(idBar, e,barrio);
        }

       
    }
}
