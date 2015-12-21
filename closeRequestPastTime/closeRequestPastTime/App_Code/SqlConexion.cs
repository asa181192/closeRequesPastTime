using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
//@autor Alfredo Santiago Alvarado
namespace closeRequestPastTime
{
    class SqlConexion
    {
         private string connect ;
        SqlConnection myConnection;
        SqlCommand cmd;
        public SqlConexion() //Constructor genera variables para la coneccion 
        {
            connect = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
             myConnection = new SqlConnection(connect);
             cmd = new SqlCommand();
          
        }


        public List<String> consultaTickets(string query)
        {
            List<string> ticketsId = new List<string> () ;
            SqlDataReader reader ;
            try
            {
               cmd.Connection = myConnection;
               cmd.CommandText = query;
               myConnection.Open();
               reader= cmd.ExecuteReader();
                while (reader.Read()) {
                    ticketsId.Add(reader["id"].ToString());
                }
            }
            catch (Exception ex ) {
                Console.WriteLine(ex);
            }

            return ticketsId;
           
        }
        
    }
}
