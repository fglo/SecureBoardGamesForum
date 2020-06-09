namespace BBDProject.Management.Models.Models.Employee
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool LockedOut { get; set; }
    }
}

