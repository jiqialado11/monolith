package com.dataart.subcontractorstool.apitests.responseentities.staff;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class StaffPmGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    StaffPmGetData[] data;
}