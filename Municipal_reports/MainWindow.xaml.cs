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
using System.IO;
using Microsoft.Win32;

public class Municipality
{

    public String depCode { get; set; }
    public String code { get; set; }
    public String depName { get; set; }
    public String name { get; set; }
    public String type { get; set; }

}

namespace Municipal_reports
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Municipality> municipalities { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            municipalities = new List<Municipality>();
            Char[] comboBoxItems = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'ñ', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            comboBoxLetters.ItemsSource = comboBoxItems;
        }

        private void dGRep1_SelChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void loadCsv(object sender, RoutedEventArgs e)
        {
            String csvPath;
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.DefaultExt = ".csv";
            openFileDlg.Filter = "Archivos csv (.csv)|*.csv";
            if (openFileDlg.ShowDialog() == true)
            {
                csvPath = openFileDlg.FileName;
                readCsvFile(csvPath);
            }
        }

        private void readCsvFile(String csvPath)
        {
            String[] lines = File.ReadAllLines(csvPath);
            foreach (String line in lines)
            {
                String[] data = line.Split(',');

                int temp;
                if (int.TryParse(data[0], out temp))
                {
                    Municipality m = new Municipality() { depCode = data[0], code = data[1], depName = data[2], name = data[3], type = data[4] };
                    municipalities.Add(m);
                    dataGridOne.Items.Add(m);

                }
            }

        }

        private void letterSelected(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
