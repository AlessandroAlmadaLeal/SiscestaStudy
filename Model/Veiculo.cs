using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siscesta.Model
{
    public class Veiculo : ClasseBase
    {
        public string Placa { get; set; }
        public string Cor { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
        public int ModeloId { get; set; }
        public Modelo Modelo { get; set; }
        public int PessoaId { get; set; }
        public Pessoa pessoa { get; set; }

    }
}
