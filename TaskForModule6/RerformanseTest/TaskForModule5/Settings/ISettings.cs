using System.Collections.Generic;

namespace TaskForModule5.Settings
{
    public interface ISettings
    {
        List<string> Folders { get; set; }
        Dictionary<string, string> Rules { get; set; }
        bool AddNumber { get; set; }
        bool AddDate{ get; set; }
    }
}