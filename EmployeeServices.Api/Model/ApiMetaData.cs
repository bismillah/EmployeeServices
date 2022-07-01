namespace EmployeeServices.Api.Model
{
    public class ApiMetaData
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public class Meta
        {
            public Pagination pagination { get; set; }
        }

        public class Pagination
        {
            public int total { get; set; }
            public int pages { get; set; }
            public int page { get; set; }
            public int limit { get; set; }
        }
    }
}
