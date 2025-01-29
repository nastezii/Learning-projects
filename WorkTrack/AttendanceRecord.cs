namespace WorkTrack
{
    internal class AttendanceRecord
    {
        public void CheckEmployeeAttendance(List<Employee> employees)
        {
            Console.WriteLine("Enter '+' if the employee is present, '-' if absent," +
                " or the number of hours worked:");
            foreach (Employee employee in employees)
            {
                ProcessAttendanceInput(employee);
            }
        }

        private void ProcessAttendanceInput(Employee employee)
        {
            Console.WriteLine($"{employee.SurName} {employee.Name}: ");
            string input = Console.ReadLine();

            while (true)
            {
                if (input == "+")
                {
                    employee.SetPresence(true);
                    employee.SetDailySalary();
                    break;
                }
                else if (input == "-")
                {
                    employee.SetPresence(false);
                    employee.SetDailySalary(0);
                    employee.SetStatus(SetStatus());
                    break;
                }
                else if (int.TryParse(input, out int hoursWorked)
                    && hoursWorked > 0 && hoursWorked <= 12)
                {
                    employee.SetPresence(true);
                    employee.SetDailySalary(hoursWorked);
                    break;
                }
                else
                    Console.WriteLine("Invalid input. Please, try again.");
            }
        }

        private Status SetStatus()
        {
            Console.WriteLine("Please select the reason for your absence:\n1 - on vacation\n2 - unexcused absence\n3 - sick leave.");

            while (true) 
            {
                string input = Console.ReadLine();

                if (input == "1")
                    return Status.OnVacation;
                else if (input == "2")
                    return Status.UnexcusedAbsence;
                else if (input == "3")
                    return Status.SickLeave;
                else
                    Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
            }
        }
    }
}
