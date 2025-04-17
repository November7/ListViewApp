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
        public int saveMode = 0;

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
                        textBox.Foreground = new SolidColorBrush(Colors.Black);
                        textBox.Background = new SolidColorBrush(Colors.White);
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
                textBox.Foreground = new SolidColorBrush(Colors.LightGray);
                textBox.Background = new SolidColorBrush(Colors.White);
            }

        }

      

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (var textBox in textBoxDictionary.Keys)
            {
                if (textBoxDictionary[textBox].IsRequired && !textBoxDictionary[textBox].IsSet)
                {
                    textBox.Background = new SolidColorBrush(Colors.Red);
                    return;
                }
            }

            foreach (var textBox in textBoxDictionary.Keys)
            {
                if(!textBoxDictionary[textBox].IsRequired)
                {
                    textBox.Text = "";
                }
            }
            Close();            
        }        
    }
}
