using System;
using System.Collections.Generic;
using employee.skill.common.dtos.Vms.Skills;
using employee.skill.common.infrastructure.Domain;
using employees.skills.model.Employees;

namespace employees.skills.model.Skills
{
    public class Skill : EntityBase<Guid>, IAggregateRoot
    {
        public Skill()
        {
            this.OnCreated();
        }

        private void OnCreated()
        {
            this.Active = true;
            this.CreatedDate = DateTime.Now;

            this.Employees = new HashSet<EmployeeSkill>();
        }

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual bool Active { get; set; }
        
        public virtual ISet<EmployeeSkill> Employees { get; set; }

        protected override void Validate()
        {
        }

        public virtual void InjectWithInitialAttributes(SkillCreationUiModel newSkillUiModel)
        {
            this.Name = newSkillUiModel.Name;
            this.Description = newSkillUiModel.Description;
        }

        public virtual void SoftDeleted()
        {
            this.Active = false;
        }
    }
}