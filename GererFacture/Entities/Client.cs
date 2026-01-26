using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GererFacture.Entities
{
    internal class Client : BaseEntity
    {
        public Client(string email, string name) : base(name)
        {
            Email = Email;

        }
        public string Email { get; set; }

    }
}
