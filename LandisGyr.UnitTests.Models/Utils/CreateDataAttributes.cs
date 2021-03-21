using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using AutoMapper;
using LandisGyr.ConsoleApp;
using Microsoft.EntityFrameworkCore;
using System;

namespace LandisGyr.UnitTests.Models.Utils
{
    public class CreateDataAttributes : AutoDataAttribute
    {
        public CreateDataAttributes() : base(SetupLandisGyrFixture)
        {

        }

        private static IFixture SetupLandisGyrFixture()
        {
            var fix = new Fixture();
            fix.Customize(new AutoNSubstituteCustomization());
            SetupCellContext(fix);
            SetupAutoMapper(fix);
            return fix;
        }

        private static void SetupAutoMapper(Fixture fix)
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(Startup));
            });
            fix.Inject(autoMapperConfig);
            fix.Inject(autoMapperConfig.CreateMapper());
        }

        private static void SetupCellContext(Fixture fix)
        {
            var dbOptions = new DbContextOptionsBuilder()
                                        .UseInMemoryDatabase(Guid.NewGuid().ToString());
            fix.Inject(new LandisGyrContext(dbOptions.Options));
        }
    }
}
