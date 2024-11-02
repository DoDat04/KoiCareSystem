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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.FoodCalc
{
    /// <summary>
    /// Interaction logic for FoodPage.xaml
    /// </summary>
    /// 
    public partial class FoodPage : Page
    {
        public string MemberIdText { get; set; }

        public FoodPage()
        {
            InitializeComponent();
            var session = UserSession.GetInstance();
            MemberIdText = $"Member ID: {session.MemberId}";
            this.DataContext = this;
        }
    }
}
