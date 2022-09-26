package com.dataart.subcontractorstool.apitests.responseentities.budget;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class BudgetPaymentMethodsGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    BudgetPaymentMethodsGetData[] data;
}