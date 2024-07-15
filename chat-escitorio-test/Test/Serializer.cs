using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    internal class Serializer {

        public static byte[] Serialize(object obj) {
            try {
                using (MemoryStream ms = new MemoryStream()) {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            } catch { return null; }
        }

        public static object Deserialize(byte[] data) {
            try {
                using (MemoryStream ms = new MemoryStream(data)) {
                    BinaryFormatter bf = new BinaryFormatter();
                    return bf.Deserialize(ms);
                }
            } catch { return null; }
        }
    }
}
