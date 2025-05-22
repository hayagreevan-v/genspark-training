## Question - Solution Explanation

Implementation of SOLID Principles

**S - Single Responsibility**
- Seperate Classes created for StandardBankAccount, MinorBankAccount, CreateBankAccount, DisplayBankAcount, TransferMoney
- Each class take care of single functionality

**O - Open Closed Principle**
- Create seperate Methods for creation of Different Accounts (Standard and Minor).
- In future, Any New Bank Acoount can be added and created with new method. No need of modifiication of existing methods.

**L - Liskov Substitution**
- Both StandardBankAccount and MinorBankAccount are implemented from IBankAccount Interface
- Both classes are capable of replacing its Parent Interface

**I - Interface Segregation**
- Interface is segragated as `IBankAccount` and `IMoneyTransfer` to seperate MinorBankAccount (which doesn't allow money transfer, only supports deposit and withdraw)
from StandardBankAccount.

**D - Dependency Inversion**
- In TransferMoney.Transfer method `IMoneyTransfer` is defined as parameter datatype
- In future, it supports all new classes which implements `IMoneyTransfer` interface
