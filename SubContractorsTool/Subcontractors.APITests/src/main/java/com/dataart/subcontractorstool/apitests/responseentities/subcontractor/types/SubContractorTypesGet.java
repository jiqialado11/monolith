package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.types;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorTypesGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorTypesGetData[] data;
}