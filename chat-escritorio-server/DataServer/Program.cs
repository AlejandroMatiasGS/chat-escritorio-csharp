using DataServer.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataServer {
    internal class Program {
        static void Main(string[] args) {
            int opc = -1;
            Servicio s = null;
            Intercessor inter;
            bool running = false;


            do {
                Console.WriteLine("Menú Server");
                Console.WriteLine("");
                Console.WriteLine("(1) Iniciar Servidor.");
                Console.WriteLine("(2) Detener Servidor.");
                Console.WriteLine("(0) Salir.");
                Console.WriteLine("");
                opc = -1;

                while (opc == -1) {
                    try {
                        opc = Convert.ToInt32(Console.ReadLine());
                    } catch (FormatException) {
                        Console.WriteLine("Ingrese sólo números.");
                        Console.WriteLine();
                    }
                }

                switch (opc) {
                    case 1:
                        if(!running) {
                            inter = new Intercessor();

                            if(inter.Open()) {
                                s = new Servicio(inter);
                                s.Start();
                                Console.WriteLine("Servidor iniciado.");
                                Console.WriteLine();
                                running = true;
                            }else {
                                Console.WriteLine("No se pudo conectar a la BD");
                                Console.WriteLine("");  
                            }                            
                        }else {
                            Console.WriteLine("El servidor está en ejcución.");
                            Console.WriteLine();
                        }
                        break;

                    case 2:
                        if(running) {
                            s.Stop();
                            s.Wait();
                            Console.WriteLine("Servidor detenido.");
                            Console.WriteLine("");
                            running = false;
                        }else {
                            Console.WriteLine("El servidor está detenido.");
                            Console.WriteLine();
                        }
                        break;

                    case 0:
                        if(running) {
                            Console.WriteLine("Debe detener el servidor antes de salir.");
                            Console.WriteLine();
                            opc = -1;
                        }
                        break;
                }

                   
            } while (opc != 0);

        }
    }
}

