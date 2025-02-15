﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeSalaries
{
    /// <summary>
    /// Main program.
    /// </summary>
    class Program
    {
        // Program global variables
        static Dictionary<string, Employee> employeeDict;

        /// <summary>
        /// Converts a text file into a dictionary. All strings are capitalized for formatting consistency.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static Dictionary<string, Employee> TextToDict(string filePath)
        {
            // Function variables
            Dictionary<string, Employee> employeeDict = new Dictionary<string, Employee>();
            string employeeId;
            string firstName;
            string lastName;
            string payType;
            double salary;
            string startDate;
            string state;
            double hours;

            // Read in the text file line by line
            string line;

            StreamReader file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                // Split the line on commas
                var lineItems = line.Split(new[] { ',' });

                // Make sure line only contains 8 elemsnts
                if (lineItems.Length != 8)
                {
                    throw new SystemException("Parsing error. Employee line item did not contain 8 items.");
                }

                // Determine employee ID
                if (lineItems[0] != "" || lineItems[0] != null)
                {
                    employeeId = lineItems[0].ToUpper();
                }
                else
                {
                    throw new SystemException("Parsing error. Employee ID item was empty or null.");
                }

                // determine first name
                if (lineItems[1] != "" || lineItems[1] != null)
                {
                    firstName = lineItems[1].ToUpper();
                }
                else
                {
                    throw new SystemException("Parsing error. Employee first name was empty or null.");
                }

                // Determine last name
                if (lineItems[2] != "" || lineItems[2] != null)
                {
                    lastName = lineItems[2].ToUpper();
                }
                else
                {
                    throw new SystemException("Parsing error. Employee last name was empty or null.");
                }

                // Determine is employee is hourly or salary
                if (lineItems[3] != "" || lineItems[3] != null)
                {
                    // Check for invalid input
                    if (lineItems[3].ToUpper() == "H" || lineItems[3].ToUpper() == "S")
                    {
                        payType = lineItems[3].ToUpper();
                    }
                    else
                    {
                        throw new SystemException("Parsing error. Employee pay type was not defined as hourly (H) or Salary (S). Pay type was defined as: " + lineItems[3]);
                    }
                }
                else
                {
                    throw new SystemException("Parsing error. Employee pay type was empty or null.");
                }

                // Determine employee salary
                if (lineItems[4] != "" || lineItems[4] != null)
                {
                    salary = Double.Parse(lineItems[4]);
                }
                else
                {
                    throw new SystemException("Parsing error. Employee salary was empty or null.");
                }

                // Determine employee date of initial employement
                if (lineItems[5] != "" || lineItems[5] != null)
                {
                    // Check for valid date formats
                    var dateItems = lineItems[5].Split(new[] { '/' });

                    // Make sure there are only 3 items in the date string
                    if (dateItems.Length != 3)
                    {
                        throw new SystemException("Parsing error. Employee start date should have 3 items, but had " + dateItems.Length + " items. Employee start date input was " + lineItems[5]);
                    }
                    // Check for valid month range
                    else if (int.Parse(dateItems[0]) < 1 || int.Parse(dateItems[0]) > 12)
                    {
                        throw new SystemException("Parsing error. Employee start month should be between 1 - 12. Start month was " + dateItems[0]);
                    }
                    // Check for valid date range
                    else if (int.Parse(dateItems[1]) < 1 || int.Parse(dateItems[1]) > 31)
                    {
                        throw new SystemException("Parsing error. Employee start day of the month should be between 1 - 31. Start day of the month was " + dateItems[1]);
                    }
                    // Checl for correct year formatting
                    else if (dateItems[2].Length != 2)
                    {
                        throw new SystemException("Parsing error. Invalid employee start year format. Sould only be 2 character long (e.g 05 for 2005). Start year was " + dateItems[2]);
                    }
                    // Store the correct date
                    else
                    {
                        startDate = lineItems[5].ToUpper();
                    }
                }
                else
                {
                    throw new SystemException("Parsing error. Employee start date was empty or null.");
                }

                // Determine employee state of employement
                if (lineItems[6] != "" || lineItems[6] != null)
                {
                    // Check for valid formatting
                    if (lineItems[6].Length != 2)
                    {
                        throw new SystemException("Parsing error. Employee state was in an invalid format. State shoud be two letter (e.g. UT for Utah). State was actually " + lineItems[6]);
                    }
                    else
                    {
                        state = lineItems[6].ToUpper();
                    }
                }
                else
                {
                    throw new SystemException("Parsing error. Employee state of employement was empty or null");
                }

                if (lineItems[7] != "" || lineItems[7] != null)
                {
                    hours = int.Parse(lineItems[7]);
                }
                else
                {
                    throw new SystemException("Parsing error. Employee hours for the last two weeks was empty or null");
                }


                // Create a new employee and add it to the dictionary
                Employee newEmployee = new Employee(employeeId, firstName, lastName, payType, salary, startDate, state, hours);
                employeeDict.Add(employeeId, newEmployee);
            }

            // Close the stream
            file.Close();

            // Return the dictionary
            return employeeDict;
        }

        /// <summary>
        /// Retreives employee info given an employee ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        static Employee GetByEmployeeId(string employeeId)
        {
            if (employeeId == "" || employeeId == null)
            {
                throw new SystemException("Employee can not be empty or null");
            }
            else if (employeeDict.ContainsKey(employeeId) == false)
            {
                throw new SystemException("Employee ID is not valid or does not exist.");
            }
            return employeeDict[employeeId];
        }

        /// <summary>
        /// Main program. Calls helper methods to generate text output files.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // File path of the employees text document
            string filePath = "Employees.txt";

            // Create a dictionary of employees from the text file
            employeeDict = TextToDict(filePath);

            // Create a list of paychecks for all employees
            List<PayCheck> employeePayChecks = new List<PayCheck>();
            foreach (KeyValuePair<string, Employee> employee in employeeDict)
            {
                // Create a new paycheck for this employee
                PayCheck currentPayCheck = new PayCheck(employeeDict, employee.Value.employeeId);
                employeePayChecks.Add(currentPayCheck);
            }

            // Sort the list of paychecks by gross pay (high to low)
            employeePayChecks.Sort((x, y) => -1 * x.grossPay.CompareTo(y.grossPay));

            // Write the sorted list to a file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("PayChecks.txt"))
            {
                foreach (PayCheck payCheck in employeePayChecks)
                {
                    string line = payCheck.employeeId + ", " + payCheck.firstName + ", " + payCheck.lastName + "," + payCheck.grossPay + ", " + payCheck.federalTax + ", " + payCheck.stateTax + ", " + payCheck.netPay;
                    file.WriteLine(line);
                }
            }

            // Convert to employee dictionary to a list and sort it
            var employeeList = employeeDict.OrderBy(x => -1 * x.Value.grossPay).ThenBy(x => x.Value.startDate).ThenBy(x => x.Value.lastName).ThenBy(x => x.Value.firstName).ToList();

            // Write the top 15% of earners
            int topEarnersCount = Convert.ToInt32(employeeDict.Count * 0.15);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("TopEarners.txt"))
            {
                for (int i = 0; i < topEarnersCount; i++)
                {
                    Employee currentEmployee = employeeList[i].Value;
                    int yearsWorked = DateTime.Now.Year - currentEmployee.startDate.Year;
                    string line = currentEmployee.firstName + ", " + currentEmployee.lastName + ", " + yearsWorked + ", " + currentEmployee.grossPay;
                    file.WriteLine(line);
                }
            }

            // Calculate stats for each state.
            Dictionary<string, StateInfo> states = new Dictionary<string, StateInfo>();
            foreach (KeyValuePair<string, Employee> employee in employeeDict)
            {
                // Add to the dictionary of states
                if (states.ContainsKey(employee.Value.state))
                {
                    states[employee.Value.state].UpdateState(employee.Value);
                }
                else
                {
                    StateInfo newState = new StateInfo(employee.Value.state);
                    states.Add(employee.Value.state, newState);
                    states[employee.Value.state].UpdateState(employee.Value);
                }
            }

            // Write state data to file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("States.txt"))
            {

                // Sort the states alphabetically.
                var sortedStates = states.Values.ToList();
                sortedStates.Sort((x, y) => x.state.CompareTo(y.state));

                // Calculate the state data for each state.
                foreach (StateInfo state in sortedStates)
                {
                    string line = state.state + ", " + state.MedianTimeWorked() + ", " + state.MedianNetPay() + ", " + state.StateTaxes();
                    file.WriteLine(line);
                }
            }
        }
    }
}
