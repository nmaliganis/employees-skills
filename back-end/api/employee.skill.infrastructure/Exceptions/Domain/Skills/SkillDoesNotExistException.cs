using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Skills;

public class SkillDoesNotExistException : Exception
{
    public Guid SkillId { get; }

    public SkillDoesNotExistException(Guid SkillId)
    {
        this.SkillId = SkillId;
    }

    public override string Message => $"Skill with Id: {SkillId}  doesn't exists!";
}