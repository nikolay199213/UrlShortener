create database UrlShortener

create table Urls
(
	Id serial Primary Key,
	TargetUrl CHARACTER VARYING(2000),
	ShortGuid CHARACTER VARYING(8)
)
CREATE INDEX short_guid_index ON Urls (shortguid);