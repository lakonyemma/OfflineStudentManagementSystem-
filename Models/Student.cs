using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OfflineStudentManagementSystem.Models
{
    public class Student : INotifyPropertyChanged
    {
        private decimal amountAlreadyPaid;

        public string StudentId { get; set; }
        public string Password { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
        public string YearOfStudy { get; set; }
        public string RegistrationStatus { get; set; }
        public decimal TotalFees { get; set; }
        public decimal AmountAlreadyPaid
        {
            get { return amountAlreadyPaid; }
            set
            {
                amountAlreadyPaid = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(OutstandingBalance));
            }
        }
        public ObservableCollection<ModuleResult> Modules { get; set; }

        public decimal OutstandingBalance
        {
            get { return TotalFees - AmountAlreadyPaid; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
