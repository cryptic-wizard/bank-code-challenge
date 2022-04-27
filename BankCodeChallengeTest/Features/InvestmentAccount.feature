Feature: InvestmentAccount

Background: 
	Given there is a bank named MyBank
	And the bank has a checking account with owner John
	And the bank has a checking account with owner Tom
	And the bank has an individual investment account with owner Bob

Scenario: Deposit to Individual Investment Account
	Given user Bob has a balance of 20.00
	When user Bob makes a deposit of 20.25
	Then the transaction was approved
	And user Bob has a balance of 40.25

Scenario: Valid Withdrawal From Individual Investment Account
	Given user Bob has a balance of 505.00
	When user Bob makes a withdrawal of 500.00
	Then the transaction was approved
	And user Bob has a balance of 5.00

Scenario: Invalid Maximum Withdrawal From Individual Investment Account
	Given user Bob has a balance of 505.00
	When user Bob makes a withdrawal of 500.01
	Then the transaction was denied
	And user Bob has a balance of 505.00

Scenario: Insufficient Funds Withdrawal From Individual Investment Account
	Given user Bob has a balance of 20.00
	When user Bob makes a withdrawal of 20.25
	Then the transaction was denied
	And user Bob has a balance of 20.00

Scenario: Valid Transfer From Individual Investment to Checking
	Given user Bob has a balance of 100.00
	Given user John has a balance of 20.00
	When user Bob makes a transfer of 50.00 to user John
	Then the transaction was approved
	And user Bob has a balance of 50.00
	And user John has a balance of 70.00

Scenario: Invalid Transfer From Individual Investment to Checking
	Given user Bob has a balance of 100.00
	Given user John has a balance of 20.00
	When user Bob makes a transfer of 100.01 to user John
	Then the transaction was denied
	And user Bob has a balance of 100.00
	And user John has a balance of 20.00