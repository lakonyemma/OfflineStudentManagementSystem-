using OfflineStudentManagementSystem.Data;
using OfflineStudentManagementSystem.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OfflineStudentManagementSystem
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginErrorTextBlock.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(StudentIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(StudentPasswordBox.Password) ||
                DepartmentComboBox.SelectedItem == null)
            {
                LoginErrorTextBlock.Text = "Enter the Student ID and password, then select a department.";
                return;
            }

            string department = ((ComboBoxItem)DepartmentComboBox.SelectedItem).Content.ToString();
            Student student = StudentRepository.Authenticate(
                StudentIdTextBox.Text,
                StudentPasswordBox.Password,
                department);

            if (student == null)
            {
                LoginErrorTextBlock.Text = "The Student ID, password, or department is incorrect.";
                return;
            }

            Frame.Navigate(typeof(OnCanvasPage), student);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            StudentIdTextBox.Text = string.Empty;
            StudentPasswordBox.Password = string.Empty;
            DepartmentComboBox.SelectedIndex = -1;
            LoginErrorTextBlock.Text = string.Empty;
            StudentIdTextBox.Focus(FocusState.Programmatic);
        }
    }
}

