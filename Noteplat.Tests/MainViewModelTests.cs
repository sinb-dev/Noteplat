using Noteplat.Models;
using Noteplat.ViewModels;
using ReactiveUI;
using System.Text;
namespace Noteplat.Tests
{

    public class MainViewModelTests
    {

        UnitTestRepository _repository = new UnitTestRepository();
        [Fact]
        public void SaveText()
        {
            var filename =Path.GetTempFileName();
            MainViewModel mv = new(_repository);

            //Tests
            mv.TextDocument = new TextDocument(filename, GenerateLargeText(10000));
            Assert.Equal(filename,mv.TextDocument.Filename);
            
            //Save document
            mv.SaveCommand.Execute();

            //Check document exists
            Assert.True(File.Exists(filename));
            File.Delete(mv.TextDocument.Filename);
        }

        [Fact]
        public void LoadText()
        {
            var filename =Path.GetTempFileName();
            var contents = "Hello";
            _repository.Save(filename, contents);

            MainViewModel mv = new(_repository);


            _repository.SetFilePick(filename);
            mv.LoadCommand.Execute();

            Assert.Equal(contents, mv.TextDocument.Text);
        }

        public static string GenerateLargeText(int length)
        {
            // Define the characters to use in the text
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // Create a random number generator
            Random random = new Random();

            // Build the large text string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(characters[random.Next(characters.Length)]);
            }

            return sb.ToString();
        }
    }
}