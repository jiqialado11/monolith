package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor;

import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxesGetData;
import lombok.AllArgsConstructor;
import lombok.Getter;

import java.util.List;

@Getter
@AllArgsConstructor
public class SubContractorsGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorsGetData data;
}