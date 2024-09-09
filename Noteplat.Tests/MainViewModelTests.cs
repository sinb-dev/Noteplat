using Noteplat.Models;
using Noteplat.ViewModels;
using ReactiveUI;
using System.Reactive.Threading.Tasks;
using System.Text;
namespace Noteplat.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public async void LoadAndSaveText()
        {
            //Create some file
            var filename = "file.txt";
            var contents = "Hello";
            File.WriteAllText(filename, contents);

            //Load the file using commands
            EditViewModel mv = new();
            //Assert.Null(mv.SaveCommand);

            await mv.LoadCommand.Execute().ToTask();

            //Assert.NotNull(mv.SaveCommand);
            Assert.Equal(contents, mv.TextDocument.Text);

            //Change contents and save
            mv.TextDocument = new TextDocument(filename, GenerateLargeText(10000));

            //Save document
            mv.SaveCommand.Execute();
        }

        [Fact]
        public async void SaveAsTest()
        {
            var filename = "file.txt";
            var contents = "Testing Save As";

            EditViewModel mv = new();

            Assert.NotNull(mv.SaveAsCommand); //Save as should always be possible
            filename = await mv.PickFile();

            mv.TextDocument = new TextDocument(filename, contents);

            //Setup the repository to pick filename
            await mv.SaveAsCommand.Execute().ToTask();

            //Reset mv
            mv = new();
            await mv.LoadCommand.Execute().ToTask();
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