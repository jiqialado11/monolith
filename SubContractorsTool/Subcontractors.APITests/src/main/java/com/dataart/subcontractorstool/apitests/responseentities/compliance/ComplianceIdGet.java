package com.dataart.subcontractorstool.apitests.responseentities.compliance;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ComplianceIdGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    ComplianceIdGetData data;
}