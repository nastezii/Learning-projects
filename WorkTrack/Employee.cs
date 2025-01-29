namespace WorkTrack
{
    abstract class Employee
    {
        private string name;
        private string surName;
        private double hourlyRate;
        private double dailySalary;
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty or whitespace.");
                }
                name = value;
            }
        }

        public string SurName
        {
            get => surName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Surname cannot be empty or whitespace.");
                }
                surName = value;
            }
        }
        public double HourlyRate
        {
            get => hourlyRate;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Hourly rate must be greater than zero.");
                hourlyRate = value;
            }
        }
        public double DailySalary { get; private set; }
        public Status Status { get; private set; }
        public WorkMode WorkMode { get; set; }
        public bool Presence { get; private set; }

        public Employee((string name, string surName, double hourlyRate) employeeData)
        {
            Name = employeeData.name;
            SurName = employeeData.surName;
            HourlyRate = employeeData.hourlyRate;
        }

        public void SetPresence(bool isPresent)
        {
            if (isPresent)
                Presence = true;
            else
                Presence = false;
        }

        public void SetStatus(Status status) => Status = status;
        
        public double SetDailySalary(int hoursWorked = 8) => DailySalary = HourlyRate * hoursWorked;

        public void UpdateHourlyRate(double hourlyRate) => HourlyRate = hourlyRate; 
    }
}
