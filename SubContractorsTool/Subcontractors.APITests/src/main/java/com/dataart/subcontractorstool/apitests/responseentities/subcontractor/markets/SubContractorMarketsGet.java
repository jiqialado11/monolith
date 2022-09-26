package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.markets;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorMarketsGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorMarketsGetData[] data;
}