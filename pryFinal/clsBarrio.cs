using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryFinal
{
    internal class clsBarrio
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private string CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD_Clientes.mdb";
        private string Tabla = "Barrio";

        public void Listar(ComboBox combo)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();

                adaptador.Fill(DS);

                combo.DataSource = DS.Tables[0];
                combo.DisplayMember = "Nombre";
                combo.ValueMember = "idBarrio";

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Recibe el id y devuelve el nombre del barrio
        public string DevolverNombre(Int32 id)
        {
            try
            {
                string nombre = "";
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[Tabla].Rows)
                    {
                        if (Convert.ToInt32(dr["idBarrio"]) == id)
                        {
                            nombre = dr["Nombre"].ToString();
                        }
                    }
                }
                conexion.Close();
                return nombre;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}
