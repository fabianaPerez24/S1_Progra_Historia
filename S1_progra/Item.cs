using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1.scripts
{
    public class Item
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public Item(string nombre, string descripcion)
        {
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }
}
