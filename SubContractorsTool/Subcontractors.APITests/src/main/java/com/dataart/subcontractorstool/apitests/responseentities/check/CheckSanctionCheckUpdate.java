package com.dataart.subcontractorstool.apitests.responseentities.check;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CheckSanctionCheckUpdate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
}