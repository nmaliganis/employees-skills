using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Skills;

public class SkillDoesNotExistAfterMadePersistentException : Exception
{
    public string Name { get; private set; }

    public SkillDoesNotExistAfterMadePersistentException(string name)
    {
        this.Name = name;
    }

    public override string Message => $" Skill with Name: {Name} was not made Persistent!";
}