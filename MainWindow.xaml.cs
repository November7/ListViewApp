using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System;

namespace ListViewApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                mainList.Items.Clear();
                string filePath = openFileDialog.FileName;
                int selectedFilterIndex = openFileDialog.FilterIndex;
                string delimiter = ";";
                if (selectedFilterIndex == 1)
                {
                    delimiter = ",";
                }
                Encoding encoding = Encoding.UTF8;

                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath,encoding);
                    foreach (var line in lines)
                    {
                        var columns = line.Split(delimiter);


                        mainList.Items.Add(new
                        {
                            m_strPESEL = columns.ElementAtOrDefault(0),
                            m_strName = columns.ElementAtOrDefault(1),
                            m_strSecName = columns.ElementAtOrDefault(2),
                            m_strSurname = columns.ElementAtOrDefault(3)
                        });


                    }
                }
            }

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void RemoveSel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            mainList.Items.Add(new {m_strPESEL = "12312312123",m_strName = "Marcin" });
        }
    }
}