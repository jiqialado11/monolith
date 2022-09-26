package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax;

import lombok.AllArgsConstructor;
import lombok.Getter;

import java.util.List;

@Getter
@AllArgsConstructor
public class SubContractorTaxTypeGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    List<SubContractorTaxTypeGetData> data;
}