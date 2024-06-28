using Xunit;
using System;
using System.IO;

namespace DiaryManagerTests
{
    public class AllTests
    {
        private const string DiaryFilePath = "../../../mydiary.txt"; // Adjust path as necessary

        [Fact]
        public void TestAddEntry()
        {
            // Create a new entry
            var newEntry = new DiaryManager.Entry("2023-01-01", "Test entry");

            // Call the AddEntry method
            var diary = new DiaryManager.DailyDiary();
            diary.AddEntry(newEntry);

            // Assert: Check if the entry is added to the diary file
            string diaryContent = File.ReadAllText(DiaryFilePath);
            Assert.Contains("2023-01-01", diaryContent);
            Assert.Contains("Test entry", diaryContent);

            // Clean up: delete the test file after the test
            File.Delete(DiaryFilePath);
        }

        [Fact]
        public void TestDeleteEntry()
        {
            // Create a sample diary file for testing
            string[] lines = { "2023-01-01", "Entry content 1", "2023-01-02", "Entry content 2" };
            File.WriteAllLines(DiaryFilePath, lines);

            // Call the DeleteEntry method
            var diary = new DiaryManager.DailyDiary();
            diary.DeleteEntry("2023-01-01");

            // Assert: Check if the entry is deleted from the diary file
            string diaryContent = File.ReadAllText(DiaryFilePath);
            Assert.DoesNotContain("2023-01-01", diaryContent);
            Assert.DoesNotContain("Entry content 1", diaryContent);

            // Clean up: delete the test file after the test
            File.Delete(DiaryFilePath);
        }
    }
}
