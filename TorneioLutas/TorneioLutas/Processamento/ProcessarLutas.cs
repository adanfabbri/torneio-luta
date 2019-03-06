using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TorneioLutas.Models;

namespace TorneioLutas.Processamento
{
    public class ProcessarLutas
    {

        public Vencedores IniciarTorneio(List<Lutador> listaLutadores)
        {
            Vencedores oVencedores = new Vencedores();

            try
            {
                //1 - Cria Grupos 
                List<Lutador> G1 = new List<Lutador>();
                List<Lutador> G2 = new List<Lutador>();
                List<Lutador> G3 = new List<Lutador>();
                List<Lutador> G4 = new List<Lutador>();
                int aux = 1;

                foreach (var item in listaLutadores.ToList().OrderBy(x => x.idade))
                {
                    if (aux > 4)
                        aux = 1;

                    switch (aux)
                    {
                        case 1:
                            G1.Add(item);
                            break;
                        case 2:
                            G2.Add(item);
                            break;
                        case 3:
                            G3.Add(item);
                            break;
                        case 4:
                            G4.Add(item);
                            break;
                        default:
                            break;
                    }

                    aux += 1;
                }


                // 2 - Fase de Grupos
                G1 = FaseGrupos(G1);
                G2 = FaseGrupos(G2);
                G3 = FaseGrupos(G3);
                G4 = FaseGrupos(G4);

                // 3 - Quartas de final
                Lutador Q1 = Lutar(G1[0], G2[1]);
                Lutador Q2 = Lutar(G1[1], G2[0]);
                Lutador Q3 = Lutar(G3[0], G4[1]);
                Lutador Q4 = Lutar(G3[1], G4[0]);

                // 4 - Semifinal
                Lutador S1 = Lutar(Q1, Q2);
                Lutador S2 = Lutar(Q3, Q4);

                // 5 - Terceiro
                Lutador P1 = S1 == Q1 ? Q2 : Q1;
                Lutador P2 = S2 == Q3 ? Q4 : Q3;

                oVencedores.terceiro = Lutar(P1, P2);

                //6 - Final
                oVencedores.primeiro = Lutar(S1, S2);
                oVencedores.segundo = oVencedores.primeiro == S1 ? S2 : S1;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oVencedores;

        }

        public Lutador Lutar(Lutador lutador1, Lutador lutador2)
        {
            //var teste = (107 / 115) * 100;
            var perc1 = Convert.ToInt32((Convert.ToDecimal(lutador1.vitorias) / Convert.ToDecimal(lutador1.lutas)) * 100);
            var perc2 = Convert.ToInt32((Convert.ToDecimal(lutador2.vitorias) / Convert.ToDecimal(lutador2.lutas)) * 100);

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

        public List<Lutador> FaseGrupos(List<Lutador> grupo)
        {
            List<Lutador> ret = new List<Lutador>();

            int aux = 1;
            foreach (var lutador in grupo)
            {
                switch (aux)
                {
                    case 1:

                        if (Lutar(lutador, grupo[1]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[1].pontos += 1;

                        if (Lutar(lutador, grupo[2]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[2].pontos += 1;

                        if (Lutar(lutador, grupo[3]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[3].pontos += 1;

                        if (Lutar(lutador, grupo[4]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[4].pontos += 1;

                        break;

                    case 2:

                        if (Lutar(lutador, grupo[2]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[2].pontos += 1;

                        if (Lutar(lutador, grupo[3]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[3].pontos += 1;

                        if (Lutar(lutador, grupo[4]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[4].pontos += 1;

                        break;

                    case 3:

                        if (Lutar(lutador, grupo[3]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[3].pontos += 1;

                        if (Lutar(lutador, grupo[4]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[4].pontos += 1;

                        break;

                    case 4:

                        if (Lutar(lutador, grupo[4]) == lutador)
                            lutador.pontos += 1;
                        else
                            grupo[4].pontos += 1;

                        break;


                    default:
                        break;
                }

                aux += 1;
            }

            ret.Add(grupo.OrderByDescending(x => x.pontos).ToList()[0]);
            ret.Add(grupo.OrderByDescending(x => x.pontos).ToList()[1]);

            return ret;
        }


    }
}