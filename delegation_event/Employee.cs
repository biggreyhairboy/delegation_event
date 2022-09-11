namespace delegation_event
{
    public class Employee
    {
        public Employee(string name, decimal salary)
        {
            this.Name = name;
            this.Salary = salary;
        }

        public decimal Salary { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1:C}", Name, Salary);
        }

        public static bool CompareSalary(Employee e1, Employee e2)
        {
            return e1.Salary < e2.Salary;
        }
    }
}