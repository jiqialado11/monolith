package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementAddendaGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    AgreementAddendaGetData[] data;
}