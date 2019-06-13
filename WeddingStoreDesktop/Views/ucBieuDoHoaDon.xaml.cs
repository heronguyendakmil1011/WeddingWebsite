using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ucBieuDoHoaDon.xaml
    /// </summary>
    public partial class ucBieuDoHoaDon : UserControl
    {
        ViewModels.ucBieuDoHoaDonViewModel vm;

        public float rectSpace { get; set; }
        public float rectWidth { get; set; }
        public ucBieuDoHoaDon(string thang)
        {
            InitializeComponent();
            vm = new ViewModels.ucBieuDoHoaDonViewModel(thang);
            DataContext = vm;
        }

        public ucBieuDoHoaDon(string from, string to)
        {
            InitializeComponent();
            vm = new ViewModels.ucBieuDoHoaDonViewModel(from, to);
            DataContext = vm;
        }

        // Define some Property
        // Property for Source for Chart
        private static readonly DependencyProperty dataSource = DependencyProperty.Register("dataSource", typeof(string), typeof(ucBieuDoHoaDon));
        public string DataSource
        {
            get { return (string)this.GetValue(dataSource); }
            set { this.SetValue(dataSource, value); }
        }

        // Chart bars color
        private static readonly DependencyProperty barsColor = DependencyProperty.Register("barsColor", typeof(Brush), typeof(ucBieuDoHoaDon));
        public Brush BarsColor
        {
            get { return (Brush)this.GetValue(barsColor); }
            set { this.SetValue(barsColor, value); }
        }

        // Method for Drawing bar/rectangles
        public void DrawBars()
        {
            float section = 525 / vm.LstHoaDon.Count;
            // space between bars, 20% of section
            rectSpace = (section * 20) / 100;
            // bars Width
            rectWidth = (section * 80) / 100;

            // Actual Drawing
            for (int i = 0; i < vm.LstHoaDon.Count; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = rectWidth;
                rect.Height = vm.LstHoaDon[i].TongTien.Value / 100000;
                rect.Margin = new Thickness(rectSpace, (350 - rect.Height), 0, 25);
                rect.Fill = BarsColor;

                wPanel.Children.Add(rect);

                // Effect of Animation
                DoubleAnimation ani = new DoubleAnimation
                {
                    From = 0,
                    To = vm.LstHoaDon[i].TongTien / 100000,
                    Duration = new TimeSpan(0, 0, 2)
                };
                // Add Animation
                ani.FillBehavior = FillBehavior.HoldEnd;
                rect.BeginAnimation(HeightProperty, ani);
                rect.Effect = new DropShadowEffect
                {
                    Color = new Color { A = 1, R = 0, G = 139, B = 139 },
                    Direction = 45,
                    ShadowDepth = 10,
                    Opacity = 0.8,
                    BlurRadius = 10
                };

                // Add lable
                Label lable = new Label();
                lable.Content = vm.LstHoaDon[i].MaHD;
                lable.Background = Brushes.Transparent;
                lable.Foreground = Brushes.SkyBlue;
                lable.Margin = new Thickness(rectSpace, 320, 0, 0);
                lable.FontSize = 20 - vm.LstHoaDon[i].MaHD.Length;
                lable.Width = rect.Width;
                lable.HorizontalAlignment = HorizontalAlignment.Center;
                lable.Height = 30;
                stkPanel.Children.Add(lable);

                // Show value on mouse hover
                rect.MouseEnter += rect_MouseEnter;
            }
            // Add side line/Scale Lines, each separated by value of 20
            for (int i = 20; i < wPanel.Height; i += 20)
            {
                Line line = new Line();
                line.X1 = 5;
                line.Y1 = i;
                line.X2 = 25;
                line.Y2 = line.Y1;
                line.Stroke = Brushes.SkyBlue;
                line.StrokeThickness = 1;
                gridThongKe.Children.Add(line);
            }
        }

        void DrawBarsCustom()
        {
            float section = 525 / vm.dicHoaDon.Count * 3;
            // space between bars, 20% of section
            rectSpace = (section * 20) / 100;
            // bars Width
            rectWidth = (section * 80) / 100;

            for (int i = 0; i < vm.dicHoaDon.Count; i++)
            {
                Rectangle recthd = new Rectangle();
                recthd.Width = 50;
                recthd.Height = vm.dicHoaDon[vm.dicHoaDon.Keys.ElementAt(i)] / 100000;
                recthd.Margin = new Thickness(10, (350 - recthd.Height), 0, 25);
                recthd.Fill = Brushes.Aqua;

                wPanel.Children.Add(recthd);

                Rectangle rectVL = new Rectangle();
                rectVL.Width = 50;
                rectVL.Height = vm.dicVatLieuNhap[vm.dicVatLieuNhap.Keys.ElementAt(i)] / 100000;
                rectVL.Margin = new Thickness(10, (350 - rectVL.Height), 0, 25);
                rectVL.Fill = Brushes.Red;

                wPanel.Children.Add(rectVL);

                Rectangle rectLuong = new Rectangle();
                rectLuong.Width = 50;
                rectLuong.Height = vm.dicLuongNhanVien[vm.dicLuongNhanVien.Keys.ElementAt(i)] / 100000;
                rectLuong.Margin = new Thickness(10, (350 - rectLuong.Height), 0, 25);
                rectLuong.Fill = Brushes.Blue;

                wPanel.Children.Add(rectLuong);
            }

            // Add side line/Scale Lines, each separated by value of 20
            for (int i = 20; i < wPanel.Height; i += 20)
            {
                Line line = new Line();
                line.X1 = 5;
                line.Y1 = i;
                line.X2 = 25;
                line.Y2 = line.Y1;
                line.Stroke = Brushes.SkyBlue;
                line.StrokeThickness = 1;
                gridThongKe.Children.Add(line);
            }
        }

        private void rect_MouseEnter(object sender, MouseEventArgs e)
        {
            // Add animation
            DoubleAnimation anim = new DoubleAnimation
            {
                From = 0.7,
                To = 1,
                Duration = new TimeSpan(0, 0, 1)
            };
            ((Rectangle)sender).BeginAnimation(OpacityProperty, anim);
            popWindow.ClearValue(Popup.IsOpenProperty);
            popWindow.IsOpen = true;
            lblPopup.Content = "Value: " + ((Rectangle)sender).Height.ToString();
        }

        private void ucBieuDoHoaDon_Loaded(object sender, RoutedEventArgs e)
        {
            DrawBarsCustom();
        }
    }
}
