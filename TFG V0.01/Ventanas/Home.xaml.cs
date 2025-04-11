using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Web.WebView2.Core;

namespace TFG_V0._01.Ventanas
{
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            InitializeWebView();
            webView.Visibility = Visibility.Visible;
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

        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.Navigate("https://www.poderjudicial.es/search/");
        }

        
    }
}
