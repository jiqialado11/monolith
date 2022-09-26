package com.dataart.subcontractorstool.apitests.responseentities.compliance;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ComplianceTypesGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    ComplianceTypesGetData[] data;
}