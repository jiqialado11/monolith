package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.status;

import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.markets.SubContractorMarketsGetData;
import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorStatusGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorStatusGetData[] data;
}