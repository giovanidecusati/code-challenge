using FluentAssertions;
using HttpLogParser.ConsoleApp.Reporting;
using Shouldly;

namespace HttpLogParser.UnitTest.Reporting;

public class LogReportingUnitTest
{
    private readonly LogReporting _sut;

    public LogReportingUnitTest()
    {
        var builder = new LogItemModelBuilder()
            .With(RemoteHostAddress: "10.0.0.1", RequestAndProtocol: "GET /asset.css")
            .With(RemoteHostAddress: "10.0.0.1", RequestAndProtocol: "GET /index.html")
            .With(RemoteHostAddress: "10.0.0.2", RequestAndProtocol: "GET /asset.css")
            .With(RemoteHostAddress: "10.0.0.2", RequestAndProtocol: "GET /index.html")
            .With(RemoteHostAddress: "10.0.0.2", RequestAndProtocol: "GET /asset.css")
            .With(RemoteHostAddress: "10.0.0.2", RequestAndProtocol: "GET /index.html")
            .With(RemoteHostAddress: "10.0.0.3", RequestAndProtocol: "GET /asset.css")
            .With(RemoteHostAddress: "10.0.0.3", RequestAndProtocol: "GET /index.html")
            .With(RemoteHostAddress: "10.0.0.3", RequestAndProtocol: "GET /asset.css")
            .With(RemoteHostAddress: "10.0.0.3", RequestAndProtocol: "GET /index.html")
            .With(RemoteHostAddress: "10.0.0.3", RequestAndProtocol: "GET /asset.css")
            .With(RemoteHostAddress: "10.0.0.3", RequestAndProtocol: "GET /index.html")
            .With(RemoteHostAddress: "10.0.0.3", RequestAndProtocol: "POST /index.html")
            .With(RemoteHostAddress: "10.0.0.4", RequestAndProtocol: "PUT /index.html")
            .With(RemoteHostAddress: "10.0.0.4", RequestAndProtocol: "PUT /products.html")
            .With(RemoteHostAddress: "10.0.0.5", RequestAndProtocol: "GET /index.js")
            .With(RemoteHostAddress: "10.0.0.6", RequestAndProtocol: "GET /contact.html")
            .Build();

        _sut = new LogReporting(builder);
    }

    [Fact]
    public void Should_Return_UniqueIp_Addresses()
    {
        // arrange
        var expectedCount = 6;
        var expectedIp1 = "10.0.0.1";
        var expectedIp2 = "10.0.0.2";
        var expectedIp3 = "10.0.0.3";
        var expectedIp4 = "10.0.0.4";
        var expectedIp5 = "10.0.0.5";
        var expectedIp6 = "10.0.0.6";

        // act
        var actual = _sut.GetNumberOfUniqueIpAddresses();

        // assert
        actual.Count().Should().Be(expectedCount);
        actual.Should().Contain(expectedIp1);
        actual.Should().Contain(expectedIp2);
        actual.Should().Contain(expectedIp3);
        actual.Should().Contain(expectedIp4);
        actual.Should().Contain(expectedIp5);
        actual.Should().Contain(expectedIp6);
    }

    [Fact]
    public void Should_Return_Top_3_Most_Active_Ip_Addresses()
    {
        // arrange
        var expectedCount = 4;
        var expectedIp1 = "10.0.0.3";
        var expectedIp1Count = 7;
        var expectedIp2 = "10.0.0.2";
        var expectedIp2Count = 4;
        var expectedIp3 = "10.0.0.1";
        var expectedIp3Count = 2;
        var expectedIp4 = "10.0.0.4";
        var expectedIp4Count = 2;
        var notExpectedIps = new string[] { "10.0.0.5", "10.0.0.6" };

        // act
        var actual = _sut.GetTop3MostActiveIpAddresses();

        // assert
        actual.Count().Should().Be(expectedCount);
        actual.First(i => i.RemoteHostAddress == expectedIp1).Should().NotBeNull();
        actual.First(i => i.RemoteHostAddress == expectedIp1).Count.Should().Be(expectedIp1Count);
        actual.First(i => i.RemoteHostAddress == expectedIp2).Should().NotBeNull();
        actual.First(i => i.RemoteHostAddress == expectedIp2).Count.Should().Be(expectedIp2Count);
        actual.First(i => i.RemoteHostAddress == expectedIp3).Should().NotBeNull();
        actual.First(i => i.RemoteHostAddress == expectedIp3).Count.Should().Be(expectedIp3Count);
        actual.First(i => i.RemoteHostAddress == expectedIp4).Should().NotBeNull();
        actual.First(i => i.RemoteHostAddress == expectedIp4).Count.Should().Be(expectedIp4Count);
        actual.Any(i => notExpectedIps.Contains(i.RemoteHostAddress)).Should().BeFalse();
    }

    [Fact]
    public void Should_Return_Top3_Visited_Urls()
    {
        // arrange
        var expectedCount = 5;
        var expectedRequestAndProtocol1 = "/index.html";
        var expectedRequestAndProtocol1Count = 8;
        var expectedRequestAndProtocol2 = "/asset.css";
        var expectedRequestAndProtocol2Count = 6;
        var expectedRequestAndProtocol3 = "/products.html";
        var expectedRequestAndProtocol3Count = 1;
        var expectedRequestAndProtocol4 = "/index.js";
        var expectedRequestAndProtocol4Count = 1;
        var expectedRequestAndProtocol5 = "/contact.html";
        var expectedRequestAndProtocol5Count = 1;

        // act
        var actual = _sut.GetTop3MostVisitedUrls();

        // assert
        actual.Count().Should().Be(expectedCount);
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol1).Should().NotBeNull();
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol1).Count.Should().Be(expectedRequestAndProtocol1Count);
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol2).Should().NotBeNull();
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol2).Count.Should().Be(expectedRequestAndProtocol2Count);
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol3).Should().NotBeNull();
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol3).Count.Should().Be(expectedRequestAndProtocol3Count);
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol4).Should().NotBeNull();
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol4).Count.Should().Be(expectedRequestAndProtocol4Count);
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol5).Should().NotBeNull();
        actual.First(i => i.RequestAndProtocol == expectedRequestAndProtocol5).Count.Should().Be(expectedRequestAndProtocol5Count);
    }
}
