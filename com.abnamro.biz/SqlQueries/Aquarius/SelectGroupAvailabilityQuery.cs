using com.abnamro.agents;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Aquarius
{
    internal class SelectGroupAvailabilityQuery : IDataQuery<IDataRow, GroupAvailabilityData>, IDataMapper<IDataRow, GroupAvailabilityData>
    {
        private enum InputParameterName
        {
              GroupNumber
            , ServiceCompanyId
        }

        private enum OutputColumnName
        {
              AgreedOverpayment
            , Availability
            , CurrencyCode
            , FundsInUse
            , GroupName
            , MaxCreditFacility
            , Retentions
            , SalesLedger
            , ServiceProviderGuarantees
        }

        private readonly int _groupNumber;

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, GroupAvailabilityData>.Query => $@"
--declare @__in_GroupNumber int = 5036
--declare @__in_ServiceCompanyId bigint = 119

declare @clientGroupNumber nvarchar(7) = format(@{nameof(InputParameterName.GroupNumber)}, '000000#')

declare @serviceCompanyCurrencyCode nvarchar(3) = 
(
	select currency.CurrencyCode
	from       Currency                  as currency
	inner join CurrencyExchangeRateTable as currencyExchangeRateTable on currencyExchangeRateTable.BaseCurrencyForTableId = currency.Id
	inner join Party                     as serviceCompanyParty on serviceCompanyParty.RateTableForScId = currencyExchangeRateTable.Id
	where 
		serviceCompanyParty.Id = @{nameof(InputParameterName.ServiceCompanyId)}
)

declare @currentAccountingDate datetime = 	
(
	select
		accountingLocation.CurrentAccountingDate
	from       AccountingLocation as accountingLocation 
	inner join Party              as  serviceCompanyParty on serviceCompanyParty.AccountingLocationId = accountingLocation.Id
	where 
		serviceCompanyParty.ID = @{nameof(InputParameterName.ServiceCompanyId)}
)

declare @clientGroupAgreementId decimal
declare @clientGroupTermsOfAgreementId decimal
declare @clientGroupCurrencyId decimal
declare @groupDivideByRateToBaseCurrency decimal(13,6)

select 
   @clientGroupAgreementId          = clientGroupAgreement.Id
  ,@clientGroupCurrencyId           = clientGroupAgreement.CurrencyId
  ,@clientGroupTermsOfAgreementId   = clientGroupAgreement.TermsOfAgreementId
  ,@groupDivideByRateToBaseCurrency = case when currencyConversionRate.IsConversionByDivision = 1 then currencyConversionRate.Rate / power(10,currencyConversionRate.Quote)
			                               else  1/(currencyConversionRate.Rate / power(10,currencyConversionRate.Quote)) 
		                              end
from  Agreement                      as clientGroupAgreement
inner join CurrencyConversionRate    as currencyConversionRate    on currencyConversionRate.ConvertFromCurrencyId = clientGroupAgreement.CurrencyId
inner join CurrencyExchangeRateTable as currencyExchangeRateTable on currencyExchangeRateTable.Id                 = currencyConversionRate.RateTableForRateId
inner join Party                     as serviceCompanyParty       on serviceCompanyParty.RateTableForScId         = currencyExchangeRateTable.Id
where  
    clientGroupAgreement.OjbConcreteClass = 'aquarius.agreement.AggregationAgreement'
and clientGroupAgreement.ServiceCompanyId = @{nameof(InputParameterName.ServiceCompanyId)}
and clientGroupAgreement.ClientNumber     = @clientGroupNumber
and currencyConversionRate.EndDate        is null
and serviceCompanyParty.Id                = @{nameof(InputParameterName.ServiceCompanyId)}

declare @clientAccountTable table
(
	  AgreementId decimal not null
	 ,CurrencyId  decimal not null
	 ,DivideByRateToBaseCurrency decimal(13,6)
	 ,TermsOfAgreementId decimal null
)

insert into @clientAccountTable (AgreementId, CurrencyId, DivideByRateToBaseCurrency, TermsOfAgreementId) 
	select 
          clientAccountAgreement.Id
		 ,clientAccountAgreement.CurrencyId
		 ,case 
			when currencyConversionRate.IsConversionByDivision = 1 then currencyConversionRate.Rate / power(10,currencyConversionRate.Quote)
			else  1/(currencyConversionRate.Rate / power(10,currencyConversionRate.Quote)) 
		  end
		 ,clientAccountAgreement.TermsOfAgreementId
	from       Agreement                 as clientAccountAgreement
	left join  AggregationAgreementSa    as aggregationAgreementSa    on aggregationAgreementSa.ServiceAgreementId    = clientAccountAgreement.Id 
				                                                     and aggregationAgreementSa.EndDate               is null
	inner join CurrencyConversionRate    as currencyConversionRate    on currencyConversionRate.ConvertFromCurrencyId = clientAccountAgreement.CurrencyId
    inner join CurrencyExchangeRateTable as currencyExchangeRateTable on currencyExchangeRateTable.Id                 = currencyConversionRate.RateTableForRateId
    inner join Party                     as serviceCompanyParty       on serviceCompanyParty.RateTableForScId         = currencyExchangeRateTable.Id
	where  
	    aggregationAgreementSa.AggregationAgreementId = @clientGroupAgreementId
    and currencyConversionRate.EndDate                is null
    and serviceCompanyParty.Id                        = @{nameof(InputParameterName.ServiceCompanyId)}

declare @clientGroupName nvarchar(70) = 
(
	select isNull(isNull(agreement.AgreementTitle, agreementPartyDetails.PartyName), partyName.PartyName) as ClientgroupName
    from       Agreement            as agreement
    inner join RelationShip         as agreementRelationShip on agreementRelationShip.Id      = agreement.RelationShipId
				                                            and agreementRelationShip.EndDate is null
    inner join [Role]               as relationShipRole      on relationShipRole.Id           = agreementRelationShip.RoleBId
    inner join Party                as party                 on party.Id                      = relationShipRole.PartyId
    inner join PartyName            as partyName             on party.Id                      = partyName.PartyId 
				                                            and partyName.EndDate             is null 
							                                and partyName.NameTypeCode        = '0001' -- 0001 = main name // 0120 = Postname
    left join AgreementPartyDetails as agreementPartyDetails on agreement.Id                  = agreementPartyDetails.AgreementId
	where
		agreement.Id = @clientGroupAgreementId
)

declare @clientGroupMaxCreditFacility money = 
(
	select
	   cast(round(_conditionLimit.Amount / @groupDivideByRateToBaseCurrency,2) as money)
	from       AgreementCondition as _agreementCondition
	inner join ConditionLimit     as _conditionLimit       on _conditionLimit.ConditionSettingId = _agreementCondition.CurrentConditionSettingId
	                                                      and _conditionLimit.StatusIndicator    in (0,14)
	                                                      and _conditionLimit.OjbConcreteClass   = 'aquarius.agreement.ConditionLimit'
	                                                      and _conditionLimit.EffectiveDate     <= @currentAccountingDate
	                                                      and (_conditionLimit.EndDate           > @currentAccountingDate or _conditionLimit.EndDate is null)
	inner join ConditionType      as _conditionType        on _conditionType.Id                  = _conditionLimit.ConditionTypeId 
													      and _conditionType.BaseCode            = '8040' 
													      and _conditionType.StandardCode        = '0100' 
													      and _conditionType.VariationCode       = '0100'
	where
		_agreementCondition.TermsOfAgreementId = @clientGroupTermsOfAgreementId
)

declare @agreedOverpaymentAmount money =
(
	select
	   cast(round(_conditionAvailability.IncreaseValue / @groupDivideByRateToBaseCurrency,2) as money)
	from       AgreementCondition    as _agreementCondition 
	inner join ConditionAvailability as _conditionAvailability on _conditionAvailability.ConditionSettingId  = _agreementCondition.CurrentConditionSettingId
															  and _conditionAvailability.StatusIndicator     in (0, 14)
															  and _conditionAvailability.OjbConcreteClass     = 'aquarius.agreement.ConditionPaymentOverAdvance'
															  and _conditionAvailability.EffectiveDate       <=  @currentAccountingDate  
															  and (_conditionAvailability.EndDate             > @currentAccountingDate or _conditionAvailability.EndDate is null)
	inner join ConditionType         as _conditionType         on _conditionType.Id                           = _conditionAvailability.ConditionTypeId
															  and _conditionType.BaseCode                     = '5040' 
															  and _conditionType.StandardCode                 = '0100' 
															  and _conditionType.VariationCode                = '0100'
	where
		_agreementCondition.TermsOfAgreementId = @clientGroupTermsOfAgreementId
)

select 
	 @clientGroupName                                                  as [{nameof(OutputColumnName.GroupName)}]
	,@serviceCompanyCurrencyCode                                       as [{nameof(OutputColumnName.CurrencyCode)}]
	,@clientGroupMaxCreditFacility                                     as [{nameof(OutputColumnName.MaxCreditFacility)}]
	,isNull(@agreedOverpaymentAmount, 0)                               as [{nameof(OutputColumnName.AgreedOverpayment)}]
	,sum(clientAccountAvailabilityDetails.SalesLedger)                 as [{nameof(OutputColumnName.SalesLedger)}]
	,sum(clientAccountAvailabilityDetails.Retentions)                  as [{nameof(OutputColumnName.Retentions)}]
	,sum(clientAccountAvailabilityDetails.FundsInUse)                  as [{nameof(OutputColumnName.FundsInUse)}]
	,sum(clientAccountAvailabilityDetails.[Availability])              as [{nameof(OutputColumnName.Availability)}]
	,sum(clientAccountAvailabilityDetails.[ServiceProviderGuarantees]) as [{nameof(OutputColumnName.ServiceProviderGuarantees)}]
from
(
	select
		 cast(round(clientAccountDetails.SalesLedger / DivideByRateToBaseCurrency,2) as money)                      as [SalesLedger]
		,cast(round((clientAccountDetails.FundingDisapproved
				   + clientAccountDetails.ConcentrationRetention
				   + (((100 - clientAccountDetails.AdvanceRateOr100) / 100) * (clientAccountDetails.SalesLedger - clientAccountDetails.FundingDisapproved - clientAccountDetails.ConcentrationRetention))
				   + clientAccountDetails.PendingPayments
				   + abs((select min(x) from (values (0), (clientAccountDetails.MoneyInTransit)) as value(x)))
				   + abs(clientAccountDetails.ReserveFundNonFIUNonCB)
				   + abs(clientAccountDetails.ReserveFundNonFIU)
				   + abs(clientAccountDetails.ServiceProviderGuarantees)) / DivideByRateToBaseCurrency,2) as money) as [Retentions]
		,cast(round(clientAccountDetails.FundsInUse / DivideByRateToBaseCurrency,2) as money)                       as [FundsInUse]
		,abs(cast(round(clientAccountDetails.ServiceProviderGuarantees / DivideByRateToBaseCurrency,2) as money))   as [ServiceProviderGuarantees]
		,cast(round(
			((-- expressionA = ((SalesLedgerBalance - FundingDisapprovedBalance - ConcentrationRetention) * (ConditionAdvanceRate / 100)) - MITCreditBalanceOnly - (ReserveFundNonFIU + ReserveFundNonFIUNonCB)
				(clientAccountDetails.SalesLedger - clientAccountDetails.FundingDisapproved - clientAccountDetails.ConcentrationRetention) * (clientAccountDetails.AdvanceRateOr100/100 )
			  - (case when clientAccountDetails.MoneyInTransit >= 0 then 0 else abs(clientAccountDetails.MoneyInTransit) end) 
			  - (abs(clientAccountDetails.ReserveFundNonFIU) + abs(clientAccountDetails.ReserveFundNonFIUNonCB))
			 )
		   - ( -- expressionB = (FundsInUse + PendingPayments + ServiceProviderGuarantees)
				clientAccountDetails.FundsInUse + clientAccountDetails.PendingPayments + abs(clientAccountDetails.ServiceProviderGuarantees)
			 )) / DivideByRateToBaseCurrency,2) as money)                                                           as [Availability]
	from
	(
		select
			 clientAccountTable.DivideByRateToBaseCurrency            as [DivideByRateToBaseCurrency]
			,isNull(clientAccountAdvanceRate.AdvanceRate,100)         as [AdvanceRateOr100]
			,isNull(paymentDetails.PendingPayments,0)                 as [PendingPayments]
			,isNull(clientAccountBalance.ConcentrationRetention,0)    as [ConcentrationRetention]
			,isNull(clientAccountBalance.FundingDisapproved,0)        as [FundingDisapproved]
			,isNull(clientAccountBalance.FundsInUse,0)                as [FundsInUse]
			,isNull(clientAccountBalance.MoneyInTransit,0)            as [MoneyInTransit]
			,isNull(clientAccountBalance.ReserveFundNonFIU,0)         as [ReserveFundNonFIU]
			,isNull(clientAccountBalance.ReserveFundNonFIUNonCB,0)    as [ReserveFundNonFIUNonCB]
			,isNull(clientAccountBalance.ServiceProviderGuarantees,0) as [ServiceProviderGuarantees]
			,isNull(clientAccountBalance.SalesLedger,0)               as [SalesLedger]
		from 
			       @clientAccountTable as clientAccountTable
		-- client account advance rate
		left join (select
						 _clientAccountTable.AgreementId           as ClientAccountgreementId
						,_advanceConditionAvailability.AdvanceRate as AdvanceRate
				   from       @clientAccountTable    as _clientAccountTable 
				   inner join AgreementCondition     as _clientAccountAgreementCondition on _clientAccountAgreementCondition.TermsOfAgreementId = _clientAccountTable.TermsOfAgreementId
				   inner join ConditionAvailability  as _advanceConditionAvailability    on _advanceConditionAvailability.ConditionSettingId = _clientAccountAgreementCondition.CurrentConditionSettingId
				   inner join ConditionType          as _conditionType                   on _conditionType.Id = _advanceConditionAvailability.ConditionTypeId 
																					    and _conditionType.BaseCode      = '5120' 
																			      	    and _conditionType.StandardCode  = '0100' 
																					    and _conditionType.VariationCode = '0100'
				   where
						not _clientAccountTable.TermsOfAgreementId    is null
				   and 	_advanceConditionAvailability.StatusIndicator in (0,14)
				   and _advanceConditionAvailability.OjbConcreteClass  = 'aquarius.agreement.ConditionAdvances'
				   and _advanceConditionAvailability.EffectiveDate    <= @currentAccountingDate
				   and (_advanceConditionAvailability.EndDate > @currentAccountingDate or _advanceConditionAvailability.EndDate is null)
		) as clientAccountAdvanceRate on clientAccountAdvanceRate.ClientAccountgreementId = clientAccountTable.AgreementId
		-- pending payments
		left join
		(
			select 
				 _clientAccountTable.AgreementId                               as ClientAccountAgreementId
				,round(sum(isNull(_outPaymentSource.SourceOrigCurrValue,0)),5) as PendingPayments
			from      @clientAccountTable as _clientAccountTable
			left join OutPayment          as _outPayment      on  _outPayment.AgreementId  = _clientAccountTable.AgreementId  
															  and _outPayment.IsPending    =  1 
															  and _outPayment.CategoryCode in ('0010', '0031', '0030')
															  and _outPayment.StatusCode   <>  '0270'  
															  and _outPayment.StatusCode   <>  '0280'  
															  and (_outPayment.ExpiryDate  is null or (_outPayment.ExpiryDate >= @currentAccountingDate)) 
			left join OutPaymentSource    as _outPaymentSource on  _outPaymentSource.OutPaymentId  = _outPayment.Id
			                                                  and _outPaymentSource.SourceOfFunds in (0, 1, 4, 5, 6)
			group by _clientAccountTable.AgreementId
		) as paymentDetails on paymentDetails.ClientAccountAgreementId = clientAccountTable.AgreementId
		-- client account balance 
		left join(select
					 _clientAccountTable.AgreementId                                                                                            as ClientAccountAgreementId
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
				inner join Account             as _account            on _balance.AccountId              = _account.Id
				inner join AccountType         as _accountType        on _account.AccountTypeId          = _accountType.Id
				inner join @clientAccountTable as _clientAccountTable on _clientAccountTable.AgreementId = _account.AgreementId
				where 
					(_balance.BalanceStartDate <= @currentAccountingDate)
				and (   (_balance.BalanceEndDate > @currentAccountingDate) 
					 or (_balance.BalanceEndDate is null))
				group by _clientAccountTable.AgreementId
		) as clientAccountBalance on clientAccountBalance.ClientAccountAgreementId = clientAccountTable.AgreementId
	) as clientAccountDetails
) as clientAccountAvailabilityDetails
";

        IDataMapper<IDataRow, GroupAvailabilityData> IDataQuery<IDataRow, GroupAvailabilityData>.DataMapper => this;

        internal SelectGroupAvailabilityQuery(GroupNumberKeyForAquarius groupNumberKeyForAquarius)
        {
            if (groupNumberKeyForAquarius == default(GroupNumberKeyForAquarius)) throw new ArgumentNullException(nameof(groupNumberKeyForAquarius));

            _groupNumber = groupNumberKeyForAquarius.GroupNumber;
            QueryParameters = new Dictionary<string, object>
                {
                      [nameof(InputParameterName.GroupNumber)] = groupNumberKeyForAquarius.GroupNumber
                    , [nameof(InputParameterName.ServiceCompanyId)] = groupNumberKeyForAquarius.AquariusServiceCompanyId
            };
        }

        GroupAvailabilityData IDataMapper<IDataRow, GroupAvailabilityData>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return new GroupAvailabilityData(
                       _groupNumber
                    , dataRow.GetString(nameof(OutputColumnName.CurrencyCode))
                    , dataRow.GetString(nameof(OutputColumnName.GroupName))
                    , dataRow.GetDecimal(nameof(OutputColumnName.AgreedOverpayment))
                    , dataRow.GetDecimal(nameof(OutputColumnName.Availability))
                    , dataRow.GetDecimal(nameof(OutputColumnName.FundsInUse))
                    , dataRow.GetDecimal(nameof(OutputColumnName.MaxCreditFacility))
                    , dataRow.GetDecimal(nameof(OutputColumnName.Retentions))
                    , dataRow.GetDecimal(nameof(OutputColumnName.SalesLedger))
                    , dataRow.GetDecimal(nameof(OutputColumnName.ServiceProviderGuarantees)));
        }
    }
}
