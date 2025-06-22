using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chaski_tours_desk.Modelos
{
    public class Transporte
    {
        public int id_vehiculo { get; set; }
        public string matricula { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int capacidad { get; set; }
        public string anio { get; set; }
        public int disponible { get; set; }
        public int activo { get; set; }

        

    }
}
