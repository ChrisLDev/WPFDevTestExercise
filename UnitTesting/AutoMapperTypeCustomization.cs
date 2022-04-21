using AutoFixture;
using AutoFixture.Kernel;
using AutoMapper;
using System;

namespace UnitTesting
{
	public class AutoMapperTypeCustomization : ICustomization
	{
		private readonly Type _profileType;

		public AutoMapperTypeCustomization(Type profileType)
		{
			_profileType = profileType;
		}

		public void Customize(IFixture fixture)
		{
			var mapper = CreateMapper(fixture, _profileType);
			fixture.Inject(mapper);
		}

		private static IMapper CreateMapper(IFixture fixture, Type profileType) =>
			new MapperConfiguration(opts => opts.AddProfile(profileType))
			.CreateMapper(type => new SpecimenContext(fixture).Resolve(type));
	}

	public class AutoMapperProfileCustomization : ICustomization
	{
		private readonly Profile _profile;

		public AutoMapperProfileCustomization(Profile profile)
		{
			_profile = profile;
		}

		public void Customize(IFixture fixture)
		{
			var mapper = CreateMapper(fixture, _profile);
			fixture.Inject(mapper);
		}

		private static IMapper CreateMapper(IFixture fixture, Profile profile) =>
			new MapperConfiguration(opts => opts.AddProfile(profile))
			.CreateMapper(type => new SpecimenContext(fixture).Resolve(type));
	}
}
