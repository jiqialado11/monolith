package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorTaxDeleteValidationError {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    SubContractorTaxDeleteValidationErrorErrors errors;
}