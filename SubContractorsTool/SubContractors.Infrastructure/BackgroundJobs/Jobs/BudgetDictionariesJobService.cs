using Microsoft.Extensions.Logging;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Budget;
using SubContractors.Infrastructure.BackgroundJobs.Interfaces;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem;
using SubContractors.Infrastructure.ExternalServices.BudgetSystem.ResponseModels.BudgetDictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Currency = SubContractors.Domain.Common.Currency;
using PaymentMethod = SubContractors.Domain.Budget.PaymentMethod;

namespace SubContractors.Infrastructure.BackgroundJobs.Jobs
{
    public class BudgetDictionariesJobService : IBudgetDictionariesJobService
    {
        private readonly ISqlRepository<Currency, int> _currencySqlRepository;
        private readonly ISqlRepository<PaymentMethod, int> _paymentMethodSqlRepository;
        private readonly ISqlRepository<BudgetGroup, int> _budgetGroupSqlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBudgetsService _budgetsService;
        private readonly ILogger<BudgetDictionariesJobService> _logger;

        public BudgetDictionariesJobService(ISqlRepository<Currency, int> currencySqlRepository,
                                                  ISqlRepository<PaymentMethod, int> paymentMethodSqlRepository,
                                                  IUnitOfWork unitOfWork,
                                                  IBudgetsService budgetsService,
                                                  ILogger<BudgetDictionariesJobService> logger,
                                                  ISqlRepository<BudgetGroup, int> budgetGroupSqlRepository)
        {
            _budgetGroupSqlRepository = budgetGroupSqlRepository;
            _currencySqlRepository = currencySqlRepository;
            _paymentMethodSqlRepository = paymentMethodSqlRepository;
            _unitOfWork = unitOfWork;
            _budgetsService = budgetsService;
            _logger = logger;
        }

        public async Task MigrateBudgetDataAsync()
        {
            try
            {
                var budgetDictionariesResponse = await _budgetsService.GetBudgetDictionariesAsync();
                if (!budgetDictionariesResponse.IsError)
                {
                    var currencies = await _currencySqlRepository.FindAsync(x => true);
                    if (currencies is null || !currencies.Any())
                    {
                        foreach (var currency in budgetDictionariesResponse.Response.Currencies)
                        {
                            var model = new Currency
                            {
                                IsDeleted = false,
                                BudgetSystemId = currency.Id,
                                Name = currency.Name,
                                Code = currency.Code,
                            };
                            await _currencySqlRepository.AddAsync(model);
                        }
                    }

                    var paymentMethods = await _paymentMethodSqlRepository.FindAsync(x => true);
                    if (paymentMethods is null || !paymentMethods.Any())
                    {
                        foreach (var paymentMethod in budgetDictionariesResponse.Response.PaymentMethods)
                        {
                            var model = new PaymentMethod
                            {
                                IsActive = paymentMethod.IsInactive,
                                BudgetSystemId = paymentMethod.Id,
                                Name = paymentMethod.Name
                            };
                            await _paymentMethodSqlRepository.AddAsync(model);
                        }
                    }

                    var budgetGroups = await _budgetGroupSqlRepository.FindAsync(x => true);
                    if (budgetGroups is null || !budgetGroups.Any())
                    {
                        foreach (var budgetGroup in budgetDictionariesResponse.Response.Budgets)
                        {
                            var model = new BudgetGroup
                            {
                                IsActive = !budgetGroup.IsInactive,
                                BudgetGroupName = budgetGroup.Name,
                                BudgetSystemId = budgetGroup.Id,
                            };

                            await _budgetGroupSqlRepository.AddAsync(model);
                        }
                    }

                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "couldn't migrate currency, budget group and payment methods from budget service");
            }
        }

        public async Task SynchronizeBudgetDataAsync()
        {
            try
            {
                var budgetDictionariesResponse = await _budgetsService.GetBudgetDictionariesAsync();
                var currencies = await _currencySqlRepository.FindAsync(x => true);
                var paymentMethods = await _paymentMethodSqlRepository.FindAsync(x => true);
                var budgetGroups = await _budgetGroupSqlRepository.FindAsync(x => true);

                if (budgetDictionariesResponse.IsError)
                {
                    throw new Exception("Couldn't retrieve data from currency, budget group and payment method service");
                }

                if ((currencies is null || !currencies.Any()) || (paymentMethods is null || !paymentMethods.Any()))
                {
                    await MigrateBudgetDataAsync();
                    return;
                }                

                await SynchronizeData(budgetDictionariesResponse, currencies, paymentMethods, budgetGroups);

                await _unitOfWork.SaveAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred during synchronization of dictionaries from budget system");
            }
        }

        private async Task SynchronizeData(BudgetDictionariesResponse budgetDictionariesResponse, IEnumerable<Currency> currencies, IEnumerable<PaymentMethod> paymentMethods, IEnumerable<BudgetGroup> budgetGroups)
        {
            foreach (var budgetCurrency in budgetDictionariesResponse.Response.Currencies)
            {
                var currency = currencies.FirstOrDefault(x => x.BudgetSystemId == budgetCurrency.Id);
                if (currency == null)
                {
                    var newCurrency = new Currency
                    {
                        Name = budgetCurrency.Name,
                        IsDeleted = false,
                        Code = budgetCurrency.Code,
                    };

                    await _currencySqlRepository.AddAsync(newCurrency);
                }
            }

            foreach (var budgetPaymentMethod in budgetDictionariesResponse.Response.PaymentMethods)
            {
                var paymentMethod = paymentMethods.FirstOrDefault(x => x.BudgetSystemId == budgetPaymentMethod.Id);
                if (paymentMethod == null)
                {
                    var newPaymentMethod = new PaymentMethod
                    {
                        Name = budgetPaymentMethod.Name,
                        IsDeleted = budgetPaymentMethod.IsInactive,
                        BudgetSystemId = budgetPaymentMethod.Id
                    };

                    await _paymentMethodSqlRepository.AddAsync(newPaymentMethod);
                }
                else
                {
                    if (paymentMethod.IsDeleted != budgetPaymentMethod.IsInactive)
                    {
                        paymentMethod.IsDeleted = budgetPaymentMethod.IsInactive;
                    }
                    await _paymentMethodSqlRepository.UpdateAsync(paymentMethod);
                }
            }

            foreach (var budgetGroup in budgetDictionariesResponse.Response.Budgets)
            {
                var budgetGroupModel = budgetGroups.FirstOrDefault(x => x.BudgetSystemId == budgetGroup.Id);
                if (budgetGroupModel == null)
                {
                    var newBudgetGroup = new BudgetGroup
                    {
                        BudgetGroupName = budgetGroup.Name,
                        IsDeleted = budgetGroup.IsInactive,
                        BudgetSystemId = budgetGroup.Id,
                        IsActive = !budgetGroup.IsInactive
                    };

                    await _budgetGroupSqlRepository.AddAsync(newBudgetGroup);
                }
                else
                {
                    if (budgetGroupModel.IsDeleted != budgetGroup.IsInactive)
                    {
                        budgetGroupModel.IsDeleted = budgetGroup.IsInactive;
                        budgetGroupModel.IsActive = !budgetGroup.IsInactive;
                    }
                    await _budgetGroupSqlRepository.UpdateAsync(budgetGroupModel);
                }
            }
        }
    }
}
