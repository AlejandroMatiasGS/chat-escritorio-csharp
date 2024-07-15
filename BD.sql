create database chat_c#

use chat_c#

create table Usuario(
Id integer identity(1,1),
Nombre nvarchar(32) not null,
Correo nvarchar(255) not null,
Contrasena nvarchar(24) not null
)

alter table Usuario add constraint Usuario_PK primary key (Id)

create table Mensaje(
Id Integer identity(1,1),
Mensaje nvarchar(255) not null,
Emisor Integer not null,
Receptor Integer not null,
Leido bit not null
)

alter table Mensaje add constraint Mensaje_Usuario_Emisor foreign key (Emisor) references Usuario(Id)
alter table Mensaje add constraint Mensaje_Usuario_Receptor foreign key (Receptor) references Usuario(Id)

insert into Usuario values ('Alejandro', 'alejandro@gmail.com', 'dev123')
insert into Usuario values ('María', 'maria@gmail.com', 'dev123')
insert into Usuario values ('Almendra', 'almendra@gmail.com', 'dev123')
