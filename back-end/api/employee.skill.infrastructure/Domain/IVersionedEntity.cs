﻿namespace employee.skill.common.infrastructure.Domain
{
    public interface IVersionedEntity
    {
        int Revision { get; set; }
    }
}