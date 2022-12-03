create database Inventario

/*TABLA TIPOUSUARIO*/
create table TipoUsuario
(
	idTipo int identity(1,1) not null,
	tipo varchar(20) primary key not null,
	habilitado int not null
)

insert into TipoUsuario values ('Programador',1),
('Gerente',1),
('Jefe',1),
('Supervisor',1),
('Encargado',1);
select * from TipoUsuario



/*TABLA EMPLEADOS*/
create table Empleados
(
	idEmpleado int identity(1,1) primary key not null,
	nombres varchar(50) not null,
	apellidos varchar(50) not null,
	edad int not null,
	sexo varchar(10) not null,
	dui varchar(10) not null,
	nit varchar(17) not null,
	telefono varchar(8) null,
	celular varchar(8) null,
	direccion varchar(100) not null,
	habilitado int not null,
	usuarioAutorizado varchar(6) not null,
	fecha varchar(20) not null,
)

insert into Empleados values ('Diego Fernando','Martínez Oliva',21,'Masculino','05887524-7','0614-240999-128-0','76153847','25280639','Col. El Paraiso 4ta Calle Ote Casa #1-A',1,'DM5247','2020-11-17 18:52:00')
select * from Empleados



/*TABLA USUARIOS*/
create table Usuarios
(
	idUsuario int foreign key references Empleados on delete cascade not null,
	nombreCompleto varchar(100) not null,
	usuario varchar(6) primary key not null,
	contraseña varchar(20) not null,
	tipo varchar(20) foreign key references TipoUsuario on delete cascade not null,
	habilitado int not null,
	usuarioAutorizado varchar(6) not null,
	fecha varchar(20) not null
)

insert into Usuarios values (1,'Diego Fernando Martínez Oliva','DM5247','123456','Programador',1,'DM5247','2020-11-17 18:52:00')
select * from Usuarios



/*TABLA PRODUCTOS*/
create table Productos
(
	idProducto int identity(1,1) primary key not null,
	codigo varchar(20) not null,
	nombre varchar(50) not null,
	descripcion varchar(100) not null,
	cantidad int not null,
	precio float not null,
	usuarioAutorizado varchar(6) not null,
	fecha varchar(20) not null
)

insert into Productos values ('7401000705057','Jugo Tampico Punch 3755ml','Bebida sabor Naranja-Mandarina-Limon Cont. Neto 3755ml',1,1.89,'DM5247','2020-11-17 18:52:00')
select * from Productos



/*TABLA ENTRADA DE PRODUCTOS*/
create table EntradaProductos
(
	idEntrada int identity(1,1) primary key not null,
	codigo varchar(20) not null,
	nombre varchar(50) not null,
	cantidad int not null,
	usuarioAutorizado varchar(6) not null,
	fecha varchar(20) not null
)

select * from EntradaProductos



/*TABLA SALIDA DE PRODUCTOS*/
create table SalidaProductos
(
	idSalida int identity(1,1) primary key not null,
	codigo varchar(20) not null,
	nombre varchar(50) not null,
	cantidad int not null,
	sucursal varchar(75) not null,
	usuarioAutorizado varchar(6) not null,
	fecha varchar(20) not null
)

select * from SalidaProductos