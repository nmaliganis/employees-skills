using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Employees
{
    public class EmployeeDoesNotExistAfterMadeTransientException : Exception
    {
        public Guid EmployeeId { get; private set; }
        public string Name { get; private set; }

        public EmployeeDoesNotExistAfterMadeTransientException(string name)
        {
            Name = name;
        }
        public EmployeeDoesNotExistAfterMadeTransientException(Guid employeeId)
        {
            EmployeeId = employeeId;
        }

        public override string Message => $" Employee with Name: {Name} or Id: {EmployeeId} was not made Transient!";
    }
}