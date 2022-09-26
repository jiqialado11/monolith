package com.dataart.subcontractorstool.apitests.responseentities.staff;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class StaffCreate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    Integer data;
}