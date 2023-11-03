using Bogus;
using Dodo.DataCatalog.Contracts.Country.v1;
using Dodo.DataCatalog.Contracts.Products.v1;
using Dodo.DataCatalog.Contracts.SizeSchemes.v1;
using Dodo.Primitives;
using Dodo.Tools.Types;

namespace AllureRepro.Test;

public class Fakes
{
    public static Faker<T> CreateDataCatalogEvent<T, TIdentity>(TIdentity id, int version = 1,
        T previousVersion = null) where T : class
    {
        var faker = new Faker<T>("en")
            .RuleForType(typeof(UUId), f => UUId.NewUUId())
            .RuleForType(typeof(Uuid), f => Uuid.NewTimeBased())
            .RuleForType(typeof(string),
                f => f.Random.String(minLength: 1, maxLength: 8, minChar: 'A', maxChar: 'Z'))
            .RuleForType(typeof(bool), f => false)
            .RuleForType(typeof(int), f => f.Random.Int(30))
            .RuleForType(typeof(int?), f => (int?)f.Random.Int(30))
            .RuleForType(typeof(double), f => f.Random.Double(30))
            .RuleForType(typeof(DoughType?), f => (DoughType?)f.PickRandom<DoughType>())
            .RuleForType(typeof(ProductType), f => f.PickRandom<ProductType>())
            .RuleForType(typeof(MaterialCategory), f => f.PickRandom<MaterialCategory>())
            .RuleForType(typeof(PizzaCustomization), f => f.PickRandom<PizzaCustomization>())
            .RuleForType(typeof(ProductionSchemeType), f => f.PickRandom<ProductionSchemeType>())
            .RuleForType(typeof(SaucesPriority), f => f.PickRandom<SaucesPriority>())
            .RuleForType(typeof(SauceInfo),
                f => new SauceInfo { Position = f.PickRandom(1, 2, 3, 4), ColorAlias = "#ffffff" })
            .RuleForType(typeof(PieceInfo), f => new PieceInfo { ParentId = UUId.NewUUId(), PiecesInParent = 8 })
            .RuleForType(typeof(Measures), f => f.PickRandom<Measures>())
            .RuleForType(typeof(ProductCategory), f => f.PickRandom<ProductCategory>())
            .RuleForType(typeof(TimeSpan), f => TimeSpan.FromHours(f.Random.Int(min: -5, max: 5)))
            .RuleForType(typeof(DeliveryParameters),
                f => new DeliveryParameters { MinutesForPrepareDelayedOrder = f.Random.Int(30) })
            .RuleForType(typeof(Composition[]), f => new[] { new Composition { MaterialTypeId = UUId.NewUUId() } })
            .RuleForType(typeof(ProductSize[]),
                f => new[]
                {
                    new ProductSize
                    {
                        Size = f.PickRandom(1, 2, 3),
                        TrackerAlias = f.Random.String(minLength: 1, maxLength: 8, minChar: 'A', maxChar: 'Z')
                    }
                })
            .RuleForType(typeof(Culture[]), f => new[]
            {
                new Culture
                {
                    Code = f.PickRandom<CountryCode>().ToString(),
                    IsDefault = true,
                    ShortName = f.Random.String(10)
                }
            })
            .RuleForType(typeof(Variation[]), f => new[]
            {
                new Variation
                {
                    Id = ((dynamic)previousVersion)?.Variations[0].Id ?? UUId.NewUUId(),
                    Composition = new[] { new Composition { MaterialTypeId = UUId.NewUUId() } },
                    DoughType = f.PickRandom<DoughType>(),
                    IsRemoved = false,
                    SizeNumber = 35,
                    SizeGroup = 0
                }
            })
            .RuleFor("Id", f => id)
            .RuleFor("Version", f => version);

        if (typeof(T) != typeof(Country))
        {
            faker.RuleFor("CountryId", f => 444);
        }

        if (typeof(T) == typeof(MaterialType))
        {
            faker.RuleFor("IsForCrust", f => true);
        }

        return faker;
    }
}