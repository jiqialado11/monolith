package com.dataart.subcontractorstool.apitests.responseentities.compliance;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ComplianceRatingsGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    ComplianceRatingsGetData[] data;
}