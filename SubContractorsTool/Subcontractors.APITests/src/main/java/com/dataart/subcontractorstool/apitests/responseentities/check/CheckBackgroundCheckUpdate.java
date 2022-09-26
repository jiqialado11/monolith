package com.dataart.subcontractorstool.apitests.responseentities.check;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CheckBackgroundCheckUpdate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
}