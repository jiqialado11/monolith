package com.dataart.subcontractorstool.apitests.responseentities.compliance;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ComplianceFileIdGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    ComplianceFileIdGetData data;
}