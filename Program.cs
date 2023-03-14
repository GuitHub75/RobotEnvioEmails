using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using EmailComunicationDealToSC.Aplication;
using System.Collections.Generic;

namespace EmailComunicationDealToSC
{
    public class Program
    {
      
        static void Main(string[] args)
        {
            DealsDAL _dal = new DealsDAL();
            _dal.SendEmail();

        }
    }
}
