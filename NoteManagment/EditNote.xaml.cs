using System.Diagnostics;

namespace NotesApp;

public partial class EditNote : ContentPage
{
	string name = "";

    protected override void OnAppearing()
    {
        base.OnAppearing();
		string path = FileSystem.Current.AppDataDirectory;
		string fullPath = Path.Combine(path, name + ".txt");
		var data = File.ReadAllText(fullPath);
		noteName.Text = name;
		note.Text = data;

    }

    public EditNote()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
	}
	public EditNote(string noteName)
	{
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage),typeof(MainPage));

        name = noteName;

	}
	private void editNote(object sender, EventArgs e)
	{

        var path = FileSystem.Current.AppDataDirectory;
        var fullpath = Path.Combine(path, noteName.Text + ".txt");


        var noteListPath = Path.Combine(path, "notelist.txt");

        if (!File.Exists(fullpath))
        {
            noteName.Text = "A note with that name doesnt exists";
        }
        else
        {
            if (File.Exists(noteListPath))
            {
                if(name != noteName.Text)
                {
                    var notelist = File.ReadAllText(noteListPath);

                    var notesData = File.ReadAllText(noteListPath).Split("\nsecret\n");
                    string textToWrite = "";
                    foreach (var data in notesData)
                    {
                        if (!data.Equals(name))
                        {
                            if (textToWrite.Equals(""))
                            {
                                textToWrite += data;
                            }
                            else
                            {
                                textToWrite += "\nsecret\n" + data;
                            }
                        }
                    }

                    notelist += "\nsecret\n" + noteName.Text;
                    File.WriteAllText(noteListPath, notelist);
                    File.WriteAllText(fullpath, note.Text);
                }


            }
            else
            {
                File.WriteAllText(noteListPath, noteName.Text);
                File.WriteAllText(fullpath, note.Text);
            }
        }


        if(noteName.Text != name)
        {
            var pathSelected = FileSystem.Current.AppDataDirectory;
            var fullpathSelected = Path.Combine(pathSelected, "notelist.txt");

            if (File.Exists(fullpathSelected))
            {
                var notesData = File.ReadAllText(fullpathSelected).Split("\nsecret\n");
                string textToWrite = "";
                foreach (var data in notesData)
                {
                    if (!data.Equals(name))
                    {
                        if (textToWrite.Equals(""))
                        {
                            textToWrite += data;
                        }
                        else
                        {
                            textToWrite += "\nsecret\n" + data;

                        }
                    }
                }
                File.WriteAllText(fullpathSelected, textToWrite);
                OnAppearing();
            }

            if (File.Exists(Path.Combine(pathSelected, name + ".txt")))
            {
                File.Delete(Path.Combine(pathSelected, name + ".txt"));
            }



            


        }
        goToMainPage();

    }
    private async void goToMainPage()
    {
        string path = FileSystem.Current.AppDataDirectory;
        string fullPath = Path.Combine(path, name + ".txt");
        Debug.WriteLine(fullPath);
        //await Navigation.PushAsync(new MainPage());
        //await Shell.Current.GoToAsync("///MainPage");
        await Shell.Current.GoToAsync("..");
    }
}