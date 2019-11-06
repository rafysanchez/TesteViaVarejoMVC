using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using TesteViaVarejoMVC.Models;

namespace TesteViaVarejoMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly string _uriServicoExterno = "http://localhost:62589/TesteViaVarejo.API/";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Logar(UsuarioViewModel usuario)
        {
            UsuarioViewModel usuVM = new UsuarioViewModel();
            if (usuario != null && usuario.EhValido())
            {
                usuVM = LogarUsuario(usuario);
            }
            return new JsonResult(usuVM);
        }

        public ActionResult Cadastrar(int id)
        {
            return View(new UsuarioViewModel());
        }

        [HttpPost]
        public JsonResult Cadastrar(UsuarioViewModel usuario)
        {
            UsuarioViewModel usuVM = new UsuarioViewModel();
            if (usuario != null && usuario.EhValido())
            {
                usuVM = CadastrarUsuario(usuario);
            }
            return new JsonResult(usuVM);
        }

        public ActionResult Atualizar(int id)
        {
            UsuarioViewModel model = BuscarUsuario(id);
            model.SenhaHash = String.Empty;
            return View(model);
        }

        [HttpPost]
        public JsonResult Atualizar(UsuarioViewModel usuario)
        {
            UsuarioViewModel usuVM = new UsuarioViewModel();
            if (usuario != null && usuario.EhValido())
            {
                usuVM = AtualizarUsuario(usuario);
            }
            return new JsonResult(usuVM);
        }        

        [HttpDelete]
        public JsonResult Excluir(int id)
        {
            if (id > 0)
            {
                ExcluirUsuario(id);
                return new JsonResult(new { data = true });
            }
            return new JsonResult(new { data = false });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region Métodos privados
        private UsuarioViewModel LogarUsuario(UsuarioViewModel usuarioView)
        {
            var client = new RestClient(_uriServicoExterno);
            var request = new RestRequest("/api/Login", Method.POST);
            var json = JsonConvert.SerializeObject(usuarioView);
            client.AddHandler("application/json", new JsonDeserializer());
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            var retorno = client.Execute(request);
            if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<UsuarioViewModel>(retorno.Content);
            }
            return null;
        }
        private UsuarioViewModel CadastrarUsuario(UsuarioViewModel usuarioView)
        {
            var client = new RestClient(_uriServicoExterno);
            var request = new RestRequest("/api/Usuario", Method.PUT);
            var json = JsonConvert.SerializeObject(usuarioView);
            client.AddHandler("application/json", new JsonDeserializer());
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            var retorno = client.Execute(request);
            if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<UsuarioViewModel>(retorno.Content);
            }
            return null;
        }
        private UsuarioViewModel AtualizarUsuario(UsuarioViewModel usuarioView)
        {
            var client = new RestClient(_uriServicoExterno);
            var request = new RestRequest("/api/Usuario", Method.PATCH);
            var json = JsonConvert.SerializeObject(usuarioView);
            client.AddHandler("application/json", new JsonDeserializer());
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            var retorno = client.Execute(request);
            if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //return JsonConvert.DeserializeObject<UsuarioViewModel>(retorno.Content);
                return BuscarUsuario(usuarioView.Id);
            }
            return null;
        }
        private UsuarioViewModel BuscarUsuario(int id)
        {
            var client = new RestClient(_uriServicoExterno);
            var request = new RestRequest("/api/Usuario", Method.GET);
            var retorno = client.Execute(request);
            if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var listaUsuario = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(retorno.Content);
                return listaUsuario.FirstOrDefault(x => x.Id == id);
            }
            return null;
        }
        private void ExcluirUsuario(int id)
        {
            var client = new RestClient(_uriServicoExterno);
            var request = new RestRequest("/api/Usuario/{id}", Method.DELETE);
            request.AddUrlSegment("id", id.ToString());
            var retorno = client.Execute(request);
            if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
            {
                
            }            
        }
        #endregion Métodos privados
    }
}
