package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementAddendumRateGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    AgreementAddendumRateGetData data;
}