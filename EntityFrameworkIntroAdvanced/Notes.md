# Notes

## Entity Framework DBContext Entry() Method
* Entity Framework behind the scene has a change tracking mechanism which tracks the changes to object that it already knows.

* EF core dbContext Entry() method helps in getting entries form  the change tracker, this method gives access to all the change tracking information that entity framework gathers behing the scence.

* This change tracking behaviour of Entity Framwork is on by default, but in some cases we need to disable that because of performance reason.

* Every Entry in the change tracker has a state i:e Dettached (No idea of the entry), Added (Added something but now written to the db), Unchanged (written to the db but no changes that EF know), Modified (There are outstanding changes to update in the database), Deleted (the item has been removed from change tracker but not written to db) etc. 

* so above are the states which an entry can go through while working with entity framework


## Entity Framework - OriginalValues Property of Entry
* Originalvalue property points to the solution that EF have for change detection, The original value alwasy saves the original value that came form database and when we change something on plain old CLR Object (POCO) and whene we save changes it simple compairs the original value and the new calue with c# classes (in memory) and it there is any diffrence it comes to know that there is change and it build the update query to update the database

* when we ask entity framwork to give back a value which is not written to DB it gives value from its change tracker therefor unwritten changes are returned when we query the dbcontext objects

* Change tracking is connecte to single db context, so the whole Change tracker is not singelton acorss all data context but change tracker is specific to each datacontext


## Entity Framework - AsNoTracking() method
* By default entity framework, when query data, keeps information of data tracking as well which cose performance and momery, but in the scenario when we don't want to update data or just want to viw data we can disable change tracker using AsNoTracking() method which helps on performance ground


## Entity Framework - Support Usage of RawSQL instead of Linq
* Use FromSqlRaw() to execute completely constant sql query
* Use FromSqlInterpolated() to execute query having parameters
* use ExecuteSqlRawAsync to execute the statement that writes data but not return row like in sql query
* Never do code which leads to SQLInjection

## Entity Framework - Support Usage of Transactions
* Use BeginTransactionAsync() to initiate the transaction scope
* Use transaction.CommitAsync() to complete the transaction
* Use transaction.RollbackAsync() to rollback the transaction

## Entity Framework - Expression Concept
* Expression of Func of something mean that c# compiler is not generating machine language but its generating a so called object tree to describee the c# code you have written and tools like EF at runtime can have a look at this object tree and translate it to a diffrent language in our case to a SQL
