﻿using System;

namespace employee.skill.common.infrastructure.Exceptions.Common
{
    [Serializable]
    public class RootObjectNotFoundException : Exception
    {
        public RootObjectNotFoundException(string message) : base(message)
        {
        }
    }
}