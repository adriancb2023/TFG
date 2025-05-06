using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using TFG_V0._01.Modelo;

namespace TFG_V0._01.Ventanas
{
    public partial class Home : Window
    {
        #region variables animacion
        private Storyboard fadeInStoryboard;
        private Storyboard shakeStoryboard;
        #endregion

        #region variables
        private DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);
        #endregion

        #region Inicializacion
        public Home()
        {
            InitializeComponent();
            InitializeAnimations();
            AplicarModoSistema();

            cargarDatos();
        }
        #endregion

        #region Aplicar modo oscuro/claro cargado por sistema
        private void AplicarModoSistema()
        {
            var button = this.FindName("ThemeButton") as Button;
            var icon = button?.Template.FindName("ThemeIcon", button) as Image;

            if (MainWindow.isDarkTheme)
            {
                // Aplicar modo oscuro
                if (icon != null)
                {
                    icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/sol.png", UriKind.Relative));
                }
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString("pack://application:,,,/TFG V0.01;component/Recursos/Background/oscuro/main.png") as ImageSource;
                CambiarIconosAClaros();
                CambiarTextosBlanco();
                backgroun_menu.Background = new SolidColorBrush(Color.FromArgb(48, 255, 255, 255)); // Fondo semitransparente
            }
            else
            {
                // Aplicar modo claro
                if (icon != null)
                {
                    icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/luna.png", UriKind.Relative));
                }
                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString("pack://application:,,,/TFG V0.01;component/Recursos/Background/claro/main.png") as ImageSource;
                CambiarIconosAOscuros();
                CambiarTextosNegro();
                backgroun_menu.Background = new SolidColorBrush(Color.FromArgb(48, 128, 128, 128)); // Gris semitransparente

            }
        }
        #endregion

        #region modo oscuro/claro + navbar
        private void CambiarIconosAOscuros()
        {
            CambiarIcono("imagenHome2", "/TFG V0.01;component/Recursos/Iconos/home.png");
            CambiarIcono("imagenDocumentos2", "/TFG V0.01;component/Recursos/Iconos/documentos.png");
            CambiarIcono("imagenClientes2", "/TFG V0.01;component/Recursos/Iconos/clientes.png");
            CambiarIcono("imagenCasos2", "/TFG V0.01;component/Recursos/Iconos/casos.png");
            CambiarIcono("imagenAyuda2", "/TFG V0.01;component/Recursos/Iconos/ayuda.png");
            CambiarIcono("imagenAgenda2", "/TFG V0.01;component/Recursos/Iconos/agenda.png");
            CambiarIcono("imagenAjustes2", "/TFG V0.01;component/Recursos/Iconos/ajustes.png");
            CambiarIcono("imagenBuscar2", "/TFG V0.01;component/Recursos/Iconos/buscar.png");
        }

        private void CambiarIconosAClaros()
        {
            CambiarIcono("imagenHome2", "/TFG V0.01;component/Recursos/Iconos/home2.png");
            CambiarIcono("imagenDocumentos2", "/TFG V0.01;component/Recursos/Iconos/documentos2.png");
            CambiarIcono("imagenClientes2", "/TFG V0.01;component/Recursos/Iconos/clientes2.png");
            CambiarIcono("imagenCasos2", "/TFG V0.01;component/Recursos/Iconos/casos2.png");
            CambiarIcono("imagenAyuda2", "/TFG V0.01;component/Recursos/Iconos/ayuda2.png");
            CambiarIcono("imagenAgenda2", "/TFG V0.01;component/Recursos/Iconos/agenda2.png");
            CambiarIcono("imagenAjustes2", "/TFG V0.01;component/Recursos/Iconos/ajustes2.png");
            CambiarIcono("imagenBuscar2", "/TFG V0.01;component/Recursos/Iconos/buscar2.png");
        }

        private void CambiarIcono(string nombreElemento, string rutaIcono)
        {
            var imagen = this.FindName(nombreElemento) as Image;
            if (imagen != null)
            {
                imagen.Source = new BitmapImage(new Uri(rutaIcono, UriKind.Relative));
            }
        }

        private void CambiarTextosNegro()
        {
            CambiarColorTexto("btnAgenda", Colors.Black);
            CambiarColorTexto("btnAjustes", Colors.Black);
            CambiarColorTexto("btnAyuda", Colors.Black);
            CambiarColorTexto("btnCasos", Colors.Black);
            CambiarColorTexto("btnClientes", Colors.Black);
            CambiarColorTexto("btnDocumentos", Colors.Black);
            CambiarColorTexto("btnHome", Colors.Black);
            CambiarColorTexto("btnBuscar", Colors.Black);
        }

        private void CambiarTextosBlanco()
        {
            CambiarColorTexto("btnAgenda", Colors.White);
            CambiarColorTexto("btnAjustes", Colors.White);
            CambiarColorTexto("btnAyuda", Colors.White);
            CambiarColorTexto("btnCasos", Colors.White);
            CambiarColorTexto("btnClientes", Colors.White);
            CambiarColorTexto("btnDocumentos", Colors.White);
            CambiarColorTexto("btnHome", Colors.White);
            CambiarColorTexto("btnBuscar", Colors.White);
        }

        private void CambiarColorTexto(string nombreElemento, Color color)
        {
            var boton = this.FindName(nombreElemento) as Button;
            if (boton != null)
            {
                boton.Foreground = new SolidColorBrush(color);
            }
        }
        #endregion

        #region navbar animacion
        private void Menu_MouseEnter(object sender, MouseEventArgs e)
        {
            inicio.Visibility = Visibility.Visible;
            buscar.Visibility = Visibility.Visible;
            documentos.Visibility = Visibility.Visible;
            clientes.Visibility = Visibility.Visible;
            casos.Visibility = Visibility.Visible;
            agenda.Visibility = Visibility.Visible;
            ajustes.Visibility = Visibility.Visible;
        }

        private void Menu_MouseLeave(object sender, MouseEventArgs e)
        {
            inicio.Visibility = Visibility.Collapsed;
            buscar.Visibility = Visibility.Collapsed;
            documentos.Visibility = Visibility.Collapsed;
            clientes.Visibility = Visibility.Collapsed;
            casos.Visibility = Visibility.Collapsed;
            agenda.Visibility = Visibility.Collapsed;
            ajustes.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region boton cambiar tema
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            // Alternar el estado del tema
            MainWindow.isDarkTheme = !MainWindow.isDarkTheme;

            // Obtener el botón y el icono
            var button = sender as Button;
            var icon = button?.Template.FindName("ThemeIcon", button) as Image;

            if (MainWindow.isDarkTheme)
            {
                // Cambiar a modo oscuro
                if (icon != null)
                {
                    icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/sol.png", UriKind.Relative));
                }

                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(
                    @"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\oscuro\main.png") as ImageSource;

                CambiarIconosAClaros();
                CambiarTextosBlanco();
                backgroun_menu.Background = new SolidColorBrush(Color.FromArgb(48, 255, 255, 255)); // Fondo semitransparente
            }
            else
            {
                // Cambiar a modo claro
                if (icon != null)
                {
                    icon.Source = new BitmapImage(new Uri("/TFG V0.01;component/Recursos/Iconos/luna.png", UriKind.Relative));
                }

                backgroundFondo.ImageSource = new ImageSourceConverter().ConvertFromString(
                    @"C:\Users\Harvie\Documents\TFG\V 0.1\TFG\TFG V0.01\Recursos\Background\claro\main.png") as ImageSource;

                CambiarIconosAOscuros();
                CambiarTextosNegro();
                backgroun_menu.Background = new SolidColorBrush(Color.FromArgb(48, 128, 128, 128)); // Gris semitransparente
            }
        }

        #endregion

        #region Control de ventana sin bordes
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (WindowState == WindowState.Maximized)
                    WindowState = WindowState.Normal;
                else
                    WindowState = WindowState.Maximized;
            }
            else
            {
                this.DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Navbar botones
        private void irHome(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void irJurisprudencia(object sender, RoutedEventArgs e)
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
            Clientes clientes = new Clientes();
            clientes.Show();
            this.Close();
        }

        private void irCasos(object sender, RoutedEventArgs e)
        {
            Casos casos = new Casos();
            casos.Show();
            this.Close();
        }

        private void irAyuda(object sender, RoutedEventArgs e)
        {
            Ayuda ayuda = new Ayuda();
            ayuda.Show();
            this.Close();
        }

        private void irAgenda(object sender, RoutedEventArgs e)
        {
            Agenda agenda = new Agenda();
            agenda.Show();
            this.Close();
        }

        private void irAjustes(object sender, RoutedEventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
            this.Close();
        }
        #endregion

        #region Animaciones
        private void InitializeAnimations()
        {
            // Animación de entrada con fade
            fadeInStoryboard = new Storyboard();
            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            Storyboard.SetTarget(fadeIn, this);
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity"));
            fadeInStoryboard.Children.Add(fadeIn);

            // Animación de shake para error
            shakeStoryboard = new Storyboard();
            DoubleAnimation shakeAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(3),
                Duration = TimeSpan.FromSeconds(0.05)
            };

            shakeStoryboard.Children.Add(shakeAnimation);
        }

        private void BeginFadeInAnimation()
        {
            this.Opacity = 0;
            fadeInStoryboard.Begin();
        }

        private void ShakeElement(FrameworkElement element)
        {
            TranslateTransform trans = new TranslateTransform();
            element.RenderTransform = trans;

            DoubleAnimation anim = new DoubleAnimation
            {
                From = 0,
                To = 5,
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(3),
                Duration = TimeSpan.FromSeconds(0.05)
            };

            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
        #endregion


        #region Carga de datos dashboard

        private void cargarDatos()
        {
            cargarCasosActivos();
            cargarScoreCasos();

            cargarClientes();
            //cargarScoreClientes();

            cargarDocumentos();
            cargarScoreDocumentos();

            cargarEventosDeHoy();

            cargarMesCalendario();
        }

        private void cargarCasosActivos()
        {
            using (var context = new TfgContext())
            {
                var estadosActivos = new[] { 1, 2, 4, 5 }; // Cambiado a int[]
                var totalCasosActivos = context.Casos
                    .Count(c => estadosActivos.Contains(c.IdEstado));

                num_casosActivos.Text = totalCasosActivos.ToString();
            }
        }

        private void cargarScoreCasos()
        {
            using (var context = new TfgContext())
            {
                var inicioMesAnterior = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                var finMesAnterior = inicioMesAnterior.AddMonths(1).AddDays(-1);

                var casosMesAnterior = context.Casos
                    .Count(c => c.FechaInicio >= inicioMesAnterior && c.FechaInicio <= finMesAnterior);

                var inicioMesActual = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
                var finMesActual = inicioMesActual.AddMonths(1).AddDays(-1);

                var casosMesActual = context.Casos
                    .Count(c => c.FechaInicio >= inicioMesActual && c.FechaInicio <= finMesActual);

                int resultado = -casosMesAnterior + casosMesActual;

                if (resultado > 0)
                {
                    score_casosActivos.Text = "+" + resultado.ToString();
                }
                else if (resultado < 0)
                {
                    score_casosActivos.Text = resultado.ToString();
                }
                else
                {
                    score_casosActivos.Text = "0";
                }
            }
        }

        private void cargarClientes()
        {
            using (var context = new TfgContext())
            {
                var totalClientes = context.Clientes.Count();
                numClientes.Text = totalClientes.ToString();
            }
        }

        private void cargarScoreClientes()
        {
            //clientes nuevos respecto al mes anterior
            using (var context = new TfgContext())
            {
                var inicioMesAnterior = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                var finMesAnterior = inicioMesAnterior.AddMonths(1).AddDays(-1);
                var clientesMesAnterior = context.Clientes
                    .Count(c => c.FechaContrato >= inicioMesAnterior && c.FechaContrato <= finMesAnterior);
                var inicioMesActual = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
                var finMesActual = inicioMesActual.AddMonths(1).AddDays(-1);
                var clientesMesActual = context.Clientes
                    .Count(c => c.FechaContrato >= inicioMesActual && c.FechaContrato <= finMesActual);
                int resultado = -clientesMesAnterior + clientesMesActual;
                if (resultado > 0)
                {
                    newClientes.Text = "+" + resultado.ToString();
                }
                else if (resultado < 0)
                {
                    newClientes.Text = "-" + resultado.ToString();
                }
                else
                {
                    newClientes.Text = "0";
                }
            }
        }

        private void cargarDocumentos()
        {
            using (var context = new TfgContext())
            {
                var totalDocumentos = context.Documentos.Count();
                TotalDocumentos.Text = totalDocumentos.ToString();
            }
        }

        private void cargarScoreDocumentos()
        {
            using (var context = new TfgContext())
            {
                var inicioMesAnterior = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                var finMesAnterior = inicioMesAnterior.AddMonths(1).AddDays(-1);
                var documentosMesAnterior = context.Documentos
                    .Count(c => c.FechaSubid >= inicioMesAnterior && c.FechaSubid <= finMesAnterior);
                var inicioMesActual = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);
                var finMesActual = inicioMesActual.AddMonths(1).AddDays(-1);
                var documentosMesActual = context.Documentos
                    .Count(c => c.FechaSubid >= inicioMesActual && c.FechaSubid <= finMesActual);
                int resultado = -documentosMesAnterior + documentosMesActual;
                if (resultado > 0)
                {
                    ScoreDoc.Text = "+" + resultado.ToString();
                }
                else if (resultado < 0)
                {
                    ScoreDoc.Text = resultado.ToString();
                }
                else
                {
                    ScoreDoc.Text = "0";
                }
            }
        }

        private void cargarEventosDeHoy()
        {
            using (var context = new TfgContext())
            {
                var fechaHoy = DateOnly.FromDateTime(DateTime.Now);

                // Contar las tareas cuya FechaFin coincide con la fecha actual.
                var cantidad = context.Tareas.Count(e => e.FechaFin == fechaHoy);

                eventHoy.Text = cantidad.ToString();
            }
        }

        private void cargarMesCalendario()
        {
            var fechaHoy = DateOnly.FromDateTime(DateTime.Now);
            var mes = fechaHoy.Month;
            var anio = fechaHoy.Year;

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            var mesText = meses[mes - 1];

            mesCalendario.Text = mesText + " " + anio.ToString();
        }

        private void mesAnterior(object sender, RoutedEventArgs e)
        {
            var mes = fechaActual.Month - 1;
            var anio = fechaActual.Year;

            if (mes < 1)
            {
                mes = 12;
                anio--;
            }

            fechaActual = new DateOnly(anio, mes, 1);

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            var mesText = meses[mes - 1];

            mesCalendario.Text = mesText + " " + anio.ToString();
        }

        private void mesSiguiente(object sender, RoutedEventArgs e)
        {
            var mes = fechaActual.Month + 1;
            var anio = fechaActual.Year;

            if (mes > 12)
            {
                mes = 1;
                anio++;
            }

            fechaActual = new DateOnly(anio, mes, 1);

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            var mesText = meses[mes - 1];

            mesCalendario.Text = mesText + " " + anio.ToString();
        }





        #endregion

    }
}
