using EmployeeServices.Domain.ModelInterface;
using EmployeeServices.Domain.ModelInterface.ApiMetaData;
using System.Collections.Generic;

namespace EmployeeServices.Domain.Model
{
    public interface IEmployeeList
    {
        List<IEmployeeModel> data { get; set; }

        int code { get; set; }

        //IMeta meta { get; set; }
    }
}
