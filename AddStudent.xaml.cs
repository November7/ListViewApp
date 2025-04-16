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
    public partial class AddStudent : Window
    {
        public int saveMode = 0;
        public AddStudent()
        {
            InitializeComponent();
        }

        public int checkFields()
        {
            int ret = 0;
            if (strName.Text != "") ret += 1;
            //if (strSecName.Text == "") return false;
            if (strSurname.Text != "") ret += 2;
            if (strPESEL.Text != "") ret += 4;
            return ret;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            saveMode = checkFields();
            if (saveMode == 7)
            {

                Close();    
            }
        }
    }
}
