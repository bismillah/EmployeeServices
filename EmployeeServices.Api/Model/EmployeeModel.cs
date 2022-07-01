using static EmployeeServices.Api.Model.ApiMetaData;

namespace EmployeeServices.Api.Model
{
    public class EmployeeModel : Meta
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Status { get; set; }
    }
}