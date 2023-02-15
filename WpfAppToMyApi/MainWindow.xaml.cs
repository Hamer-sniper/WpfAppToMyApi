using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using System.Collections.ObjectModel;

namespace WpfAppToMyApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataBookDataApi context = new DataBookDataApi();
            
            btnRef.Click += delegate { listView.ItemsSource = context.GetAllDatabooks(Login.First()).ToObservableCollection(); };
            btnAdd.Click += delegate
            {
                context.CreateDataBook(new DataBook(txtSurname.Text,
                    txtName.Text,
                    txtMiddleName.Text,
                    txtTelephoneNumber.Text,
                    txtAdress.Text,
                    txtNote.Text)
               );
            };

            btnLogin.Click += delegate
            {
                context.CreateDataBook(new DataBook(txtSurname.Text,
                    txtName.Text,
                    txtMiddleName.Text,
                    txtTelephoneNumber.Text,
                    txtAdress.Text,
                    txtNote.Text)
               );
            };
        }
    }
}
