namespace WorkTrack
{
    internal class OfficeEmployee : Employee
    {
        public OfficeEmployee((string name, string surName, double hourlyRate) employeeData) 
            : base(employeeData) => WorkMode = WorkMode.Onsite;
    }
}