using com.abnamro.agents;
using com.abnamro.datastore;
using System;
using System.Collections.Generic;

namespace com.abnamro.biz.SqlQueries.Aquarius
{
    internal class SelectClientAccountAvailabilityQuery : IDataQuery<IDataRow, ClientAccountAvailabilityData>, IDataMapper<IDataRow, ClientAccountAvailabilityData>
    {
        private enum InputParameterName
        {
            ClientAccountId
        }
        private enum OutputColumnName
        {
              AgreementId
            , AgreementNumber
            , ApprovedBalanceRetention
            , ApprovedBalanceRetentionPercentage
            , Availability
            , AvailableFunds
            , BorrowingBase
            , ClientBalance
            , ClientNumber
            , CurrencyCode
            , Disputed
            , EffectiveFinancing
            , FacilityLimit
            , FundingApproved
            , FundingDisapproved
            , FundingUnapproved
            , FundsInUse
            , GroupFacilityLimit
            , Ineligible
            , MoneyInTransit
            , PendingPayments
            , Retentions
            , ReserveFund
            , SalesLedger
            , ServiceProviderGuarantees
        }

        public IDictionary<string, object> QueryParameters { get; private set; }

        string IDataQuery<IDataRow, ClientAccountAvailabilityData>.Query => $@"
--declare @clientAccountId int = 259436

declare @clientAccountAgreementId decimal = cast(@{nameof(InputParameterName.ClientAccountId)} as decimal)

declare @currentAccountingDate datetime = 	
(
	select
		accountingLocation.CurrentAccountingDate
	from       AccountingLocation as accountingLocation 
	inner join Party              as  serviceCompanyParty on serviceCompanyParty.AccountingLocationId = accountingLocation.Id
	inner join Agreement          as agreement            on agreement.ServiceCompanyId = serviceCompanyParty.Id
	where 
		agreement.Id = @clientAccountAgreementId
)

declare @clientGroupTermsOfAgreementId decimal
declare @groupDivideByRateToBaseCurrency decimal(13,6)

select 
	@clientGroupTermsOfAgreementId    = clientGroupAgreement.TermsOfAgreementId
	,@groupDivideByRateToBaseCurrency = case when currencyConversionRate.IsConversionByDivision = 1 then currencyConversionRate.Rate / power(10,currencyConversionRate.Quote)
											else  1/(currencyConversionRate.Rate / power(10,currencyConversionRate.Quote)) 
										end
from       Agreement                 as clientAccountAgreement
left join  AggregationAgreementSa    as aggregationAgreementSa    on aggregationAgreementSa.ServiceAgreementId    = clientAccountAgreement.Id 
															     and aggregationAgreementSa.EndDate               is null
inner join Agreement                 as clientGroupAgreement      on clientGroupAgreement.Id                      = aggregationAgreementSa.AggregationAgreementId
inner join CurrencyConversionRate    as currencyConversionRate    on currencyConversionRate.ConvertFromCurrencyId = clientGroupAgreement.CurrencyId
inner join CurrencyExchangeRateTable as currencyExchangeRateTable on currencyExchangeRateTable.Id                 = currencyConversionRate.RateTableForRateId
inner join Party                     as serviceCompanyParty       on serviceCompanyParty.Id                       = clientAccountAgreement.ServiceCompanyId
																 and serviceCompanyParty.RateTableForScId         = currencyExchangeRateTable.Id
where  
	clientAccountAgreement.Id      = @clientAccountAgreementId
and currencyConversionRate.EndDate is null

declare @clientAccountDivideByRateToBaseCurrency decimal(13,6) =
(
	select 
		case when currencyConversionRate.IsConversionByDivision = 1 then currencyConversionRate.Rate / power(10,currencyConversionRate.Quote)
			 else  1/(currencyConversionRate.Rate / power(10,currencyConversionRate.Quote)) 
		end
	from       Agreement                 as clientAccountAgreement
	inner join CurrencyConversionRate    as currencyConversionRate    on currencyConversionRate.ConvertFromCurrencyId = clientAccountAgreement.CurrencyId
	inner join CurrencyExchangeRateTable as currencyExchangeRateTable on currencyExchangeRateTable.Id                 = currencyConversionRate.RateTableForRateId
	inner join Party                     as serviceCompanyParty       on serviceCompanyParty.Id                       = clientAccountAgreement.ServiceCompanyId
																     and serviceCompanyParty.RateTableForScId         = currencyExchangeRateTable.Id
    where  
	    clientAccountAgreement.Id      = @clientAccountAgreementId
    and currencyConversionRate.EndDate is null
)

declare @ineligibleAmount money = 
cast(round(isNull(
(	select
	sum(ineligibleDebtorLedgerItem.BalanceSAC) as IneligibleAmount
	from       LedgerItem             as ineligibleDebtorLedgerItem 
	inner join Agreement              as debtorAccount                 on debtorAccount.ID = ineligibleDebtorLedgerItem.DebtorAccountId
																	  and debtorAccount.OjbConcreteClass = 'aquarius.agreement.DebtorAccount'
																	  and debtorAccount.AgreementStatusCode = '0001'
	inner join LedgerItemDisappStatus as ledgerItemDisapprovedStatus   on ledgerItemDisapprovedStatus.ID = ineligibleDebtorLedgerItem.FundingDisapprovedStatusID
																	  and ineligibleDebtorLedgerItem.IsUnPostedItem <> 1 
																	  and ineligibleDebtorLedgerItem.IsDisputed <> 1
																	  and ledgerItemDisapprovedStatus.DisapprovalTypeCode in ( '0020', '0210', '0230', '0260', '0270', '0350', '0360', '0410', '0420', '0430', '0440', '0460', '0470' ) 
	where
	   debtorAccount.ServiceAgreementId = @clientAccountAgreementId
), 0), 2) as money) 

declare @maxMoneyValue Money = 922337203685477  --sql money type has 8 bytes. Maximum money value is 922,337,203,685,477.58

;with clientAccountBalances as
(
	select
		 cast(_clientAccountBalances.AgreementId as bigint)                       as [AgreementId]
		,cast(_clientAccountBalances.AgreementNumber as int)                      as [AgreementNumber]
		,cast(_clientAccountBalances.ClientNumber as int)                         as [ClientNumber]
		,_clientAccountBalances.CurrencyCode                                      as [CurrencyCode]
		,cast(round(_clientAccountBalances.ClientBalance,2) as money)             as [ClientBalance]
		,cast(round(_clientAccountBalances.Disputed,2) as money)                  as [Disputed]
		,cast(round(_clientAccountBalances.FundingDisapproved,2) as money)        as [FundingDisapproved]
		,cast(round(_clientAccountBalances.FundsInUse,2) as money)                as [FundsInUse]
		,cast(round(_clientAccountBalances.MoneyInTransit,2) as money)            as [MoneyInTransit]
		,cast(round(_clientAccountBalances.PendingPayments,2) as money)           as [PendingPayments]
		,cast(round(_clientAccountBalances.ReserveFund,2) as money)               as [ReserveFund]
		,cast(round(_clientAccountBalances.SalesLedger,2) as money)               as [SalesLedger]
		,cast(round(_clientAccountBalances.ServiceProviderGuarantees,2) as money) as [ServiceProviderGuarantees]
		,cast(round(_clientAccountBalances.ConcentrationRetention,2) as money)    as [ConcentrationRetention]
		,cast(round(
			  ((_clientAccountBalances.SalesLedger - _clientAccountBalances.FundingDisapproved - _clientAccountBalances.ConcentrationRetention) * (_clientAccountBalances.AdvanceRate/100))
			 - _clientAccountBalances.MoneyInTransitCreditBalanceOnly
			 - (abs(_clientAccountBalances.ReserveFundNoFIU) + abs(_clientAccountBalances.ReserveFundNoFIUNoCB))
		 , 2) as money)                                                           as [ExpressionA]

	 		,cast(round(
			_clientAccountBalances.FundsInUse + _clientAccountBalances.PendingPayments+ abs(_clientAccountBalances.ServiceProviderGuarantees)
		 , 2) as money)                                                           as [ExpressionB] 
		,cast(round(
			   _clientAccountBalances.FundingDisapproved
			 + _clientAccountBalances.ConcentrationRetention
			 + (((_clientAccountBalances.SalesLedger - _clientAccountBalances.FundingDisapproved - _clientAccountBalances.ConcentrationRetention) * (100 - _clientAccountBalances.AdvanceRate)/100))
			 + _clientAccountBalances.PendingPayments
			 + _clientAccountBalances.MoneyInTransitCreditBalanceOnly
			 + abs(_clientAccountBalances.ReserveFundNoFIUNoCB)
			 + abs(_clientAccountBalances.ReserveFundNoFIU)
			 + abs(_clientAccountBalances.ServiceProviderGuarantees)
			 ,2)
		 as money)                                                                as [Retentions]
		,100 - _clientAccountBalances.AdvanceRate                                 as [ApprovedBalanceRetentionPercentage]
		,cast(round(_clientAccountBalances.FacilityLimit,2) as money)             as [FacilityLimit]
		,cast(round(_clientAccountBalances.GroupFacilityLimit,2) as money)        as [GroupFacilityLimit]
		,cast(round((case 
	                   when (_clientAccountBalances.GroupFacilityLimit < isNull(_clientAccountBalances.FacilityLimit, @maxMoneyValue)) then _clientAccountBalances.GroupFacilityLimit
	                   else _clientAccountBalances.FacilityLimit
	                end) - (case when _clientAccountBalances.FundsInUse < 0 then abs(_clientAccountBalances.FundsInUse) else 0 end), 2) as money)  as [FacilityLimitToUse]
	from
	(
		select 
			 agreementData.*
			,isNull(balancesData.ClientBalance,0)                                              as [ClientBalance]
			,isNull(balancesData.ConcentrationRetention,0)                                     as [ConcentrationRetention]
			,isNull(balancesData.Disputed,0)                                                   as [Disputed]
			,isNull(balancesData.FundingDisapproved,0)                                         as [FundingDisapproved]
			,isNull(balancesData.FundsInUse,0)                                                 as [FundsInUse]
			,isNull(balancesData.MoneyInTransit,0)                                             as [MoneyInTransit]
			,abs((select min(x) from (values (0), (balancesData.MoneyInTransit)) as value(x))) as [MoneyInTransitCreditBalanceOnly]
			,isNull(pendingPaymentData.PendingPayments,0)                                      as [PendingPayments]
			,isNull(balancesData.ReserveFund,0)                                                as [ReserveFund]
			,isNull(balancesData.ReserveFundNoFIU,0)                                           as [ReserveFundNoFIU]
			,isNull(balancesData.ReserveFundNoFIUNoCB,0)                                       as [ReserveFundNoFIUNoCB]
			,isNull(balancesData.SalesLedger,0)                                                as [SalesLedger]
			,isNull(balancesData.ServiceProviderGuarantees, 0)                                 as [ServiceProviderGuarantees]
		from 
		(
		select
			 clientAccountAgreement.Id                      as [AgreementId]
			,clientAccountAgreement.ClientNumber            as [ClientNumber]
			,clientAccountAgreement.AgreementNumber         as [AgreementNumber]
			,clientAccountCurrency.CurrencyCode             as [CurrencyCode]
			,isNull(advanceRate.AdvanceRatePercentage, 100) as [AdvanceRate]
			,facilityLimit.FacilityLimitAmount              as [FacilityLimit]
			,groupFacilityLimit.GroupFacilityLimitAmount    as [GroupFacilityLimit]
		from 
				   Agreement as clientAccountAgreement
		inner join Currency  as clientAccountCurrency  on clientAccountCurrency.Id = clientAccountAgreement.CurrencyId
		left join
		(
			select
				_agreementCondition.TermsOfAgreementId       as [TermsOfAgreementId]
			   ,_conditionLimit.Amount                       as [FacilityLimitAmount]
			from       AgreementCondition as _agreementCondition 
			inner join ConditionType      as _limitConditionType on  _limitConditionType.Id            = _agreementCondition.ConditionTypeId 
															    and _limitConditionType.BaseCode       = '8040' 
															    and _limitConditionType.StandardCode   = '0100' 
															    and _limitConditionType.VariationCode  = '0100'
			inner join ConditionLimit     as _conditionLimit     on  _conditionLimit.ConditionTypeId   = _limitConditionType.Id
															    and _conditionLimit.ConditionSettingId = _agreementCondition.CurrentConditionSettingId
															    and _conditionLimit.StatusIndicator    in (0,14)
															    and _conditionLimit.OjbConcreteClass   = 'aquarius.agreement.ConditionLimit'
															    and _conditionLimit.EffectiveDate      <= @currentAccountingDate
															    and (_conditionLimit.EndDate           >  @currentAccountingDate or _conditionLimit.EndDate is null)
		) as facilityLimit on facilityLimit.TermsOfAgreementId = clientAccountAgreement.TermsOfAgreementId
		left join
		(
			select
			   (@clientAccountDivideByRateToBaseCurrency / @groupDivideByRateToBaseCurrency) *_conditionLimit.Amount  as [GroupFacilityLimitAmount]
			from       AgreementCondition as _agreementCondition
			inner join ConditionType      as _limitConditionType on _limitConditionType.Id             = _agreementCondition.ConditionTypeId 
																and _limitConditionType.BaseCode       = '8040' 
																and _limitConditionType.StandardCode   = '0100' 
																and _limitConditionType.VariationCode  = '0100'
			inner join ConditionLimit     as _conditionLimit     on _conditionLimit.ConditionTypeId    = _limitConditionType.Id
																and _conditionLimit.ConditionSettingId = _agreementCondition.CurrentConditionSettingId
																and _conditionLimit.StatusIndicator    in (0,14)
																and _conditionLimit.OjbConcreteClass   = 'aquarius.agreement.ConditionLimit'
																and _conditionLimit.EffectiveDate      <= @currentAccountingDate
																and (_conditionLimit.EndDate           >  @currentAccountingDate or _conditionLimit.EndDate is null)
			where
				 _agreementCondition.TermsOfAgreementId = @clientGroupTermsOfAgreementId
		) as groupFacilityLimit on (1=1)
		left join
		(
			select
				agreementCondition.TermsOfAgreementId
			   ,advanceConditionAvailability.AdvanceRate    as [AdvanceRatePercentage]
			from       AgreementCondition    as agreementCondition 
			inner join ConditionAvailability as advanceConditionAvailability on  advanceConditionAvailability.ConditionSettingId = agreementCondition.CurrentConditionSettingId
																			 and advanceConditionAvailability.StatusIndicator    in (0,14)
																			 and advanceConditionAvailability.OjbConcreteClass   =  'aquarius.agreement.ConditionAdvances'
																			 and advanceConditionAvailability.EffectiveDate      <= @currentAccountingDate
																			 and (advanceConditionAvailability.EndDate           >  @currentAccountingDate or advanceConditionAvailability.EndDate is null)
			left join ConditionType          as advanceConditionType         on  advanceConditionType.Id                         = advanceConditionAvailability.ConditionTypeId
																			 and advanceConditionType.BaseCode                   = '5120' 
																			 and advanceConditionType.StandardCode               = '0100' 
																			 and advanceConditionType.VariationCode              = '0100'

		) as advanceRate on advanceRate.TermsOfAgreementId = clientAccountAgreement.TermsOfAgreementId
		where
			clientAccountAgreement.Id = @clientAccountAgreementId
		) as agreementData
		left join
		 -- client account balances 
		(
			select
				 sum(case when (_accountType.BaseCode = '1010' and _accountType.StandardCode = '0100')
							or (_accountType.BaseCode = '3150' and _accountType.StandardCode = '0200') then _balance.BalanceSac else 0 end) as SalesLedger 
				,sum(case when  _accountType.BaseCode = '2010' and _accountType.StandardCode = '0100'  then _balance.BalanceSac else 0 end) as FundingDisapproved
				,sum(case when  _accountType.BaseCode = '2110' and _accountType.StandardCode = '0100'  then _balance.BalanceSac else 0 end) as Disputed
				,sum(case when  _accountType.BaseCode = '3010' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as ClientBalance
				,sum(case when  _accountType.BaseCode = '3020' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as FundsInUse
				,sum(case when  _accountType.BaseCode = '3100'                                         then _balance.BalanceSac else 0 end) as ReserveFund
				,sum(case when  _accountType.BaseCode = '3100' and _accountType.StandardCode = '0310'  then _balance.BalanceSac else 0 end) as ReserveFundNoFIU
				,sum(case when  _accountType.BaseCode = '3100' and _accountType.StandardCode = '0320'  then _balance.BalanceSac else 0 end) as ReserveFundNoFIUNoCB
				,sum(case when  _accountType.BaseCode = '3110' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as ServiceProviderGuarantees
				,sum(case when  _accountType.BaseCode = '3130' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as MoneyInTransit
				,sum(case when  _accountType.BaseCode = '3520' and _accountType.StandardCode = '0300'  then _balance.BalanceSac else 0 end) as ConcentrationRetention
			from       Balance     as _balance 
			inner join Account     as _account     on _balance.AccountId     = _account.Id
			inner join AccountType as _accountType on _account.AccountTypeId = _accountType.Id
			where 
				_account.AgreementId = @clientAccountAgreementId
			and ( _balance.BalanceStartDate <= @currentAccountingDate)
			and (   (_balance.BalanceEndDate > @currentAccountingDate) 
					or (_balance.BalanceEndDate is null))
			group by _account.AgreementId
		)  as balancesData on 1=1
		left join
		-- pending payment data
		(
			select 
				 round(sum(isNull(_outPaymentSource.SourceOrigCurrValue,0)),5) as PendingPayments
			from      OutPayment       as _outPayment     
			left join OutPaymentSource as _outPaymentSource on  _outPaymentSource.OutPaymentId  = _outPayment.Id
															and _outPaymentSource.SourceOfFunds in (0, 1, 4, 5, 6)
			where  
				_outPayment.AgreementId  = @clientAccountAgreementId
			and _outPayment.IsPending    =  1 
			and _outPayment.CategoryCode in ('0010', '0031', '0030')
			and _outPayment.StatusCode   <>  '0270'  
			and _outPayment.StatusCode   <>  '0280'  
			and (_outPayment.ExpiryDate  is null or (_outPayment.ExpiryDate >= @currentAccountingDate)) 
			group by _outPayment.AgreementId
		) as pendingPaymentData on 1=1
	) as _clientAccountBalances
)
select 
	 instance.AgreementId                                                as [{nameof(OutputColumnName.AgreementId)}]
	,instance.AgreementNumber                                            as [{nameof(OutputColumnName.AgreementNumber)}]
	,instance.ClientNumber                                               as [{nameof(OutputColumnName.ClientNumber)}]
	,instance.CurrencyCode                                               as [{nameof(OutputColumnName.CurrencyCode)}]
	,instance.ExpressionA                                                as [{nameof(OutputColumnName.BorrowingBase)}]
	,instance.ClientBalance                                              as [{nameof(OutputColumnName.ClientBalance)}]
	,instance.Disputed                                                   as [{nameof(OutputColumnName.Disputed)}]
	,instance.FundsInUse                                                 as [{nameof(OutputColumnName.FundsInUse)}]
	,instance.MoneyInTransit                                             as [{nameof(OutputColumnName.MoneyInTransit)}]
	,instance.PendingPayments                                            as [{nameof(OutputColumnName.PendingPayments)}]
	,instance.ReserveFund                                                as [{nameof(OutputColumnName.ReserveFund)}]
	,instance.Retentions                                                 as [{nameof(OutputColumnName.Retentions)}]
	,instance.SalesLedger                                                as [{nameof(OutputColumnName.SalesLedger)}]
	,instance.ServiceProviderGuarantees                                  as [{nameof(OutputColumnName.ServiceProviderGuarantees)}]
	,instance.FundingDisapproved                                         as [{nameof(OutputColumnName.FundingDisapproved)}]
	,instance.FundingDisapproved - instance.Disputed - @ineligibleAmount as [{nameof(OutputColumnName.FundingUnapproved)}]
	,instance.SalesLedger  -instance.FundingDisapproved                  as [{nameof(OutputColumnName.FundingApproved)}]
	,instance.ExpressionA - instance.ExpressionB                         as [{nameof(OutputColumnName.Availability)}]
	,(select 
	    min(x) 
	  from (values (instance.ExpressionA - instance.ExpressionB), (instance.FacilityLimitToUse)) as value(x)
	  )                                                                  as [{nameof(OutputColumnName.AvailableFunds)}]
	,instance.FacilityLimit                                              as [{nameof(OutputColumnName.FacilityLimit)}]
	,instance.GroupFacilityLimit                                         as [{nameof(OutputColumnName.GroupFacilityLimit)}]
	,cast((case 
	    when (instance.SalesLedger > 1) then round((100 * (instance.ExpressionA - instance.ExpressionB + instance.FundsInUse) / instance.SalesLedger),0)
		else 0
	  end)as smallint)                                                   as [{nameof(OutputColumnName.EffectiveFinancing)}]
	,cast(instance.ApprovedBalanceRetentionPercentage as smallint)       as [{nameof(OutputColumnName.ApprovedBalanceRetentionPercentage)}]
	,cast(round((instance.ApprovedBalanceRetentionPercentage / 100) * (instance.SalesLedger - instance.FundingDisapproved - instance.ConcentrationRetention), 2) as money) as [{nameof(OutputColumnName.ApprovedBalanceRetention)}]
    ,@ineligibleAmount                                                   as [{nameof(OutputColumnName.Ineligible)}]
from clientAccountBalances as instance
";

        IDataMapper<IDataRow, ClientAccountAvailabilityData> IDataQuery<IDataRow, ClientAccountAvailabilityData>.DataMapper => this;

        internal SelectClientAccountAvailabilityQuery(ClientAccountKey clientAccountKey)
        {
            if (clientAccountKey == default(ClientAccountKey)) throw new ArgumentNullException(nameof(clientAccountKey));

            QueryParameters = new Dictionary<string, object>
            {
                [nameof(InputParameterName.ClientAccountId)] = clientAccountKey.ClientAccountId
            };
        }

        ClientAccountAvailabilityData IDataMapper<IDataRow, ClientAccountAvailabilityData>.MapData(IDataRow dataRow)
        {
            if (dataRow == default(IDataRow)) throw new ArgumentNullException(nameof(dataRow));

            return new ClientAccountAvailabilityData(
                            new ClientAccountKey(dataRow.GetLong(nameof(OutputColumnName.AgreementId)))
                        , dataRow.GetInt(nameof(OutputColumnName.ClientNumber))
                        , dataRow.GetInt(nameof(OutputColumnName.AgreementNumber))
                        , dataRow.GetString(nameof(OutputColumnName.CurrencyCode))
                        , dataRow.GetDecimal(nameof(OutputColumnName.Availability))
                        , dataRow.GetDecimal(nameof(OutputColumnName.SalesLedger))
                        , dataRow.GetDecimal(nameof(OutputColumnName.Retentions))
                        , dataRow.GetDecimal(nameof(OutputColumnName.ClientBalance))
                        , dataRow.GetDecimal(nameof(OutputColumnName.FundsInUse))
                        , dataRow.GetDecimal(nameof(OutputColumnName.BorrowingBase))
                        , dataRow.GetShort(nameof(OutputColumnName.EffectiveFinancing))
                        , dataRow.GetShort(nameof(OutputColumnName.ApprovedBalanceRetentionPercentage))
                        , dataRow.GetDecimal(nameof(OutputColumnName.ApprovedBalanceRetention))
                        , dataRow.GetDecimal(nameof(OutputColumnName.FundingDisapproved))
                        , dataRow.GetDecimal(nameof(OutputColumnName.FundingApproved))
                        , dataRow.GetDecimal(nameof(OutputColumnName.PendingPayments))
                        , dataRow.GetDecimal(nameof(OutputColumnName.ServiceProviderGuarantees))
                        , dataRow.GetDecimalOrDefault(nameof(OutputColumnName.FacilityLimit))
                        , dataRow.GetDecimal(nameof(OutputColumnName.GroupFacilityLimit))
                        , dataRow.GetDecimal(nameof(OutputColumnName.AvailableFunds))
                        , dataRow.GetDecimal(nameof(OutputColumnName.Disputed))
                        , dataRow.GetDecimal(nameof(OutputColumnName.FundingUnapproved))
                        , dataRow.GetDecimal(nameof(OutputColumnName.Ineligible))
                        , dataRow.GetDecimal(nameof(OutputColumnName.ReserveFund))
                        , dataRow.GetDecimal(nameof(OutputColumnName.MoneyInTransit)));
        }
    }
}
