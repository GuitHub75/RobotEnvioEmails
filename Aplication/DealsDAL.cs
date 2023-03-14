using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EmailComunicationDealToSC.Model;
using System.IO;
using System.Net;
using Mandrill;

namespace EmailComunicationDealToSC.Aplication
{
    public class DealsDAL
    {
        public MandrillApi apimd = new MandrillApi("Wt_XKvUnRBpY6pyHMa9j-w");


        DealsEN _deal = new DealsEN();
        conexion _conn = new conexion();
        EmailToUser _email = new EmailToUser();
        RegisterDAL _regdl = new RegisterDAL();
        public int SendEmail()
        {
            List<DealsEN> _list = new List<DealsEN>();
            _list = getDeals();
            int result = 0;
            foreach (var deal in _list)
            {
                int response = ValidateDeal(deal.PersonID);
                if (response == 200)
                {
                    result = _email.SendEmail(deal.Email, deal.Name , deal.PhoneNumber, deal.stage, deal.Days);
                    if (result == 200)
                    {
                      int var = _regdl.resgisterEmailState(deal.PersonID);
                    }
                }
                //else
                //{
                //    Console.WriteLine("El usuario con Id: " + deal.PersonID + " ya fue comunicado a sc");
                //}
            }
            return result;
        }

        public List<DealsEN> getDeals()
        {
            List<DealsEN> _list = new List<DealsEN>();
            try
            {
                var connetion = _conn.CrearConexion();
                _list = connetion.Query<DealsEN>(@"select 
                                  
                                  pp.FirstName+' '+ pp.LastName as Name,
                                  ph.PhoneNumber,
		                          em.Email,
		                          dl.PersonID ,
                                  st.code as stage,
                                       DATEDIFF(DAY, dl.RegDateMovimentDeal , DATEADD(hh, -6 , GETDATE())) AS Days 
                                    from 
                                 CrmMkt.Deals dl 
		                         inner join Person.EmailAddress em on em.PersonID = dl.PersonID
                                 inner Join Person.PersonPhone ph on ph.PersonID = dl.PersonID
                                 inner join Person.Person pp on pp.PersonID = dl.PersonID
                                 inner join CrmMkt.Stages st on st.Id = dl.StageID
                                 where  DATEDIFF(DAY, dl.RegDateMovimentDeal , DATEADD(hh, -6 , GETDATE())) > 5
                     ").ToList();
                  _conn.CerrarConexion(connetion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return _list;
        }

        public int ValidateDeal(int PersonId)
        {
            int result = 0;

            try
            {
                var connection = _conn.CrearConexion();
                int response = connection.Query<int>(@"select PersonId from CrmMkt.EmailToSC where PersonId=@PersonId", new { PersonId = PersonId }).FirstOrDefault();
                if (response > 0)
                {
                    result = 400;//ya se mando correo a los usuarios
                }
                else
                {
                    result = 200;//no se ha mandado correo a los usuarios
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

       

    }
}
