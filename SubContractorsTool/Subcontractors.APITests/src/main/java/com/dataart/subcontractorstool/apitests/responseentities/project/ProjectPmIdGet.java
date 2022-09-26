package com.dataart.subcontractorstool.apitests.responseentities.project;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ProjectPmIdGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    ProjectPmIdGetData data;
}