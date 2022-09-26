using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Compliance;

namespace SubContractors.Application.Handlers.Compliance.Commands.UploadComplianceFile
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class UploadComplianceFileHandler : IRequestHandler<UploadComplianceFile, Result<UploadComplianceFileDto>>
    {
        private readonly ISqlRepository<ComplianceFile, Guid> _complianceFileSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadComplianceFileHandler(
            ISqlRepository<ComplianceFile, Guid> complianceFileSqlRepository,
            IUnitOfWork unitOfWork
            )
        {
            _complianceFileSqlRepository = complianceFileSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UploadComplianceFileDto>> Handle(UploadComplianceFile request, CancellationToken cancellationToken)
        {
            UploadComplianceFileDto result = new UploadComplianceFileDto();

            var file = request.File;
            var fileName = Path.GetFileName(file.FileName);
            var fileExtension = Path.GetExtension(fileName);
            
         
                var memoryStream = new MemoryStream();

                try
                {
                    await file.CopyToAsync(memoryStream, cancellationToken);

                    var complianceFile = new ComplianceFile();

                    var identifier = Guid.NewGuid();
                    complianceFile.Create(identifier, fileName,
                        memoryStream.Length, memoryStream.ToArray(),fileExtension);

                    result.Filename = fileName;
                    result.Id = identifier;

                    await _complianceFileSqlRepository.AddAsync(complianceFile);

                    await _unitOfWork.SaveAsync();


                }
                finally
                {
                    memoryStream.Close();
                    await memoryStream.DisposeAsync();
                }

                return Result.Ok(value: result);

            
        }

    }
}
