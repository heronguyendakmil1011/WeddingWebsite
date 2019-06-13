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
using System.Windows.Navigation;
using System.Windows.Shapes;

using WeddingStoreDesktop.ViewModels;
using WeddingStoreDesktop.Interfaces.Dialog;

namespace WeddingStoreDesktop.Views
{
    /// <summary>
    /// Interaction logic for ModifyThongTin.xaml
    /// </summary>
    public partial class ModifyThongTin : Window, IDialog
    {
        //ModifyThongTinViewModel vm;
        public ModifyThongTin()
        {
            InitializeComponent();
        }

        private void modifyThongTin_Load(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 1;
            animation.Duration = TimeSpan.FromSeconds(4);
            animation.EasingFunction = new QuarticEase();

            this.BeginAnimation(OpacityProperty,animation);
        }
    }
}
