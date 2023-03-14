using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace EmailComunicationDealToSC.Aplication
{
    public class RegisterDAL
    {

        conexion _CON = new conexion();

        public int resgisterEmailState(int personId)
        {
            int response = 0;
            try
            {
               var CON = _CON.CrearConexion();

                    bool resp = CON.Query<bool>(@"insert into CrmMkt.EmailToSC values(GETDATE(),@personId)", new { personId = personId}).FirstOrDefault();
                if (true)
                {
                    response = 200;
                }
                _CON.CerrarConexion(CON);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                response = 500;
            }
            return response;
        }
    }
}
