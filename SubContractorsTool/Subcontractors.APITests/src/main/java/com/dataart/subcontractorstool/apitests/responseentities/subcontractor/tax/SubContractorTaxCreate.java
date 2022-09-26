package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorTaxCreate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    Integer data;
}