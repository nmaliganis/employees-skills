using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Skills;

public class SkillDoesNotExistAfterMadePersistentException : Exception
{
    public string Name { get; private set; }
    public Guid Id { get; private set; }

    public SkillDoesNotExistAfterMadePersistentException(string name)
    {
        this.Name = name;
    }
    
    public SkillDoesNotExistAfterMadePersistentException(Guid id)
    {
        this.Id = id;
    }

    public override string Message => $" Skill with Name: {Name} or Id :{Id} was not made Persistent!";
}