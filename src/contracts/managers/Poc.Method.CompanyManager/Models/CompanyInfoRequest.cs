namespace Poc.Method.CompanyManager.Models
{
    public record CompanyDetailRequest
    {
        public int Id { get; init; }

        public int PageSize { get; init; }
    }

    public record CompanyListRequest
    {

    }
}
