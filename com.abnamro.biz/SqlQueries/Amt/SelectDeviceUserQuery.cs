using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Amt
{
    internal class SelectDeviceUserQuery : IDataQuery<IDataRow, DeviceUser>, IDataMapper<IDataRow, DeviceUser>
    {
        private enum InputParameterName
        {
            UserDeviceId
        }

        private enum OutputColumnName
        {
             DeviceId
            ,UserId
            ,GroupNumber
            ,IsoLanguageCode
            ,IsoCountryCode
            ,AmtServiceProviderId
            ,AquariusServiceCompanyId
        }

        public IDictionary<string, object> QueryParameters { get; private set; }
        string IDataQuery<IDataRow, DeviceUser>.Query => $@"
--declare @userDeviceId int = 1

declare @userId int = 
(
	select userId
	from UserDevices as userDevice
	where
	    userDevice.Id        = @{nameof(InputParameterName.UserDeviceId)}
	and userDevice.LockedOut = 0 
)

select
     distinct
     userDevice.DeviceId                as [{nameof(OutputColumnName.DeviceId)}]
    ,userDevice.UserId                  as [{nameof(OutputColumnName.UserId)}]
    ,userDevice.IsoCountryCode          as [{nameof(OutputColumnName.IsoCountryCode)}]
    ,userDevice.IsoLanguageCode         as [{nameof(OutputColumnName.IsoLanguageCode)}]
    ,clientGroups.ClientGroupNumber     as [{nameof(OutputColumnName.GroupNumber)}]
    ,acfEntity.Id                       as [{nameof(OutputColumnName.AmtServiceProviderId)}]
    ,acfEntity.ExternalServiceCompanyId as [{nameof(OutputColumnName.AquariusServiceCompanyId)}]
from 
(
	select
		 isNull(mappings.TargetAdministrationId, administrations.AdministrationId) as AdministrationId
	from
	(
		select
			distinct uaa.ClientId as [ClientId]
		from UsersAllowedAdministrations as uaa 
		where
			uaa.UserId = @userId 
		union
		select
			distinct agaa.ClientId as [ClientId]
		from       AdministrationGroupAllowedAdministrations as agaa
		inner join UserInAdministrationGroups                as uag  on uag.AdministrationGroupId = agaa.AdministrationGroupId
		where 
			uag.UserId = @userId
	) as UserAllowedClients
	inner join Administrations                 as administrations on administrations.ClientId = userAllowedClients.ClientId
	left  join MigrationAdministrationMappings as mappings        on mappings.SourceAdministrationId = administrations.AdministrationId
													             and mappings.IsActive = 1
) as UserAllowedAdministrations
inner join Administrations administations on administations.AdministrationId  = UserAllowedAdministrations.AdministrationId
                                         and administations.IsActive          = 1
inner join Clients         clients        on clients.ClientID                 = administations.ClientId
inner join ClientGroups    clientGroups   on clientGroups.ClientGroupId       = clients.ClientGroupId
inner join ACFEntities     acfEntity      on acfEntity.Id                     = clientGroups.ServiceProviderId
inner join AgreementTypes  agreementTypes on agreementTypes.AgreementTypeId   = acfEntity.CoreFactoringSystemTypeId
                                         and agreementTypes.AgreementTypeCode = 'Aquarius'
inner join UserDevices     userDevice     on userDevice.Id                    = @{nameof(InputParameterName.UserDeviceId)}
";

        IDataMapper<IDataRow, DeviceUser> IDataQuery<IDataRow, DeviceUser>.DataMapper => this;

        internal SelectDeviceUserQuery(UserDeviceId userDeviceId)
        {
            if (userDeviceId == default(UserDeviceId)) throw new ArgumentNullException(nameof(userDeviceId));
            if (userDeviceId.Value < 0) throw new ArgumentException($"Value-of-property {nameof(userDeviceId.Value)} must be greater than zero.", nameof(userDeviceId));

            QueryParameters = new Dictionary<string, object>
            {
                [nameof(InputParameterName.UserDeviceId)] = userDeviceId.Value,
            };
        }

        DeviceUser IDataMapper<IDataRow, DeviceUser>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return new DeviceUser(
                 dataRow.GetInt(nameof(OutputColumnName.UserId))
                ,dataRow.GetString(nameof(OutputColumnName.DeviceId))
                ,dataRow.GetString(nameof(OutputColumnName.IsoCountryCode))
                ,dataRow.GetString(nameof(OutputColumnName.IsoLanguageCode))
                ,dataRow.GetInt(nameof(OutputColumnName.GroupNumber))
                ,dataRow.GetInt(nameof(OutputColumnName.AmtServiceProviderId))
                ,dataRow.GetInt(nameof(OutputColumnName.AquariusServiceCompanyId)));
        }
    }
}
