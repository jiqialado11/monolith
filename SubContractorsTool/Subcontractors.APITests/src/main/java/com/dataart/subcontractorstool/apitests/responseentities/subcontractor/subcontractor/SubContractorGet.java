package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorGetData data;
}