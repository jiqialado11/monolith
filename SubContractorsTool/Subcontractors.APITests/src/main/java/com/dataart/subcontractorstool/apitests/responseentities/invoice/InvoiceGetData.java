package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class InvoiceGetData {
    Integer id;
    String startDate;
    String endDate;
    String project;
    Integer amount;
    String currency;
    String invoiceNumber;
    Integer invoiceStatusId;
    String invoiceStatus;
    String projectId;
}