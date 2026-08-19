using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Org.BouncyCastle.Security;
using ProjetoNilson4.Libraries.Login;
using ProjetoNilson4.Models.Constant;

namespace ProjetoNilson4.Libraries.FIltro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private string _tipoColaboradorAutorizado;
        public ColaboradorAutorizacaoAttribute(string tipoColaboradorAutorizado = ColaboradorTipoConstant.Comum)
        {
            _tipoColaboradorAutorizado = tipoColaboradorAutorizado;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            Models.Colaborador colaborador = _loginColaborador.GetColaborador();
            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if(colaborador.Tipo == ColaboradorTipoConstant.Comum && _tipoColaboradorAutorizado == ColaboradorTipoConstant.Gerente)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
