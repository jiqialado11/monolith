package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class InvoiceIdGetData {
    Integer id;
    String startDate;
    String endDate;
    String invoiceDate;
    String projectId;
    Integer addendumId;
    Integer subcontractorId;
    Integer agreementId;
    Integer amount;
    Integer taxRate;
    Integer taxAmount;
    String currency;
    String invoiceNumber;
    String comment;
    Integer invoiceStatusId;
    String invoiceStatus;
    InvoiceIdGetDataInvoiceFile invoiceFile;
    InvoiceIdGetDataSupportingDocuments[] supportingDocuments;
}