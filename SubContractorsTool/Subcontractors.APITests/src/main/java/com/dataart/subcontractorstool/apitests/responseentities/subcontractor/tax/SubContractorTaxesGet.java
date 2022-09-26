package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax;

import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorsGetData;
import lombok.AllArgsConstructor;
import lombok.Getter;

import java.util.List;

@Getter
@AllArgsConstructor
public class SubContractorTaxesGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    List<SubContractorTaxesGetData> data;
}