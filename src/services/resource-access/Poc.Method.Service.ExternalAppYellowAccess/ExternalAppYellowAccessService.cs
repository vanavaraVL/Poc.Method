using Poc.Method.ExternalAppYellowAccess;
using Poc.Method.ExternalAppYellowAccess.Models;

namespace Poc.Method.Service.ExternalAppYellowAccess
{
    public class ExternalAppYellowAccessService: IExternalAppYellowAccess
    {
        private readonly IHttpClientAppYellow _applicationYellowHttpClient;

        public ExternalAppYellowAccessService(IHttpClientAppYellow applicationYellowHttpClient)
        {
            _applicationYellowHttpClient = applicationYellowHttpClient ?? throw new ArgumentNullException(nameof(applicationYellowHttpClient));
        }

        public async Task<CompanyCreateResponse> CreateCompany(CompanyCreateRequest request)
        {
            var result = await _applicationYellowHttpClient.CreateCompanyAsync(new CreateCompanyCommand()
            {
                AdditionInfo = request.AdditionInfo,
                Description = request.Description,
                Name = request.Name
            });

            return new CompanyCreateResponse()
            {
                Id = result.Id
            };
        }

        public async Task<CompanyAssignEmployeeResponse> AssignEmployeeToCompany(CompanyAssignEmployeeRequest request)
        {
            var result =
                await _applicationYellowHttpClient.AssignEmployeeToCompany(request.CompanyId, request.PersonId);

            return new CompanyAssignEmployeeResponse { IsSuccess = result.IsSuccess };
        }
    }
}