using System.Diagnostics;
using System.Xml.Linq;

namespace NotesApp;

public partial class AddNote : ContentPage
{
	public AddNote()
	{
		InitializeComponent();
	}

	private void saveNote(object sender, EventArgs e)
	{

        var path = FileSystem.Current.AppDataDirectory;
        var noteListPath = Path.Combine(path, "notelist.txt");
        var fullpath = Path.Combine(path, noteName.Text + ".txt");

		if (File.Exists(fullpath))
		{
			noteName.Text = "A note with that name already exists";
		}
		else
		{
			if (File.Exists(noteListPath))
			{
				var notelist = File.ReadAllText(noteListPath);
				notelist += "\nsecret\n" + noteName.Text;
				File.WriteAllText(noteListPath, notelist);
                File.WriteAllText(fullpath, note.Text);

			}
			else
			{
                File.WriteAllText(noteListPath, noteName.Text);
                File.WriteAllText(fullpath, note.Text);
            }
        }

		goToMainPage();

    }


	private async void goToMainPage()
	{
        await Navigation.PushAsync(new MainPage());

    }


}