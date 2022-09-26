using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Commands.UpdateAddendum
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class UpdateAddendumHandler : IRequestHandler<UpdateAddendum, Result<Unit>>
    {
        private readonly IAgreementSqlRepository _agreementSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectsSqlRepository;
        private readonly ISqlRepository<PaymentTerm, int> _paymentTermsSqlRepository;
        private readonly ISqlRepository<Currency, int> _currenciesSqlRepository;
        private readonly IAddendaSqlRepository _sqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAddendumHandler(
            IAgreementSqlRepository agreementSqlRepository, 
            ISqlRepository<Domain.Project.Project, Guid> projectsSqlRepository, 
            ISqlRepository<PaymentTerm, int> paymentTermsSqlRepository, 
            ISqlRepository<Currency, int> currenciesSqlRepository, 
            IAddendaSqlRepository sqlRepository, 
            IUnitOfWork unitOfWork)
        {
            _agreementSqlRepository = agreementSqlRepository;
            _projectsSqlRepository = projectsSqlRepository;
            _paymentTermsSqlRepository = paymentTermsSqlRepository;
            _currenciesSqlRepository = currenciesSqlRepository;
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateAddendum request, CancellationToken cancellationToken)
        {
            var addendum = await _sqlRepository.GetAsync(x => x.Id == request.Id,
                new string[]
                {
                    nameof(Addendum.PaymentTerm),
                    nameof(Addendum.Projects),
                    nameof(Addendum.Currency),
                    nameof(Addendum.Agreement)
                });
            if (addendum == null)
            {
                return Result.NotFound($"Addendum wasn't found in database with provided identifier {request.Id}");
            }

            if (addendum.Agreement == null || addendum.Agreement.Id != request.AgreementId)
            {
                var agreement = await _agreementSqlRepository.GetAsync(x => x.Id == request.AgreementId, Array.Empty<string>() );
                if (agreement == null)
                {
                    return Result.NotFound($"Agreement wasn't found in database with provided identifier {request.AgreementId}");
                }

                addendum.AssignToAgreement(agreement);
            }
            if (addendum.PaymentTerm == null || addendum.PaymentTerm.Id != request.PaymentTermId)
            {
                var paymentTerm = await _paymentTermsSqlRepository.GetAsync(x => x.Id == request.PaymentTermId, Array.Empty<string>() );
                if (paymentTerm == null)
                {
                    return Result.NotFound($"PaymentTerm wasn't found in database with provided identifier {request.PaymentTermId}");
                }

                addendum.AssignPaymentTerm(paymentTerm);
            }

            if (addendum.Currency == null || addendum.Currency.Id != request.CurrencyId)
            {
                var currency = await _currenciesSqlRepository.GetAsync(x => x.Id == request.CurrencyId, Array.Empty<string>() );
                if (currency == null)
                {
                    return Result.NotFound($"Currency wasn't found in database with provided identifier {request.CurrencyId}");
                }

                addendum.AssignCurrency(currency);
            }

            var result = UpdateAddendumProjects(ref addendum, request.ProjectIds);
            if (!string.IsNullOrEmpty(result))
            {
                return Result.NotFound(result);
            }

            foreach (var projectId in request.ProjectIds)
            {
                var project = await _projectsSqlRepository.GetAsync(projectId, Array.Empty<string>() );
                if (project == null)
                {
                    return Result.NotFound($"Project wasn't found in database with provided identifier {projectId}");
                }

                addendum.AddProject(project);
            }
            
            addendum.Update(request.Title, 
                            request.Url, 
                            request.StartDate.Value, 
                            request.EndDate.Value, 
                            request.Comment, 
                            request.PaymentTermInDays, 
                            request.IsForNonBillableProjects);
            
            await _sqlRepository.UpdateAsync(addendum);

            await _unitOfWork.SaveAsync();

            return Result.Accepted();
        }

        private string UpdateAddendumProjects(ref Addendum addentum, List<Guid> updateProjectIds )
        {
            var projectIds = new List<Guid>();

            if (addentum.Projects != null && addentum.Projects.Any())
            {
                if (updateProjectIds.Count > 0)
                {
                    var removeDocumentList = new List<Domain.Project.Project>();
                    for (int i = 0; i < addentum.Projects.Count; i++)
                    {
                        var id = addentum.Projects[i].Id;
                        if (!updateProjectIds.Contains(id))
                        {
                            removeDocumentList.Add(addentum.Projects[i]);
                        }
                    }

                    if (removeDocumentList.Any())
                    {
                        foreach (var doc in removeDocumentList)
                        {
                            addentum.Projects.Remove(doc);
                        }
                    }
                }

                var idList = addentum.Projects.Select(x => x.Id)
                                              .ToList();

                var requestDocCount = updateProjectIds.Count;

                for (int i = 0; i < idList.Count; i++)
                {
                    for (int j = 0; j < requestDocCount; j++)
                    {
                        if (idList[i] == updateProjectIds[j])
                        {
                            projectIds.Add(idList[i]);
                            break;
                        }

                        if ((j + 1) == requestDocCount && idList[i] != updateProjectIds[j])
                        {
                            projectIds.Add(updateProjectIds[j]);
                        }
                    }
                }
            }

            if (addentum.Projects != null && addentum.Projects.Count > 0)
            {
                projectIds = updateProjectIds.ToList();
            }

            for (int i = 0; i < projectIds.Count; i++)
            {
                if (addentum.Projects.Select(x => x.Id).Contains(projectIds[i]))
                {
                    break;
                }
                var task = Task.Run(async () => await _projectsSqlRepository.GetAsync(x => x.Id == projectIds[i]));
                task.Start();
                var project = task.Result;
                if (project == null)
                {
                    return $"Project File wasn't found in database with provided identifier {projectIds[i]}";
                }

                addentum.Projects.Add(project);
            }

            return null;
        }
    }
}
