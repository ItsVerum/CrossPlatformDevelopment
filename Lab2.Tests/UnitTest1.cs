namespace Lab2.Tests;
using System;
using Xunit;
using System.Numerics;

public class UnitTest1
{
    // Тест на валідний вхід
    [Fact]
    public void ValidateInput_ValidInput_ReturnsCorrectTuple()
    {
        string[] input = new string[] { "4 " };
        var result = Program.ValidateInput(input);
        Assert.Equal(4, result);
    }

    // Тест на невалідний формат входу (неправильна кількість чисел)
    [Fact]
    public void ValidateInput_InvalidInputFormat_ThrowsException()
    {
        string[] input = new string[] { "5 3" };
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ValidateInput(input));
        Assert.Equal("The input file must contain 1 number written in first line!", ex.Message);
    }

    // Тест на невалідний формат чисел (нецілі числа)
    [Fact]
    public void ValidateInput_NonIntegerValues_ThrowsException()
    {
        string[] input = new string[] { "2.5" };
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ValidateInput(input));
        Assert.Equal("Input data must be integers!", ex.Message);
    }

    // Тест на число менше за мінімальне (N < 3)
    [Fact]
    public void ValidateInput_NumberLessThanThree_ThrowsException()
    {
        string[] input = new string[] { "2" };
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ValidateInput(input));
        Assert.Equal("The number must meets the conditions: 3 ≤ N ≤ 10 000.", ex.Message);
    }

    // Тест на правильне обчислення кількості тризначних простих чисел для N = 3
    [Fact]
    public void CalculateNumberOfPrimeNumbers_NIsThree_ReturnsCorrectResult()
    {
        int N = 3;
        long result = Program.CalculateNumberOfPrimeNumbers(N);
        Assert.Equal(143, result);
    }
}