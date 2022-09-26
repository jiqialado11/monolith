package com.dataart.subcontractorstool.apitests.responseentities.project;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ProjectCreate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    String data;
}