using System;

namespace employee.skill.common.infrastructure.Exceptions.Common
{
    [Serializable]
    public class ChildObjectNotFoundException : Exception
    {
        public ChildObjectNotFoundException(string message) : base(message)
        {
        }
    }
}