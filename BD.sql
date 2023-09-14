set ansi_nulls on
go
set ansi_padding on
go
set quoted_identifier on 
go

create database PraktikaSev
go

use PraktikaSev
go

create table UserRole
(
	ID_User_Role int not null identity(1,1),
	User_Role_Name varchar(50) not null,
	constraint PK_User_Role primary key clustered (ID_User_Role ASC) on [PRIMARY]
)

insert into UserRole values ('Администратор'), ('Пользователь')

create table [User]
(
	ID_User int not null identity(1,1),
	User_Login varchar(50) not null,
	User_Password varchar(50) not null,
	User_FName varchar(50) not null,
	User_LName varchar(50) not null,
	User_Role_ID int not null,
	Salt varchar(8) not null,
	constraint PK_User primary key clustered (ID_User ASC) on [PRIMARY],
	constraint FK_User_Role foreign key (User_Role_ID) references UserRole(ID_User_Role) on delete cascade
)

alter table [User] alter column Salt varchar(100) not null

select * from UserRole
select * from [User];

 

create table [Category]
(
	ID_Category int not null identity(1,1),
	Category_Name varchar(50) not null,
	constraint PK_Category primary key clustered (ID_Category ASC) on [PRIMARY]
)

create table [Position]
(
	ID_Position int not null identity(1,1),
	Position_Name varchar(50) not null,
	Position_Quantity int not null,
	Position_SLife date not null,
	Category_ID int not null,
	Photo_URL varchar(50) not null,
	IsExists bit not null,
	constraint PK_Position primary key clustered (ID_Position ASC) on [PRIMARY],
	constraint FK_Category foreign key (Category_ID) references Category(ID_Category) on delete cascade
)

insert into Position values ('Печенье дамские пальчики', 1, '10.06.2023', 1, 'a', 1, 1, 200)

alter table Position add Position_Price float not null

create table [Status]
(
	ID_Status int not null identity(1,1),
	Status_Name varchar(50) not null,
	constraint PK_Status primary key clustered (ID_Status ASC) on [PRIMARY]
)

create table [Order]
(
	ID_Order int not null identity(1,1),
	Order_Num varchar(7) not null,
	Order_Date date not null,
	[User_ID] int not null,
	Status_ID int not null,
	Order_Sum float not null,
	Delivery_ID int not null,
	constraint PK_Order primary key clustered (ID_Order ASC) on [PRIMARY],
	constraint FK_User foreign key ([User_ID]) references [User](ID_User),
	constraint FK_Status foreign key ([Status_ID]) references [Status](ID_Status),
	constraint FK_Delivery foreign key (Delivery_ID) references Delivery(ID_Delivery)
)


create table [Positions_In_Order]
(
	ID_Position_In_Order int not null identity(1,1),
	Position_ID int not null,
	Order_ID int not null,
	constraint PK_Positions_In_Order primary key clustered (ID_Position_In_Order ASC) on [PRIMARY],
	constraint FK_Position foreign key (Position_ID) references Position(ID_Position),
	constraint FK_Order foreign key (Order_ID) references [Order](ID_Order)
)

create table Delivery_Type
(
	ID_Delivery_Type int not null identity(1,1),
	Delivery_Type_Name varchar(50) not null,
	constraint PK_Delivery_Type primary key clustered (ID_Delivery_Type ASC) on [PRIMARY]
)

insert into Delivery_Type values ('Самовывоз'), ('Доставка курьером')

create table Delivery_Status
(
	ID_Delivery_Status int not null identity(1,1),
	Delivery_Status_Name varchar(50) not null,
	constraint PK_Delivery_Status primary key clustered (ID_Delivery_Status ASC) on [PRIMARY]
)

insert into Delivery_Status values ('В пути'), ('Ожидает выдачи')

create table Delivery
(
	ID_Delivery int not null identity(1,1),
	Delivery_Cost float not null,
	Delivery_Type_ID int not null,
	Delivery_Status_ID int not null,
	constraint PK_Delivery primary key clustered (ID_Delivery ASC) on [PRIMARY],
	constraint FK_Delivery_Type foreign key (Delivery_Type_ID) references Delivery_Type(ID_Delivery_Type),
	constraint FK_Delivery_Status foreign key (Delivery_Status_ID) references Delivery_Status(ID_Delivery_Status)
)

insert into Delivery values (0, 1, 1), (0, 1, 2), (200, 2, 1), (200, 2, 2)

create table Favorites
(
	ID_Favorites int not null identity(1,1),
	[User_ID] int not null,
	Position_ID int not null,
	constraint PK_Favorites primary key clustered (ID_Favorites ASC) on [PRIMARY],
	constraint FK_User_Favorite foreign key ([User_ID]) references [User](ID_User),
	constraint FK_Position_Favorite foreign key (Position_ID) references Position(ID_Position),
)

create table DiscountPercent
(
	ID_DiscountPercent int not null identity(1,1),
	Discount_Percent_Value varchar(2) not null,
	constraint PK_DiscountPercent primary key clustered (ID_DiscountPercent ASC) on [PRIMARY]
)

insert into DiscountPercent values ('10'), ('15'), ('20')

create table Discount
(
	ID_Discount int not null identity(1,1),
	DiscountPercent_ID int not null,
	Discount_Name varchar(50) not null,
	constraint PK_Discount primary key clustered (ID_Discount ASC) on [PRIMARY],
	constraint FK_DiscountPercent foreign key (DiscountPercent_ID) references DiscountPercent(ID_DiscountPercent)
)

insert into Discount values (1, 'Постоянная скидка'), (2, 'Разовая скидка'), (3, 'Постоянная скидка')

create table UsersDiscount
(
	ID_UserDiscount int not null identity(1,1),
	[User_ID] int not null,
	Discount_ID int not null,
	constraint PK_UsersDiscount primary key clustered (ID_UserDiscount ASC) on [PRIMARY],
	constraint FK_User_UserDiscount foreign key ([User_ID]) references [User](ID_User),
	constraint FK_Discount_UserDiscount foreign key (Discount_ID) references [Discount](ID_Discount)
)

create table Dealer
(
	ID_Dealer int not null identity(1,1),
	Dealer_Name varchar(50) not null,
	Dealer_Addr varchar(max) not null,
	constraint PK_Dealer primary key clustered (ID_Dealer ASC) on [PRIMARY]
)

insert into Dealer values ('Поставщик 1', 'Улица Пушкина дом Колотушкина'), ('Поставщик 2', 'Улица Колотушкина дом Пушкина')

alter table Position add constraint FK_Dealer foreign key(Dealer_ID) references Dealer(ID_Dealer)





create or alter view UsersData as
select [User].User_FName as 'Имя',
		[User].User_LName as 'Фамилия',
		[User].User_Login as 'Логин',
		[User].User_Password as 'Пароль',
		[UserRole].User_Role_Name as 'Роль пользователя',
from [User] inner join UserRole on [User].User_Role_ID = UserRole.ID_User_Role

select * from UsersData


create or alter view OrderData as 
select [Order].Order_Num as 'Номер заказа',
		[Order].Order_Date as 'Дата заказа',
		[Order].Order_Sum as 'Сумма заказа',
		[Status].Status_Name as 'Статус заказа',
		[Delivery_Status].Delivery_Status_Name as 'Статус доставки',
		[Delivery_Type].Delivery_Type_Name as 'Тип доставки'
from [Order] inner join Delivery on Delivery.ID_Delivery = [Order].Delivery_ID 
inner join [Status] on [Order].Status_ID = [Status].ID_Status
inner join Delivery_Status on Delivery.Delivery_Status_ID = Delivery_Status.ID_Delivery_Status
inner join Delivery_Type on Delivery.Delivery_Type_ID = Delivery_Type.ID_Delivery_Type

select * from OrderData



create or alter view DiscountData as
select [User].User_Login as 'Логин пользователя',
		[DiscountPercent].Discount_Percent_Value as 'Процент скидки',
		[Discount].Discount_Name as 'Тип скидки'
from [User] inner join UsersDiscount on [User].ID_User = UsersDiscount.[User_ID]
inner join Discount on Discount.ID_Discount = UsersDiscount.Discount_ID
inner join DiscountPercent on DiscountPercent.ID_DiscountPercent = Discount.DiscountPercent_ID

select * from DiscountData



create or alter view PositionData as
select [Position].Position_Name as 'Название товара',
		[Position].Photo_URL as 'Ссылка на фото',
		[Position].Position_Price as 'Цена товара',
		[Position].Position_Quantity as 'Количество товара',
		[Position].Position_SLife as 'До какой даты годен',
		[Category].Category_Name as 'Название категории товара',
		[Dealer].Dealer_Name as 'Поставщик товара',
		[Dealer].Dealer_Addr as 'Адрес поставщика товара'
from Position inner join Category on Position.Category_ID = Category.ID_Category
inner join Dealer on Dealer.ID_Dealer = Position.Dealer_ID

select * from PositionData



create or alter trigger User_Delete on [User]
after delete
as
begin
	delete from [Favorites]
	where [User_ID] in (select [ID_User] from deleted)

	delete from [UsersDiscount]
	where [User_ID] in (select [ID_User] from deleted)

	delete from Positions_In_Order
	where [Order_ID] in (select [ID_Order] from [Order] where [User_ID] in (select [ID_User] from deleted))

	delete from [Order]
	where [User_ID] in (select [ID_User] from deleted)
end



create or alter trigger Positions_In_Order_Edit on [Positions_In_Order]
after insert, update, delete
as
begin
	declare @OrderID int
	select @OrderID = [Order_ID] from inserted
	if @OrderID is null return

	update [Order]
	set Order_Sum = (
		select sum(Position_Price) 
		from Positions_In_Order 
		inner join Position on Positions_In_Order.Position_ID = Position.ID_Position)
		where [ID_Order] = @OrderID
end



create or alter trigger Order_Insert on [Order]
for insert
as
begin
	update [Order]
	set Status_ID = (select ID_Status from [Status] where Status_Name='Открыт')
	from [Order] inner join inserted on [Order].ID_Order = inserted.ID_Order
end


create or alter trigger Default_Role_ForUser on [User]
after insert
as
begin
	declare @DefaultRoleValue int = (select ID_User_Role from UserRole where [User_Role_Name] = 'Пользователь')

	update [User]
	set [User_Role_ID] = @DefaultRoleValue
	from [User] inner join inserted on inserted.ID_User = [User].ID_User
	where inserted.User_Role_ID is null
end

alter table Position alter column Photo_URL varchar(max) not null

insert into Position values ('Торт Красный Бархат', 1, '12-01-2024', 1, 'https://img1.russianfood.com/dycontent/images_upl/314/big_313572.jpg', 1, 1, 700)
