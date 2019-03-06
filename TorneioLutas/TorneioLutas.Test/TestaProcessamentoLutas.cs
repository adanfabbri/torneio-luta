using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TorneioLutas.Models;
using TorneioLutas.Processamento;

namespace TorneioLutas.Test
{
    [TestClass]
    public class TestaProcessamentoLutas
    {
        [TestMethod]
        public void TestaLutar()
        {
            Lutador oLutador1 = new Lutador();
            oLutador1.nome = "Teste 1";
            oLutador1.artesMarciais = new List<string>();
            oLutador1.artesMarciais.Add("Karate");
            oLutador1.derrotas = 10;
            oLutador1.idade = 30;
            oLutador1.lutas = 40;
            oLutador1.vitorias = 30;

            Lutador oLutador2 = new Lutador();
            oLutador2.nome = "Teste 2";
            oLutador2.artesMarciais = new List<string>();
            oLutador2.artesMarciais.Add("Jiu Jitsu");
            oLutador2.artesMarciais.Add("Boxe");
            oLutador2.derrotas = 5;
            oLutador2.idade = 33;
            oLutador2.lutas = 50;
            oLutador1.vitorias = 45;

            ProcessarLutas oPL = new ProcessarLutas();

            var lResultado = oPL.Lutar(oLutador1, oLutador2);

            Assert.AreEqual("Teste 1", lResultado.nome);
                       

        }
    }
}
