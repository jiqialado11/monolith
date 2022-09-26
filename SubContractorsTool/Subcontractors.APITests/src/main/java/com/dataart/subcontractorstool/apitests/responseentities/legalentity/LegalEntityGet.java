package com.dataart.subcontractorstool.apitests.responseentities.legalentity;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class LegalEntityGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    LegalEntityGetData[] data;
}