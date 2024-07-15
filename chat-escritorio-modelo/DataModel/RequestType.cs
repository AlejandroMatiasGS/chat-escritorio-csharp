using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel {

    [Serializable]
    public enum RequestType {
        InicioSesion,
        SendMessage,
        ReceiveMessage,
        GetUsuario,
        SetLeido
    }
}
