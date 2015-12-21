using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using System.Threading;

/* @Author Alfredo Santiago Alvarado 
 * 18-12-2015
 * proceso para cerrar tickets de la aplicacion servicedesk , en cuestion la aplicacion
 * ejecuta un SP que devuelve los Id de los tickets que se encuentran en status Resolved 
 * con mas de 5 dias , para posteriormente cerrarlos por medio de el API se Service Desk 
 * mas info : https://www.manageengine.com/products/service-desk/help/adminguide/api/rest-api.html
 * 
 * Actualizaciones  
 * 
 * */
namespace closeRequestPastTime
{
    class Program
    {
        static void Main(string[] args)
        {
            String Url, ticket;
            SqlConexion conn = new SqlConexion();
            List<string> lista = new List<string>();

            lista = conn.consultaTickets("execute SPticketsPendingClose");
            HttpUtility enCoding = new HttpUtility();

            foreach (string value in lista)
            {
                try
                {
                    ticket = value;
                    Url = "http://helpdesk.transnetwork.com:8383/sdpapi/request/{0}/?TECHNICIAN_KEY=9CA0D982-58CC-456F-9525-CEEF00B5B7E0&OPERATION_NAME=CLOSE_REQUEST";
                    Url = string.Format(Url, ticket);

                    HttpWebRequest req = WebRequest.Create(Url) as HttpWebRequest;
                    using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                    {
                        StreamReader reader = new StreamReader(resp.GetResponseStream());
                        string resultado = reader.ReadToEnd();
                        Console.WriteLine(resultado);
                    }
                  
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }                       


               
            } // end foreach 
        }// end main 
    }// end class
}