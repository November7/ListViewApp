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

namespace ListViewApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddStudent.xaml
    /// </summary>
    /// 



    public class TextBoxMetadata
    {
        public string Placeholder { get; set; }
        public bool IsRequired { get; set; }
        public bool IsSet { get; set; } = false;
    }

    public partial class AddStudent : Window
    {
        public int m_bCloseMode = 0;
        Color m_warnBgColor = Colors.Red;  
        Color m_warnFgColor = Colors.White;  
        Color m_placeholderFgColor = Colors.LightGray;
        Color m_stdBgColor = Colors.White;
        Color m_stdFgColor = Colors.Black;

        private Dictionary<TextBox, TextBoxMetadata> textBoxDictionary = new Dictionary<TextBox, TextBoxMetadata>();

        public AddStudent()
        {
            InitializeComponent();

            textBoxDictionary[strName] = new TextBoxMetadata {Placeholder = "Imię ucznia", IsRequired = true};
            textBoxDictionary[strSecName] = new TextBoxMetadata {Placeholder = "Drugie imię ucznia", IsRequired = false};
            textBoxDictionary[strSurname] = new TextBoxMetadata { Placeholder = "Nazwisko ucznia", IsRequired = true };
            textBoxDictionary[strPESEL] = new TextBoxMetadata { Placeholder = "Numer PESEL", IsRequired = true };


            foreach (var textBox in textBoxDictionary.Keys)
            {
                setPlaceholder(textBox, textBoxDictionary[textBox].Placeholder);
                textBox.GotFocus += (s, e) =>
                {
                    if (!textBoxDictionary[textBox].IsSet)
                    {
                        textBox.Text = "";
                        textBox.Foreground = new SolidColorBrush(m_stdFgColor);
                        textBox.Background = new SolidColorBrush(m_stdBgColor);
                        textBoxDictionary[textBox].IsSet = true;
                    }
                };
                textBox.LostFocus += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        setPlaceholder(textBox, textBoxDictionary[textBox].Placeholder);
                        textBoxDictionary[textBox].IsSet = false;
                    }
                };
            }

            void setPlaceholder(TextBox textBox, string placeholder)
            {
                textBox.Text = placeholder;
                textBox.Foreground = new SolidColorBrush(m_placeholderFgColor);
                textBox.Background = new SolidColorBrush(m_stdBgColor);
            }

        }

        private bool checkPesel()
        {
            string pesel = strPESEL.Text;
            strPESEL.Background = new SolidColorBrush(m_warnBgColor);
            strPESEL.Foreground = new SolidColorBrush(m_warnFgColor);
            if (pesel.Length != 11)
            {
                
                return false;
            }
            if (!long.TryParse(pesel, out _))
            {
                
                return false;
            }

            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += (pesel[i] - '0') * weights[i];
            }
            int controlDigit = (10 - (sum % 10)) % 10;
            if (controlDigit != (pesel[10] - '0'))
            {
                
                return false;
            }
            strPESEL.Background = new SolidColorBrush(m_stdBgColor);
            strPESEL.Foreground = new SolidColorBrush(m_stdFgColor);
            return true;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int errorCount = 0;
            foreach (var textBox in textBoxDictionary.Keys)
            {
                if (textBoxDictionary[textBox].IsRequired && !textBoxDictionary[textBox].IsSet)
                {
                    textBox.Background = new SolidColorBrush(m_warnBgColor);
                    errorCount++;
                    
                }
            }
            if (errorCount > 0)
            {
                MessageBox.Show("Wypełnij wszystkie wymagane pola!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var textBox in textBoxDictionary.Keys)
            {
                if(!textBoxDictionary[textBox].IsRequired)
                {
                    textBox.Text = "";
                }
            }

            if (!checkPesel())
            {
                MessageBox.Show("Nieprawidłowy numer PESEL", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            m_bCloseMode = 1;
            Close();            
        }        
    }
}
