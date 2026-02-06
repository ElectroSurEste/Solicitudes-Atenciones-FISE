using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M0109100164666.Models
{
    public class Coordenadas
    {
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }

        public Coordenadas() { }

        public Coordenadas(DataRow dr)
        {
            this.Latitud = Convert.ToDouble(dr["Latitud"]);
            this.Longitud = Convert.ToDouble(dr["Longitud"]);
        }
    }
}
