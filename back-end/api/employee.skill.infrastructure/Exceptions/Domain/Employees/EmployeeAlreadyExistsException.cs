using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Employees
{
    public class EmployeeAlreadyExistsException : Exception
    {
        public string Name { get; }
        public string BrokenRules { get; }

        public EmployeeAlreadyExistsException(string name, string brokenRules)
        {
            this.Name = name;
            this.BrokenRules = brokenRules;
        }

        public override string Message => $" Employee with Name:{Name} already Exists!\n Additional info:{BrokenRules}";
    }
}