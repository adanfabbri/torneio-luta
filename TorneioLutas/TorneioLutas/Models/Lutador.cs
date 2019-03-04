using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TorneioLutas.Models
{
    public class Lutadores
    {
        public List<Lutador> lutadores { get; set; }
       
    }

    public class Lutador
    {
        public string nome { get; set; }
        public int idade { get; set; }
        public List<string> artesMarciais { get; set; }
        public int lutas { get; set; }
        public int derrotas { get; set; }
        public int vitorias { get; set; }
        public bool selecionado { get; set; }
    }
}