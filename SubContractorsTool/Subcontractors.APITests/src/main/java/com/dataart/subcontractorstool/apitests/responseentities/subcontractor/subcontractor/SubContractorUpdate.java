package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorUpdate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
}