using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace pregunta8
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }
        //
        //metodos para la base
        //
        [WebMethod]
        public DataSet usuarios()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"server=DESKTOP-4NU7F5H;database=bdmireyaconsuelomamanicarita;user=mizmi;pwd=12345";
            SqlDataAdapter ada = new SqlDataAdapter();
            ada.SelectCommand = new SqlCommand();
            ada.SelectCommand.Connection = con;
            ada.SelectCommand.CommandText = "SELECT * FROM usuario WHERE activo=1";
            DataSet ds = new DataSet();
            ada.Fill(ds, "usuario");
            return ds;
        }
        [WebMethod]
        public void ModificarUsuario(int id, string nombre, string tipoUs, string ci, DateTime fecha, int telefono, string pwd)
        {
            string connectionString = @"server=DESKTOP-4NU7F5H;database=bdmireyaconsuelomamanicarita;user=mizmi;pwd=12345";
            SqlConnection con = new SqlConnection(connectionString);
            string query = "UPDATE usuario SET nombre = @nombre, tipo_us = @tipoUs, ci = @ci, fecha = @fecha, telefono = @telefono, pwd = @pwd WHERE id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@tipoUs", tipoUs);
            cmd.Parameters.AddWithValue("@ci", ci);
            cmd.Parameters.AddWithValue("@fecha", fecha);
            cmd.Parameters.AddWithValue("@telefono", telefono);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        [WebMethod]
        public void EliminarUsuario(int idUsuario)
        {
            string connectionString = @"server=DESKTOP-4NU7F5H;database=bdmireyaconsuelomamanicarita;user=mizmi;pwd=12345";
            SqlConnection con = new SqlConnection(connectionString);
            string queryUsuario = "UPDATE usuario SET activo = 0 WHERE id = @idUsuario";
            string queryCuentas = "UPDATE cuenta SET activo = 0 WHERE id_us = @idUsuario";
            SqlCommand cmdUsuario = new SqlCommand(queryUsuario, con);
            SqlCommand cmdCuentas = new SqlCommand(queryCuentas, con);
            cmdUsuario.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmdCuentas.Parameters.AddWithValue("@idUsuario", idUsuario);
            con.Open();
            cmdUsuario.ExecuteNonQuery();
            cmdCuentas.ExecuteNonQuery();
            con.Close();
        }
        [WebMethod]
        public void AgregarUsuario(string nombre, string tipoUs, string ci, DateTime fecha, int telefono, string pwd)
        {
            string connectionString = @"server=DESKTOP-4NU7F5H;database=bdmireyaconsuelomamanicarita;user=mizmi;pwd=12345";
            SqlConnection con = new SqlConnection(connectionString);
            string query = "INSERT INTO usuario (nombre, ci,tipo_us, fecha, telefono, pwd, activo) VALUES (@nombre, @ci,@tipoUs ,@fecha, @telefono, @pwd, 1)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@tipoUs", tipoUs);
            cmd.Parameters.AddWithValue("@ci", ci);
            cmd.Parameters.AddWithValue("@fecha", fecha);
            cmd.Parameters.AddWithValue("@telefono", telefono);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        [WebMethod]
        public bool VerificarUsuarioExiste(string nombre, string ci)
        {
            string connectionString = @"server=DESKTOP-4NU7F5H;database=bdmireyaconsuelomamanicarita;user=mizmi;pwd=12345";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM usuario WHERE nombre = @nombre OR ci = @ci";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@ci", ci);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }


    }
}
