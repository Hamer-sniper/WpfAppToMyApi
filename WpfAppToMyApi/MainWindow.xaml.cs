using System;
using System.Windows;

namespace WpfAppToMyApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBookDataApi context;
        public MainWindow()
        {
            InitializeComponent();            

            // Для проверки
            //txtLogin.Text = "User1";
            txtLogin.Text = "Administrator";
            txtPass.Text = "123456aA!";

            context = new DataBookDataApi();

            btnLogin.Click += delegate
            {
                txtToken.Text = context.GetToken(txtLogin.Text, txtPass.Text);
            };

            btnRegister.Click += delegate
            {
                txtToken.Text = context.Register(txtLogin.Text, txtPass.Text);
            };

            // Обновить
            btnRef.Click += delegate
            {
                Refresh();
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
                Refresh();
            };

            // Изменить
            btnEdit.Click += delegate
            {
                if (string.IsNullOrEmpty(txtId.Text))
                    return;

                context.UpdateDataBookById(Convert.ToInt32(txtId.Text),
                    new DataBook(txtSurname.Text,
                    txtName.Text,
                    txtMiddleName.Text,
                    txtTelephoneNumber.Text,
                    txtAdress.Text,
                    txtNote.Text)
               );
                Refresh();
            };

            // Удалить
            btnDelete.Click += delegate
            {
                if (string.IsNullOrEmpty(txtId.Text))
                    return;

                context.DeleteDataBookById(Convert.ToInt32(txtId.Text));
                Refresh();
            };
        }

        public void Refresh()
        {
            listView.ItemsSource = context.GetAllDatabooks().ToObservableCollection();
        }
    }
}
