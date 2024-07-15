using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataServer.Controlador {
    internal class Serializer {
        internal static byte[] Serialize(object obj) {
            try {
                using (MemoryStream ms = new MemoryStream()) {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return null; 
            }
        }

        internal static object Deserialize(byte[] data) {
            try {
                using (MemoryStream ms = new MemoryStream(data)) {
                    BinaryFormatter bf = new BinaryFormatter();
                    return bf.Deserialize(ms);
                }
            }catch(Exception e) {
                Console.WriteLine(e.Message);
                return null; 
            }
        }
    }
}
