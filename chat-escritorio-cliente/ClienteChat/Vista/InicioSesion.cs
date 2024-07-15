using ClienteChat.Controlador;
using ClienteChat.Modelo;
using DataModel;    
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteChat.Vista {
    public partial class InicioSesion : Form {
        public InicioSesion() {
            
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e) {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            string correo = txtCorreo.Text;
            string pass = txtPass.Text;

            if (correo != String.Empty && pass != String.Empty) {
                if (regex.IsMatch(correo)) {
                    Host h = new Host();

                    if (h.Conectar("127.0.0.1", 777)) {
                        object[] data = new object[2];
                        data[0] = correo;
                        data[1] = pass;

                        Request req = new Request(RequestType.InicioSesion, data);
                        byte[] _req = Serializer.Serialize(req);

                        if (h.Enviar(_req)) {
                            byte[] _res = h.Recibir();

                            if (_res != null) {
                                object res = Serializer.Deserialize(_res);

                                if (res != null) {
                                    if(res is bool) {
                                        MessageBox.Show("Credenciales inválidas!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }

                                    object[] _data = (object[])res;

                                    Home home = new Home(new Usuario(Convert.ToInt32(_data[0]), _data[1].ToString(), "", ""), this);
                                    this.Visible = false;
                                    txtCorreo.Text = "";
                                    txtPass.Text = "";
                                    home.Show();

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
                    MessageBox.Show("Correo inválido", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } else {
                MessageBox.Show("Complete los campos!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        
    }
}
