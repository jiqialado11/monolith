package com.dataart.subcontractorstool.apitests.responseentities.staff;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class StaffPmIdGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    StaffPmIdGetData data;
}