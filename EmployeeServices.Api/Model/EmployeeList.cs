using System.Collections.Generic;

namespace EmployeeServices.Api.Model
{
    public class EmployeeList : ApiMetaData
    {
        public List<EmployeeModel> data { get; set; }
    }
}
