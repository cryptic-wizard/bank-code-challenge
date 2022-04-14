Feature: Bank

Background: 
	Given there is a bank named MyBank
	And the bank has a checking account with owner John

Scenario: Deposit to an Empty Checking Account
	Given user John has a balance of 0
	When user John makes a deposit of 50.25
	Then user John has a balance of 50.25

Scenario: Deposit to a Non Empty Checking Account
	Given user John has a balance of 50.25
	When user John makes a deposit of 50.25
	Then user John has a balance of 100.50

Scenario: Valid Withdrawal From Checking Account
	Given user John has a balance of 100.00
	When user John makes a withdrawal of 50.00
	Then the transaction was approved
	And user John has a balance of 50.00

Scenario: Invalid Withdrawal From Checking Account
	Given user John has a balance of 50.00
	When user John makes a withdrawal of 100.00
	Then the transaction was denied
	And user John has a balance of 50.00