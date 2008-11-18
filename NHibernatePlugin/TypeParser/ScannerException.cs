using System;

namespace NHibernatePlugin.TypeParser
{
    public class ScannerException : Exception
    {
        public ScannerException(string message)
            : base(message) {
        }
    }
}