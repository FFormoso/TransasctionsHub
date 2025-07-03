create table dbo.Global_Jobs
(
    ID              int identity
        primary key,
    Name            varchar(250) not null,
    IsRunning       bit          not null,
    Assembly        varchar(250) not null,
    TypeName        varchar(250) not null,
    LastStartDate   datetime,
    LastStopDate    datetime,
    MachineNameList varchar(100)
)
go