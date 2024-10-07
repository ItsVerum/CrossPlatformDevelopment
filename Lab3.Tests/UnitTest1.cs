namespace Lab3.Tests;
using System;
using Xunit;
using System.Collections.Generic;
using Lab3;


public class UnitTest1
{
    // Параметризовані тести для перевірки правильності циклів в процедурах
    [Theory]
    [InlineData(new string[] { "3", "p1", "2", "p1", "p2", "*****", "p2", "1", "p1", "*****", "p3", "1", "p1", "*****" }, new string[] { "YES", "YES", "NO" })]
    [InlineData(new string[] { "2", "p1", "1", "p2", "*****", "p2", "0", "*****" }, new string[] { "NO", "NO" })]
    [InlineData(new string[] { "2", "p1", "1", "p2", "*****", "p2", "1", "p1", "*****" }, new string[] { "YES", "YES" })]
    [InlineData(new string[] { "1", "p1", "0", "*****" }, new string[] { "NO" })]
    [InlineData(new string[] { "3", "p1", "2", "p2", "p3", "*****", "p2", "0", "*****", "p3", "1", "p2", "*****" }, new string[] { "NO", "NO", "NO" })]
    public void CheckProcedures_ReturnsCorrectResults(string[] input, string[] expectedOutput)
    {
        // Arrange
        var (procedures, n) = Program.ReadInput(input);

        // Act
        var result = Program.CheckProcedures(procedures);

        // Assert
        Assert.Equal(string.Join(Environment.NewLine, expectedOutput), result.ToString().Trim());
    }

    // Тест на валідний вхід для ReadInput
    [Fact]
    public void ReadInput_ValidInput_ReturnsCorrectProcedures()
    {
        // Arrange
        string[] input = new string[] { "2", "p1", "1", "p2", "*****", "p2", "0", "*****" };

        // Act
        var (procedures, n) = Program.ReadInput(input);

        // Assert
        Assert.Equal(2, n);
        Assert.Equal("p1", procedures[0].Id);
        Assert.Equal("p2", procedures[1].Id);
        Assert.Single(procedures[0].CalledProcedures);
        Assert.Empty(procedures[1].CalledProcedures);
    }

    // Тест на неправильний формат входу (відсутній рядок з «*****»)
    [Fact]
    public void ReadInput_MissingTerminationLine_ThrowsException()
    {
        // Arrange
        string[] input = new string[] { "2", "p1", "1", "p2", "p2", "0", "*****" };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ReadInput(input));
        Assert.Equal("Missing termination line (*****) for procedure p1 at line 5.", ex.Message);
    }

    // Тест на невалідний ідентифікатор процедури
    [Fact]
    public void ReadInput_InvalidProcedureIdentifier_ThrowsException()
    {
        // Arrange
        string[] input = new string[] { "1", "P1", "0", "*****" }; // Ідентифікатор містить велику літеру

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ReadInput(input));
        Assert.Equal("Invalid procedure identifier at line 2. It must be non-empty, less than 100 characters, and contain only lowercase letters or digits.", ex.Message);
    }

    // Тест на перевищення допустимої кількості процедур
    [Fact]
    public void ReadInput_TooManyProcedures_ThrowsException()
    {
        // Arrange
        string[] input = new string[] { "101" };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ReadInput(input));
        Assert.Equal("The first line must contain an integer n (1 <= n <= 100)!", ex.Message);
    }

    // Параметризовані тести для перевірки ідентифікаторів процедур
    [Theory]
    [InlineData("p1", true)]
    [InlineData("p123", true)]
    [InlineData("p@", false)]  // недопустимий символ
    [InlineData("p P", false)] // пробіл
    [InlineData("", false)]    // порожній ідентифікатор
    public void IsValidIdentifier_ReturnsExpectedResult(string identifier, bool expected)
    {
        // Act
        var result = Program.IsValidIdentifier(identifier);

        // Assert
        Assert.Equal(expected, result);
    }
}