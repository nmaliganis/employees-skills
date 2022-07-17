using System;

namespace dl.wm.suite.common.infrastructure.Exceptions.Domain.Skills
{
    public class SkillDoesNotExistAfterMadeTransientException : Exception
    {
        public Guid SkillId { get; private set; }
        public string Name { get; private set; }

        public SkillDoesNotExistAfterMadeTransientException(string name)
        {
            Name = name;
        }
        public SkillDoesNotExistAfterMadeTransientException(Guid SkillId)
        {
            SkillId = SkillId;
        }

        public override string Message => $" Skill with Name: {Name} or Id: {SkillId} was not made Transient!";
    }
}