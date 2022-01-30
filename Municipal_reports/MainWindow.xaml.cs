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
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

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

            SeriesCollection = new SeriesCollection
                {
                    new PieSeries
                {
                    Title = "Municipio",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(0) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Area no municipalizada",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(0) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Isla",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(0) },
                    DataLabels = true
                }

                };

            DataContext = this;

        }

        public SeriesCollection SeriesCollection { get; set; }

        private void R2Tab_Clicked(object sender, MouseButtonEventArgs e)
        {
            
            if (municipalities.Count != 0) {
                Console.Out.WriteLine("HELLOOOO");

                int mCounter = 0;
                int iCounter = 0;
                int ACounter = 0;

                foreach (Municipality m in municipalities)
                {
                    if (m.type.Equals("Municipio"))
                    {
                        mCounter++;
                    }
                    else if (m.type.Equals("Isla"))
                    {
                        iCounter++;
                    }
                    else
                    {
                        ACounter++;
                    }

                }
                pieChart.Series.Clear();
                SeriesCollection seriesCollectionUpdated = new SeriesCollection 
                {
                    new PieSeries
                {
                    Title = "Municipio",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(mCounter) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Area no municipalizada",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(iCounter) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Isla",
                    Values = new ChartValues<ObservableValue>{ new ObservableValue(ACounter) },
                    DataLabels = true
                }

                };

                pieChart.Series = seriesCollectionUpdated;


            }
            
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

        private void letterSelected(object sender, SelectionChangedEventArgs e)
        {
            char selectedLetter = comboBoxLetters.SelectedItem.ToString()[0];

            loadTableTwo(filteredMunicipalities(selectedLetter));

        }

        private List<Municipality> filteredMunicipalities(char letter)
        {
            List<Municipality> filteredMunicipalities = new List<Municipality>();

            foreach (Municipality m in municipalities)
            {
                if (m.depName[0].Equals(char.ToUpperInvariant(letter)))
                {
                    filteredMunicipalities.Add(m);

                }

            }

            return filteredMunicipalities;
        }

        private void loadTableTwo(List<Municipality> filteredMunicipalities)
        {
            dataGridTwo.Items.Clear();
            foreach (Municipality m in filteredMunicipalities)
            {
                dataGridTwo.Items.Add(m);

            }

        }

    }
}
