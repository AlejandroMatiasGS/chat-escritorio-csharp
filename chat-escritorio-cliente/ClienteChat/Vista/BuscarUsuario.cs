using ClienteChat.Controlador;
using ClienteChat.Modelo;
using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteChat.Vista {
    public partial class BuscarUsuario : Form {
        private Home home;
        public BuscarUsuario(Home home) {
            InitializeComponent();
            this.home = home;   
        }

        private void btnBuscar_Click(object sender, EventArgs e) {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            string correo = txtCorreo.Text;
            string nombre = txtNombre.Text;

            if(correo != String.Empty && nombre != String.Empty) {
                if(regex.IsMatch(correo)) {
                    Host h = new Host();

                    if (h.Conectar("127.0.0.1", 777)) {
                        Request req = new Request(RequestType.GetUsuario, correo);
                        byte[] _req = Serializer.Serialize(req);

                        if (h.Enviar(_req)) {
                            byte[] _res = h.Recibir();

                            if (_res != null) {
                                object res = Serializer.Deserialize(_res);

                                if (res != null) {
                                    
                                    if(res is bool) {
                                        MessageBox.Show("No existe un usuario con este correo!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                    int id = (int)res;

                                    home.AddUsuario(new Usuario(id, nombre, correo, ""));
                                    this.Close();

                                } else {
                                    MessageBox.Show("Error al deserializar datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            } else {
                                MessageBox.Show("Error al recibir datos del servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        } else {
                            MessageBox.Show("Error al enviar datos al servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } else {
                        MessageBox.Show("Error al conectar con el servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    MessageBox.Show("Ingrese un correo válido.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }else {
                MessageBox.Show("Debe ingresar algún correo.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
