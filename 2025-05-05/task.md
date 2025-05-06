# Task

**Case 1: A Simple Case**
•	A video store rents movies to members.
•	Each movie in the store has a title and is identified by a unique movie number.
•	A movie can be in VHS, VCD, or DVD format.
•	Each movie belongs to one of a given set of categories (action, adventure, comedy, ... )
•	The store has a name and a (unique) phone number for each member.
•	Each member may provide a favorite movie category (used for marketing purposes).
•	There are two types of members: 
	o	Golden Members:
	o	Bronze Members:
•	Using  their credit cards gold members can rent one or more movies and bronze members max. of one movie.  
•	A member may have a number of dependents (with known names).
•	Each dependent is allowed to rent one (1) movie at a time.


## My Approach

CatgoryMaster
-	CategoryId, Name

FormatMaster
-	FormatId, Name

MemberTypeMaster (Golden, Bronze, Dependent)
-	MemberTypeId, Type, max_rental_limit  

Movies
-	MovieNo, Name, CategoryId

Movies-Format
-	id, MovieNo, FormatId, Price, Availabile_Quantity

Users
-	UserId, Name, PhoneNo, FavMovieCategory, MemberTypeId, current_rental_count

User-Dependent
-	id, MemberID, DependentMemberID

Rentals
-	id, UserId, MovieFormatId, issue_date, return_date, Amount
