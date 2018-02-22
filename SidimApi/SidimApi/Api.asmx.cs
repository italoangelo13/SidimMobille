using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SidimApi
{
    /// <summary>
    /// Summary description for Api
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Api : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void autenticar(string user, string senha)
        {
            BancoDados banco = new BancoDados();
            string json;
            string query = @"SELECT USU_CODIGO_ID 
                               FROM USUARIOS
                              WHERE USU_NOME = ?USU_NOME 
                AND USU_SENHA = ?USU_SENHA";
            banco.Query(query);
            banco.SetParametro("?USU_NOME", user);
            banco.SetParametro("?USU_SENHA", FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "md5"));
            DataTable dt = banco.ExecutarDataTable();
            if (dt.Rows.Count > 0)
            {
                json = "{autenticado:true,codUsuario:" + dt.Rows[0]["USU_CODIGO_ID"].ToString() + "}"; 
            }
            else
            {
                json = "{autenticado:false}";
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(json));
            
        }
    }
}
