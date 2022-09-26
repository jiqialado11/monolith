package com.dataart.subcontractorstool.apitests.responseentities.common;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CommonCurrenciesGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    CommonCurrenciesGetData[] data;
}