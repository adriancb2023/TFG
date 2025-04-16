using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using Microsoft.Web.WebView2.Core;

namespace TFG_V0._01.Ventanas
{
    public partial class Home : Window
    {
        #region variables

        #endregion

        #region Inicializacion
        public Home()
        {
            InitializeComponent();
            AplicarModoSistema();
        }
        #endregion

        #region barra superior
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                MaximizeButton_Click(sender, e);
            }
            else
            {
                this.DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Aplicar modo oscuro/claro cargado por sistema
        private void AplicarModoSistema()
        {
            var button = this.FindName("ThemeButton") as Button;
            var icon = button?.Template.FindName("ThemeIcon", button) as Image;
            if (MainWindow.isDarkTheme)
            {
                bool modo = true;
                // Aplicar modo oscuro
                if (icon != null)
                {
                    icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/sol.png", UriKind.Relative));
                }
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\oscuro\main.png") as ImageSource;
                CambiarIconosAOscuros();
                CambiarTextosBlanco();
                TextIconBack(modo);
            }
            else
            {
                bool modo = false;
                // Aplicar modo claro
                if (icon != null)
                {
                    icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/luna.png", UriKind.Relative));
                }
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\claro\main.png") as ImageSource;
                CambiarIconosAOscuros();
                CambiarTextosBlanco();
                TextIconBack(modo);
            }
        }
        #endregion

        #region modo oscuro/claro + navbar
        private void CambiarIconosAOscuros()
        {
            imagenHome1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/home.png", UriKind.Relative));
            imagenDocumentos1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/documentos.png", UriKind.Relative));
            imagenClientes1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/clientes.png", UriKind.Relative));
            imagenCasos1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/casos.png", UriKind.Relative));
            imagenAyuda1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/ayuda.png", UriKind.Relative));
            imagenAgenda1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/agenda.png", UriKind.Relative));
            imagenAjustes1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/ajustes.png", UriKind.Relative));
            imagenHome2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/home.png", UriKind.Relative));
            imagenDocumentos2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/documentos.png", UriKind.Relative));
            imagenClientes2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/clientes.png", UriKind.Relative));
            imagenCasos2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/casos.png", UriKind.Relative));
            imagenAyuda2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/ayuda.png", UriKind.Relative));
            imagenAgenda2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/agenda.png", UriKind.Relative));
            imagenAjustes2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/ajustes.png", UriKind.Relative));
            imagenBuscar1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/buscar.png", UriKind.Relative));
            imagenBuscar2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/buscar.png", UriKind.Relative));
        }

        private void CambiarIconosAClaros()
        {
            imagenHome1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/home2.png", UriKind.Relative));
            imagenDocumentos1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/documentos2.png", UriKind.Relative));
            imagenClientes1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/clientes2.png", UriKind.Relative));
            imagenCasos1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/casos2.png", UriKind.Relative));
            imagenAyuda1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/ayuda2.png", UriKind.Relative));
            imagenAgenda1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/agenda2.png", UriKind.Relative));
            imagenAjustes1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/ajustes2.png", UriKind.Relative));
            imagenHome2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/home2.png", UriKind.Relative));
            imagenDocumentos2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/documentos2.png", UriKind.Relative));
            imagenClientes2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/clientes2.png", UriKind.Relative));
            imagenCasos2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/casos2.png", UriKind.Relative));
            imagenAyuda2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/ayuda2.png", UriKind.Relative));
            imagenAgenda2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/agenda2.png", UriKind.Relative));
            imagenAjustes2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/ajustes2.png", UriKind.Relative));
            imagenBuscar1.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/buscar2.png", UriKind.Relative));
            imagenBuscar2.Source = new BitmapImage(new Uri($"/TFG V0.01;component/Recursos/Iconos/buscar2.png", UriKind.Relative));
        }

        private void CambiarTextosNegro()
        {
            btnAgenda.Foreground = new SolidColorBrush(Colors.Black);
            btnAjustes.Foreground = new SolidColorBrush(Colors.Black);
            btnAyuda.Foreground = new SolidColorBrush(Colors.Black);
            btnCasos.Foreground = new SolidColorBrush(Colors.Black);
            btnClientes.Foreground = new SolidColorBrush(Colors.Black);
            btnDocumentos.Foreground = new SolidColorBrush(Colors.Black);
            btnHome.Foreground = new SolidColorBrush(Colors.Black);
            btnBuscar.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void CambiarTextosBlanco()
        {
            btnAgenda.Foreground = new SolidColorBrush(Colors.White);
            btnAjustes.Foreground = new SolidColorBrush(Colors.White);
            btnAyuda.Foreground = new SolidColorBrush(Colors.White);
            btnCasos.Foreground = new SolidColorBrush(Colors.White);
            btnClientes.Foreground = new SolidColorBrush(Colors.White);
            btnDocumentos.Foreground = new SolidColorBrush(Colors.White);
            btnHome.Foreground = new SolidColorBrush(Colors.White);
            btnBuscar.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TextIconBack(bool modo)
        {
            if (modo)
            {
                var claro = (Color)ColorConverter.ConvertFromString("#ecfdf5");
                backgroun_desplegado.Background = new SolidColorBrush(claro);
                backgroun_contraido.Background = new SolidColorBrush(claro);
                CambiarIconosAOscuros();
                CambiarTextosNegro();
            }
            else
            {
                var oscuro = (Color)ColorConverter.ConvertFromString("#424242");
                backgroun_desplegado.Background = new SolidColorBrush(oscuro);
                backgroun_contraido.Background = new SolidColorBrush(oscuro);
                CambiarIconosAClaros();
                CambiarTextosBlanco();
            }
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.isDarkTheme = !MainWindow.isDarkTheme;
            var button = sender as Button;
            var icon = button.Template.FindName("ThemeIcon", button) as Image;
            if (MainWindow.isDarkTheme)
            {
                icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/sol.png", UriKind.Relative));
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\oscuro\main.png") as ImageSource;
                TextIconBack(MainWindow.isDarkTheme);
            }
            else
            {
                icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/luna.png", UriKind.Relative));
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\claro\main.png") as ImageSource;
                TextIconBack(MainWindow.isDarkTheme);
            }
        }
        #endregion

        #region navbar animacion
        private void Menu_MouseEnter(object sender, MouseEventArgs e)
        {
            menu_desplegado.Visibility = Visibility.Visible;
            menu_contraido.Visibility = Visibility.Collapsed;
        }

        private void Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            menu_contraido.Visibility = Visibility.Visible;
            menu_desplegado.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Navbar botones
        private void irHome(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void irJurisprudencia (object sender, RoutedEventArgs e)
        {
            BusquedaJurisprudencia busquedaJurisprudencia = new BusquedaJurisprudencia();
            busquedaJurisprudencia.Show();
            this.Close();
        }

        private void irDocumentos(object sender, RoutedEventArgs e)
        {
            Documentos documentos = new Documentos();
            documentos.Show();
            this.Close();
        }

        private void irClientes(object sender, RoutedEventArgs e)
        {
            //Clientes clientes = new Clientes();
            //clientes.Show();
            //this.Close();
        }

        private void irCasos(object sender, RoutedEventArgs e)
        {
            //Casos casos = new Casos();
            //casos.Show();
            //this.Close();
        }

        private void irAyuda(object sender, RoutedEventArgs e)
        {
            //Ayuda ayuda = new Ayuda();
            //ayuda.Show();
            //this.Close();
        }

        private void irAgenda(object sender, RoutedEventArgs e)
        {
            //Agenda agenda = new Agenda();
            //agenda.Show();
            //this.Close();
        }

        private void irAjustes(object sender, RoutedEventArgs e)
        {
            //Ajustes ajustes = new Ajustes();
            //ajustes.Show();
            //this.Close();
        }

        #endregion
    }
}
