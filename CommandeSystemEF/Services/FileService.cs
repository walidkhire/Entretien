using System;
using System.Collections.Generic;
using System.Text;

namespace CommandeSystemEF.Services
{
    public class FileService
    {
        public async Task WriteLogToFileAsync(string content)
        {
            await File.AppendAllTextAsync("operations.log", content + Environment.NewLine);
        }
    }
}
