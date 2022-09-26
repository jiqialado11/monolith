package com.dataart.subcontractorstool.apitests.responseentities.staff;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class StaffUpdate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
}