namespace Lab7AutomatedSolution.Test;

public class StringServiceTest
{
    [Theory]
    [InlineData("There is no reason in this text.", 0, 5, "There")]
    [InlineData("There is no reason in this text.", 6, 18, "is no reason")]
    [InlineData("Cadein.et", 4, 6, "in")]
    public void Substring_WithValidInput_ReturnsExpectedSubstring(string source, int startIndex, int endIndex, string expectedResult)
    {
        // act
        var result = StringService.Substring(source, startIndex, endIndex);

        // assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("text.", -1, 5)]
    [InlineData("text.", 0, -5)]
    [InlineData("text.", 0, 10)]
    [InlineData("text.", 10, 20)]
    public void Substring_WithInvalidIndexes_ThrowsArgumentOutOfRangeException(string source, int startIndex, int endIndex)
    {
        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            StringService.Substring(source, startIndex, endIndex));
    }

    [Fact]
    public void Substring_WithNullSourceInput_ThrowsNullReferenceException()
    {
        // arrange
        string nullSource = null;
        int sIndex = 0;
        int eIndex = 1;

        // act, assert
        Assert.Throws<NullReferenceException>(() =>
            StringService.Substring(nullSource, sIndex, eIndex));
    }

    [Theory]
    [InlineData("Hello, world!", 0, 5, "Hello")]
    [InlineData("abcdef", -4, -1, "cde")]
    [InlineData("abcdef", -6, 3, "abc")]
    public void Slice_WithValidRangeIncludingNegativeIndices_ReturnsExpected(string input, int start, int end, string expected)
    {
        // act
        var result = StringService.Slice(input, start, end);

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("abc", -10, 2)]
    [InlineData("abc", 1, 10)]
    [InlineData("abc", 2, 1)]
    public void Slice_WithInvalidRange_ThrowsArgumentOutOfRangeException(string input, int start, int end)
    {
        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            StringService.Slice(input, start, end));
    }

    [Fact]
    public void Slice_NullInput_ThrowsNullReferenceException()
    {
        // act, assert
        Assert.Throws<NullReferenceException>(() =>
            StringService.Slice(null, 0, 1));
    }

    [Theory]
    [InlineData("There is no reason in this text.", "There", 0)]
    [InlineData("There is no reason in this text.", ";", -1)]
    [InlineData("There is no reason in this text.", "ere", 2)]
    public void IndexOf_WithValidInput_ReturnsExpectedIndex(string source, string search, int expectedIndex)
    {
        // act
        var result = StringService.IndexOf(source, search);

        // assert
        Assert.Equal(expectedIndex, result);
    }

    [Fact]
    public void IndexOf_WithNullSourceInput_ThrowsNullReferenceException()
    {
        // arrange
        string nullSource = null;
        string search = "";

        // act, assert
        Assert.Throws<NullReferenceException>(() =>
            StringService.IndexOf(nullSource, search));
    }
}