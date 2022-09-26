package com.dataart.subcontractorstool.apitests.responseentities.project;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ProjectPmIdGetData {
    Integer projectPmId;
    Integer projectGroupId;
    String projectGroup;
    Integer projectManagerId;
    String projectManager;
    String startDate;
    String estimatedFinishDate;
    String finishDate;
    Integer statusId;
    String status;
}