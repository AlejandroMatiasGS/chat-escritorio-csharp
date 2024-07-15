using DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DataServer.Controlador {
    internal class Intercessor {
        private ConexionBD con;

        internal Intercessor() {
            con = new ConexionBD();
        }

        internal bool Open() {
            return con.Open();
        }

        internal object IniciarSesion(object _req) {
            object[] req = (object[])_req;
            string correo = Convert.ToString(req[0]);
            string pass = Convert.ToString(req[1]);
            SqlDataReader reader = con.ExecuteReader("Select Id, Nombre From Usuario Where Correo='"+correo+"' and Contrasena='"+pass+"'");

            if(reader.Read()) {
                object[] data = new object[2];
                data[0] = reader[0];
                data[1] = reader[1];
                return data;
            }else { return false; }
        }

        internal bool Close() {
            return con.Close();
        }

        internal bool EnviarMensaje(object _req) {
            object[] req = (object[])_req;
            int idE = Convert.ToInt32(req[0]);
            int idR = Convert.ToInt32(req[1]);
            string msg = req[2].ToString();

            if (con.ExecuteUpdate("insert into Mensaje values ('"+msg+"', "+idE+", "+idR+");")) return true;
            else { return false; }
        }

        internal object RecibirMensaje(object _req) {
            object[] req = (object[])_req;
            int id = Convert.ToInt32(req[0]);
            List<object[]> mensajes = new List<object[]>();

            SqlDataReader reader = con.ExecuteReader("select Id, Mensaje, Emisor from Mensaje where Receptor="+id+" and Leido=0;");

            while(reader.Read()) {
                object[] obj = new object[3];
                obj[0] = reader.GetInt32(0);
                obj[1] = reader[1];
                obj[2] = reader.GetInt32(2);
                mensajes.Add(obj);
            }

            if (mensajes.Count > 0) return mensajes;
            else return null;
        }   

        internal object GetUsuario(object _req) {
            string correo = _req.ToString();

            SqlDataReader reader = con.ExecuteReader("Select Id from Usuario where Correo='"+correo+"'");

            if(reader.Read()) {
                return reader.GetInt32(0);
            }else { return false; }
        }

        internal object SetLeido(object _req) {
            object[] req = (Object[])_req;
            int id = Convert.ToInt32(req[0]);

            if (con.ExecuteUpdate("Update Mensaje Set Leido=1 where Id="+id+"")) return true;
            else { return false; }
        }
    }
}
