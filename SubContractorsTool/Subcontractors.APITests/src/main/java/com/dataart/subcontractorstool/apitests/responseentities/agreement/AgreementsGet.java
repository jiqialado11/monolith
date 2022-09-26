package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementsGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    AgreementsGetData[] data;
}