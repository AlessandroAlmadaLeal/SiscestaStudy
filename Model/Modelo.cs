using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscesta.Model
{
    public class Modelo : ClasseBase
    {
        public string Descricao { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
    }
}
