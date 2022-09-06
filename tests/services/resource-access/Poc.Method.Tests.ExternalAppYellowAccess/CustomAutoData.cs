using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NUnit3;
using Poc.Method.Service.ExternalAppYellowAccess;
using RichardSzalay.MockHttp;

namespace Poc.Method.Tests.ExternalAppYellowAccess
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

            fixture.AddMockHttp();

            fixture.Customize<ExternalAppYellowAccessService>(o => o.FromFactory((MockHttpMessageHandler handler) =>
            {
                var http = handler.ToHttpClient();

                http.BaseAddress = new Uri("http://localhost");

                return new ExternalAppYellowAccessService(new HttpClientAppYellow(http));
            }));

            fixture.Customize<AssignEmployeeToCompanyResponse>(r => r.With(p => p.IsSuccess, () => true));

            return fixture;
        }
    }
}
