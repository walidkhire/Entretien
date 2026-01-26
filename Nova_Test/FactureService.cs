using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Nova_Test
{
    public class FacturationService
    {
        private readonly IRepository _repository;
        private readonly IEmailService _emailService;
        public FacturationService(IRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;


        }
        //public void Facturer(Str, double montant)
        //{
        //    // Calcule TTC
        //    montant = montant * 1.20;

        //    // Insère en base
        //    var connection = new SqlConnection("…");
        //    connection.Open();
        //    new SqlCommand($"INSERT INTO FACTURES VALUES ({c.Id},{montant})", connection).ExecuteNonQuery();

        //    // Envoie email
        //    new SmtpClient().Send("contact@veolia.com", c.Email, "Facture", "Montant : " + montant);
        //}
    }

}
