using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NUnit3;
using Poc.Method.ContextStorageAccess.Models;
using Poc.Method.Core.Dtos.Persons;

namespace Poc.Method.Tests.PersonManagerService
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

            var personList = fixture.CreateMany<PersonDto>();

            fixture.Customize<PersonListResponse>(x => x
                .With(p => p.Items, () => new ReadOnlyCollection<PersonDto>(personList.ToList())));


            return fixture;
        }
    }
}
