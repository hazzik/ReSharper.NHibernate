namespace NHibernatePlugin.TypeNames.Scanners
{
    public static class Extensions
    {
        public static bool IsNameCharacter(this char ch) {
            return char.IsLetterOrDigit(ch) || ch == '.' || ch == '+';
        }

        public static bool IsNameStartCharacter(this char ch) {
            return char.IsLetter(ch);
        }

        public static bool IsWhiteSpace(this char ch) {
            return char.IsWhiteSpace(ch);
        }
    }
}