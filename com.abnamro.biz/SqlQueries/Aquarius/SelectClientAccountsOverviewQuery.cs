using System;
using System.Collections.Generic;
using com.abnamro.agents;
using com.abnamro.datastore;

namespace com.abnamro.biz.SqlQueries.Aquarius
{
    internal class SelectClientAccountsOverviewQuery : IDataQuery<IDataRow, ClientAccountOverview>, IDataMapper<IDataRow, ClientAccountOverview>
    {
        private enum InputParameterName
        {
              GroupNumber
            , ServiceCompanyId
        }
        private enum OutputColumnName
        {
              Id
            , ClientNumber
            , AgreementNumber
            , ClientAccountName
            , CurrencyCode
            , Availability
            , AgreementTypeDescription
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, ClientAccountOverview>.Query => $@"
--declare @groupNumber int = 6645
--declare @ServiceCompanyId bigint = 119

declare @clientGroupNumber nvarchar(7) = format(@{nameof(InputParameterName.GroupNumber)}, '000000#')

declare @currentAccountingDate datetime = 	
(
	select
		accountingLocation.CurrentAccountingDate
	from       AccountingLocation as accountingLocation 
	inner join Party              as  serviceCompanyParty on serviceCompanyParty.AccountingLocationId = accountingLocation.Id
	where 
		serviceCompanyParty.ID = @{nameof(InputParameterName.ServiceCompanyId)}
)

declare @clientAccountTable table
(
	  AgreementId        decimal(10,0) not null
	 ,ClientNumber       nvarchar(7) null
	 ,AgreementNumber    nvarchar(3) null
	 ,CurrencyId         decimal (10,0) null
	 ,AgreementTypeId    decimal (10,0) null
	 ,TermsOfAgreementId decimal (10,0) null
	 ,RelationShipId     decimal (10,0) null
	 ,AgreementTitle     nvarchar(50) null
)

insert into @clientAccountTable (AgreementId, ClientNumber, AgreementNumber, CurrencyId, AgreementTypeId, TermsOfAgreementId, RelationShipId, AgreementTitle) 
	select 
          clientAccountAgreement.Id
		 ,clientAccountAgreement.ClientNumber
		 ,clientAccountAgreement.AgreementNumber
		 ,clientAccountAgreement.CurrencyId
		 ,clientAccountAgreement.AgreementTypeId
         ,clientAccountAgreement.TermsOfAgreementId
		 ,clientAccountAgreement.RelationShipId
		 ,clientAccountAgreement.AgreementTitle
	from       Agreement              as clientAccountAgreement
	left join  AggregationAgreementSa as aggregationAgreementSa on aggregationAgreementSa.ServiceAgreementId = clientAccountAgreement.Id 
				                                               and aggregationAgreementSa.EndDate is null
	inner join Agreement              as clientGroupAgreement   on clientGroupAgreement.Id = aggregationAgreementSa.AggregationAgreementId
	where  
        clientAccountAgreement.OjbConcreteClass = 'aquarius.agreement.ServiceAgreement'
    and clientAccountAgreement.ServiceCompanyId = @{nameof(InputParameterName.ServiceCompanyId)}
	and clientGroupAgreement.ClientNumber       = @clientGroupNumber

select
	 cast(clientAccountDetails.AgreementId as bigint)                            as [{nameof(OutputColumnName.Id)}]
	,clientAccountDetails.[Name]                                                 as [{nameof(OutputColumnName.ClientAccountName)}]
	,cast(clientAccountDetails.ClientNumber as int)                              as [{nameof(OutputColumnName.ClientNumber)}]
	,cast(clientAccountDetails.AgreementNumber as int)                           as [{nameof(OutputColumnName.AgreementNumber)}]
	,clientAccountDetails.CurrencyCode                                           as [{nameof(OutputColumnName.CurrencyCode)}]
	,clientAccountDetails.AgreementTypeDescription                               as [{nameof(OutputColumnName.AgreementTypeDescription)}]
    ,cast(round((
			     (clientAccountDetails.SalesLedger - clientAccountDetails.FundingDisapproved - clientAccountDetails.ConcentrationRetention) * (clientAccountDetails.AdvanceRateOr100/100 )
			   - (case when clientAccountDetails.MoneyInTransit >= 0 then 0 else abs(clientAccountDetails.MoneyInTransit) end) 
			   - (abs(clientAccountDetails.ReserveFundNonFIU) + abs(clientAccountDetails.ReserveFundNonFIUNonCB) + clientAccountDetails.PendingReserveFundPayments)
			    )
		      - (
                 clientAccountDetails.FundsInUse + clientAccountDetails.PendingOutpayments + abs(clientAccountDetails.ServiceProviderGuarantees)
		        ),2) as money)                                                   as [{nameof(OutputColumnName.Availability)}]
from
(
	select
	     clientAccountTable.AgreementId                                                                            as [AgreementId]
		,clientAccountTable.ClientNumber                                                                           as [ClientNumber]
		,clientAccountTable.AgreementNumber                                                                        as [AgreementNumber]
		,clientAccountCurrency.CurrencyCode                                                                        as [CurrencyCode]
        ,isNull(isNull(clientAccountTable.AgreementTitle, _agreementPartyDetails.PartyName), _partyName.PartyName) as [Name]
		,isNull(clientAccountAdvanceRate.AdvanceRate,100)                                                          as [AdvanceRateOr100]
		,isNull(clientAccountPendingOutPayments.PendingOutpayments,0)                                              as [PendingOutpayments]
		,isNull(clientAccountPendingReserveFundPayments.PendingReserveFundPayments,0)                              as [PendingReserveFundPayments]
		,isNull(clientAccountBalance.ConcentrationRetention,0)                                                     as [ConcentrationRetention]
		,isNull(clientAccountBalance.FundingDisapproved,0)                                                         as [FundingDisapproved]
		,isNull(clientAccountBalance.FundsInUse,0)                                                                 as [FundsInUse]
		,isNull(clientAccountBalance.MoneyInTransit,0)                                                             as [MoneyInTransit]
		,isNull(clientAccountBalance.ReserveFundNonFIU,0)                                                          as [ReserveFundNonFIU]
		,isNull(clientAccountBalance.ReserveFundNonFIUNonCB,0)                                                     as [ReserveFundNonFIUNonCB]
		,isNull(clientAccountBalance.ServiceProviderGuarantees,0)                                                  as [ServiceProviderGuarantees]
		,isNull(clientAccountBalance.SalesLedger,0)                                                                as [SalesLedger]
		,isNull(clientAccountDescription.AgreementTypeDescription,'unknown-agreement-type')                        as [AgreementTypeDescription]
	from 
			   @clientAccountTable as clientAccountTable
	inner join Currency            as clientAccountCurrency on clientAccountCurrency.Id = clientAccountTable.CurrencyId
    -- alternative client account name stems from  agreement party details or party name.
    inner join RelationShip _relationShip                   on _relationShip.Id = clientAccountTable.RelationShipId 
                                                           and _relationShip.EndDate is null
    inner join [Role] _role                                 on _role.Id = _relationShip.RoleBid 
    inner join Party _party                                 on _party.Id = _role.PartyId
    inner join PartyName _partyName                         on _partyName.PartyId = _party.Id
                                                           and _partyName.enddate is null
		    											   and _partyName.NameTypeCode = '0001' -- 0001 = main name; 0120 = Postname
    left join AgreementPartyDetails _agreementPartyDetails  on _agreementPartyDetails.AgreementId = clientAccountTable.AgreementId
	left join
	(
		select 
			 _agreementType.Id    as [AgreementTypeId]
			,_translation.[Value] as [AgreementTypeDescription]
		from      AgreementType      _agreementType
		left join TranslatableString _translation  on _translation.StringId  = _agreementType.[Description]
                                                  and _translation.LanguageId = 0
	) as clientAccountDescription on clientAccountDescription.AgreementTypeId = clientAccountTable.AgreementTypeId
	-- client account advance rate
	left join (select
					 _clientAccountTable.AgreementId           as ClientAccountAgreementId
					,_advanceConditionAvailability.AdvanceRate as AdvanceRate
				from       @clientAccountTable                 as _clientAccountTable 
				inner join AgreementCondition                  as _clientAccountAgreementCondition on _clientAccountAgreementCondition.TermsOfAgreementId = _clientAccountTable.TermsOfAgreementId
				inner join ConditionAvailability               as _advanceConditionAvailability    on _advanceConditionAvailability.ConditionSettingId = _clientAccountAgreementCondition.CurrentConditionSettingId
				inner join ConditionType                       as _conditionType                   on _conditionType.Id = _advanceConditionAvailability.ConditionTypeId 
																					              and _conditionType.BaseCode      = '5120' 
																			      	              and _conditionType.StandardCode  = '0100' 
																					              and _conditionType.VariationCode = '0100'
				where
				not _clientAccountTable.TermsOfAgreementId is null
				and _advanceConditionAvailability.StatusIndicator in (0,14)
				and _advanceConditionAvailability.OjbConcreteClass = 'aquarius.agreement.ConditionAdvances'
				and _advanceConditionAvailability.EffectiveDate <= @currentAccountingDate
				and (_advanceConditionAvailability.EndDate > @currentAccountingDate or _advanceConditionAvailability.EndDate is null)
	) as clientAccountAdvanceRate on clientAccountAdvanceRate.ClientAccountAgreementId = clientAccountTable.AgreementId
	-- client account pending outpayments     
	left join (select
					_clientAccountTable.AgreementId                                as ClientAccountAgreementId
					,Round(Sum(isNull(_outPaymentSource.SourceOrigCurrValue,0)),5) as PendingOutpayments
				from      @clientAccountTable                                      as _clientAccountTable
				left join OutPayment                                               as _outPayment       on _outPayment.AgreementId = _clientAccountTable.AgreementId
																                                       and _outPayment.IsPending =  1 
																                                       and _outPayment.CategoryCode in ('0010', '0031', '0030')
																                                       and _outPayment.StatusCode <>  '0270'  
																                                       and _outPayment.StatusCode <>  '0280'  
																                                       and (_outPayment.ExpiryDate is null or (_outPayment.ExpiryDate >= @currentAccountingDate)) 
				left join OutPaymentSource                                         as _outPaymentSource on _outPaymentSource.OutPaymentId = _outPayment.Id
																                                       and _outPaymentSource.SourceOfFunds in (0, 4, 5, 6)
				group by _clientAccountTable.AgreementId
	) as clientAccountPendingOutPayments on clientAccountPendingOutPayments.ClientAccountAgreementId = clientAccountTable.AgreementId
	-- client account pending reserve fund payments
	left join (select 
					_clientAccountTable.AgreementId                                as ClientAccountAgreementId
					,Round(Sum(isNull(_outPaymentSource.SourceOrigCurrValue,0)),5) as PendingReserveFundPayments
				from      @clientAccountTable                                      as _clientAccountTable
				left join OutPayment                                               as _outPayment       on _outPayment.AgreementId = _clientAccountTable.AgreementId
																	                                   and _outPayment.IsPending = 1 
																	                                   and _outPayment.CategoryCode in ('0010', '0031', '0030')
																	                                   and _outPayment.StatusCode <>  '0270'  
																	                                   and _outPayment.StatusCode <>  '0280'  
																	                                   and (_outPayment.ExpiryDate is null or (_outPayment.ExpiryDate >= @currentAccountingDate)) 
				left join OutPaymentSource                                         as _outPaymentSource on _outPaymentSource.OutPaymentId = _outPayment.Id
																	                                   and _outPaymentSource.SourceOfFunds = 1
				group by _clientAccountTable.AgreementId
	) as clientAccountPendingReserveFundPayments on clientAccountPendingReserveFundPayments.ClientAccountAgreementId = clientAccountTable.AgreementId
	-- client account balance 
	left join(select
				_clientAccountTable.AgreementId                                                                                             as ClientAccountAgreementId
				,sum(case when (_accountType.BaseCode = '1010' and _accountType.StandardCode = '0100')
							or (_accountType.BaseCode = '3150' and _accountType.StandardCode = '0200') then _balance.BalanceSac else 0 end) as SalesLedger 
				,sum(case when  _accountType.BaseCode = '2010' and _accountType.StandardCode = '0100'  then _balance.BalanceSac else 0 end) as FundingDisapproved
				,sum(case when  _accountType.BaseCode = '3520' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as ConcentrationRetention
				,sum(case when  _accountType.BaseCode = '3130' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as MoneyInTransit
				,sum(case when  _accountType.BaseCode = '3020' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as FundsInUse
				,sum(case when  _accountType.BaseCode = '3100' and _accountType.StandardCode = '0310'  then _balance.BalanceSac else 0 end) as ReserveFundNonFIU
				,sum(case when  _accountType.BaseCode = '3100' and _accountType.StandardCode = '0320'  then _balance.BalanceSac else 0 end) as ReserveFundNonFIUNonCB
				,sum(case when  _accountType.BaseCode = '3110' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as ServiceProviderGuarantees
			from       Balance             as _balance 
			inner join Account             as _account            on _balance.AccountId = _account.Id
			inner join AccountType         as _accountType        on _account.AccountTypeId = _accountType.Id
			inner join @clientAccountTable as _clientAccountTable on _clientAccountTable.AgreementId = _account.AgreementId
			where 
				( _balance.BalanceStartDate <= @currentAccountingDate)
			and (   (_balance.BalanceEndDate > @currentAccountingDate) 
				 or (_balance.BalanceEndDate is null))
			group by _clientAccountTable.AgreementId
	) as clientAccountBalance on clientAccountBalance.ClientAccountAgreementId = clientAccountTable.AgreementId
) as clientAccountDetails
order by [{nameof(OutputColumnName.ClientNumber)}], [{nameof(OutputColumnName.AgreementNumber)}]
";

        internal SelectClientAccountsOverviewQuery(GroupNumberKeyForAquarius groupNumberKeyForAquarius)
        {
            if (groupNumberKeyForAquarius == default(GroupNumberKeyForAquarius)) throw new ArgumentNullException(nameof(groupNumberKeyForAquarius));

            QueryParameters = new Dictionary<string, object>
                {
                      [nameof(InputParameterName.GroupNumber)] = groupNumberKeyForAquarius.GroupNumber
                    , [nameof(InputParameterName.ServiceCompanyId)] = groupNumberKeyForAquarius.AquariusServiceCompanyId
                };
        }

        IDataMapper<IDataRow, ClientAccountOverview> IDataQuery<IDataRow, ClientAccountOverview>.DataMapper => this;

        ClientAccountOverview IDataMapper<IDataRow, ClientAccountOverview>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return new ClientAccountOverview(
                        dataRow.GetLong(nameof(OutputColumnName.Id))
                    , dataRow.GetInt(nameof(OutputColumnName.ClientNumber))
                    , dataRow.GetInt(nameof(OutputColumnName.AgreementNumber))
                    , dataRow.GetString(nameof(OutputColumnName.ClientAccountName))
                    , dataRow.GetString(nameof(OutputColumnName.CurrencyCode))
                    , dataRow.GetDecimal(nameof(OutputColumnName.Availability))
                    , dataRow.GetString(nameof(OutputColumnName.AgreementTypeDescription)));
        }
    }
}
