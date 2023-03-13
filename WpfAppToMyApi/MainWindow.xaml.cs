using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using WpfAppToMyApi.Authentification;
using static System.Net.Mime.MediaTypeNames;

namespace WpfAppToMyApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataBookDataApi context;

        private string token = string.Empty;

        private string userLogin = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            // Для проверки
            //txtLogin.Text = "User1";
            txtLogin.Text = "Administrator";
            txtPass.Password = "123456aA!";

            context = new DataBookDataApi();

            btnLogin.Click += delegate
            {
                token = context.Login(new UserLogin()
                {
                    LoginProp = txtLogin.Text,
                    Password = txtPass.Password,
                    ReturnUrl = "/"
                });

                context.AddTokenToClient(token);

                Refresh();
            };

            btnRegister.Click += delegate
            {
                token = context.Register(new UserRegistration()
                {
                    LoginProp = txtReg.Text,
                    Password = txtPassReg.Password,
                    ConfirmPassword = txtPassRegConfirm.Password
                });
                context.AddTokenToClient(token);

                Refresh();
            };

            btnLogout.Click += delegate
            {
                token = string.Empty;
                context.RemoveTokenFromClient();
                Refresh();
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
            UserIsAutorized();

            listView.ItemsSource = context.GetAllDatabooks().ToObservableCollection();
        }

        public void UserIsAutorized()
        {
            if (string.IsNullOrEmpty(token) == false)
            {
                // Признак авторизации.
                userLogin = txtLogin.Text;
                TextBlockLogin.Text = userLogin + " в системе.";
                TextBlockLogin.HorizontalAlignment = HorizontalAlignment.Center;
                btnLogout.Visibility = Visibility.Visible;

                // Логин.
                txtLogin.Visibility = Visibility.Hidden;
                TextBlockPass.Visibility = Visibility.Hidden;
                txtPass.Visibility = Visibility.Hidden;
                btnLogin.Visibility = Visibility.Hidden;

                if (userLogin == "Administrator")
                {
                    // Регистрация.
                    TextBlockNewLogin.Visibility = Visibility.Visible;
                    txtReg.Visibility = Visibility.Visible;
                    TextBlockNewPass.Visibility = Visibility.Visible;
                    txtPassReg.Visibility = Visibility.Visible;
                    TextBlockNewPassConfirm.Visibility = Visibility.Visible;
                    txtPassRegConfirm.Visibility = Visibility.Visible;
                    btnRegister.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // Признак авторизации.
                userLogin = string.Empty;
                TextBlockLogin.Text = "Логин";
                TextBlockLogin.HorizontalAlignment = HorizontalAlignment.Left;
                btnLogout.Visibility = Visibility.Hidden;

                // Логин.
                txtLogin.Visibility = Visibility.Visible;
                TextBlockPass.Visibility = Visibility.Visible;
                txtPass.Visibility = Visibility.Visible;
                btnLogin.Visibility = Visibility.Visible;

                // Регистрация.
                TextBlockNewLogin.Visibility = Visibility.Hidden;
                txtReg.Visibility = Visibility.Hidden;
                TextBlockNewPass.Visibility = Visibility.Hidden;
                txtPassReg.Visibility = Visibility.Hidden;
                TextBlockNewPassConfirm.Visibility = Visibility.Hidden;
                txtPassRegConfirm.Visibility = Visibility.Hidden;
                btnRegister.Visibility = Visibility.Hidden;
            }
        }
    }
}
