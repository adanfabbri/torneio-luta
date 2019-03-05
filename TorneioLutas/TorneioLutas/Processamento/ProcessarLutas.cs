using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TorneioLutas.Models;

namespace TorneioLutas.Processamento
{
    public class ProcessarLutas
    {

        public Lutador Lutar(Lutador lutador1, Lutador lutador2)
        {
            var perc1 = Convert.ToInt32((lutador1.vitorias / lutador1.lutas) * 100);
            var perc2 = Convert.ToInt32((lutador2.vitorias / lutador2.lutas) * 100);

            if (perc1 > perc2)
                return lutador1;
            else if (perc2 > perc1)
                return lutador2;
            else
            {
                //Desempate
                if (lutador1.artesMarciais.Count() > lutador2.artesMarciais.Count())
                    return lutador1;
                else if (lutador2.artesMarciais.Count() > lutador1.artesMarciais.Count())
                    return lutador2;
                else 
                {
                    if (lutador1.lutas > lutador2.lutas)
                        return lutador1;
                    else
                        return lutador2;
                }
              
            }
        }
        
    }
}