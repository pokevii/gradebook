using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void BookCalculatesAnAverageGrade()
        {
            //arrange
            var book = new InMemoryBook("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            //act
            var result = book.GetStats();
            
            //assert
            Assert.Equal(85.6, result.Average, 1);
            Assert.Equal(90.5, result.HighestGrade, 1);
            Assert.Equal(77.3, result.LowestGrade, 1);
            Assert.Equal('B', result.Letter);
        }
    }
}