namespace Poc.Method.CompanyManager.Models
{
    public record CompanyInfoResponse<T>(IReadOnlyCollection<T> Data, bool IsSuccess)
    {

    }
}
