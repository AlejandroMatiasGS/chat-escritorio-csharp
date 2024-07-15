using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer.Controlador {
    internal class Servicio {
        private Thread hilo;
        private bool stop;
        private Intercessor i;

        internal Servicio(Intercessor _i) {
            hilo = new Thread(new ThreadStart(this.Run));
            stop = false;
            i = _i;
        }

        internal void Wait() {
            hilo.Join();
        }

        internal void Start() {
            hilo.Start();
        }

        internal void Stop() {
            stop = true;
        }

        internal void Run() {
            Host h = new Host(777);

            while (!stop) {
                if (h.AceptarAsync().Result) {
                    Console.WriteLine("Conectado");

                    byte[] dataReq = h.Recibir();
                    if (dataReq != null) {
                        Request req = (Request)Serializer.Deserialize(dataReq);
                        if (req != null) {
                            object _dataRes = DelegateRequest(req);
                            byte[] dataRes = Serializer.Serialize(_dataRes);

                            if (dataRes != null) {
                                if (h.Enviar(dataRes)) {

                                } else { Console.WriteLine("No Envió"); }
                            } else { Console.WriteLine("No Serializó"); }
                        } else { Console.WriteLine("No Deserializó"); }
                    } else { Console.WriteLine("No recibió bien"); }

                    h.CloseClientSocket();
                } else { }
            }

            h.CloseServerSocket();
            i.Close();
        }

        internal object DelegateRequest(Request req) {
            switch (req.Type) {
                case RequestType.InicioSesion:
                    return i.IniciarSesion(req.Data);

                case RequestType.SendMessage:
                    return i.EnviarMensaje(req.Data);

                case RequestType.ReceiveMessage:
                    return i.RecibirMensaje(req.Data);

                case RequestType.GetUsuario:
                    return i.GetUsuario(req.Data);

                case RequestType.SetLeido:
                    return i.SetLeido(req.Data);

                default: return null;
            }
        }
    }
}
