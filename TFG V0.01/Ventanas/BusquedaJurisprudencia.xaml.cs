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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using JurisprudenciaApi.Models;
using JurisprudenciaApi.Controllers;

namespace TFG_V0._01.Ventanas
{
    /// <summary>
    /// Lógica de interacción para BusquedaJurisprudencia.xaml
    /// </summary>
    public partial class BusquedaJurisprudencia : Window
    {
        #region variables animacion
        private Storyboard fadeInStoryboard;
        private Storyboard shakeStoryboard;
        #endregion

        #region Variables API
        private static readonly HttpClient client = new HttpClient();
        private const string ApiBaseUrl = "http://localhost:5146";
        private static readonly Dictionary<string, string> TipoOrganoMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Tribunal Supremo", "11|12|13|14|15|16" },
            { "Tribunal Supremo. Sala de lo Civil", "11" },
            { "Tribunal Supremo. Sala de lo Penal", "12" },
            { "Tribunal Supremo. Sala de lo Contencioso", "13" },
            { "Tribunal Supremo. Sala de lo Social", "14" },
            { "Tribunal Supremo. Sala de lo Militar", "15" },
            { "Tribunal Supremo. Sala de lo Especial", "16" },
            { "Audiencia Nacional", "22|2264|23|24|25|26|27|28|29" },
            { "Audiencia Nacional. Sala de lo Penal", "22" },
            { "Sala de Apelación de la Audiencia Nacional", "2264" },
            { "Audiencia Nacional. Sala de lo Contencioso", "23" },
            { "Audiencia Nacional. Sala de lo Social", "24" },
            { "Audiencia Nacional. Juzgados Centrales de Instrucción", "27" },
            { "Audiencia Nacional. Juzgado Central de Menores", "26" },
            { "Audiencia Nacional. Juzgado Central de Vigilancia Penitenciaria", "25" },
            { "Audiencia Nacional. Juzgados Centrales de lo Contencioso", "29" },
            { "Audiencia Nacional. Juzgados Centrales de lo Penal", "28" },
            { "Tribunal Superior de Justicia", "31|31201202|33|34" },
            { "Tribunal Superior de Justicia. Sala de lo Civil y Penal", "31" },
            { "Sección de Apelación Penal. TSJ Sala de lo Civil y Penal", "31201202" },
            { "Tribunal Superior de Justicia. Sala de lo Contencioso", "33" },
            { "Tribunal Superior de Justicia. Sala de lo Social", "34" },
            { "Audiencia Provincial", "37" },
            { "Audiencia Provincial. Tribunal Jurado", "38" },
            { "Tribunal de Marca de la UE", "1001" },
            { "Juzgado de Primera Instancia", "42" },
            { "Juzgado de Instrucción", "43" },
            { "Juzgado de lo Contencioso Administrativo", "45" },
            { "Juzgado de Menores", "53" },
            { "Juzgado de Primera Instancia e Instrucción", "41" },
            { "Juzgado de lo Mercantil", "47" },
            { "Juzgados de Marca de la UE", "1002" },
            { "Juzgado de lo Penal", "51" },
            { "Juzgado de lo Social", "44" },
            { "Juzgado de Vigilancia Penitenciaria", "52" },
            { "Juzgado de Violencia sobre la Mujer", "48" },
            { "Tribunal Militar Territorial", "83" },
            { "Tribunal Militar Central", "85" },
            { "Consejo Supremo de Justicia Militar", "75" },
            { "Audiencia Territorial", "36" }
        };
        public BusquedaJurisprudencia()
        {
            InitializeComponent();
            InitializeAnimations();
            AplicarModoSistema();

            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            // Cargar datos iniciales
            _ = CargarDatosInicialesAsync();
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
                    "pack://application:,,,/TFG V0.01;component/Recursos/Background/oscuro/main.png") as ImageSource;

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
                    "pack://application:,,,/TFG V0.01;component/Recursos/Background/claro/main.png") as ImageSource;

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

        private void LegislacionTextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var legislacionWindow = new Legislacion();
            legislacionWindow.ShowDialog();
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
            MessageBox.Show("Funcionalidad 'Documentos' no implementada.");
        }

        private void irClientes(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad 'Clientes' no implementada.");
        }

        private void irCasos(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad 'Casos' no implementada.");
        }

        private void irAyuda(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad 'Ayuda' no implementada.");
        }

        private void irAgenda(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad 'Agenda' no implementada.");
        }

        private void irAjustes(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad 'Ajustes' no implementada.");
        }
        #endregion

        #region API Interaction
        private async void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JurisprudenciaSearchParameters parameters = GetSearchParametersFromUI();
                string jsonParameters = JsonSerializer.Serialize(parameters, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
                var content = new StringContent(jsonParameters, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/api/Jurisprudencia/search", content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<JurisprudenciaResult>? results = JsonSerializer.Deserialize<List<JurisprudenciaResult>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    DisplayResults(results);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    string errorMsg = $"Error al buscar: {response.StatusCode}\n{errorContent}";
                    MessageBox.Show(errorMsg, "Error API", MessageBoxButton.OK, MessageBoxImage.Error);
                    DisplayResults(null);
                }
            }
            catch (HttpRequestException httpEx)
            {
                string errorMsg = "Error de conexión: Verifique que la API (" + ApiBaseUrl + ") esté ejecutándose.\n" + httpEx.Message;
                MessageBox.Show(errorMsg, "Error Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                DisplayResults(null);
            }
            catch (JsonException jsonEx)
            {
                string errorMsg = "Error al procesar la respuesta de la API:\n" + jsonEx.Message;
                MessageBox.Show(errorMsg, "Error Deserialización", MessageBoxButton.OK, MessageBoxImage.Error);
                DisplayResults(null);
            }
            catch (Exception ex)
            {
                string errorMsg = "Ocurrió un error inesperado:\n" + ex.Message;
                MessageBox.Show(errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DisplayResults(null);
            }
            finally
            {
            }
        }

        private JurisprudenciaSearchParameters GetSearchParametersFromUI()
        {
            var parameters = new JurisprudenciaSearchParameters();

            parameters.Jurisdiccion = JurisdiccionComboBox.Text;
            parameters.Roj = NumeroRojTextBox.Text;
            parameters.Ecli = EcliTextBox.Text;
            parameters.NumeroResolucion = NumeroResolucionTextBox.Text;
            parameters.NumeroRecurso = NumeroRecursoTextBox.Text;
            parameters.Ponente = PonenteTextBox.Text;
            parameters.Seccion = SeccionTextBox.Text;
            parameters.Legislacion = LegislacionTextBox.Text;
            parameters.FechaDesde = FechaDesdeDatePicker.SelectedDate;
            parameters.FechaHasta = FechaHastaDatePicker.SelectedDate;

            if (JurisdiccionComboBox.SelectedItem is ComboBoxItem jItem && jItem.Content != null)
            {
                string? jurisdiccionValue = jItem.Content.ToString();
                parameters.Jurisdiccion = (jurisdiccionValue == "Todos" || string.IsNullOrEmpty(jurisdiccionValue)) ? null : jurisdiccionValue;
            }
            if (IdiomaComboBox.SelectedItem is ComboBoxItem iItem && iItem.Content != null)
            {
                string? idiomaValue = iItem.Content.ToString();
                parameters.Idioma = (idiomaValue == "Todos" || string.IsNullOrEmpty(idiomaValue)) ? null : idiomaValue;
            }

            parameters.TiposResolucion = GetSelectedCheckboxContent(TipoResolucionComboBox);
            parameters.Localizaciones = GetSelectedCheckboxContent(LocalizacionComboBox);

            List<string> selectedOrganoNames = GetSelectedCheckboxContent(OrganoJudicialComboBox);
            var organoCodes = new List<string>();
            if (selectedOrganoNames != null)
            {
                foreach (string name in selectedOrganoNames)
                {
                    if (TipoOrganoMap.TryGetValue(name, out string? code))
                    {
                        organoCodes.AddRange(code.Split('|', StringSplitOptions.RemoveEmptyEntries));
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Organo Judicial '{name}' not found in map.");
                    }
                }
                parameters.OrganosJudiciales = organoCodes.Distinct().ToList();
            }

            return parameters;
        }

        private List<string> GetSelectedCheckboxContent(ItemsControl itemsControl)
        {
            var selectedContent = new List<string>();
            if (itemsControl == null) return selectedContent;
            FindSelectedCheckboxesRecursive(itemsControl, selectedContent);
            return selectedContent;
        }

        private void FindSelectedCheckboxesRecursive(DependencyObject parent, List<string> selectedContent)
        {
            if (parent == null) return;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is CheckBox checkBox && checkBox.IsChecked == true && checkBox.Content != null)
                {
                    if(checkBox.Content is string contentString)
                    {
                        selectedContent.Add(contentString);
                    } else {
                         selectedContent.Add(checkBox.Content.ToString() ?? string.Empty);
                    }
                }

                FindSelectedCheckboxesRecursive(child, selectedContent);

                if (child is ItemsPresenter itemsPresenter && VisualTreeHelper.GetChildrenCount(itemsPresenter) > 0)
                {
                    DependencyObject? itemsHostPanel = VisualTreeHelper.GetChild(itemsPresenter, 0);
                    FindSelectedCheckboxesRecursive(itemsHostPanel, selectedContent);
                }
                else if (child is ContentPresenter contentPresenter && VisualTreeHelper.GetChildrenCount(contentPresenter) > 0)
                {
                    DependencyObject? contentChild = VisualTreeHelper.GetChild(contentPresenter, 0);
                    FindSelectedCheckboxesRecursive(contentChild, selectedContent);
                }
            }
        }

        private void DisplayResults(List<JurisprudenciaResult>? results)
        {
            var resultadosTextBlock = this.FindName("ResultadosTextBlock") as TextBlock;
            if (resultadosTextBlock == null)
            {
                var resultsBorder = this.FindName("ResultadosBorder") as Border;
                if (resultsBorder?.Child is StackPanel sp && sp.Children.Count > 1 && sp.Children[1] is TextBlock tb)
                {
                    resultadosTextBlock = tb;
                }
            }

            if (resultadosTextBlock == null)
            {
                MessageBox.Show("No se encontró el área de resultados.", "Error UI");
                return;
            }

            if (results == null || !results.Any())
            {
                resultadosTextBlock.Text = "(No se encontraron resultados o hubo un error.)";
                resultadosTextBlock.FontStyle = FontStyles.Italic;
            }
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Resultados encontrados: {results.Count}");
                sb.AppendLine("--------------------");
                foreach (var result in results.Take(20))
                {
                    sb.AppendLine($"ROJ: {result.Roj ?? "N/A"}");
                    sb.AppendLine($"ECLI: {result.Ecli ?? "N/A"}");
                    sb.AppendLine($"Fecha: {result.FechaResolucion ?? "N/A"}");
                    sb.AppendLine($"Órgano: {result.OrganoJudicial ?? "N/A"}");
                    sb.AppendLine($"Tipo: {result.TipoResolucion ?? "N/A"}");
                    sb.AppendLine($"Resumen: {result.Resumen?.Substring(0, Math.Min(result.Resumen.Length, 150)) ?? "N/A"}...");
                    sb.AppendLine($"URL: {result.UrlDocumento ?? "N/A"}");
                    sb.AppendLine("---");
                }
                resultadosTextBlock.Text = sb.ToString();
                resultadosTextBlock.FontStyle = FontStyles.Normal;
            }
        }
        private async Task CargarDatosInicialesAsync()
        {
            try
            {
                // Llamada a la API para obtener los datos iniciales
                HttpResponseMessage response = await client.GetAsync($"{ApiBaseUrl}/api/Jurisprudencia/initialData");
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var initialData = JsonSerializer.Deserialize<InitialDataResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Asignar los datos a los ComboBox
                JurisdiccionComboBox.ItemsSource = initialData.Jurisdicciones;
                TipoResolucionComboBox.ItemsSource = initialData.TiposResolucion;
                OrganoJudicialComboBox.ItemsSource = initialData.OrganosJudiciales;
                LocalizacionComboBox.ItemsSource = initialData.Localizaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos iniciales: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
