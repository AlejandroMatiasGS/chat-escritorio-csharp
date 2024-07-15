using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServer.Controlador {
    internal class ConexionBD {
        private SqlConnection con;

        public ConexionBD() {
            con = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=chat_c#;Persist Security Info=True;User ID=sa;Password=sqlserversa");
        }

        public bool Open() {
            try {
                con.Open();
                return true;
            } catch { return false; }
        }

        public bool ExecuteUpdate(string query) {
            try {
                SqlCommand cmd = new SqlCommand(query, con);
                return cmd.ExecuteNonQuery() > 0;
            }catch { return false; }
        }

        public SqlDataReader ExecuteReader(string query) {
            try {
                SqlCommand cmd = new SqlCommand(query, con);
                return cmd.ExecuteReader();
            }catch(Exception e) { 
                Console.WriteLine(e.Message);
                return null; 
            }
        }

        public bool Close() {
            try {
                con.Close();
                return true;
            }catch { return false; }
        }
    }
}
