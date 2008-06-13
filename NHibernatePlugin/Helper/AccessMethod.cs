using System.Linq;

namespace NHibernatePlugin.Helper
{
    public class AccessMethod
    {
        public const string AccessMethodProperty = "property";
        public const string AccessMethodField = "field";
        public const string AccessMethodNosetter = "nosetter";
        private readonly string[] AccessMethods = { AccessMethodProperty, AccessMethodField, AccessMethodNosetter };

        private const string NamingLowercase = "lowercase";
        private const string NamingLowercaseUnderscore = "lowercase-underscore";
        private const string NamingCamelCase = "camelcase";
        private const string NamingCamelCaseUnderscore = "camelcase-underscore";
        private const string NamingPascalCase = "pascalcase";
        private const string NamingPascalCaseM = "pascalcase-m";
        private const string NamingPascalCaseUnderscore = "pascalcase-underscore";
        private const string NamingPascalCaseMUnderscore = "pascalcase-m-underscore";


        private readonly string[] NamingMethods = { NamingLowercase, NamingLowercaseUnderscore,
                                                    NamingCamelCase, NamingCamelCaseUnderscore,
                                                    NamingPascalCase, NamingPascalCaseM, NamingPascalCaseUnderscore, NamingPascalCaseMUnderscore };

        private readonly string m_AccessMethod;

        public AccessMethod(string accessMethod) {
            m_AccessMethod = accessMethod;
        }

        // TODO: use an enum type instead of strings
        public string Method {
            get {
                if (string.IsNullOrEmpty(m_AccessMethod)) {
                    return AccessMethodProperty;
                }
                string[] split = m_AccessMethod.Split('.');
                if (split.Length > 0) {
                    return split[0];
                }
                return "";
            }
        }

        public string Naming {
            get {
                if (string.IsNullOrEmpty(m_AccessMethod)) {
                    return "";
                }
                string[] split = m_AccessMethod.Split('.');
                if (split.Length > 1) {
                    return split[1];
                }
                return "";
            }
        }

        public bool Unknown {
            get {
                if ((Method == AccessMethodNosetter) && (!NamingMethods.Contains(Naming))) {
                    return true;
                }
                if ((Method == AccessMethodProperty) && (!string.IsNullOrEmpty(Naming))) {
                    return true;
                }
                bool methodUnknown = (!string.IsNullOrEmpty(Method)) && !AccessMethods.Contains(Method);
                bool namingUnknown = (!string.IsNullOrEmpty(Naming)) && !NamingMethods.Contains(Naming);
                return methodUnknown || namingUnknown;
            }
        }

        public string Name(string name) {
            switch (Naming) {
                case "":
                    return name;
                case NamingLowercase:
                    return name.ToLower();
                case NamingLowercaseUnderscore:
                    return "_" + name.ToLower();
                case NamingCamelCase:
                    return ToFirstCharLower(name);
                case NamingCamelCaseUnderscore:
                    return "_" + ToFirstCharLower(name);
                case NamingPascalCase:
                    return ToFirstCharUpper(name);
                case NamingPascalCaseUnderscore:
                    return "_" + ToFirstCharUpper(name);
                case NamingPascalCaseMUnderscore:
                    return "m_" + ToFirstCharUpper(name);
                case NamingPascalCaseM:
                    return "m" + ToFirstCharUpper(name);
            }
            return name;
        }

        public static string ToFirstCharUpper(string name) {
            if (name.Length <= 1) {
                return name.ToUpper();
            }
            char[] letters = name.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }

        public static string ToFirstCharLower(string name) {
            if (name.Length <= 1) {
                return name.ToLower();
            }
            char[] letters = name.ToCharArray();
            letters[0] = char.ToLower(letters[0]);
            return new string(letters);
        }
    }
}