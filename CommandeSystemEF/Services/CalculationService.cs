using System;
using System.Collections.Generic;
using System.Text;

namespace CommandeSystemEF.Services
{
    public class CalculationService
    {
        public Task<double> HeavyCalculationAsync()
        {
            return Task.Run(() =>
            {
                double result = 0;
                for (int i = 0; i < 50_000_000; i++)
                    result += Math.Sqrt(i);
                return result;
            });
        }
    }
}
