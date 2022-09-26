package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

import java.math.BigDecimal;

@Getter
@AllArgsConstructor
public class InvoicePagesAllGetDataItem {
    Integer id;
    Integer paymentNumber;
    Integer referralId;
    BigDecimal amount;
    String currencyCode;
    String startDate;
    String endDate;
    String invoiceDate;
    String paidDate;
    String plannedPaidDate;
    String subContractorName;
    String subContractorType;
    Integer subContractorTypeId;
    String invoiceNumber;
    Integer invoiceStatusId;
    String invoiceStatus;
    String legalEntity;
    String accountManagerName;
    String approverName;
    String projectId;
}