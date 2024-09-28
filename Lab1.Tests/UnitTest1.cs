namespace Lab1.Tests;
using Xunit;
using Xunit;
using System;
using System.Numerics;

public class UnitTest1
{
    // Тест на валідний вхід
    [Fact]
    public void ValidateInput_ValidInput_ReturnsCorrectTuple()
    {
        string[] input = new string[] { "5 3 2" };
        var result = Program.ValidateInput(input);
        Assert.Equal((5, 3, 2), result);
    }

    // Тест на невалідний формат входу (неправильна кількість чисел)
    [Fact]
    public void ValidateInput_InvalidInputFormat_ThrowsException()
    {
        string[] input = new string[] { "5 3" };
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ValidateInput(input));
        Assert.Equal("The input file must contain 3 numbers written in one line!", ex.Message);
    }

    // Тест на невалідний формат чисел (нецілі числа)
    [Fact]
    public void ValidateInput_NonIntegerValues_ThrowsException()
    {
        string[] input = new string[] { "5.5 3 a" };
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ValidateInput(input));
        Assert.Equal("Input data must be integers!", ex.Message);
    }

    // Тест на неправильні умови чисел
    [Fact]
    public void ValidateInput_InvalidNumberConditions_ThrowsException()
    {
        string[] input = new string[] { "21 3 2" }; // N > 20
        var ex = Assert.Throws<InvalidOperationException>(() => Program.ValidateInput(input));
        Assert.Equal("The numbers must meet the conditions: 1 ≤ N ≤ 20, 0 ≤ A, B ≤ 20. The numbers cannot be negative.", ex.Message);
    }

    // Тест на правильний результат обчислення комбінацій
    [Fact]
    public void CalculateNumberOfWays_ValidInput_ReturnsCorrectResult()
    {
        int N = 2, A = 1, B = 1;
        BigInteger result = Program.CalculateNumberOfWays(N, A, B);
        Assert.Equal(new BigInteger(9), result);
    }
}
