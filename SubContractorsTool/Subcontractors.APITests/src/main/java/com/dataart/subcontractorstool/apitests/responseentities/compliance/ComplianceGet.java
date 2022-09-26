package com.dataart.subcontractorstool.apitests.responseentities.compliance;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ComplianceGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    ComplianceGetData[] data;
}