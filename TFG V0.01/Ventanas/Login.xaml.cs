using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace TFG_V0._01.Ventanas
{
    public partial class Login : Window
    {
        #region variables
        private bool isDarkTheme = true;
        private SupabaseAutentificacion _authService;
        #endregion

        public Login()
        {
            InitializeComponent();
            _authService = new SupabaseAutentificacion("https://ddwyrkqxpmwlznjfjrwv.supabase.co",
              "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRkd3lya3F4cG13bHpuamZqcnd2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQzOTcwNjQsImV4cCI6MjA1OTk3MzA2NH0.G2LzHWbC09LC69bj9wONzhD_a6AfFI1ZYFuQ3KD7XhI");
        }

        #region modo oscuro/claro
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            isDarkTheme = !isDarkTheme;
            var button = sender as Button;
            var icon = button.Template.FindName("ThemeIcon", button) as Image;
            if (isDarkTheme)
            {
                icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/sol.png", UriKind.Relative));
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\oscuro\main.png") as ImageSource;

            }
            else
            {
                icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/luna.png", UriKind.Relative));
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\claro\main.png") as ImageSource;

            }
        }
        #endregion

        #region Login
        private async void Loguearse(object sender, RoutedEventArgs e)
        {
            string email = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                var resultado = await _authService.SignInAsync(email, password);
                MessageBox.Show("Inicio de sesión exitoso ✅", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                var home = new Home();
                home.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Registro
        private void irRegistrarse(object sender, RoutedEventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
            this.Close();
        }
        #endregion

        #region saltarse el inicio de sesión con root
        private async void saltarInicio(object sender, RoutedEventArgs e)
        {
            string email = "root@root.com";
            string password = "root";

            try
            {
                var resultado = await _authService.SignInAsync(email, password);
                var home = new Home();
                home.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
