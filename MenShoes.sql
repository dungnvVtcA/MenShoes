drop database if exists MenShoes;

create database MenShoes;

use MenShoes ;

create table Users 
(
	User_id int primary key auto_increment ,
    User_name varchar(50) not null ,
    Phone int  ,
    User_Address varchar(100) ,
    AccountName varchar(50) not null unique,
    User_Password varchar(50) not null,
    Email varchar(50) ,
    User_Type int not null
    
    
    
);


create table Orders(
	Or_ID int primary key auto_increment,
    User_id int ,
    Or_Date datetime not null default now(),
    Or_Status int 
    );
    
    alter table Orders add constraint fk_Orders foreign key(User_id) references Users(User_id); 
Create table Trademark(
	TM_id int auto_increment primary key,
    TM_name varchar(50) not null,
    Origin varchar(50) not null 
);

Create table Shoes(
	Shoes_id int auto_increment primary key,
    TM_id int ,
    Price decimal(18,2) not null default 0 ,
    Material varchar(50) not null,
    Shoes_name varchar(50) not null ,
    Color varchar(20) not null,
    Size int not null ,
    Style varchar(50),
    Manufacturers varchar(100) ,
    Amount int not null default 0
);
alter table Shoes add constraint fk_Shoes foreign key(TM_id) references Trademark(TM_id); 

Create  table OrderDetail
(
    Or_ID int ,
    Shoes_id int ,
    Amount int ,
    Unitprice decimal(20,2)  default 0,
    OD_status int ,
    constraint pk_OrderDetail primary key(Or_ID, Shoes_id)
);


alter table OrderDetail add constraint fk_OrderDetail_Orders foreign key(Or_ID) references Orders(Or_ID);

alter table OrderDetail add constraint fk_OrderDetail_Shoes foreign key(Shoes_id) references Shoes(Shoes_id);



delimiter $$

create trigger tg_before_insert before insert

	on Shoes for each row

    begin

		if new.Amount < 0 then

            signal sqlstate '45001' set message_text = 'tg_before_insert: amount must > 0';

        end if;

    end $$
delimiter ;



insert into Users(User_name,Phone,User_Address,AccountName,User_Password,Email,User_Type) values

('Nguyen Van A',0976548987,'Ha Noi','Nguyenvana','A12345678','Anv@gmail.com',0),
('Nguyen Van B',0912348987,'Nha TRang','Nguyenvanb','B12345678','Bnv@gmail.com',0),
('Nguyen Van C',0976549999,'Sai Gon','Nguyenvanc','C12345678','Cnv@gmail.com',0);

insert into Users(User_name,AccountName,User_Password,User_Type)
values
('Nguyen Van dung','Nguyenvandung','dung12345678',1);

select *from Users;


insert into Trademark(TM_name,Origin) values

('OEM','China'),
('RINOS','Hồng kông'),
('HAPU','Việt Nam'),
('no','no');

select *from Trademark;

insert into Shoes(TM_id,Price,Shoes_name,Material,Size,Color,Manufacturers,Style,Amount) values

(1,150000.00,'Sneaker OEM','vải đan',41,'đen','tư nhân','thể thao thời trang',100),
(1,160000.00,'Sneaker OEM','vải đan',40,'xám','tư nhân','thể thao thời trang',100),
(1,160000.00,'Sneaker OEM','vải đan',40,'Xanh là thẫm','tư nhân','thể thao thời trang',100),
(4,130000.00,'thể thao 3FASHION','da pu + vải đan',41,'đen','tư nhân','Koria',100),
(4,130000.00,'thể thao 3FASHION','da pu + vải đan',41,'xanh đen','tư nhân','Koria',100),
(4,130000.00,'thể thao 3FASHION','da pu + vải đan',41,'hồng','tư nhân','Koria',100),
(2,101500.00,'Sneaker RINOS','lưới + cao su',41,'xám','CÔNG TY CỔ PHẦN MẠNG TRỰC TUYẾN META','thời trang',100),
(2,123000.00,'Sneaker RINOS','lưới +cao su',40,'đen','CÔNG TY CỔ PHẦN MẠNG TRỰC TUYẾN META','thời trang',200),
(3,189000.00,'CV CH2 cao cổ','vải tơ thô',36,'đỏ','công ty cổ phần  HAPULICO','Công sở',50),
(3,189000.00,'CV CH2 cao cổ','vải tơ thô',40,'trắng','công ty cổ phần  HAPULICO','Công sở',30),
(3,189000.00,'CV CH2 cao cổ','vải tơ thô',41,'đen','công ty cổ phần  HAPULICO','Công sở',80);
 select *from Shoes;
 
select *from orderdetail;


 select LAST_INSERT_ID();
 
 create user if not exists  'Staff'@'localhot' identified by 'staff123';
grant  all on Users to 'Staff'@'localhot';
grant all on Orders to 'Staff'@'localhot';
grant all on OrderDetail to  'Staff'@'localhot';
grant all on MenShoes.* to 'Staff'@'localhot';
select *from Orders;
select *from OrderDetail inner join Orders on OrderDetail.Or_ID= Orders.Or_ID where User_id = 2 and OD_status = 2;
select Price from Shoes where Shoes_id = 3;












