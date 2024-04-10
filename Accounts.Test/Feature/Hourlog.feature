@Hourlog
Feature: Hourlog

Scenario: User Tries to Get ProjectHourLog
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/HourLogEntry/GetProjectHourLogs
	| key     | value      |
	| startDt | 01-01-2020 |
	| endDt   | 01-02-2020 |
	When I call GET methods of api Authorized
	Then I verify api response in ProjectHourLog

Scenario: User Tries to Get PayRollHourLogReport
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/HourLogEntry/GetPayrollHourLogsReport
	| key   | value |
	| Month | 1     |
	| Year  | 2020  |
	When I call GET methods of api Authorized
	Then I verify api response in PayRollHourLogReport

Scenario: User Tries to Get ProjectHourMonthlyReport
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/HourLogEntry/GetProjectMonthlyHourReport
	| key          | value |
	| ProjectId    | 2     |
	When I call GET methods of api Authorized
	Then I verify api response in ProjectHourMonthlyReport

Scenario: User Tries to Get the list of All  Hourlog
	Given I am in Accounts
	And I have an endpoint /api/services/app/HourLogEntry/GetAll
	When I call GET methods of api Authorized
	Then I verify api response in GetAllHourLog
	