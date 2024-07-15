using ClienteChat.Controlador;
using ClienteChat.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteChat.Vista {
    public partial class Home : Form {
        private Usuario userHome;
        private Usuario selUser;
        private InicioSesion i;
        private List<Usuario> usuarios;
        private UsuarioFile fileUsers;

        public Home(Usuario u, InicioSesion i) {
            InitializeComponent();
            fileUsers = new UsuarioFile();
            Initialize();
            pnlChatUser.FlowDirection = FlowDirection.TopDown;
            this.userHome = u;
            this.i = i;
        }

        private void Initialize() {
            usuarios = fileUsers.GetUsers(userHome.Id);

            foreach(Usuario user in usuarios) {
                Button btn = new Button();
                btn.Font = new Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular);
                btn.Size = new Size(pnlChatUser.Width - 10, 85);
                btn.Text = user.Nombre;
                btn.Click += (s, e) => { selUser = user; };

                pnlChatUser.Controls.Add(btn);
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e) {
            i.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e) {
            Button b = new Button();
            b.Size = new Size(330, 80);
            b.Text = "HOLAAAAs";
            pnlChatUser.Controls.Add(b);
        }

        private void btnEnviar_Click(object sender, EventArgs e) {

            MessageBox.Show(selUser.Id.ToString() + " " + selUser.Nombre);

            //Label l = new Label();
            //l.Font = new Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular);
            //l.Text = "CHAT: lorem ipsum ajkdahskjdhaskjdakjsdhaskjdhakaldlkasjdlaksjlkajsdlkadjsdhaksdhakjshdakshdkajsdhk";
            //l.BackColor = SystemColors.GrayText;
            //l.AutoSize = true;

            //pnlChat.Controls.Add(l);
        }

        private void btnAgregar_Click(object sender, EventArgs e) {
            BuscarUsuario bu = new BuscarUsuario(this);
            bu.ShowDialog();
        }

        internal void AddUsuario(Usuario usuario) {
            usuarios.Add(usuario);

            Button btn = new Button();
            btn.Font = new Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular);
            btn.Size = new Size(pnlChatUser.Width - 10, 85);
            btn.Text = usuario.Nombre;
            btn.Click += (s, e) => { selUser = usuario; };

            pnlChatUser.Controls.Add(btn);

            fileUsers.Guardar(userHome, usuario);
        }
    }
}
