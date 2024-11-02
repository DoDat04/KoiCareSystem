using BusinessObject;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TestDatabase.xaml
    /// </summary>
    public partial class TestDatabase : Window
    {
        public TestDatabase()
        {
            InitializeComponent();
            var dbContext = new KoiCareContext();
            var fishList = dbContext.Fish.ToList();
            dgFish.ItemsSource = fishList;
        }
    }
}
