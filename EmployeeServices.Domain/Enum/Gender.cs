using System.ComponentModel.DataAnnotations;
namespace EmployeeServices.Domain.Enum
{
    public enum Gender
    {
        [Display(Name = "Male")]
        male,
        [Display(Name = "Female")]
        female
    }
}
