using AutoMapper;
using CsvHelper;
using MediatR;
using SubContractors.Application.Common.Helpers.Export;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Invoice;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Queries.ExportInvoicesQuery
{
    [RequestLogging]
    [RequestValidation]
    public class ExportInvoicesQueryHandler : IRequestHandler<ExportInvoicesQuery, Result<ExportInvoicesDto>>
    {
        private readonly IInvoiceSqlRepository _invoiceSqlRepository;
        private readonly IMapper _mapper;

        public ExportInvoicesQueryHandler(IInvoiceSqlRepository invoiceSqlRepository, IMapper mapper)
        {
            _invoiceSqlRepository = invoiceSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<ExportInvoicesDto>> Handle(ExportInvoicesQuery request, CancellationToken cancellationToken)
        {
            IList<Invoice> invoices;
            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                invoices = (await _invoiceSqlRepository.FindAsync(x => x.StartDate.Date == request.StartDate.Value.Date && x.EndDate.Date == request.EndDate.Value.Date))
                    .OrderByDescending(x => x.InvoiceDate)
                    .ToList();
            }
            else
            {
                invoices = (await _invoiceSqlRepository.FindAsync(x => true))
                    .OrderByDescending(x => x.InvoiceDate)
                    .ToList();
            }


            if (!invoices.Any())
            {
                return Result.NotFound<ExportInvoicesDto>($"Couldn't find entities with provided parameters");
            }


            var exportDtos = invoices.Select(x=> _mapper.Map<InvoiceExportModel>(x));

            var memoryStream = new MemoryStream();
            byte[] content;
            await using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                await using (var csv = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    await csv.WriteRecordsAsync(exportDtos, cancellationToken);

                    await streamWriter.FlushAsync();
                    content = memoryStream.ToArray();
                }
            }

            var result = new ExportInvoicesDto
            {
                FileName = $"invoice_list_export_{DateTime.Now}",
                ContentType = ".csv",
                Content = content
            };


            return Result.Ok(value: result);
        }
    }
}
