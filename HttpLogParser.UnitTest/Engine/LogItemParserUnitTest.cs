using FluentAssertions;
using HttpLogParser.ConsoleApp.Engine;

namespace HttpLogParser.UnitTest.Engine;


public class LogItemParserUnitTest
{
    private readonly LogItemParser _sut = new LogItemParser();

    [Theory]
    [InlineData("50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] \"GET /asset.js HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6\"")]
    public void Should_Parse_RemoteHostAddress_From_String(string log)
    {
        // arrange
        var expected = "50.112.00.11";

        // act
        var actual = _sut.ParseRemoteHostAddress(log);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] \"GET /asset.js HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6\"")]
    public void Should_Parse_RemoteLogName_From_String(string log)
    {
        // arrange
        var expected = "";

        // act
        var actual = _sut.ParseRemoteLogName(log);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] \"GET /asset.js HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6\"")]
    public void Should_Parse_Username_From_String(string log)
    {
        // arrange
        var expected = "admin";

        // act
        var actual = _sut.ParseUsername(log);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] \"GET /asset.js HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6\"")]
    public void Should_Parse_DateTimeAsGmt_From_String(string log)
    {
        // arrange
        var expected = DateTimeOffset.Parse("2018-07-11T17:31:56+0200");

        // act
        var actual = _sut.ParseDateTimeAsGmt(log);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] \"GET /asset.js HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6\"")]
    public void Should_Parse_RequestAndProtocol_From_String(string log)
    {
        // arrange
        var expected = "GET /asset.js HTTP/1.1";

        // act
        var actual = _sut.ParseRequestAndProtocol(log);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] \"GET /asset.js HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6\"")]
    public void Should_Parse_ServiceStatuscode_From_String(string log)
    {
        // arrange
        var expected = "200";

        // act
        var actual = _sut.ParseServiceStatuscode(log);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] \"GET /asset.js HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6\"")]
    public void Should_Parse_BytesSent_From_String(string log)
    {
        // arrange
        var expected = 3574d;

        // act
        var actual = _sut.ParseBytesSent(log);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("50.112.00.11 - admin [11/Jul/2018:17:31:56 +0200] \"GET /asset.js HTTP/1.1\" 200 3574 \"-\" \"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6\"")]
    public void Should_Parse_ClientInformation_From_String(string log)
    {
        // arrange
        var expected = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.6 (KHTML, like Gecko) Chrome/20.0.1092.0 Safari/536.6";

        // act
        var actual = _sut.ParseClientInformation(log);

        // assert
        actual.Should().Be(expected);
    }
}
