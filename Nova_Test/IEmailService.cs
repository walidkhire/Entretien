using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova_Test
{
    public interface IEmailService
    {
        public void SendMail(string to, string sujet,string contenu);
    }
}
