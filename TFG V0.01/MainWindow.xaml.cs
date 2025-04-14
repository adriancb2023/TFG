using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using TFG_V0._01.Ventanas;

namespace TFG_V0._01
{
    public partial class MainWindow : Window
    {
        #region Campos
        private DispatcherTimer progressTimer = null!;
        private DateTime startTime;
        private readonly TimeSpan duration = TimeSpan.FromSeconds(0.5);
        public static bool isDarkTheme;
        #endregion

        #region Constructor
        public MainWindow()
        {
            DetectarModo();
            InitializeComponent();
            StartLoadingAnimation();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion

        #region Animacion de carga

            #region Animación de Carga
            private void StartLoadingAnimation()
            {
                startTime = DateTime.Now;
                progressTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(50)
                };
                progressTimer.Tick += UpdateProgressArc!;
                progressTimer.Start();
            }
            #endregion

            #region Actualización del Progreso
            private void UpdateProgressArc(object? sender, EventArgs e)
            {
                TimeSpan elapsed = DateTime.Now - startTime;
                double progress = Math.Min(elapsed.TotalMilliseconds / duration.TotalMilliseconds, 1.0);
                double angle = progress * 360;

                UpdateArc(angle);

                if (progress >= 1.0)
                {
                    progressTimer.Stop();
                    OpenNewWindow();
                }
            }
            #endregion

            #region Actualización del Arco
            private void UpdateArc(double angle)
            {
                double radius = 50;
                double centerX = 60;
                double centerY = 60;

                double radians = (Math.PI / 180) * (angle - 90);
                double x = centerX + radius * Math.Cos(radians);
                double y = centerY + radius * Math.Sin(radians);

                // Asegúrate de que ProgressArc esté definido en tu XAML
                ProgressArc.Point = new Point(x, y);
                ProgressArc.IsLargeArc = angle > 180;
            }
            #endregion

        #endregion

        #region Abrir Nueva Ventana
        private void OpenNewWindow()
        {
            Login login = new Login
            {
               // Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            login.Show();
            this.Close();
        }
        #endregion

        #region detectar modo claro/oscuro del sistema
        private void DetectarModo()
        {
            var theme = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", 1);
            isDarkTheme = theme != null && (int)theme == 0;
        }
        #endregion
    }
}
