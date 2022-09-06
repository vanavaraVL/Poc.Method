using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NUnit3;
using AutoMapper;
using Poc.Method.Service.ExternalAppRedAccess;
using Poc.Method.Service.ExternalAppRedAccess.Mappers;
using RichardSzalay.MockHttp;

namespace Poc.Method.Tests.ExternalAppRedAccess
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute() : base(FixtureHelpers.CreateFixture)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public CustomInlineAutoDataAttribute(params object[] args) : base(FixtureHelpers.CreateFixture, args)
        {
        }
    }

    internal static class FixtureHelpers
    {
        public static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true,
                GenerateDelegates = true
            });

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Customizations.Add(new TypeRelay(typeof(IReadOnlySet<>), typeof(HashSet<>)));

            var personAppModelList = fixture.CreateMany<EmployeModel>();

            fixture.Customize<GetEmployeesInCompanyResponse>(x => x
                .With(p => p.Employees, () => new List<EmployeModel>(personAppModelList)));

            fixture.AddMockHttp();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            fixture.Register<IMapper>(() => new Mapper(config));

            fixture.Customize<ExternalAppRedAccessService>(o => o.FromFactory((MockHttpMessageHandler handler, IMapper mapper) =>
            {
                var http = handler.ToHttpClient();

                http.BaseAddress = new Uri("http://localhost");

                return new ExternalAppRedAccessService(new HttpClientAppRed(http), mapper);
            }));

            return fixture;
        }
    }
}
