using ClienteChat.Modelo;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ClienteChat.Controlador {
    internal class UsuarioFile {
        private static readonly string ConnectionString = @"Data Source=users.db";
        private SqliteConnection con;

        public UsuarioFile() {
            con = new SqliteConnection(ConnectionString);
            con.Open();
        }

        public void Guardar(Usuario u, Usuario usuario) {
            try {
                string sql = "insert into user values (" + u.Id + ", " + usuario.Id + ", '" + usuario.Nombre + "', '" + usuario.Correo + "');";
                using (var cmd = new SqliteCommand(sql, con)) {
                    cmd.ExecuteNonQuery();
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Usuario> GetUsers(int id) {
            List<Usuario> users = new List<Usuario>();
            try {
                string sql = "Select IdUserChat, Nombre, Correo from user where Id=" + id + "";
                using(var cmd = new SqliteCommand(sql, con)) {
                    SqliteDataReader reader = cmd.ExecuteReader();

                    while(reader.Read()) {
                        users.Add(new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), ""));
                    }
                }

                return users;
            }catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return new List<Usuario>();
            }
        }
    }
}
