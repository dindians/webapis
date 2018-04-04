use [AMT_DEV]

/*
AMT_DEV:
user-code	|	group-number	|	group-name
[NL6344001]	|	[6344]			|	[Payper Intermediairs B.V.]
[NL5001001]	|	[5001]			|	[VCKG Holding B.V.]
[nl6645001]	|	[6645]			|	[ANTALIS FRANCE]
[nl6724001]	|	[6724]			|	[Czarnikow Group Ltd.]
*/

declare @userId int
declare @userCode nvarchar(50) = 'NL5001001'
select @userId = Id from Users where Code = @userCode
if not @userId is null
begin
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, '69e5c6035e086633', 'en', 'GB', '97531', 'HTC Sensation Z710e (Android 4.03 - API 15)')
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, 'cb1a463a307ad63f', 'fr', 'FR', '12345', 'Joao - Emulator Android 4.4 (API 19)')
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, 'af84ae55eded1960', 'en', 'GB', '12345', 'Gary - Emulator Android 4 (API 19)')
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, 'c3fbe17d670d954',  'en', 'GB', '12345', 'Gary - Emulator Android 6 (API 23)')
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, 'b4e43594c79c9607', 'en', 'GB', '12345', 'Joao - Emulator Android 4.4 (API 19)')
end

select @userCode = 'NL6344001'
select @userId = Id from Users where Code = @userCode
if not @userId is null
begin
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, 'dae330f9239caff9', 'nl', 'NL', '$2a$08$dHrZGr4mTS2MfCIcu2YO3O12ns2ImVIasA5MChXPn8jpJh7SKXdZW', '5" KitKat (4.4) XXHDPI Phone (Android 4.4 - API 19) Emuator')
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, 'f00f6f31e915ce8d', 'nl', 'NL', '12345', 'Ronald - Samsung')
end

select @userCode = 'nl6645001'
select @userId = Id from Users where Code = @userCode
if not @userId is null
begin
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, '54c86bd31c07f885', 'nl', 'NL', '12345', 'Albert - Samsung')
end

select @userCode = 'nl6724001'
select @userId = Id from Users where Code = @userCode
if not @userId is null
begin
	insert into [UserDevices] (UserId, DeviceId, IsoLanguageCode, IsoCountryCode, PincodeHash, [Description]) values (@userId, 'postman-client', 'nl', 'NL', '$2a$08$dHrZGr4mTS2MfCIcu2YO3O12ns2ImVIasA5MChXPn8jpJh7SKXdZW', 'test-account-for-postman-client [pwd=13579]')
end

