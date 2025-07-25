namespace NotesApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddNote), typeof(AddNote));
        }

        private void onAddNote(object sender, EventArgs e)
        {
            GoToAddNote();
        }
        private async void GoToAddNote()
        {
            await Shell.Current.GoToAsync(nameof(AddNote));
        }
    }

}
