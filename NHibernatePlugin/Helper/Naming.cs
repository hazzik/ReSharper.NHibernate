using System.Collections.Generic;
using JetBrains.ReSharper.Psi;
using JetBrains.Util;

namespace NHibernatePlugin.Helper
{
    public static class Naming
    {
        public static void AddOtherNames(ICollection<string> result, IDeclaredElement element) {
            AddOtherNames(result, element.ShortName);
        }

        private static void AddOtherNames(ICollection<string> result, string name) {
            AddOtherName(result, "_", name);
            AddOtherName(result, "m_", name);
        }

        private static void AddOtherName(ICollection<string> result, string prefixToRemove, string name) {
            string otherName = name.TrimFromStart(prefixToRemove);
            if (otherName != name) {
                Logger.LogMessage("Other name for {0} is {1}", name, otherName);
                result.Add(otherName);
            }
            string camelCase = AccessMethod.ToFirstCharLower(otherName);
            if (camelCase != name) {
                Logger.LogMessage("Other name for {0} is {1}", name, camelCase);
                result.Add(camelCase);
            }
            string pascalCase = AccessMethod.ToFirstCharUpper(otherName);
            if (pascalCase != name) {
                Logger.LogMessage("Other name for {0} is {1}", name, pascalCase);
                result.Add(pascalCase);
            }
        }
    }
}