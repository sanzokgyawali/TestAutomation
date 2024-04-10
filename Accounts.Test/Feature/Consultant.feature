@Consultant
Feature:Consultant

Scenario: User Tries to Get Consultant Details
	And I am in Accounts
	And I have parameter in an endpoint /api/services/app/Consultant/Get
	| key | value |
	| Id  | 1    |
	When I call GET methods of api Authorized
	Then I verify api response in ConsultantDetails

Scenario: User Tries to Search Consutant
	And I am in Accounts
	And I have parameter in an endpoint /api/services/app/Consultant/Search
	| key        | value  |
	| searchText | Prajit |
	When I call GET methods of api Authorized
	Then I verify api response in ConsultantSearch

Scenario: User Tries to Update Consultant
	Given I am in Accounts
	And I have an endpoint /api/services/app/Consultant/Update
	When I call PUT method with Authorized Api Consultant
	| key       | value  |
	| id        | 30     |
	| firstName | Sanjok |
	Then I check StatusCode as 200
