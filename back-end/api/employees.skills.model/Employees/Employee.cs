using System;
using System.Collections.Generic;
using employee.skill.common.dtos.Vms.Employees;
using employee.skill.common.infrastructure.Domain;

namespace employees.skills.model.Employees
{
    public class Employee : EntityBase<Guid>, IAggregateRoot
    {

        public Employee()
        {
            this.OnCreated();
        }

        private void OnCreated()
        {
            this.Active = true;
            this.CreatedDate = DateTime.Now;

            this.Skills = new HashSet<EmployeeSkill>();
        }

        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual bool Active { get; set; }
        public virtual ISet<EmployeeSkill> Skills { get; set; }

        protected override void Validate()
        {
        }

        public virtual void InjectWithInitialAttributes(EmployeeCreationUiModel newEmployeeUiModel)
        {
            this.Firstname = newEmployeeUiModel.Firstname;
            this.Lastname = newEmployeeUiModel.Lastname;
            this.Email = newEmployeeUiModel.Email;
        }

        public virtual void InjectWithSkill(EmployeeSkill employeeSkillToBeInjected)
        {
            this.Skills.Add(employeeSkillToBeInjected);
            employeeSkillToBeInjected.Employee = this;
        }
    }
}