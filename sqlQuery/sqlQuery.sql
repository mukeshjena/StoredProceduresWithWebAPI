create table TestEmployee(
	Id int identity(1,1) primary key,
	Name varchar(100),
	Age int,
	Active int)

select * from TestEmployee;

--insert new record
create proc usp_AddEmployee(
	@Name varchar(100),
	@Age int,
	@Active int)
	as
	Begin
		Insert into TestEmployee(
			Name,
			Age,
			Active)
		values(
			@Name,
			@Age,
			@Active);
	End;
--select all
create proc usp_GetAllEmployees
as
Begin
	select * from TestEmployee;
End
--select by id
create proc usp_GetEmployeeById(@Id int)
as
Begin
	select * from TestEmployee where Id = @Id;
End
--update existing record
create proc usp_UpdateEmployee(
	@Id int,
	@Name varchar(100),
	@Age int,
	@Active int)
as
Begin
	update TestEmployee set Name   = @Name,
							Age    = @Age,
							Active = @Active
						where Id = @Id;
End
--delete existing record
create proc usp_DeleteEmployee(@Id int)
as
Begin
	delete from TestEmployee where Id = @Id;
End