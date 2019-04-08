using System.Globalization;
using Microsoft.Extensions.Localization;

namespace TaskForModule5.Localization
{
    public class CustomStringLocalizer : ICustomStringLocalizer
    {
        private IStringLocalizer _stringLocalizer;

        public string this[string index] => _stringLocalizer[index];

        public CustomStringLocalizer(IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public void UpdateCulture()
        {
            _stringLocalizer = _stringLocalizer.WithCulture(CultureInfo.CurrentUICulture);
        }
    }
}