use PbdServer
go

drop table [opendatasource]
go
drop table [odsmetadata2]
go

create table [opendatasource] (id int identity,filename varchar(80), bron varchar(255))
go

create table [odsmetadata2] (
  id                   int identity
 ,ods_id               int
 ,FieldIndex           int
 ,FieldName            varchar(80)
 ,AverageFieldLength   int
 ,MaximumFieldLength   int
 ,MinimumFieldLength   int
 ,FieldType            char(20)
 ,FieldConsistenceList varchar(255)
 ,FieldIsConsistent    char(1)
 ,ConsistentDataType   char(20)
 ,DamerauValue         float)
go

create unique clustered index opendatasource_cux on [opendatasource] (id)
go
create unique clustered index odsmetadata2_cux on [odsmetadata2] (id)
go
create nonclustered index odsmetadata2_nx1 on [odsmetadata2] (ods_id)
go

select * from [opendatasource]
select * from [odsmetadata2]
go