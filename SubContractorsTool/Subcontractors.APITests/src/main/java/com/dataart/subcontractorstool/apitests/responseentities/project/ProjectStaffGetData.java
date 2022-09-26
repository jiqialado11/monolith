package com.dataart.subcontractorstool.apitests.responseentities.project;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ProjectStaffGetData {
    String id;
    String name;
    Integer projectGroupId;
    String projectGroupName;
    Integer projectManagerId;
    String projectManagerName;
    String startDate;
    String finishDate;
    Integer statusId;
    String status;
}