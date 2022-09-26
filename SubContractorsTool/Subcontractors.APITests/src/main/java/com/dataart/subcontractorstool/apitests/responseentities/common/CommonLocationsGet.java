package com.dataart.subcontractorstool.apitests.responseentities.common;

import com.dataart.subcontractorstool.apitests.responseentities.budget.BudgetOfficeGetData;
import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CommonLocationsGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    CommonLocationsGetData[] data;
}