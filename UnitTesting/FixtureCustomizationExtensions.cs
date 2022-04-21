using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using System;

namespace UnitTesting
{
	public static class FixtureCustomizationExtensions
	{
		public static IFixture CustomizeWithAutoMapper(this IFixture fixture, Type profile)
		{
			return fixture.Customize(new AutoMapperTypeCustomization(profile));
		}

		public static IFixture CustomizeWithAutoMapper(this IFixture fixture, Profile profile)
		{
			return fixture.Customize(new AutoMapperProfileCustomization(profile));
		}

		public static IFixture CustomizeWithAutoMoq(this IFixture fixture)
		{
			return fixture.Customize(new AutoMoqCustomization());
		}
	}
}