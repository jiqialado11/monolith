package com.dataart.subcontractorstool.apitests.responseentities.check;

import lombok.AllArgsConstructor;
import lombok.Getter;

import java.util.List;

@Getter
@AllArgsConstructor
public class CheckSanctionChecksGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    List<CheckSanctionChecksGetData> data;
}