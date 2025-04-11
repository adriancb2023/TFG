using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace TFG_V0._01.Ventanas
{
    public partial class Login : Window
    {
        private SupabaseAutentificacion _authService;
        public Login()
        {
            InitializeComponent();
            _authService = new SupabaseAutentificacion("https://ddwyrkqxpmwlznjfjrwv.supabase.co",
              "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRkd3lya3F4cG13bHpuamZqcnd2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQzOTcwNjQsImV4cCI6MjA1OTk3MzA2NH0.G2LzHWbC09LC69bj9wONzhD_a6AfFI1ZYFuQ3KD7XhI");
        }

        #region modo oscuro/claro
        private void ThemeSwitch_Checked(object sender, RoutedEventArgs e)
        {
            // Cambiar a modo claro
            backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\claro\main.png") as ImageSource;
            Titulo.Foreground = new SolidColorBrush(Colors.Black);

            // Quitar el foco del switch después de hacer clic
            Keyboard.ClearFocus();
        }

        private void ThemeSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            // Cambiar a modo oscuro
            backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\oscuro\main.png") as ImageSource;
            Titulo.Foreground = new SolidColorBrush(Colors.White);

            // Quitar el foco del switch después de hacer clic
            Keyboard.ClearFocus();
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
    }
}
