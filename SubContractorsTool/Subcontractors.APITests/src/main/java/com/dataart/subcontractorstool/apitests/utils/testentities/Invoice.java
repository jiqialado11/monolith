package com.dataart.subcontractorstool.apitests.utils.testentities;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class Invoice {
    String number;
    String subContractorName;
    String startDate;
    String endDate;
    String invoiceDate;
    String paymentNumber;
    String taxRate;
    String taxAmount;
    String comment;
    String status;
}