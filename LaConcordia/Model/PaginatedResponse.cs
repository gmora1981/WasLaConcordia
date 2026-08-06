namespace LaConcordia.Model
{
    public class PaginatedResponse<T>
    {
        public T Response { get; set; }
        public int TotalAmountPages { get; set; }
        public int TotalAmountRecords { get; set; }
    }
}
