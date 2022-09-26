package com.dataart.subcontractorstool.apitests.responseentities.project;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ProjectSubContractorIdGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    ProjectSubContractorIdGetData[] data;
}