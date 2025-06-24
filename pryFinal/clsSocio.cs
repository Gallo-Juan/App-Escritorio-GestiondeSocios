using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryFinal
{
    internal class clsSocio
    {
        private OleDbConnection conexion = new OleDbConnection();
        private OleDbCommand comando = new OleDbCommand();
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private string CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD_Clientes.mdb";
        private string Tabla = "Socio";

        private Int32 idSoc;
        private string nom;
        private string dir;
        private Int32 idBar;
        private Int32 idAct;
        private decimal deu;

        private decimal deuMayor;
        private decimal deuMenor;
        private Int32 cant;
        private decimal totDeu;

        public Int32 IdSocio
        {
            get { return idSoc; }
            set { idSoc = value; }
        }

        public string Nombre
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Direccion
        {
            get { return dir; }
            set { dir = value; }
        }

        public Int32 idBarrio
        {
            get { return idBar; }
            set { idBar = value; }
        }

        public Int32 idActividad
        {
            get { return idAct; }
            set { idAct = value; }
        }

        public decimal Deuda
        {
            get { return deu; }
            set { deu = value; }
        }

        public decimal DeudaMayor
        {
            get { return deuMayor; }
        }

        public decimal DeudaMenor
        {
            get { return deuMenor; }
        }

        public Int32 Cantidad
        {
            get { return cant; }
        }

        public decimal TotalDeuda
        {
            get { return totDeu; }
        }

        public decimal Promedio
        {
            get { return totDeu / cant; }
        }

        //Agregar nuevo socio con SQL
        public void Agregar()
        {
            try
            {
                string sql = "INSERT INTO Socio (IdSocio,Nombre,Direccion,idBarrio,idActividad,Deuda) VALUES (" + idSoc.ToString() + ",'" + nom + "','" + dir + "'," + idBar.ToString() + "," + idAct.ToString() + "," + deu.ToString() + ")";
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;

                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool VerificarDni(Int32 dni)
        {
            try
            {
                bool esta=false;
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
                        if (Convert.ToInt32(dr["IdSocio"]) == dni)
                        {
                            esta = true;
                        }
                    }
                }
                return esta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        //Eliminar socio con SQL
        public void Eliminar()
        {
            try
            {
                string sql = "DELETE * FROM Socio WHERE IdSocio= " + IdSocio.ToString();
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;

                comando.ExecuteNonQuery();
                conexion.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Buscar socio 
        public void Buscar(Int32 dni)
        {
            try
            {
                idSoc = 0;
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
                        if (Convert.ToInt32(dr["IdSocio"]) == dni)
                        {
                            idSoc = Convert.ToInt32(dr["IdSocio"]);
                            nom = dr["Nombre"].ToString();
                            dir = dr["Direccion"].ToString();
                            idBar = Convert.ToInt32(dr["idBarrio"]);
                            idAct = Convert.ToInt32(dr["idActividad"]);
                            deu = Convert.ToDecimal(dr["Deuda"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Modificar()
        {
            try
            {
                string sql = "UPDATE Socio SET Nombre='" + nom + "', Direccion='" + dir + "',idBarrio=" + idBar.ToString() + ",idActividad=" + idAct.ToString() + ",Deuda=" + deu.ToString() + " WHERE IdSocio=" + idSoc.ToString();
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;

                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ListarNombres(ComboBox combo)
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
                combo.ValueMember = "idSocio";

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Listar(DataGridView Grilla)
        {
            try
            {
                Grilla.Rows.Clear();
                InicializarMaxYMin();
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
                        
                        cant++;
                        totDeu = totDeu + Convert.ToDecimal(dr["Deuda"]);
                        Grilla.Rows.Add(dr["IdSocio"], dr["Nombre"], dr["Deuda"]);
                        if (Convert.ToDecimal(dr["Deuda"]) > deuMayor)
                        {
                            deuMayor = Convert.ToDecimal(dr["Deuda"]);
                        }
                        if (Convert.ToDecimal(dr["Deuda"]) < deuMenor)
                        {
                            deuMenor = Convert.ToDecimal(dr["Deuda"]);
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Listar nombre con deuda en maximo o minimo

        public void ListarNombresDeudas(ListBox Lista, decimal deuda)
        {
            try
            {
                Lista.Items.Clear();
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();

                adaptador.Fill(DS,Tabla);
                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[Tabla].Rows)
                    {
                        if (Convert.ToDecimal(dr["Deuda"]) == deuda)
                        {
                            Lista.Items.Add(dr["Nombre"]);
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ListarDeudores(DataGridView Grilla)
        {
            try
            {
                Grilla.Rows.Clear();
                InicializarMaxYMin();
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
                        if (Convert.ToDecimal(dr["deuda"]) > 0)
                        {

                            cant++;
                            totDeu = totDeu + Convert.ToDecimal(dr["Deuda"]);
                            Grilla.Rows.Add(dr["IdSocio"], dr["Nombre"], dr["Deuda"]);
                            if (Convert.ToDecimal(dr["Deuda"]) > deuMayor)
                            {
                                deuMayor = Convert.ToDecimal(dr["Deuda"]);
                            }
                            if (Convert.ToDecimal(dr["Deuda"]) < deuMenor)
                            {
                                deuMenor = Convert.ToDecimal(dr["Deuda"]);
                            }
                        }
                    }
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ListarPorActividad(DataGridView Grilla, Int32 id)
        {
            try
            {
                Grilla.Rows.Clear();
                InicializarMaxYMin();
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
                        if (Convert.ToDecimal(dr["idActividad"]) == id)
                        {

                            cant++;
                            totDeu = totDeu + Convert.ToDecimal(dr["Deuda"]);
                            Grilla.Rows.Add(dr["IdSocio"], dr["Nombre"], dr["Deuda"]);
                            if (Convert.ToDecimal(dr["Deuda"]) > deuMayor)
                            {
                                deuMayor = Convert.ToDecimal(dr["Deuda"]);
                            }
                            if (Convert.ToDecimal(dr["Deuda"]) < deuMenor)
                            {
                                deuMenor = Convert.ToDecimal(dr["Deuda"]);
                            }
                        }
                    }
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ListarPorBarrio(DataGridView Grilla, Int32 id)
        {
            try
            {
                Grilla.Rows.Clear();
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
                        if (Convert.ToDecimal(dr["idBarrio"]) == id)
                        {

                            cant++;
                            totDeu = totDeu + Convert.ToDecimal(dr["Deuda"]);
                            Grilla.Rows.Add(dr["IdSocio"], dr["Nombre"], dr["Deuda"]);
                            
                        }
                    }
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ReporteSocios(String NombreArchivo)
        {
            try
            {
                clsBarrio objBarrio = new clsBarrio();
                clsActividad objActividad=new clsActividad();
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                StreamWriter AD = new StreamWriter(NombreArchivo, false, Encoding.UTF8);

                AD.WriteLine("Listado de Socios\n");
                AD.WriteLine("Codigo;Nombre;Direccion;Barrio;Actividad;Deuda");
                cant = 0;
                totDeu = 0;

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[Tabla].Rows)
                    {
                        AD.Write(dr["IdSocio"]);
                        AD.Write(";");
                        AD.Write(dr["Nombre"]);
                        AD.Write(";");
                        AD.Write(dr["Direccion"]);
                        AD.Write(";");
                        AD.Write(objBarrio.DevolverNombre(Convert.ToInt32(dr["idBarrio"])));
                        AD.Write(";");
                        AD.Write(objActividad.DevolverNombre(Convert.ToInt32(dr["idActividad"])));
                        AD.Write(";");
                        AD.WriteLine("$" + dr["Deuda"]);
                        cant++;
                        totDeu = totDeu + Convert.ToDecimal(dr["Deuda"]);
                    }
                }
                AD.Write("\nCantidad de Clientes:;;;;");
                AD.WriteLine(cant);
                AD.Write("Total de Deuda:;;;;");
                AD.WriteLine(totDeu);
                conexion.Close();
                AD.Close();
                AD.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void ReporteSociosDeudores(String NombreArchivo)
        {
            try
            {
                clsBarrio objBarrio = new clsBarrio();
                clsActividad objActividad = new clsActividad();
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                StreamWriter AD = new StreamWriter(NombreArchivo, false, Encoding.UTF8);

                AD.WriteLine("Listado de Socios\n");
                AD.WriteLine("Codigo;Nombre;Direccion;Barrio;Actividad;Deuda");
                cant = 0;
                totDeu = 0;

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[Tabla].Rows)
                    {
                        if (Convert.ToInt32(dr["Deuda"]) > 0)
                        {
                            AD.Write(dr["IdSocio"]);
                            AD.Write(";");
                            AD.Write(dr["Nombre"]);
                            AD.Write(";");
                            AD.Write(dr["Direccion"]);
                            AD.Write(";");
                            AD.Write(objBarrio.DevolverNombre(Convert.ToInt32(dr["idBarrio"])));
                            AD.Write(";");
                            AD.Write(objActividad.DevolverNombre(Convert.ToInt32(dr["idActividad"])));
                            AD.Write(";");
                            AD.WriteLine("$" + dr["Deuda"]);
                            cant++;
                            totDeu = totDeu + Convert.ToDecimal(dr["Deuda"]);
                        }
                    }
                }
                AD.Write("\nCantidad de Clientes:;;;;");
                AD.WriteLine(cant);
                AD.Write("Total de Deuda:;;;;");
                AD.WriteLine(totDeu);
                conexion.Close();
                AD.Close();
                AD.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ReporteSociosPorActividad(Int32 idAct,string NombreArchivo)
        {
            try
            {
                clsBarrio objBarrio = new clsBarrio();
                clsActividad objActividad = new clsActividad();
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                StreamWriter AD = new StreamWriter(NombreArchivo, false, Encoding.UTF8);

                string actividad = objActividad.DevolverNombre(idAct);

                AD.WriteLine("Listado de Socios de "+actividad);
                AD.WriteLine("\nCodigo;Nombre;Direccion;Barrio;Deuda");
                cant = 0;
                totDeu = 0;

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[Tabla].Rows)
                    {
                        if (Convert.ToInt32(dr["idActividad"]) == idAct)
                        {
                            string barrio = objBarrio.DevolverNombre(Convert.ToInt32(dr["idBarrio"]));
                             
                            AD.Write(dr["IdSocio"]);
                            AD.Write(";");
                            AD.Write(dr["Nombre"]);
                            AD.Write(";");
                            AD.Write(dr["Direccion"]);
                            AD.Write(";");
                            AD.Write(barrio);
                            AD.Write(";");
                            AD.WriteLine("$" + dr["Deuda"]);
                            cant++;
                            totDeu = totDeu + Convert.ToDecimal(dr["Deuda"]);
                        }
                    }
                }
                AD.Write("\nCantidad de Socios:;;;;");
                AD.WriteLine(cant);
                AD.Write("Total de Deuda:;;;;");
                AD.WriteLine(totDeu);
                conexion.Close();
                AD.Close();
                AD.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ReporteSociosPorBarrio(Int32 idBar,string NombreArchivo) 
        {
            try
            {
                clsBarrio objBarrio = new clsBarrio();
                clsActividad objActividad = new clsActividad();
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                StreamWriter AD = new StreamWriter(NombreArchivo, false, Encoding.UTF8);

                string barrio = objBarrio.DevolverNombre(idBar);
                AD.WriteLine("Listado de Socios del barrio "+barrio);
                AD.WriteLine("\nCodigo;Nombre;Direccion;Actividad;Deuda");
                cant = 0;
                totDeu = 0;

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow dr in DS.Tables[Tabla].Rows)
                    {
                        if (Convert.ToInt32(dr["idBarrio"]) == idBar)
                        {
                            string actividad = objActividad.DevolverNombre(Convert.ToInt32(dr["idActividad"]));
                            AD.Write(dr["IdSocio"]);
                            AD.Write(";");
                            AD.Write(dr["Nombre"]);
                            AD.Write(";");
                            AD.Write(dr["Direccion"]);
                            AD.Write(";");
                            AD.Write(actividad);
                            AD.Write(";");
                            AD.WriteLine("$" + dr["Deuda"]);
                            cant++;
                            totDeu = totDeu + Convert.ToDecimal(dr["Deuda"]);
                        }
                    }
                }
                AD.Write("\nCantidad de Clientes:;;;;");
                AD.WriteLine(cant);
                AD.Write("Total de Deuda:;;;;");
                AD.WriteLine(totDeu);
                conexion.Close();
                AD.Close();
                AD.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Imprimir(PrintPageEventArgs reporte)
        {
            try
            {
                Font letraTitulo1 = new Font("Arial", 20);
                Font letraTitulo2 = new Font("Arial", 14);
                Font letraTexto = new Font("Arial", 10);
                Int32 f = 220;

                reporte.Graphics.DrawString("Listado de Socios", letraTitulo1, Brushes.Red, 100, 100);
                reporte.Graphics.DrawString("Codigo", letraTitulo2, Brushes.Blue, 100, 180);
                reporte.Graphics.DrawString("Nombre del Socio", letraTitulo2, Brushes.Blue, 200, 180);
                reporte.Graphics.DrawString("Deuda", letraTitulo2, Brushes.Blue, 450, 180);

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
                        reporte.Graphics.DrawString(dr["IdSocio"].ToString(), letraTexto, Brushes.Black, 100, f);
                        reporte.Graphics.DrawString(dr["Nombre"].ToString(), letraTexto, Brushes.Black, 200, f);
                        reporte.Graphics.DrawString("$ "+dr["Deuda"].ToString(), letraTexto, Brushes.Black, 450, f);
                        f = f + 20;
                    }
                }
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        public void ImprimirDeudores(PrintPageEventArgs reporte)
        {
            try
            {
                Font letraTitulo1 = new Font("Arial", 20);
                Font letraTitulo2 = new Font("Arial", 14);
                Font letraTexto = new Font("Arial", 10);
                Int32 f = 220;

                reporte.Graphics.DrawString("Listado de Socios Deudores", letraTitulo1, Brushes.Red, 100, 100);
                reporte.Graphics.DrawString("Codigo", letraTitulo2, Brushes.Blue, 100, 180);
                reporte.Graphics.DrawString("Nombre del Socio", letraTitulo2, Brushes.Blue, 200, 180);
                reporte.Graphics.DrawString("Deuda", letraTitulo2, Brushes.Blue, 450, 180);

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
                        if (Convert.ToInt32(dr["Deuda"]) > 0)
                        {
                            reporte.Graphics.DrawString(dr["IdSocio"].ToString(), letraTexto, Brushes.Black, 100, f);
                            reporte.Graphics.DrawString(dr["Nombre"].ToString(), letraTexto, Brushes.Black, 200, f);
                            reporte.Graphics.DrawString("$ "+dr["Deuda"].ToString(), letraTexto, Brushes.Black, 450, f);
                            f = f + 20;
                        }
                    }

                }
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void ImprimirPorActividad(Int32 idAct,PrintPageEventArgs reporte, string act)
        {
            try
            {
                Font letraTitulo1 = new Font("Arial", 20);
                Font letraTitulo2 = new Font("Arial", 14);
                Font letraTexto = new Font("Arial", 10);
                Int32 f = 220;

                reporte.Graphics.DrawString("Listado de Socios de la actividad: "+act, letraTitulo1, Brushes.Red, 100, 100);
                reporte.Graphics.DrawString("Codigo", letraTitulo2, Brushes.Blue, 100, 180);
                reporte.Graphics.DrawString("Nombre del Socio", letraTitulo2, Brushes.Blue, 200, 180);
                reporte.Graphics.DrawString("Deuda", letraTitulo2, Brushes.Blue, 450, 180);

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
                        if (Convert.ToInt32(dr["idActividad"])==idAct)
                        {
                            reporte.Graphics.DrawString(dr["IdSocio"].ToString(), letraTexto, Brushes.Black, 100, f);
                            reporte.Graphics.DrawString(dr["Nombre"].ToString(), letraTexto, Brushes.Black, 200, f);
                            reporte.Graphics.DrawString("$"+dr["Deuda"].ToString(), letraTexto, Brushes.Black, 450, f);
                            f = f + 20;
                        }
                    }

                }
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void ImprimirPorBarrio(Int32 idBar, PrintPageEventArgs reporte,string barrio)
        {
            try
            {
                Font letraTitulo1 = new Font("Arial", 20);
                Font letraTitulo2 = new Font("Arial", 14);
                Font letraTexto = new Font("Arial", 10);
                Int32 f = 220;

                reporte.Graphics.DrawString("Listado de Socios del barrio: "+barrio, letraTitulo1, Brushes.Red, 100, 100);
                reporte.Graphics.DrawString("Codigo", letraTitulo2, Brushes.Blue, 100, 180);
                reporte.Graphics.DrawString("Nombre del Socio", letraTitulo2, Brushes.Blue, 200, 180);
                reporte.Graphics.DrawString("Deuda", letraTitulo2, Brushes.Blue, 450, 180);

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
                        if (Convert.ToInt32(dr["idBarrio"]) == idBar)
                        {
                            reporte.Graphics.DrawString(dr["IdSocio"].ToString(), letraTexto, Brushes.Black, 100, f);
                            reporte.Graphics.DrawString(dr["Nombre"].ToString(), letraTexto, Brushes.Black, 200, f);
                            reporte.Graphics.DrawString("$ "+dr["Deuda"].ToString(), letraTexto, Brushes.Black, 450, f);
                            f = f + 20;
                        }
                    }

                }
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        //Asigna a deudas maxima y minima valores extraordinarios
        //que serviran para comparar y obtener los valores definitivos
        private void InicializarMaxYMin()
        {
            deuMayor = 0;
            deuMenor = 10000000;
        }
    }

}


