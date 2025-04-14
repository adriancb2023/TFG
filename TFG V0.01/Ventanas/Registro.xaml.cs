using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TFG_V0._01.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        #region variables
        private readonly SupabaseAutentificacion _supabaseAutentificacion;
        #endregion

        #region InitializeComponent
        public Registro()
        {
            InitializeComponent();
            _supabaseAutentificacion = new SupabaseAutentificacion("https://ddwyrkqxpmwlznjfjrwv.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRkd3lya3F4cG13bHpuamZqcnd2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQzOTcwNjQsImV4cCI6MjA1OTk3MzA2NH0.G2LzHWbC09LC69bj9wONzhD_a6AfFI1ZYFuQ3KD7XhI");
        }
        #endregion

        #region modo oscuro/claro
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.isDarkTheme = !MainWindow.isDarkTheme;
            var button = sender as Button;
            var icon = button.Template.FindName("ThemeIcon", button) as Image;
            if (MainWindow.isDarkTheme)
            {
                icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/sol.png", UriKind.Relative));
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\oscuro\main.png") as ImageSource;
                Titulo.Foreground = Brushes.White;
                correo.Foreground = Brushes.White;
                Pass1.Foreground = Brushes.White;
                Pass2.Foreground = Brushes.White;
            }
            else
            {
                icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/luna.png", UriKind.Relative));
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\claro\main.png") as ImageSource;
                Titulo.Foreground = Brushes.Black;
                correo.Foreground = Brushes.Black;
                Pass1.Foreground = Brushes.Black;
                Pass2.Foreground = Brushes.Black;
            }
        }
        #endregion

        #region volver al login
        private void VolverLogin(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        #endregion

        #region Registro usuario
        private async void RegistrarUser(object sender, RoutedEventArgs e)
        {
            try
            {
                var email = Email.Text;
                var password = Pass.Password;
                var confirmPassword = pass2.Password;

                if (password != confirmPassword)
                {
                    MessageBox.Show("Las contraseñas no coinciden");
                    return;
                }

                var result = await _supabaseAutentificacion.SignUpAsync(email, password);
                MessageBox.Show("Registro exitoso. Confirme su correo antes de acceder.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el registro: {ex.Message}");
            }
        }
        #endregion
    }
}
