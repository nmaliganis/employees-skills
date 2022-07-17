using System;

namespace employee.skill.common.infrastructure.Exceptions.Domain.Skills;

public class InvalidSkillException : Exception
{
    public string BrokenRules { get; private set; }

    public InvalidSkillException(string brokenRules)
    {
        this.BrokenRules = brokenRules;
    }
}