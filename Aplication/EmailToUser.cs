using Mandrill;
using Mandrill.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailComunicationDealToSC.Aplication
{
    class EmailToUser
    {
        public int SendEmail(string Email, string Name, string PhoneNumber, string stage, int days)
        {
            int response = 0;
            try
            {
                var apikey = "Wt_XKvUnRBpY6pyHMa9j-w";
                var client = new MandrillApi(apikey);
                var message = new MandrillMessage
                {
                    FromEmail = "info@yovendorecarga.com",
                    FromName = "CRM-WEB",
                    To = new List<MandrillMailAddress>
                {
                    new MandrillMailAddress
                    {
                        Email = "erickescobar2500@gmail.com",
                        Name = "SERVICIO AL CLIENTE"
                    }
                },
                    MergeLanguage = MandrillMessageMergeLanguage.Mailchimp,
                    Subject = "CONTACTAR A USUARIO",
                    Text = "Buen dia Comunicar al usuario con la siguiente informacion: \n Correo: " + Email +"\n Nombre: "+Name + "\n Telefono: "+ PhoneNumber+"\n Etapa: "+stage+"\n Dias en la etapa: "+days,
                };

                var result = client.Messages.SendAsync(message);
                response = 200;
                return  response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return response;
        }
    }
}
