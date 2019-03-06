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
using TorneioLutas.Processamento;

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
            try
            {
                var listaSelecionados = listaLutadores.Where(x => x.selecionado).ToList();

                if (listaSelecionados.Count != 20)
                {
                    TempData["Msg"] = "Necessário selecionar 20 lutadores para iniciar o torneio";
                    return View(listaLutadores);
                }
                else
                    TempData["Msg"] = "";

                ProcessarLutas oProcessarLutas = new ProcessarLutas();
                var oVencedores = oProcessarLutas.IniciarTorneio(listaSelecionados);

                return RedirectToAction("Resultado", "Torneio", new { campeao = oVencedores.primeiro.nome, segundo = oVencedores.segundo.nome, terceiro = oVencedores.terceiro.nome });
                
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Erro durante o procesamento. Erro: " + ex.Message;
                return View();
            }
          
        }

        [HttpGet]
        public ActionResult Resultado(string campeao, string segundo, string terceiro)
        {
            try
            {
                Resultado oResultado = new Resultado();
                oResultado.campeao = campeao;
                oResultado.segundo = segundo;
                oResultado.terceiro = terceiro;
                return View(oResultado);
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Erro: " + ex.Message;
                return View();
            }
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