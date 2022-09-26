package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementAddendumRatesGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    AgreementAddendumRatesGetData[] data;
}