namespace EmployeeServices.Domain.ModelInterface.ApiMetaData
{
    public interface IPagination
    {
        int total { get; set; }
        int pages { get; set; }
        int page { get; set; }
        int limit { get; set; }
    }
}
