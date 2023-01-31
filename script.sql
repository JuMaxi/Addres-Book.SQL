create Table Contact (
Id		int				identity,
Name	nvarchar(40)	not null,
Address nvarchar(40)	null,
Email	nvarchar(40)	null,
constraint PK_Contact primary key (Id)
)
go

create Table Phones (
ContactId 	int 			not null,
Kind		nvarchar(1)		not null,
Phones		nvarchar(20) 	not null,
)
go

