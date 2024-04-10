@Project
Feature: Project

Scenario:  User Tries to create Projects
	Given I am in Accounts
	And I have an endpoint /api/services/app/Project/Create
	When I call POST method with Authorized Api CreateProject
	|key|value|
	Then I check StatusCode as 200

Scenario: User Tries to Search Projects
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Project/Search
	| key      | value  |
	| IsActive | true   |
	| Name     | Active |
	When I call GET methods of api Authorized
	Then I verify api response in SearchProject

Scenario:  User Tries Get Attachment Related to Projects
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Project/GetAttachments
	| key       | value |
	| ProjectId | 1     |
	When I call GET methods of api Authorized
	Then I verify api response in GetAttachments

Scenario: User Tries Get  Projects
	Given I am in Accounts
	And I have parameter in an endpoint /api/services/app/Project/Get
	| key | value |
	| Id  | 1     |
	When I call GET methods of api Authorized
	Then I verify api response in GetProject
	
	