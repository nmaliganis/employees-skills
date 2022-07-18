using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Skills;

public class SkillAlreadyExistsException : Exception
{
    public string Name { get; }
    public string BrokenRules { get; }

    public SkillAlreadyExistsException(string name, string brokenRules)
    {
        this.Name = name;
        this.BrokenRules = brokenRules;
    }
    
    public SkillAlreadyExistsException(string name)
    {
        this.Name = name;
    }

    public override string Message => $" Skill with Name:{Name} already Exists!\n Additional info:{BrokenRules}";
}