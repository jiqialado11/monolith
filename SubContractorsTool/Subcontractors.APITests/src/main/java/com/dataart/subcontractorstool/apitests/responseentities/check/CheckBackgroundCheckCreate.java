package com.dataart.subcontractorstool.apitests.responseentities.check;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CheckBackgroundCheckCreate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    Integer data;
}