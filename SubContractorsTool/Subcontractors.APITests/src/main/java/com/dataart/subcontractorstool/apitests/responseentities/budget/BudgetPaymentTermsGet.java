package com.dataart.subcontractorstool.apitests.responseentities.budget;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class BudgetPaymentTermsGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    BudgetPaymentTermsGetData[] data;
}