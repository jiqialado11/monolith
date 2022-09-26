package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorTaxCreateValidationErrorErrors {
    String[] SubContractorId;
    String[] TaxTypeId;
    String[] Name;
    String[] TaxNumber;
    String[] Url;
    String[] Date;
}