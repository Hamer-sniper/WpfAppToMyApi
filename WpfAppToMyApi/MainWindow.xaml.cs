using System;
using System.Windows;

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

            txtLogin.Text = "Administrator";
            txtPass.Text = "123456aA!";

            DataBookDataApi context = new DataBookDataApi();

            btnLogin.Click += delegate
            {
                txtToken.Text = Login.GetToken(txtLogin.Text, txtPass.Text);
                context.AddTokenToClient(txtToken.Text);
            };

            // Обновить
            btnRef.Click += delegate
            { 
                listView.ItemsSource = context.GetAllDatabooks().ToObservableCollection(); 
            };
            
            // Добавить
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

            // Изменить
            btnEdit.Click += delegate
            {
                context.UpdateDataBookById(Convert.ToInt32(txtId.Text),
                    new DataBook(txtSurname.Text,
                    txtName.Text,
                    txtMiddleName.Text,
                    txtTelephoneNumber.Text,
                    txtAdress.Text,
                    txtNote.Text)
               );
            };

            // Удалить
            btnDelete.Click += delegate
            {
                context.DeleteDataBookById(Convert.ToInt32(txtId.Text)                    
               );
            };
        }
    }
}
