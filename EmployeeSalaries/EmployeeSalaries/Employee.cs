using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeSalaries
{
    /// <summary>
    /// Employee class.
    /// </summary>
    public class Employee
    {
        // Employee properties
        public string employeeId;
        public string firstName;
        public string lastName;
        public string payType;
        public double salary;
        public DateTime startDate;
        public string state;
        public double hours;
        public double grossPay;

        /// <summary>
        /// Constructor for a new employee.
        /// </summary>
        /// <param name="employeeId">   Employee ID</param>
        /// <param name="firstName">    Employee first name</param>
        /// <param name="lastName">     Employee last lame</param>
        /// <param name="payType">      Employee pay type (hourly rate or salary)</param>
        /// <param name="salary">       Employee salary or hourly rate</param>
        /// <param name="startDate">    Employee date of initial employement</param>
        /// <param name="state">        Employee state of employment</param>
        /// <param name="hours">        Employee hours worked in last 2 weeks</param>
        public Employee(String employeeId, string firstName, string lastName, string payType, double salary, string startDate, string state, double hours)
        {
            this.employeeId = employeeId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.payType = payType;
            this.salary = salary;

            // Set the date as a DateTime
            var dateItems = startDate.Split(new[] { '/' });
            if (00 <= int.Parse(dateItems[2]) && int.Parse(dateItems[2]) <= 18)
            {
                this.startDate = new DateTime(int.Parse("20" + dateItems[2]), int.Parse(dateItems[0]), int.Parse(dateItems[1]));
            }
            else
            {
                this.startDate = new DateTime(int.Parse("19" + dateItems[2]), int.Parse(dateItems[0]), int.Parse(dateItems[1]));
            }

            this.state = state;
            this.hours = hours;

            // Determine gross pay for sorting purposes
            if (this.payType == "H")
            {
                if (this.hours <= 80)
                {
                    this.grossPay = this.hours * this.salary;
                }
                else if (80 < this.hours || this.hours <= 90)
                {
                    double remainingHours = this.hours;
                    this.grossPay = 80 * this.salary;
                    remainingHours = this.hours - 80;
                    this.grossPay += remainingHours * this.salary * 1.5;
                }
                else
                {
                    double remainingHours = this.hours;
                    this.grossPay = 80 * this.salary;
                    remainingHours = this.hours - 80;
                    this.grossPay += remainingHours * this.salary * 1.5;
                    remainingHours -= 10;
                    this.grossPay += remainingHours * this.salary * 1.75;
                }
            }
            else if (this.payType == "S")
            {
                this.grossPay = this.salary / (52 / 2);
            }
            else
            {
                throw new SystemException("Invalid employee pay type found. Employee paytype was: " + this.payType);
            }

            // Round gross pay
            this.grossPay = Math.Round(this.grossPay, 2);
        }
    }
}
