using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;


namespace Application.UnitTests;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class AutoMoqDataAttribute: AutoDataAttribute
{
    internal enum AutoMoqBehavior
    {
        None = 0,
        ConfigureMembers = 1,
        AllowCircularReferences = 2,
        UseMoqDelegates = 4,
    }

    internal static class FixtureFactory
    {
        public static IFixture CreateFixture(AutoMoqBehavior behaviors)
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization
            {
                ConfigureMembers = behaviors.HasFlag(AutoMoqBehavior.ConfigureMembers),
                GenerateDelegates = behaviors.HasFlag(AutoMoqBehavior.UseMoqDelegates)
            });

            if (behaviors.HasFlag(AutoMoqBehavior.AllowCircularReferences))
            {
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            }

            return fixture;
        }
    }

    public AutoMoqDataAttribute(AutoMoqBehavior behaviors = AutoMoqBehavior.None) : base(() =>
        FixtureFactory.CreateFixture(behaviors))
    {
    }
}