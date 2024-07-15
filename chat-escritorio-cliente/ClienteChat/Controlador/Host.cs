using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClienteChat.Controlador {
    internal class Host {
        private Socket s;

        internal Host() {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.ReceiveTimeout = 5000;
            s.SendTimeout = 5000;
        }

        internal bool Conectar(string ip, int port) {
            try {
                IAsyncResult result = s.BeginConnect(ip, port, null, s);
                if (!result.AsyncWaitHandle.WaitOne(1000)) return false;

                s.EndConnect(result);
                return true;
            } catch {
                return false;
            }
        }

        internal bool Enviar(byte[] data) {
            try {
                byte[] bytesL = BitConverter.GetBytes(data.Length);
                s.Send(bytesL);
                s.Send(data);
                return true;
            } catch {
                return false;
            }
        }

        internal byte[] Recibir() {
            try {
                byte[] bytesL = new byte[4];
                s.Receive(bytesL);
                int largo = BitConverter.ToInt32(bytesL, 0);
                byte[] data = new byte[largo];
                s.Receive(data);
                return data;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
