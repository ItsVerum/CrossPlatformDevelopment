namespace Lab3.Tests;
using System;
using Xunit;
using System.Collections.Generic;
using Lab3;
using Lab3.Library;


public class UnitTest1
{
    [Theory]
    [InlineData(new string[] { "3", "p1", "2", "p1", "p2", "*****", "p2", "1", "p1", "*****", "p3", "1", "p1", "*****" }, new string[] { "YES", "YES", "NO" })]
    [InlineData(new string[] { "2", "p1", "1", "p2", "*****", "p2", "0", "*****" }, new string[] { "NO", "NO" })]
    [InlineData(new string[] { "2", "p1", "1", "p2", "*****", "p2", "1", "p1", "*****" }, new string[] { "YES", "YES" })]
    [InlineData(new string[] { "1", "p1", "0", "*****" }, new string[] { "NO" })]
    [InlineData(new string[] { "3", "p1", "2", "p2", "p3", "*****", "p2", "0", "*****", "p3", "1", "p2", "*****" }, new string[] { "NO", "NO", "NO" })]
    public void CheckProcedures_ReturnsCorrectResults(string[] input, string[] expectedOutput)
    {
        var (procedures, n) = RecursiveCheck.ReadInput(input);

        var result = RecursiveCheck.CheckProcedures(procedures);

        Assert.Equal(string.Join(Environment.NewLine, expectedOutput), result.ToString().Trim());
    }

    [Fact]
    public void ReadInput_ValidInput_ReturnsCorrectProcedures()
    {
        string[] input = new string[] { "2", "p1", "1", "p2", "*****", "p2", "0", "*****" };

        var (procedures, n) = RecursiveCheck.ReadInput(input);

        Assert.Equal(2, n);
        Assert.Equal("p1", procedures[0].Id);
        Assert.Equal("p2", procedures[1].Id);
        Assert.Single(procedures[0].CalledProcedures);
        Assert.Empty(procedures[1].CalledProcedures);
    }

    [Fact]
    public void ReadInput_MissingTerminationLine_ThrowsException()
    {
        string[] input = new string[] { "2", "p1", "1", "p2", "p2", "0", "*****" };

        var ex = Assert.Throws<InvalidOperationException>(() => RecursiveCheck.ReadInput(input));
        Assert.Equal("Missing termination line (*****) for procedure p1 at line 5.", ex.Message);
    }

    [Fact]
    public void ReadInput_InvalidProcedureIdentifier_ThrowsException()
    {
        string[] input = new string[] { "1", "P1", "0", "*****" };

        var ex = Assert.Throws<InvalidOperationException>(() => RecursiveCheck.ReadInput(input));
        Assert.Equal("Invalid procedure identifier at line 2. It must be non-empty, less than 100 characters, and contain only lowercase letters or digits.", ex.Message);
    }

    [Fact]
    public void ReadInput_TooManyProcedures_ThrowsException()
    {
        string[] input = new string[] { "101" };

        var ex = Assert.Throws<InvalidOperationException>(() => RecursiveCheck.ReadInput(input));
        Assert.Equal("The first line must contain an integer n (1 <= n <= 100)!", ex.Message);
    }

    [Theory]
    [InlineData("p1", true)]
    [InlineData("p123", true)]
    [InlineData("p@", false)]
    [InlineData("p P", false)]
    [InlineData("", false)]
    public void IsValidIdentifier_ReturnsExpectedResult(string identifier, bool expected)
    {
        var result = RecursiveCheck.IsValidIdentifier(identifier);

        Assert.Equal(expected, result);
    }
}