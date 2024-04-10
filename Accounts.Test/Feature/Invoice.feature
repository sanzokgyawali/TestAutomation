@Invoice
Feature: Invoice

Scenario: User tries to get InvoiceMonthReport
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Invoice/GetInvoicesByMonthReport
	|key|value|
	When I call GET methods of api Authorized
	Then I verify api response in GetInvoiceByMonthReport

Scenario: User tries to Search the  Invoice
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Invoice/Search
	|key|value|
	When I call GET methods of api Authorized
	Then I verify api response in SearchInvoice

Scenario: User Tries to get the Invoice
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Invoice/Get
	| key | value |
	| Id  | 1     |
	When I call GET methods of api Authorized
	Then I verify api response in GetInvoice

Scenario: User Tries to Generate the  Invoice
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Invoice/GenerateInvoice
	| key         | value |
	| timesheetId | 33    |
	When I call GET methods of api Authorized
	Then I verify api response in GenerateInvoice

Scenario: User Tries to Generate  and Save the  Invoice
	Given I am in Accounts
	And I have an endpoint /api/services/app/Invoice/GenerateAndSubmit
	When I call POST method with Authorized Api GenerateSubmitInvoice
	| key         | value |
	| timesheetId | 32   |
	Then I check StatusCode as 500
