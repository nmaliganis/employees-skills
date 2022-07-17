using System;
using System.ComponentModel.DataAnnotations;

namespace employee.skill.common.dtos.Vms.Base
{
    public interface IUiModel
    {
        [Key]
        Guid Id { get; set; }
        [Editable(false)]
        string Message { get; set; }
    }
}