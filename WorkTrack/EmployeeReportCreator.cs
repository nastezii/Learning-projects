namespace WorkTrack
{
    internal class EmployeeReportCreator
    {
        public string Today { get; private set; }
        public string FilePath { get; private set; }

        public EmployeeReportCreator()
        {
            Today = DateTime.Now.ToString("yyyy-MM-dd"); 
            FilePath = $"Employee_Report_{Today}.txt";   
        }

        public void CreateReport(List<Employee> employees)
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                writer.WriteLine($"Report for {DateTime.Now:dd.MM.yyyy}");
                writer.WriteLine(new string('-', 90));
                writer.WriteLine($"{"First Name",-15} {"Last Name",-15} {"Hourly Rate",-12} {"Presence",-10} {"Status",-18} {"Daily Salary",-12}");
                writer.WriteLine(new string('-', 90));

                foreach (var employee in employees)
                {
                    string presenceText = employee.Presence ? "Yes" : "No";
                    string statusText = employee.Status.ToString();

                    string formattedLine =
                        $"{employee.Name,-15} {employee.SurName,-15} {employee.HourlyRate,10:F2} ₴   {presenceText,-10} {statusText,-18} {employee.DailySalary,10:F2} ₴";

                    writer.WriteLine(formattedLine);
                }
            }
        }

    }
}
