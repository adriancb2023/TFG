using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TFG_V0._01.Ventanas
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void ThemeSwitch_Checked(object sender, RoutedEventArgs e)
        {
            // Cambiar a modo claro
            backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\source\repos\TFG V0.01\TFG V0.01\Recursos\Background\claro\main.png") as ImageSource;
            Titulo.Foreground = new SolidColorBrush(Colors.Black);

            // Quitar el foco del switch después de hacer clic
            Keyboard.ClearFocus();
        }

        private void ThemeSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            // Cambiar a modo oscuro
            backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\source\repos\TFG V0.01\TFG V0.01\Recursos\Background\oscuro\main.png") as ImageSource;
            Titulo.Foreground = new SolidColorBrush(Colors.White);

            // Quitar el foco del switch después de hacer clic
            Keyboard.ClearFocus();
        }
    }
}
