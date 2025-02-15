# Employee Salaries
Small program that calculates employee salaries from a large text file.

## Generated Files and Methods
1.	Paychecks.txt 		Paycheck for each employee ordered by gross pay (highest to lowest).
2.	TopEarners.txt 		List of the top 15% of earners sorted by years worked at the company (highest to lowest), then alphabetically by last name then first name.
3.	States.txt 			Median time worked, median net pay, and paid state taxes for each state, ordered alphabetically.

Additionally,

**Source code I wrote to generate the files is found in Employee-Salaries/EmployeeSalaries/EmployeeSalaries/**

GetByEmployeeId(string employeeId) is the method for getting an employee's data. Returns an employee object. Employee objects fields are publicly accessible.


## Directions
Pay is calculated for hourly employees as hourly rate for the first 80 hours, the next 10 hours are at 150% of their rate, the rest is 175% of their rate.
For salaried employees they are paid every 2 weeks assuming a 52 week year. They are paid for 2 weeks regardless of hours worked.
The federal tax for every employee is 15%.
The state tax for every employee is found are as follows:

5%		: UT,WY,NV

6.5%	: CO, ID, AZ, OR
	
7%		: WA, NM, TX


## Requirements
Generate a text file for each of the following scenarios (3 files total)
1.	Calculate pay checks for every line in the text file. The output should be: employee id, first name, last name, gross pay, federal tax, state tax, net pay. Ordered by gross pay (highest to lowest).
2.	A list of the top 15% earners sorted by the number or years worked at the company (highest to lowest), then alphabetically by last name then first. The output should be first name, last name, number of years worked, gross pay.
3.	A list of all states with median time worked, median net pay, and total state taxes paid. The output should be state, median time worked, median net pay, state taxes. Ordered by states alphabetically.

Additionally,

4.	Write a method that given an employee id would efficiently bring back an employee’s data 
	* example, Employee GetByEmployeeId(string employeeId)

All numbers should be rounded to 2 decimal places.