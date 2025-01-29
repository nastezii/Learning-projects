namespace WorkTrack
{
    internal class EmployeeRoster
    {
        private List<Employee> employees = new List<Employee>()
        {
            new RemoteEmployee(("John", "Smith", 15)),
            new RemoteEmployee(("Emily", "Johnson", 12)),
            new OfficeEmployee(("Michael", "Brown", 19)),
            new OfficeEmployee(("Sophia", "Davis", 20)),
        };

        public List<Employee> Employees
        {
            get { return employees; }
        }

        public void AddEmployee(Employee employee)
        {
            if (!employees.Contains(employee))
            {
                employees.Add(employee);
                Console.WriteLine("Employee successfully added to the database.");
            }
            else
                Console.WriteLine("This employee already exists in the database.");
        }

        public void DeleteEmployee(Employee employee)
        { 
            employees.Remove(employee);
            Console.WriteLine("Employee successfully removed from the database.");
        }

        public void PrintAllEmployees()
        {
            CheckAvailability();

            foreach (Employee employee in employees) 
                PrintEmployeeInfo(employee);
        }

        public void PrintEmployeesByWorkMode(WorkMode workMode)
        {
            CheckAvailability();

            var employeesByWorkMode = employees.Where(e => e.WorkMode.Equals(workMode)).ToList();
            if (employeesByWorkMode.Count == 0)
            {
                Console.WriteLine("There are currently no employees with this work mode in the database.");
                return;
            }

            foreach (var employee in employeesByWorkMode)
            {
                Console.WriteLine($"Name: {employee.SurName} {employee.Name}");
            }
        }

        public void PrintEmployeesByPresence(bool isPresent)
        {
            CheckAvailability();

            var employeesWithPresence = employees.Where(e => e.Presence == isPresent).ToList();
            if (employeesWithPresence.Count == 0)
            {
                Console.WriteLine("There are currently no employees with the specified presence status in the database.");
                return;
            }

            foreach (var employee in employeesWithPresence)
            {
                Console.WriteLine($"{employee.SurName} {employee.Name}");
            }
        }

        public Employee FindEmployee(string name, string surName)
        {
            var employee = Employees.FirstOrDefault(e =>
                     e.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                     e.SurName.Equals(surName, StringComparison.OrdinalIgnoreCase));
            if (employee == null)
                throw new ArgumentException("This employee doesn't exists in the database.");
            return employee;
        }

        private void CheckAvailability()
        {
            if (employees.Count() == 0)
                throw new Exception("There are currently no employees in the database.");
        }

        private void PrintEmployeeInfo(Employee employee)
        {
            Console.WriteLine($"Employee:   {employee.SurName} {employee.Name}");
            Console.WriteLine($"Hourly Rate:{employee.HourlyRate} $");
            Console.WriteLine($"Work Mode:  {employee.WorkMode}");
            Console.WriteLine($"Status:     {employee.Status}");
            Console.WriteLine($"Presence:   {(employee.Presence ? "Present" : "Absent")}");
            Console.WriteLine();
        }
    }
}
