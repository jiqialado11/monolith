package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementAddendumGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    AgreementAddendumGetData data;
}