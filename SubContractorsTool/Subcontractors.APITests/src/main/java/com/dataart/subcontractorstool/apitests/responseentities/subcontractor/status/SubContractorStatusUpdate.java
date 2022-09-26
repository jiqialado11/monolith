package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.status;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorStatusUpdate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
}