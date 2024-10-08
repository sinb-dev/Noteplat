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
            UnitTestRepository _repository = new UnitTestRepository();
            //Create some file
            var filename = Path.GetTempFileName();
            var contents = "Hello";
            _repository.Save(filename, contents);

            //Load the file using commands
            MainViewModel mv = new(_repository);
            Assert.Null(mv.SaveCommand);

            _repository.SetFilePick(filename);
            await mv.LoadCommand.Execute().ToTask();

            Assert.NotNull(mv.SaveCommand);
            Assert.Equal(contents, mv.TextDocument.Text);

            //Change contents and save
            mv.TextDocument = new TextDocument(filename, GenerateLargeText(10000));

            //Save document
            mv.SaveCommand.Execute();
        }


        [Fact]
        public async void SaveAsTest()
        {
            UnitTestRepository _repository = new UnitTestRepository();
            var filename = Path.GetTempFileName();
            var contents = "Testing Save As";

            MainViewModel mv = new(_repository);
            Assert.Null(mv.SaveCommand); // In order to use the SaveCommand a file must be loaded first

            Assert.NotNull(mv.SaveAsCommand); //Save as should always be possible

            mv.TextDocument = new TextDocument(filename, contents);

            //Setup the repository to pick filename
            _repository.SetFilePick(filename);
            await mv.SaveAsCommand.Execute().ToTask();

            //Reset mv
            mv = new(_repository);
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