using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Employees
{
    public class EmployeeDoesNotExistAfterMadePersistentException : Exception
    {
        public Guid Id { get; }
        public string Name { get; private set; }

        public EmployeeDoesNotExistAfterMadePersistentException(string name)
        {
            this.Name = name;
        }
        
        public EmployeeDoesNotExistAfterMadePersistentException(Guid id)
        {
            Id = id;
        }

        public override string Message => $" Employee with Name: {Name} or Id: {Id} was not made Persistent!";
    }
}