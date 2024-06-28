using System;
using System.IO;
using Xunit;
using DiaryManager;  // Ensure this namespace is correctly referenced

namespace DiaryManagerTests
{
    public class DailyTests
    {
        private const string TestDiaryFilePath = "../../../testdiary.txt";

        [Fact]
        public void ReadDiaryFile_FileExists_DisplaysContent()
        {
            // Arrange
            var diary = new DailyDiary();
            File.WriteAllText(TestDiaryFilePath, "2024-06-28: Test entry\n");

            // Act
            diary.ReadDiaryFile();

            // Assert
            // Manually verify the console output if necessary
        }

        [Fact]
        public void AddEntry_ValidEntry_IncreasesLineCount()
        {
            // Arrange
            var diary = new DailyDiary();
            File.WriteAllText(TestDiaryFilePath, string.Empty);
            var entry = new Entry(DateTime.Now, "New test entry");

            // Act
            diary.AddEntry(entry);

            // Assert
            var lines = File.ReadAllLines(TestDiaryFilePath);
            Assert.Single(lines);
        }
    }
}
