using Dodo.DataCatalog.Contracts.Country.v1;
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
        var version1 = Fakes.CreateDataCatalogEvent<Country, int>(42, 1).Generate();
        var version2 = Fakes.CreateDataCatalogEvent<Country, int>(42, 1, version1).Generate();

        Steps.Step1(version1);
        Steps.Step2(version2);

        Assert.Pass();
    }
}