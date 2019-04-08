using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace TaskForModule5.Settings
{
    public class Settings : ISettings
    {
        public Settings(IConfiguration configuration)
        {
            configuration.GetSection("Settings").Bind(this);
        }

        public List<string> Folders { get; set; }
        public Dictionary<string, string> Rules { get; set; }
        public bool AddNumber{ get; set; }
        public bool AddDate{ get; set; }
    }
}