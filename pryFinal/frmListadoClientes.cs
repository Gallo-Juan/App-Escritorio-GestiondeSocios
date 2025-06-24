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
    public partial class frmListadoClientes : Form
    {
        public frmListadoClientes()
        {
            InitializeComponent();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            clsSocio objSocio = new clsSocio();

            objSocio.Listar(dgvListado);
            lblMayorDeuda.Text = objSocio.DeudaMayor.ToString("0.00");
            lblMenorDeuda.Text = objSocio.DeudaMenor.ToString("0.00");
            lblPromedio.Text = objSocio.Promedio.ToString("0.00");
            lblTotal.Text=objSocio.TotalDeuda.ToString("0.00");
            objSocio.ListarNombresDeudas(lstMayorDeuda, objSocio.DeudaMayor);
            objSocio.ListarNombresDeudas(lstMenorDeuda, objSocio.DeudaMenor);
            btnImprimir.Enabled = true;
            btnExportar.Enabled = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog objArchivo = new SaveFileDialog();
            objArchivo.Title = "Seleccione carpeta y asigne un nombre al archivo";
            objArchivo.RestoreDirectory = true;
            objArchivo.Filter = "Archivo separado por coma (*.csv)|*.csv|Archivo de texto (*.txt)|*.txt";
            objArchivo.ShowDialog();

            clsSocio socio = new clsSocio();
            socio.ReporteSocios(objArchivo.FileName);
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
            clsSocio objSocio = new clsSocio();
            objSocio.Imprimir(e);
        }

      
    }
}
