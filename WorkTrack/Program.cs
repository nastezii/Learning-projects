using WorkTrack;

CommandProcessor commandProcessor = new CommandProcessor();

string input = "";
Console.WriteLine("Commands:\n\"add employeee\", \"delete employee\", \"update hourly rate\"" +
    "\n\"print all employees\", \"print employees by work mode\", \"print employees by presence\"" +
    "\n\"record attendance\", \"сreate a report\"");
Console.WriteLine("To finish enter \"end\".");

commandProcessor.ProcessCommand();
