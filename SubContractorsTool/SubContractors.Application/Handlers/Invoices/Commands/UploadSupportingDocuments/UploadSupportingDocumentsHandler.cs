using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Invoice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Commands.UploadSupportingDocuments
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class UploadSupportingDocumentsHandler : IRequestHandler<UploadSupportingDocuments, Result<IList<UploadSupportingDocumentsDto>>>
    {
        private readonly ISqlRepository<SupportingDocument, Guid> _supportingDocumentationSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadSupportingDocumentsHandler(
            ISqlRepository<SupportingDocument, Guid> supportingDocumentationSqlRepository,
            IUnitOfWork unitOfWork
            )
        {
            _supportingDocumentationSqlRepository = supportingDocumentationSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IList<UploadSupportingDocumentsDto>>> Handle(UploadSupportingDocuments request, CancellationToken cancellationToken)
        {
            IList<UploadSupportingDocumentsDto> result = new List<UploadSupportingDocumentsDto>();

            foreach (var file in request.Files)
            {
                var name = file.FileName.Replace(@"\\\\", @"\\");
                if (file.Length > 0)
                {
                    var memoryStream = new MemoryStream();

                    try
                    {
                        await file.CopyToAsync(memoryStream, cancellationToken);

                        var supportingDocumentation = new SupportingDocument();

                        var identifier = Guid.NewGuid();
                        supportingDocumentation.Create(identifier, Path.GetFileName(name),
                            memoryStream.Length, memoryStream.ToArray());

                        result.Add(new UploadSupportingDocumentsDto { Id = identifier, Filename = Path.GetFileName(name) });

                        await _supportingDocumentationSqlRepository.AddAsync(supportingDocumentation);

                        await _unitOfWork.SaveAsync();
                    }
                    finally
                    {
                        memoryStream.Close();
                        await memoryStream.DisposeAsync();
                    }
                }
                else
                {
                    return Result.NotFound<IList<UploadSupportingDocumentsDto>>($"Length of file with name {name} must be not equel to 0");
                }
            }

            return Result.Ok(value: result);
        }
    }
}
