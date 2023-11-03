using Bogus;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AllureRepro.Test;

[TestFixture]
[AllureNUnit]
public class Tests
{
    [Test]
    [AllureId(1234)]
    public void Test1()
    {
        var country = new Faker<Country>()
            .RuleFor(c => c.Name, f => f.Random.String(30));

        Steps.CreateCountry(country);

        Assert.Pass();
    }
}