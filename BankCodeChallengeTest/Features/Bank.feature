Feature: Bank

# Checking Unit Tests
Background: 
	Given there is a bank named MyBank
	And the bank has a checking account with owner John
	And the bank has a checking account with owner Tom
	And the bank has an individual investment account with owner Bob

Scenario: Deposit to an Empty Checking Account
	Given user John has a balance of 0
	When user John makes a deposit of 50.25
	Then user John has a balance of 50.25

Scenario: Deposit to a Non Empty Checking Account
	Given user John has a balance of 50.25
	When user John makes a deposit of 50.25
	Then user John has a balance of 100.50

Scenario: Valid Withdrawal From Checking Account
	Given user John has a balance of 100.50
	When user John makes a withdrawal of 50.25
	Then the transaction was approved
	And user John has a balance of 50.25

Scenario: Invalid Withdrawal From Checking Account
	Given user John has a balance of 50.00
	When user John makes a withdrawal of 100.00
	Then the transaction was denied
	And user John has a balance of 50.00

Scenario: Valid Transfer Between Checking Accounts
	Given user John has a balance of 100.25
	And user Tom has a balance of 25.25
	When user John makes a transfer of 50.25 to user Tom
	Then the transaction was approved
	And user John has a balance of 50.00
	And user Tom has a balance of 75.50

Scenario: Invalid Transfer Between Checking Accounts
	Given user John has a balance of 25.25
	And user Tom has a balance of 50.25
	When user John makes a transfer of 30.25 to user Tom
	Then the transaction was denied
	And user John has a balance of 25.25
	And user Tom has a balance of 50.25

# Individual Investment Unit Tests
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