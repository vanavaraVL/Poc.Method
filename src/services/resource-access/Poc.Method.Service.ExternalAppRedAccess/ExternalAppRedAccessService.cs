using AutoMapper;
using Poc.Method.Core.Dtos.Persons;
using Poc.Method.ExternalAppRedAccess;
using Poc.Method.ExternalAppRedAccess.Models;

namespace Poc.Method.Service.ExternalAppRedAccess
{
    public class ExternalAppRedAccessService: IExternalAppRedAccess
    {
        private readonly HttpClientAppRed _applicationRedHttpClient;
        private readonly IMapper _mapper;

        public ExternalAppRedAccessService(HttpClientAppRed applicationRedHttpClient, IMapper mapper)
        {
            _applicationRedHttpClient = applicationRedHttpClient ?? throw new ArgumentNullException(nameof(applicationRedHttpClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CompanyEmployeesResponse> GetCompanyEmployees(CompanyEmployeesRequest request)
        {
            const int pageSize = 15;

            var personList = await _applicationRedHttpClient.CompanyAsync(request.CompanyId,
                request.Page != default ? request.Page : 1, pageSize);


            return new CompanyEmployeesResponse()
            {
                Persons = _mapper.Map<ICollection<PersonDto>>(personList.Employees).ToList()
            };
        }
    }
}
