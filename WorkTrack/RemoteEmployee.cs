namespace WorkTrack
{
    internal class RemoteEmployee : Employee
    {
        public RemoteEmployee((string name, string surName, double hourlyRate) employeeData)
           : base(employeeData) => WorkMode = WorkMode.Remote;
    }
}
