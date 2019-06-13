using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja
{
    public class ErrorLogger
    {
        public string LastError { get; set; }

        public event EventHandler<Guid> ErrorLogged;

        public void Log(string error)
        {
            if (String.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException();

            LastError = error;

            // Write to storage
            // ...

            OnErrorLogged(Guid.NewGuid());
        }

        protected virtual void OnErrorLogged(Guid errorId)
        {
            ErrorLogged?.Invoke(this, Guid.NewGuid());
        }
    }
}
