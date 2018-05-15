using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeSalaries
{
    class StateInfo
    {
        public string state;
        private List<Employee> employeeList;

        public StateInfo(string state)
        {
            this.state = state;
            this.employeeList = new List<Employee>();
        }

        public bool UpdateState(Employee employee)
        {
            this.employeeList.Add(employee);
            return true;
        }

        public double MedianTimeWorked()
        {
            // Sort the employee list by hours worked
            employeeList.Sort((x, y) => x.hours.CompareTo(y.hours));

            // Return the median hours worked
            int medianIndex = employeeList.Count / 2;
            return Math.Round(employeeList[medianIndex].hours, 2);
        }

        public double MedianNetPay()
        {
            // Sort the employee list by net pay
            employeeList.Sort((x, y) => x.netPay.CompareTo(y.netPay));

            // Return the median hours worked
            int medianIndex = employeeList.Count / 2;
            return Math.Round(employeeList[medianIndex].netPay, 2);
        }

        public double StateTaxes()
        {
            double totalTaxPaid = 0;

            foreach (Employee employee in employeeList)
            {
                totalTaxPaid += employee.stateTax;
            }

            return Math.Round(totalTaxPaid, 2);
        }

    }
}
