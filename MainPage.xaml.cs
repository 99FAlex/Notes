using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace NotesApp
{
    public partial class MainPage : ContentPage
    {
        protected override void OnAppearing()
        {
            noteLayout.Children.Clear();
            base.OnAppearing();
            var pathSelected = FileSystem.Current.AppDataDirectory;
            var fullpathSelected = Path.Combine(pathSelected, "notelist.txt");

            if (File.Exists(fullpathSelected))
            {
                var notesData = File.ReadAllText(fullpathSelected).Split("\nsecret\n");
                
                    foreach (var data in notesData)
                    {
                        if (!data.Equals(""))
                        {
                            Grid grid = new Grid();
                            Label noteName = new Label()
                            {
                                Text = data,
                                WidthRequest = 210,
                                HorizontalOptions = LayoutOptions.Start,
                                VerticalOptions = LayoutOptions.Center,
                                LineBreakMode = LineBreakMode.WordWrap,
                                Margin = 10,
                                FontAttributes = FontAttributes.Bold
                            };
                            Button edit = new Button()
                            {
                                Text = "Edit",
                                HorizontalOptions = LayoutOptions.Center,
                                Margin = new Thickness(130, 5, 5, 5),
                                Background = Colors.Blue,

                            };
                        edit.Clicked += (sender, e) => onEdit(sender, e);

                        Button delete = new Button()
                            {
                                Text = "delete",
                                HorizontalOptions = LayoutOptions.End,
                                Margin = new Thickness(5, 5, 5, 5),
                                Background = Colors.Red,
                                TextColor = Colors.Black,

                            };
                            delete.Clicked += (sender, e) => ondelete(sender, e);
                            grid.Add(noteName);
                            grid.Add(edit);
                            grid.Add(delete);

                            Border border = new Border() { Content = grid, Stroke = Colors.Black, Margin = new Thickness(0,2)};


                            noteLayout.Add(border);

                            //                        <Button Text="Delete" HorizontalOptions="End" Margin="5" BackgroundColor="Red" TextColor="Black"/>
                            //        < Button Text = "Edit" HorizontalOptions = "Center" Margin = "130 , 5, 5, 5" Background = "Blue" />
                        }

                    }
                
                

            }


        }
        public MainPage()
        {
            InitializeComponent();
        }
        private void onAddNote(object sender, EventArgs e)
        {
            GoToAddNote();
        }

        private void ondelete(object sender, EventArgs e)
        {
            var tappedNote = ((sender as Button).Parent as Grid).Children.OfType<Label>().First().Text;

            var pathSelected = FileSystem.Current.AppDataDirectory;
            var fullpathSelected = Path.Combine(pathSelected, "notelist.txt");

            if (File.Exists(fullpathSelected))
            {
                var notesData = File.ReadAllText(fullpathSelected).Split("\nsecret\n");
                string textToWrite = "";
                foreach(var data in notesData)
                {
                    if (!data.Equals(tappedNote))
                    {
                        if (textToWrite.Equals(""))
                        {
                            textToWrite += data;
                        }
                        else
                        {
                            textToWrite += "\nsecret\n" + data ;
                        }
                    }
                }
                File.WriteAllText(fullpathSelected, textToWrite);
                OnAppearing();
            }

            if(File.Exists(Path.Combine(pathSelected, tappedNote + ".txt")))
            {
                File.Delete(Path.Combine(pathSelected, tappedNote + ".txt"));
            }


        }

        private void onEdit(object sender, EventArgs e)
        {
            var tappedNote = ((sender as Button).Parent as Grid).Children.OfType<Label>().First().Text;
            GoToEditNote(tappedNote);         


        }
        private async void GoToAddNote()
        {
            await Navigation.PushAsync(new AddNote());

            //await Shell.Current.GoToAsync(nameof(AddNote));
        }
        private async void GoToEditNote(string name)
        {

            await Navigation.PushAsync(new EditNote(name));
        }


    }

}
