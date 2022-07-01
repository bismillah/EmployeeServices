namespace EmployeeServices.Domain.ModelInterface.ApiMetaData
{
    public interface IApiMetaData
    {
        int code { get; set; }

        IMeta meta { get; set; }
    }
}
