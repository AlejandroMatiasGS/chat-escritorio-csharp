using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    internal class Program {
        static void Main(string[] args) {
            Host host = new Host();

            if (host.Conectar("127.0.0.1", 777)) {
                object[] _data = new object[2];
                _data[0] = "maria@gmail.com";
                _data[1] = "dev123";
                Request r = new Request(RequestType.InicioSesion, _data);
                byte[] data = Serializer.Serialize(r);
                if(host.Enviar(data)) {
                    byte[] _req = host.Recibir();
                    object res = Serializer.Deserialize(_req);
                    object[] _res = (object[])res;
                    Console.WriteLine(_res[0].ToString());
                    Console.WriteLine(_res[1].ToString());
                    Console.ReadLine();
                }else {
                    Console.WriteLine("No se envió");
                }
            }else {
                Console.WriteLine("No se pudo conectar");
            }
        }
    }
}
