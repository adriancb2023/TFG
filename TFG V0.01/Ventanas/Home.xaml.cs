using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using TFG_V0._01.Modelo;
using TFG_V0._01.Ventanas.SubVentanas;

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
        private string mesText;
        private string anio;
        #endregion

        #region Inicializacion
        public Home()
        {
            InitializeComponent();
            InitializeAnimations();
            AplicarModoSistema();

            cargarHome();
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

        #region Cargar Elementos Home
        private void cargarHome()
        {
            diaSemana();
            cargarCasosActivos();
            cargarScoreCasos();
            cargarClientes();
            //cargarScoreClientes();
            cargarDocumentos();
            cargarScoreDocumentos();
            cargarEventosDeHoy();
            cargarMesCalendario();
            cargarTareasCalendario(mesText, anio);
            cargarTareasPendientes();

        }
        #endregion

        #region Carga de datos header
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
        #endregion

        #region 🔧 calendario
        private void diaSemana()
        {
            var diaSemana = fechaActual.DayOfWeek;
            switch (diaSemana)
            {
                case DayOfWeek.Monday:
                    lun.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case DayOfWeek.Tuesday:
                    mart.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case DayOfWeek.Wednesday:
                    mier.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case DayOfWeek.Thursday:
                    juev.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case DayOfWeek.Friday:
                    vier.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case DayOfWeek.Saturday:
                    sab.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case DayOfWeek.Sunday:
                    dom.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                default:

                    break;
            }
        }

        private void cargarMesCalendario()
        {
            var fechaHoy = DateOnly.FromDateTime(DateTime.Now);
            fechaActual = fechaHoy;

            var mes = fechaHoy.Month;
            anio = fechaHoy.Year.ToString();

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            mesText = meses[mes - 1];

            mesCalendario.Text = mesText + " " + anio;
            cargarTareasCalendario(mesText, anio);
        }

        private void mesAnterior(object sender, RoutedEventArgs e)
        {
            var mes = fechaActual.Month - 1;
            var anioInt = fechaActual.Year;

            if (mes < 1)
            {
                mes = 12;
                anioInt--;
            }

            fechaActual = new DateOnly(anioInt, mes, 1);

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            mesText = meses[mes - 1];

            anio = anioInt.ToString();
            mesCalendario.Text = mesText + " " + anio;

            cargarTareasCalendario(mesText, anio);
        }

        private void mesSiguiente(object sender, RoutedEventArgs e)
        {
            var mes = fechaActual.Month + 1;
            var anioInt = fechaActual.Year;

            if (mes > 12)
            {
                mes = 1;
                anioInt++;
            }

            fechaActual = new DateOnly(anioInt, mes, 1);

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            mesText = meses[mes - 1];

            anio = anioInt.ToString();
            mesCalendario.Text = mesText + " " + anio;

            cargarTareasCalendario(mesText, anio);
        }

        private void cargarTareasCalendario(string mesText, string anio)
        {
            var colores = new List<string>
            {
                "#FF5722", "#F4511E", "#E64A19", "#D84315",
                "#009688", "#26A69A", "#00796B", "#004D40",
                "#3F51B5", "#5C6BC0", "#3949AB", "#1A237E"
            };

            using (var context = new TfgContext())
            {
                int mes = Array.IndexOf(new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" }, mesText) + 1;
                int anioInt = int.Parse(anio);

                var tareas = context.Tareas.Where(t => t.FechaFin.Month == mes && t.FechaFin.Year == anioInt && t.Estado != "finalizado").ToList();


                var stackPanel = this.FindName("PanelEventos") as StackPanel;

                if (stackPanel != null)
                {
                    stackPanel.Children.Clear();

                    var random = new Random();
                    int offset = random.Next(colores.Count);

                    for (int i = 0; i < tareas.Count; i++)
                    {
                        var tarea = tareas[i];
                        string colorHex = colores[(i + offset) % colores.Count];

                        var border = new Border
                        {
                            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex)),
                            CornerRadius = new CornerRadius(10),
                            Padding = new Thickness(10),
                            Margin = new Thickness(0, 5, 0, 5)
                        };

                        var innerGrid = new Grid();
                        innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                        var fechaBorder = new Border
                        {
                            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#30FFFFFF")),
                            CornerRadius = new CornerRadius(5),
                            Padding = new Thickness(8, 5, 8, 5),
                            Margin = new Thickness(0, 0, 10, 0)
                        };
                        var fechaText = new TextBlock
                        {
                            Text = tarea.FechaFin.Day.ToString(),
                            FontWeight = FontWeights.Bold,
                            Foreground = Brushes.White
                        };
                        fechaBorder.Child = fechaText;

                        var detallesStack = new StackPanel();
                        var tituloText = new TextBlock
                        {
                            Text = tarea.Titulo,
                            FontWeight = FontWeights.SemiBold,
                            Foreground = Brushes.White
                        };
                        var descripcionText = new TextBlock
                        {
                            Text = tarea.Descripcion,
                            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCFFFFFF")),
                            FontSize = 12
                        };

                        detallesStack.Children.Add(tituloText);
                        detallesStack.Children.Add(descripcionText);

                        Grid.SetColumn(fechaBorder, 0);
                        Grid.SetColumn(detallesStack, 1);
                        innerGrid.Children.Add(fechaBorder);
                        innerGrid.Children.Add(detallesStack);

                        border.Child = innerGrid;
                        stackPanel.Children.Add(border);
                    }
                }
            }
        }
        #endregion

        #region Tareas Pendientes proximos 4 dias
        private void cargarTareasPendientes()
        {
            using (var context = new TfgContext())
            {
                DateOnly hoy = DateOnly.FromDateTime(DateTime.Today);
                DateOnly limite = hoy.AddDays(3);

                var tareas = context.Tareas
                    .Where(t => t.FechaFin >= hoy && t.FechaFin <= limite && t.Estado != "finalizado")
                    .OrderBy(t => t.FechaFin)
                    .ToList();

                tareasPanel.Children.Clear(); // Limpia tareas anteriores

                foreach (var tarea in tareas)
                {
                    Border border = new Border
                    {
                        Background = new SolidColorBrush(Color.FromArgb(0x30, 0xFF, 0xFF, 0xFF)),
                        CornerRadius = new CornerRadius(10),
                        Padding = new Thickness(10),
                        Margin = new Thickness(0, 0, 0, 10)
                    };

                    Grid grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    CheckBox checkBox = new CheckBox
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        Margin = new Thickness(0, 0, 10, 0),
                        IsChecked = tarea.Estado == "finalizado"
                    };
                    checkBox.Checked += (s, e) =>
                    {
                        MostrarPopupConfirmacion(tarea.Id, checkBox);
                    };
                    Grid.SetColumn(checkBox, 0);

                    StackPanel stack = new StackPanel();
                    Grid.SetColumn(stack, 1);

                    TextBlock titulo = new TextBlock
                    {
                        Text = tarea.Titulo,
                        Foreground = Brushes.White,
                        TextWrapping = TextWrapping.Wrap
                    };

                    int diasRestantes = (tarea.FechaFin.ToDateTime(TimeOnly.MinValue) - DateTime.Today).Days;
                    string vencimientoTexto = diasRestantes == 0 ? "Vence hoy" : $"Vence en {diasRestantes} días";

                    Brush color;
                    if (diasRestantes == 0)
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5252")); // Rojo
                    else if (diasRestantes <= 2)
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC107")); // Amarillo
                    else
                        color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8BC34A")); // Verde

                    TextBlock fecha = new TextBlock
                    {
                        Text = vencimientoTexto,
                        Foreground = color,
                        FontSize = 12,
                        Margin = new Thickness(0, 5, 0, 0)
                    };

                    stack.Children.Add(titulo);
                    stack.Children.Add(fecha);

                    grid.Children.Add(checkBox);
                    grid.Children.Add(stack);

                    border.Child = grid;

                    tareasPanel.Children.Add(border);
                }
            }
        }

        private void MostrarPopupConfirmacion(int tareaId, CheckBox checkBox)
        {
            Popup popup = new Popup
            {
                Placement = PlacementMode.Center,
                StaysOpen = false,
                AllowsTransparency = true,
                IsOpen = true

            };

            Border border = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x33, 0x33, 0x33)),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(20),
                Width = 300,
                Height = 150
            };

            StackPanel stackPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock textBlock = new TextBlock
            {
                Text = "¿Desea marcar esta tarea como finalizada?",
                Foreground = Brushes.White,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };

            Button btnSi = new Button
            {
                Content = "Sí",
                Width = 100,
                Margin = new Thickness(5),
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x4C, 0xAF, 0x50)), 
                Foreground = Brushes.White
            };

            Button btnNo = new Button
            {
                Content = "No",
                Width = 100,
                Margin = new Thickness(5),
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xF4, 0x43, 0x36)),
                Foreground = Brushes.White
            };

            btnSi.Click += (s, e) =>
            {
                checkTarea(tareaId); 
                popup.IsOpen = false; 
            };

            btnNo.Click += (s, e) =>
            {
                checkBox.IsChecked = false; 
                popup.IsOpen = false; 
            };

            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children = { btnSi, btnNo }
            });

            border.Child = stackPanel;
            popup.Child = border;

            DoubleAnimation fadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            border.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }

        private void checkTarea(int tareaId)
        {
            using (var context = new TfgContext())
            {
                var tarea = context.Tareas.FirstOrDefault(t => t.Id == tareaId);
                if (tarea != null)
                {
                    tarea.Estado = "finalizado";
                    context.SaveChanges();
                }
            }

            cargarTareasPendientes(); // Refresca la lista
        }

        private void nuevaTarea (object sender, RoutedEventArgs e)
        {
            NuevaTarea nuevaTarea = new NuevaTarea();
            nuevaTarea.Show();
        }
        #endregion
    }
}
