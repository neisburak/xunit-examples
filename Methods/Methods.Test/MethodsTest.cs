using System;
using System.Collections.Generic;
using Methods.Library;
using Moq;
using Xunit;

namespace Methods.Test;

public class MethodsTest
{
    private readonly Manager manager;
    private readonly Mock<IOperationService> mock;

    public MethodsTest()
    {
        mock = new Mock<IOperationService>();
        manager = new Manager(mock.Object);
    }

    [Fact]
    public void SumTest()
    {
        // Arrange
        int a = 6, b = 4;

        mock.Setup(x => x.Sum(a, b)).Returns(a + b);

        // Act
        var result = manager.Sum(a, b);

        // Assert
        Assert.Equal(10, result); // NotEqual
        mock.Verify(x => x.Sum(a, b), Times.Once); // How many times Sum method called
    }

    [Fact]
    public void ContainsTest()
    {
        // Arrange
        var name = "xUnit";

        // Act
        var result = "xUnit Tests";

        // Assert
        Assert.Contains(name, result); // DoesNotContain
    }

    [Fact]
    public void ContainsListTest()
    {
        // Arrange
        var tech = "xUnit";

        // Act
        var result = new List<string>() { "Entity Framework", "xUnit", "Ocelot" };

        // Assert
        Assert.Contains(result, x => x == tech); // DoesNotContain
    }

    [Fact]
    public void TrueTest()
    {
        Assert.True("xUnit".GetType() == typeof(string)); // False
    }

    [Fact]
    public void MatchesTest()
    {
        var pattern = @"[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        Assert.Matches(pattern, "info@test.com"); // DoesNotMatch
    }

    [Fact]
    public void StartsWithTest()
    {
        Assert.StartsWith("Sara", "Sara Samuels"); // EndsWith
    }

    [Fact]
    public void EmptyTest()
    {
        var list = new List<string>() { "Jane" };
        Assert.NotEmpty(list); // Empty
    }

    [Fact]
    public void InRangeTest()
    {
        Assert.InRange(10, 5, 15); // NotInRange
    }

    [Fact]
    public void SingleTest()
    {
        var list = new List<string>() { "Jane" };
        Assert.Single<string>(list);
    }

    [Fact]
    public void IsTypeTest()
    {
        var value = "Hans";
        Assert.IsType(typeof(string), value); // IsNotType
    }

    [Fact]
    public void IsAssignableFromTest()
    {
        Assert.IsAssignableFrom<IEnumerable<string>>(new List<string>());
    }

    [Fact]
    public void NullTest()
    {
        var name = "Jonathan";
        Assert.NotNull(name); // Null
    }

    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    public void FirstTheoryTest(int value)
    {
        mock.Setup(x => x.IsOdd(value)).Returns(value % 2 == 1);

        Assert.True(manager.IsOdd(value));
    }

    [Theory]
    [InlineData(2, 8, 10)]
    [InlineData(3, 7, 10)]
    public void SumTheoryTest(int a, int b, int sum)
    {
        mock.Setup(x => x.Sum(a, b)).Returns(a + b);

        var actual = manager.Sum(a, b);

        Assert.Equal(sum, actual);
    }

    [Theory]
    [InlineData(2, 5, 7)]
    [InlineData(6, 7, 13)]
    public void Sum_SimpleValues_ReturnTotalValue(int a, int b, int total)
    {
        mock.Setup(x => x.Sum(a, b)).Returns(a + b);

        var actual = manager.Sum(a, b);

        Assert.Equal(total, actual);
    }

    [Theory]
    [InlineData(-6, 7, 13)]
    [InlineData(-4, -5, 9)]
    [InlineData(3, 4, 7)]
    public void SumAbs_NegativeValues_ReturnTotalValue(int a, int b, int total)
    {
        var actual = 0;
        mock.Setup(x => x.SumAbs(It.IsAny<int>(), It.IsAny<int>())).Callback<int, int>((x, y) => actual = (Math.Abs(x) + Math.Abs(y)));
        manager.SumAbs(a, b);
        Assert.Equal(total, actual);
    }

    [Theory]
    [InlineData(5, 0)]
    public void Divide_ZeroValue_ReturnsException(int a, int b)
    {
        mock.Setup(x => x.Divide(a, b)).Throws(new ArgumentException("The divisor cannot be zero."));

        var ex = Assert.Throws<ArgumentException>(() => manager.Divide(a, b));

        Assert.Equal("The divisor cannot be zero.", ex.Message);
    }
}