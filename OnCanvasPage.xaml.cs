using System;
using OfflineStudentManagementSystem.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace OfflineStudentManagementSystem
{
    public sealed partial class OnCanvasPage : Page
    {
        public Student Student { get; private set; }

        public OnCanvasPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Student = e.Parameter as Student;

            if (Student == null)
            {
                Frame.Navigate(typeof(MainPage));
                return;
            }

            DataContext = Student;
        }

        private void OnCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WorkArea.Width = e.NewSize.Width;
            WorkArea.Height = e.NewSize.Height;
        }

        private async void CalculatePaymentButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement form = (FrameworkElement)((Button)sender).Parent;
            TextBox newPaymentTextBox = (TextBox)form.FindName("NewPaymentTextBox");
            TextBlock feeErrorTextBlock = (TextBlock)form.FindName("FeeErrorTextBlock");
            feeErrorTextBlock.Text = string.Empty;
            decimal newPayment;

            if (!decimal.TryParse(newPaymentTextBox.Text, out newPayment) || newPayment <= 0)
            {
                feeErrorTextBlock.Text = "Enter a valid payment amount greater than zero.";
                return;
            }

            if (newPayment > Student.OutstandingBalance)
            {
                feeErrorTextBlock.Text = "The new payment cannot be greater than the outstanding balance.";
                return;
            }

            decimal newOutstandingBalance = Student.TotalFees - Student.AmountAlreadyPaid - newPayment;
            Student.AmountAlreadyPaid += newPayment;
            newPaymentTextBox.Text = string.Empty;

            ContentDialog dialog = new ContentDialog
            {
                Title = "Payment Calculated",
                Content = "New outstanding balance: " + newOutstandingBalance.ToString("N0"),
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
        }

        private async void SubmitRequestButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement form = (FrameworkElement)((Button)sender).Parent;
            ComboBox serviceComboBox = (ComboBox)form.FindName("ServiceComboBox");
            TextBox requestDescriptionTextBox = (TextBox)form.FindName("RequestDescriptionTextBox");
            TextBox contactTextBox = (TextBox)form.FindName("ContactTextBox");
            TextBlock serviceErrorTextBlock = (TextBlock)form.FindName("ServiceErrorTextBlock");
            serviceErrorTextBlock.Text = string.Empty;

            if (serviceComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(requestDescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(contactTextBox.Text))
            {
                serviceErrorTextBlock.Text = "Select a service and enter the request description and contact number.";
                return;
            }

            string service = ((ComboBoxItem)serviceComboBox.SelectedItem).Content.ToString();
            string confirmation =
                "Student ID: " + Student.StudentId + "\n" +
                "Student Name: " + Student.StudentName + "\n" +
                "Department: " + Student.Department + "\n" +
                "Service: " + service + "\n" +
                "Description: " + requestDescriptionTextBox.Text.Trim() + "\n" +
                "Contact: " + contactTextBox.Text.Trim();

            ContentDialog dialog = new ContentDialog
            {
                Title = "Request Submitted",
                Content = confirmation,
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();

            serviceComboBox.SelectedIndex = -1;
            requestDescriptionTextBox.Text = string.Empty;
            contactTextBox.Text = string.Empty;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
