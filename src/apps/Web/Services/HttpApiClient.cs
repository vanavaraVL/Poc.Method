namespace Web.Services
{
    public interface IHttpApiClient
    {
        ICompanyClient CompanyClient { get; }

        IPersonClient PersonClient { get; }
    }

    public class HttpApiClient: IHttpApiClient
    {
        private readonly HttpClient _http;

        public HttpApiClient(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public ICompanyClient CompanyClient => new CompanyClient(_http);
        public IPersonClient PersonClient => new PersonClient(_http);
    }
}
