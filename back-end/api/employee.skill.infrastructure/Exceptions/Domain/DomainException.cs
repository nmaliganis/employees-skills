using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain
{
    public class DomainException : Exception
    {
        private readonly string _notApplicableMsg;

        public DomainException(string notApplicableMsg)
        {
            this._notApplicableMsg = notApplicableMsg;
        }
    }
}