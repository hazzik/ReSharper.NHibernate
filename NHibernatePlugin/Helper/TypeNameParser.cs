namespace NHibernatePlugin.Helper
{
    public class TypeNameParser
    {
        private const string NamespaceSeparator = ".";
        private const string AssemblySeparator = ",";
        private const string GenericsSeparator = "[";
        private readonly string m_Assembly;
        private readonly string m_Namespace;
        private string m_FullQualifiedTypeName;

        public TypeNameParser(string typeName, string assembly, string @namespace) {
            m_FullQualifiedTypeName = typeName ?? "";
            Normalize();
            m_Assembly = assembly ?? "";
            m_Namespace = @namespace ?? "";
            if (!m_FullQualifiedTypeName.Contains(NamespaceSeparator) && (!string.IsNullOrEmpty(m_Namespace))) {
                m_FullQualifiedTypeName = m_Namespace + NamespaceSeparator + m_FullQualifiedTypeName;
            }
            if (!m_FullQualifiedTypeName.Contains(AssemblySeparator) && (!string.IsNullOrEmpty(m_Assembly))) {
                m_FullQualifiedTypeName = m_FullQualifiedTypeName + AssemblySeparator + " " + m_Assembly;
            }
        }

        public string TypeName {
            get {
                string name = m_FullQualifiedTypeName.Split(new char[] {','})[0].Trim();
                int nextBracePos = name.IndexOf(GenericsSeparator, 0);
                int pos = 0;
                do {
                    int nextPos = name.IndexOf(NamespaceSeparator, pos);
                    if ((nextBracePos >= 0) && (nextPos > nextBracePos)) {
                        return name.Substring(pos, name.Length - pos).Trim();
                    }
                    if ((nextPos < 0) && (pos > 0)) {
                        return name.Substring(pos, name.Length - pos).Trim();
                    }
                    pos = nextPos + 1;
                } while (pos > 0);

                return name.Trim();
            }
        }

        public string Namespace {
            get {
                int pos = m_FullQualifiedTypeName.IndexOf(AssemblySeparator);
                string @namespace = (pos >= 0) ? m_FullQualifiedTypeName.Substring(0, pos) : m_FullQualifiedTypeName;
                if (@namespace.IndexOf(NamespaceSeparator) == -1) {
                    return "";
                }
                int nextBracePos = m_FullQualifiedTypeName.IndexOf(GenericsSeparator, 0);
                pos = 0;
                do {
                    int nextPos = @namespace.IndexOf(NamespaceSeparator, pos);
                    if ((nextBracePos >= 0) && (nextPos > nextBracePos)) {
                        return @namespace.Substring(0, pos - 1).Trim();
                    }
                    if ((nextPos < 0) && (pos > 0)) {
                        return @namespace.Substring(0, pos - 1).Trim();
                    }
                    pos = nextPos + 1;
                } while (pos > 0);
                return @namespace.Trim();
            }
        }

        public string FullQualifiedAssemblyName {
            get {
                string[] split = m_FullQualifiedTypeName.Split(new char[] {','});
                if (split.Length >= 2) {
                    string result = "";
                    for (int i = 1; i < split.Length; i++) {
                        if (!string.IsNullOrEmpty(result)) {
                            result += AssemblySeparator + " ";
                        }
                        result += split[i].Trim();
                    }
                    return result;
                }
                return "";
            }
        }

        public string AssemblyName {
            get {
                string[] split = FullQualifiedAssemblyName.Split(new char[] {','});
                if (split.Length > 1) {
                    return split[0];
                }
                return FullQualifiedAssemblyName;
            }
        }

        public string FullQualifiedTypeName {
            get { return m_FullQualifiedTypeName; }
        }

        public string QualifiedTypeName {
            get {
                if (!string.IsNullOrEmpty(Namespace)) {
                    return Namespace + NamespaceSeparator + TypeName;
                }
                return TypeName;
            }
        }

        private void Normalize() {
            string result = (!string.IsNullOrEmpty(Namespace)) ? (Namespace + NamespaceSeparator + TypeName) : TypeName;
            if (!string.IsNullOrEmpty(AssemblyName)) {
                result += AssemblySeparator + " " + FullQualifiedAssemblyName;
            }
            m_FullQualifiedTypeName = result;
        }
    }
}