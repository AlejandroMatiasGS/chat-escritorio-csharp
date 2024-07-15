using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer.Controlador {
    internal class Host {
        private Socket serverSocket;
        private Socket clientSocket;

        internal Host(int port) {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(1);
            serverSocket.ReceiveTimeout = 5000;
            serverSocket.SendTimeout = 5000;
        }

        internal bool Aceptar() {
            IAsyncResult ar = serverSocket.BeginAccept(null, null);

            if (ar.AsyncWaitHandle.WaitOne(10000)) {
                clientSocket = serverSocket.EndAccept(ar);
                return true;
            } else {
                return false;
            }
        }

        internal async Task<bool> AceptarAsync() {
            try {
                Task<Socket> taskSocket = serverSocket.AcceptAsync();

                Task completed = await Task.WhenAny(taskSocket, Task.Delay(3000));

                if(completed == taskSocket) {
                    clientSocket = taskSocket.Result;
                    return true;
                }else { 
                    return false;
                }
            } catch {
                return false;
            }
        }

        public bool Enviar(byte[] data) {
            try {
                byte[] bytesL = BitConverter.GetBytes(data.Length);
                clientSocket.Send(bytesL);
                clientSocket.Send(data);
                return true;
            }catch {
                return false;
            }
        }

        internal byte[] Recibir() {
            try {
                byte[] bytesL = new byte[4];
                clientSocket.Receive(bytesL);
                int largo = BitConverter.ToInt32(bytesL, 0);
                byte[] data = new byte[largo];
                clientSocket.Receive(data);
                return data;
            }catch(Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        internal void CloseClientSocket() {
            clientSocket.Close();
        }

        internal void CloseServerSocket() {
            serverSocket.Close();  
        }
    }
}
