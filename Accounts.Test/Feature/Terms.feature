@Term
Feature: Terms

Scenario: User Tries to Create Term
	Given I am in Accounts
	And I have an endpoint /api/services/app/Term/Create
	When I call POST method with Authorized Api CreateTerm
	|key|value|
	Then I check StatusCode as 200

Scenario: User Tries to Update Term
	Given I am in Accounts
	And I have an endpoint /api/services/app/Term/Update
	When I call PUT method with Authorized Api CreateTerm
	| key | value |
	| Id  | 4     |
	Then I check StatusCode as 200
	
Scenario: User Tries to Get Term
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Term/Get
	| key | value |
	| Id  | 1     |
	When I call GET methods of api Authorized
	Then I verify api response in GetTerm

Scenario: User Tries to Get the list of All Terms
	Given I am in Accounts
	And I have an endpoint /api/services/app/Term/GetAll
	When I call GET methods of api Authorized
	Then I verify api response in GetAllTerm