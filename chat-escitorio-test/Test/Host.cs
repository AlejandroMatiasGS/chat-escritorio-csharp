using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    internal class Host {   
        private Socket s;

        public Host() {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.ReceiveTimeout = 10000;
            s.SendTimeout = 10000;
        }

        public bool Conectar(string ip, int port) {
            try {
                IAsyncResult result = s.BeginConnect(ip, port, null, s);
                if (!result.AsyncWaitHandle.WaitOne(1000)) return false;

                s.EndConnect(result);
                return true;
            } catch {
                return false;
            }
        }

        public bool Enviar(byte[] data) {
            try {
                byte[] bytesL = BitConverter.GetBytes(data.Length);
                s.Send(bytesL);
                s.Send(data);
                return true;
            } catch {
                return false;
            }
        }

        public byte[] Recibir() {
            try {
                byte[] bytesL = new byte[4];
                s.Receive(bytesL);
                int largo = BitConverter.ToInt32(bytesL, 0);
                byte[] data = new byte[largo];
                s.Receive(data);
                return data;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
