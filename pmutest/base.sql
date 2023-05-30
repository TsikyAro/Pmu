CREATE DATABASE terrain ;
use database terrain;

CREATE TABLE cheval (
    idcheval  INT IDENTITY(1,1) PRIMARY KEY,
    vitesse     Double precision,
    radiusX     INT,
    radiusY     INT,
    localX      INT,
    localY      INT,
    numeroCheval INT
);

CREATE TABLE endurence(
    idendurence INT IDENTITY(1,1) PRIMARY KEY,
    routes      double precision,
    endurence   double precision,
    idcheval  int,
    FOREIGN key (idcheval) REFERENCES (idcheval) cheval
);

CREATE TABLE tempArrive(
    idcheval int REFERENCES cheval (idcheval),
    tempArrive  int
);
insert into cheval(vitesse,radiusX,radiusY,localX,localY,numeroCheval) VALUES 
        (3,450,350,450,150,001),
        (3,425,325,350,150,002),
        (3,400,300,250,150,003);
        (3,375,275,150,150,004),
        (3,350,250,50,150,005),

insert into endurence(routes,endurence,idcheval) values 
        (70,-10,1008),
        (70,0,1009),
        (70,10,1010),
        (70,20,1011),
        (70,-20,1012);

create view intervalle as 
select idcheval,(temparrive - (select min(temparrive) minimun from temparrive)) intervalle from temparrive;