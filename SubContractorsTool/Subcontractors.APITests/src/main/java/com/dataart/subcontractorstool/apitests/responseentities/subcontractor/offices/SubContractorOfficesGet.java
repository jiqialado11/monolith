package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.offices;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorOfficesGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorOfficesGetData[] data;
}