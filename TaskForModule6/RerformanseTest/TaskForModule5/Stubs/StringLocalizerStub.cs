using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace TaskForModule5.Application
{
    public class StringLocalizerStub : IStringLocalizer
    {
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new System.NotImplementedException();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        public LocalizedString this[string name] => throw new System.NotImplementedException();

        public LocalizedString this[string name, params object[] arguments] => throw new System.NotImplementedException();
    }
}