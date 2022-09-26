package com.dataart.subcontractorstool.apitests.responseentities.budget;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class BudgetOfficeGetData {
    Integer id;
    String name;
    String code;
    BudgetOfficeGetDataCurrency currency;
}