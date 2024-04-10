@Company
Feature: Companies

Scenario: User Tries to Create Company
	Given I am in Accounts
	And I have an endpoint /api/services/app/Company/Create
	When I call POST method with Authorized Api CreateCompany
	|key|value|
	Then I check StatusCode as 200

Scenario: User Tries to Update Company
	Given I am in Accounts
	And I have an endpoint /api/services/app/Company/Update
	When I call PUT method with Authorized Api CreateCompany
	| key | value |
	| Id  | 32    |
	Then I check StatusCode as 200
	
Scenario: User Tries to Get Company
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Company/Get
	| key | value |
	| Id  | 1     |
	When I call GET methods of api Authorized
	Then I verify api response in GetCompany

Scenario: User Tries to Get the list of All Company
	Given I am in Accounts
	And I have an endpoint /api/services/app/Company/GetAll
	When I call GET methods of api Authorized
	Then I verify api response in GetAllCompany