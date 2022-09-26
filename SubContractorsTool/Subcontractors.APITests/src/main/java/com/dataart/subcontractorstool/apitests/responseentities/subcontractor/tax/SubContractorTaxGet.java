package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorTaxGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorTaxGetData data;
}