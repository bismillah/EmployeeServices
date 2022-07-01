namespace EmployeeServices.Domain.ModelInterface
{
    public interface IEmployeeModel
    {
        int Id { get; set; }

        string Name { get; set; }

        string Email { get; set; }

        string Gender { get; set; }

        string Status { get; set; }
    }
}