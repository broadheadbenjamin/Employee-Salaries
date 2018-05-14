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
        public string startDate;
        public string state;
        public double hours;

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
            this.startDate = startDate;
            this.state = state;
            this.hours = hours;
        }
    }
}
