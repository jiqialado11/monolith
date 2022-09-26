package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.all;

import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.markets.SubContractorMarketsGetData;
import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorAllGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorAllGetData[] data;
}