using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace ProjectService
{
    [ServiceContract]
    public interface IService2
    {
        [OperationContract]
        void GuardarInformacion(string Nombre, string Apellidos,
            string Sexo, string Email, string Direccion,
            string Ciudad, string Requerimiento);
    }
}

