namespace OfflineStudentManagementSystem.Models
{
    public class ModuleResult
    {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int CreditUnits { get; set; }
        public string Lecturer { get; set; }
        public int CourseworkMark { get; set; }
        public int ExaminationMark { get; set; }

        public int OverallMark
        {
            get { return CourseworkMark + ExaminationMark; }
        }

        public string GradeStatus
        {
            get
            {
                if (OverallMark >= 80)
                    return "Excellent";
                if (OverallMark >= 70)
                    return "Very Good";
                if (OverallMark >= 60)
                    return "Good";
                if (OverallMark >= 50)
                    return "Satisfactory";
                return "At Risk";
            }
        }
    }
}

