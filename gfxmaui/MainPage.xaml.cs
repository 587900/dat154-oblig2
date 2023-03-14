using GfxMaui;

namespace gfxmaui
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            Main.Initialize(Draw);
        }

    }
}