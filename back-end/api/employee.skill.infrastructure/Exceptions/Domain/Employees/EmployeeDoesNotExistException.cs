using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Employees
{
    public class EmployeeDoesNotExistException : Exception
    {
        public Guid EmployeeId { get; }

        public EmployeeDoesNotExistException(Guid employeeId)
        {
            this.EmployeeId = employeeId;
        }

        public override string Message => $"Employee with Id: {EmployeeId}  doesn't exists!";
    }
}