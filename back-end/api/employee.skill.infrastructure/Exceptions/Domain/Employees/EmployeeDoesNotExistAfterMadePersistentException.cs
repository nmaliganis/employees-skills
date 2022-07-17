using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Employees
{
    public class EmployeeDoesNotExistAfterMadePersistentException : Exception
    {
        public string Name { get; private set; }

        public EmployeeDoesNotExistAfterMadePersistentException(string name)
        {
            this.Name = name;
        }

        public override string Message => $" Employee with Name: {Name} was not made Persistent!";
    }
}