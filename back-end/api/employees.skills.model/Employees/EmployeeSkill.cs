using System;
using employee.skill.common.infrastructure.Domain;
using employees.skills.model.Skills;

namespace employees.skills.model.Employees
{
    public class EmployeeSkill : EntityBase<Guid>
    {
        public EmployeeSkill()
        {
            this.OnCreated();
        }

        private void OnCreated()
        {
            this.Active = true;
            this.CreatedDate = DateTime.Now;
        }

        public virtual DateTime CreatedDate { get; set; }
        public virtual bool Active { get; set; }
        
        public virtual Skill Skill { get; set; }
        public virtual Employee Employee { get; set; }

        protected override void Validate()
        {
        }

        public virtual void InjectWithSkill(Skill skillToBeCreatedAndInjected)
        {
            this.Skill = skillToBeCreatedAndInjected;
            skillToBeCreatedAndInjected.Employees.Add(this);
        }
    }
}