using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeSalaries
{
    public class PayCheck
    {
        // Paycheck properties
        string firstName;
        string lastName;
        double grossPay;
        double federalTax;
        double stateTax;
        double netPay;

        public PayCheck(Dictionary<string, Employee> employeeDict, string employeeId)
        {
            // Get the employee data
            Employee employee = employeeDict[employeeId];

            // Set first and last name
            this.firstName = employee.firstName;
            this.lastName = employee.lastName;

            // Determine gross pay for hourly employees
            if (employee.payType == "H")
                {
                    if (employee.hours <= 80)
                    {
                        this.grossPay = employee.hours * employee.salary;
                    }
                    else if (80 < employee.hours || employee.hours <= 90)
                    {
                        double remainingHours = employee.hours;
                        this.grossPay = 80 * employee.salary;
                        remainingHours = employee.hours - 80;
                        this.grossPay += remainingHours * employee.salary * 1.5;
                    }
                    else
                    {
                        double remainingHours = employee.hours;
                        this.grossPay = 80 * employee.salary;
                        remainingHours = employee.hours - 80;
                        this.grossPay += remainingHours * employee.salary * 1.5;
                        remainingHours -= 10;
                        this.grossPay += remainingHours * employee.salary * 1.75;
                    }
                }
                // Determine gross pay for salary employees.
                else if (employee.payType == "S")
                {
                    this.grossPay = employee.salary / (52 / 2);
                }
                else
                {
                    throw new SystemException("Invalid employee pay type found. Employee paytype was: " + employee.payType);
                }

            // Round gross pay
            Math.Round(this.grossPay, 2);

            // Determine federal tax
            this.federalTax = Math.Round(this.grossPay * 0.15, 2);

            // Determine state tax
            switch (employee.state)
            {
                case "UT":
                case "WY":
                case "NV":
                    this.stateTax = Math.Round(this.grossPay * 0.05, 2);
                    break;
                case "CO":
                case "ID":
                case "AZ":
                case "OR":
                    this.stateTax = Math.Round(this.grossPay * 0.065, 2);
                    break;
                case "WA":
                case "NM":
                case "TX":
                    this.stateTax = Math.Round(this.grossPay * 0.07, 2);
                    break;
            }

            // Determine netpay
            this.netPay = Math.Round(this.grossPay - this.federalTax - this.stateTax, 2);
        }
    }
}
