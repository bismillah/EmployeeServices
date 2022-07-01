using System.ComponentModel.DataAnnotations;

namespace EmployeeServices.Domain.Enum
{
    public enum Status
    {
        [Display(Name = "Active")]
        active,
        [Display(Name = "InActive")]
        inactive
    }
}
