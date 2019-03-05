using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TorneioLutas.Models;

namespace TorneioLutas.Controllers
{
    public class TorneioController : Controller
    {

        #region Actions

        // GET: Torneio
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var listaLutadores = retornaLutadores();

                return View(listaLutadores);
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Erro ao buscar competidores. Erro: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(List<Lutador> listaLutadores, FormCollection collection)
        {
            var listaSelecionados = listaLutadores.Where(x => x.selecionado).ToList();

            if (listaSelecionados.Count < 20)
            {
                TempData["Msg"] = "Necessário selecionar 20 lutadores para iniciar o torneio";
                return View(listaLutadores);
            }
            else
                TempData["Msg"] = "";

            List<Lutador> G1 = new List<Lutador>();
            List<Lutador> G2 = new List<Lutador>();
            List<Lutador> G3 = new List<Lutador>();
            List<Lutador> G4 = new List<Lutador>();
            int aux = 1;
            //Cria Grupos
            foreach (var item in listaSelecionados.ToList().OrderBy(x => x.idade))
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






            Vencedores oVencedores = new Vencedores();


            return RedirectToAction("Resultado", "Torneio", oVencedores);
        }

        #endregion

        #region Métodos

        private List<Lutador> retornaLutadores()
        {
            try
            {
                List<Lutador> oListLutador = new List<Lutador>();

                using (var client = new WebClient())
                {
                    var json = client.DownloadString("http://177.36.237.87/lutadores/api/competidores");

                    byte[] bytes = Encoding.Default.GetBytes(json);
                    json = Encoding.UTF8.GetString(bytes);

                    var serializer = new JavaScriptSerializer();
                    oListLutador = serializer.Deserialize<List<Lutador>>(json);
                }

                return oListLutador;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        #endregion

    }
}