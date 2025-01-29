namespace WorkTrack
{
    internal class CommandProcessor
    {
        private EmployeeRoster roster;
        private AttendanceRecord attendance;

        public CommandProcessor()
        {
            roster = new EmployeeRoster();
            attendance = new AttendanceRecord();
        }

        public void ProcessCommand()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Enter the command:");
                string input = Console.ReadLine();
                string command = input.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(command))
                {
                    Console.WriteLine("Command can't be empty.");
                    return;
                }

                if (command == "add employee")
                {
                    try
                    {
                        if (DefineEmployeeWorkMode() == WorkMode.Remote)
                            roster.AddEmployee(new RemoteEmployee(GetEmployeeDetails()));
                        else
                            roster.AddEmployee(new OfficeEmployee(GetEmployeeDetails()));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Employee not added.");
                    }
                }
                else if (command == "delete employee")
                {
                    try
                    {
                        (string name, string surName) = GetEmployeeInitials();

                        var employee = roster.FindEmployee(name, surName);

                        roster.DeleteEmployee(employee);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (command == "update hourly rate")
                {
                    try
                    {
                        (string name, string surName) = GetEmployeeInitials();
                        var employee = roster.FindEmployee(name, surName);
                        employee.UpdateHourlyRate(SetHourlyRate());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (command == "print all employees")
                {
                    roster.PrintAllEmployees();
                }
                else if (command == "print employees by work mode")
                {
                    try
                    {
                        var workMode = GetEmployeeWorkMode();
                        roster.PrintEmployeesByWorkMode(workMode);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (command == "print employees by presence")
                {
                    try
                    {
                        roster.PrintEmployeesByPresence(SetPresence());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (command == "record attendance")
                {
                    attendance.CheckEmployeeAttendance(roster.Employees);
                }
                else if (command == "сreate a report")
                {
                    EmployeeReportCreator creator = new EmployeeReportCreator();
                    creator.CreateReport(roster.Employees);
                    Console.WriteLine("Report successfully created.");
                }
                else if (command == "end")
                {
                    Console.WriteLine("Exiting the program...");
                    flag = false;
                }
                else
                    Console.WriteLine("Invalid input. Please, try again.");
            }
        }

        private (string, string) GetEmployeeInitials()
        {
            Console.Write("Enter employee name: ");
            string name = Console.ReadLine();

            Console.Write("Enter employee surname: ");
            string surName = Console.ReadLine();

            return (name, surName);
        }

        private (string, string, double) GetEmployeeDetails()
        {
            (string name, string surName) = GetEmployeeInitials();
            return (name, surName, SetHourlyRate());
        }

        private double SetHourlyRate()
        {
            Console.Write("Enter hourly rate: ");
            if (double.TryParse(Console.ReadLine(), out double hourlyRate))
            {
                return hourlyRate;
            }
            else
                throw new Exception("Invalid hourly rate.");
        }

        private WorkMode GetEmployeeWorkMode()
        {
            Console.WriteLine("Enter work mode:");
            if (Enum.TryParse(typeof(WorkMode), Console.ReadLine(), true, out var result))
                return (WorkMode)result;
            else
                throw new Exception("Invalid employee status.");
        }

        private WorkMode DefineEmployeeWorkMode()
        {
            Console.WriteLine("Enter work mode (Remote/OnSite):");

            if (Enum.TryParse(typeof(WorkMode), Console.ReadLine(), true, out var result))
            {
                return (WorkMode)result;
            }
            else
                throw new Exception("Invalid work mode.");
        }

        private bool SetPresence()
        {
            Console.WriteLine("Should users be present or not? Enter y/n :");
            string input = Console.ReadLine();

            if (string.Equals(input, "y", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (string.Equals(input, "n", StringComparison.OrdinalIgnoreCase))
                return false;
            else
                throw new Exception("Invalid presence.");
        }
    }
}
