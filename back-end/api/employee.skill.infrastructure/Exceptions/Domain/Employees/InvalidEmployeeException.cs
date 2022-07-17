using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Employees
{
    public class InvalidEmployeeException : Exception
    {
        public string BrokenRules { get; private set; }

        public InvalidEmployeeException(string brokenRules)
        {
            BrokenRules = brokenRules;
        }
    }
}