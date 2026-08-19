using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ProjetoNilson4.Libraries.Sessao;
using ProjetoNilson4.Models;
using ProjetoNilson4.Models.Constant;
using ZstdSharp.Unsafe;
namespace ProjetoNilson4.Libraries.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private Sessao.Sessao _sessao;
        public LoginColaborador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Colaborador colaborador)
        {
            //serializar
            string colaboradorJSONString = JsonConvert.SerializeObject(colaborador);

            _sessao.Cadastrar(Key, colaboradorJSONString);
        }

        public Colaborador GetColaborador()
        {
            //deserializar
            if (_sessao.Existe(Key))
            {
                string colaboradorJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJSONString);
            }
            else
            {
                return null;
            }
        }
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }

    public class ColaboradorAutorizaçãoAtribute : Attribute, IAuthorizationFilter
    {
        private string tipoColaboradorAutorizado;
        public ColaboradorAutorizaçãoAtribute(string TipoColaboradorAutorizado = ColaboradorTipoConstant.Comum) {
            tipoColaboradorAutorizado = TipoColaboradorAutorizado;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new NotImplementedException();
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            LoginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            Models.Colaborador colaborador = LoginColaborador.GetColaborador();
            if (colaborador == null) {
                context.Result = new RedirectToActionResult("Loign, "Home", null");
            }
            else if (colaborador.Tipo == ColaboradorTipoConstant.Comum && _tipoColaboradorAutorizado == ColaboradorTipoConstant.Gerente) {
                context.Result = new ForbidResult();
            }
        }

        public IActionResult Login([FromForm] Models.Colaborador colaborador))
        {
            Models.Colaborador colaboradorDB = _repositoryColaborador.Login(colaborador.Email, colaborador.Senha)
        }
}

