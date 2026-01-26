using System;
using System.Collections.Generic;
using System.Text;

namespace CommandeSystemEF.Services
{
    public class ApiService
    {
        private readonly HttpClient _http = new();


        public async Task<string> CallExternalApiAsync()
        {
            await Task.Delay(300); // simulation réseau
            return "API OK";
        }
    }
}
