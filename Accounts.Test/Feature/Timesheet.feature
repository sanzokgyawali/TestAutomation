@Timesheet
Feature: Timesheet
	
Scenario:  User Tries to create Timesheet
	Given I am in Accounts
	And I have an endpoint /api/services/app/Timesheet/Create
	When I call POST method with Authorized Api CreateTimesheet
	|key|value|
	Then I check StatusCode as 200

Scenario: User Tries to Get MonthlyHourReport
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Timesheet/GetMonthlyHourReport
	| key          | value |
	| ConsultantId | 1     |
	| CompanyId    | 1     |
	| ProjectId    | 2     |

	When I call GET methods of api Authorized
	Then I verify api response in TimesheetMonthlyHourReport

Scenario: User tries to Get The Timesheet
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Timesheet/GetTimesheets
	| key          | value |
	| ConsultantId | 1     |
	When I call GET methods of api Authorized
	Then I verify api response in GetTimesheet
