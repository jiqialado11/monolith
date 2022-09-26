package com.dataart.subcontractorstool.apitests.responseentities.project;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ProjectIdGetData {
    String id;
    Integer pmId;
    String projectName;
    Integer projectGroupId;
    String projectGroup;
    Integer projectGroupPmId;
    Integer projectManagerId;
    String projectManager;
    String startDate;
    String estimatedFinishDate;
    String finishDate;
    Integer projectStatusId;
    String projectStatus;
}