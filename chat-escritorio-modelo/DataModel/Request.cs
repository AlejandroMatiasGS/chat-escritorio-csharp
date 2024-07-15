using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel {

    [Serializable]
    public class Request {
        public RequestType Type { get; }
        public Object Data { get; }

        public Request(RequestType type, Object data) {
            Type = type;
            Data = data;
        }
    }
}
