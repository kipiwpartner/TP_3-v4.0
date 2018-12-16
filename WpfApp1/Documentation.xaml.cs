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
using System.Windows.Xps.Packaging;


namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Documentation.xaml
    /// </summary>
    public partial class Documentation : Window
    {
        public Documentation()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            String myXpsFile = @"documentation.xps";

            XpsDocument myXpsDocument = new XpsDocument(myXpsFile, System.IO.FileAccess.Read);
            monviewer.Document = myXpsDocument.GetFixedDocumentSequence();
        }
    }
}
