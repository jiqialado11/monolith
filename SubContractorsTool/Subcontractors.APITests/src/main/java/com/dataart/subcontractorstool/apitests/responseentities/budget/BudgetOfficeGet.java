package com.dataart.subcontractorstool.apitests.responseentities.budget;

import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendaGetData;
import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class BudgetOfficeGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    BudgetOfficeGetData[] data;
}