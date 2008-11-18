using System;

namespace NHibernatePlugin.TypeNames.Scanners
{
    public class ScannerException : Exception
    {
        public ScannerException(string message)
            : base(message) {
        }
    }
}