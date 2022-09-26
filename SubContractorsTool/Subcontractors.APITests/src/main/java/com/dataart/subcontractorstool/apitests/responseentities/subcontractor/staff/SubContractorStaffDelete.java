package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.staff;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorStaffDelete {
    Boolean isSuccess;
    Integer statusCode;
    String message;
}