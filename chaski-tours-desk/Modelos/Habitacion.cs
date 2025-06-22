using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chaski_tours_desk.Modelos
{
    internal class Habitacion
    {
        public int nro_habitacion { get; set; }
        public int id_alojamiento { get; set; }
        public string tipo_habitacion { get; set; }
        public int capacidad { get; set; }
        public bool disponible { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
