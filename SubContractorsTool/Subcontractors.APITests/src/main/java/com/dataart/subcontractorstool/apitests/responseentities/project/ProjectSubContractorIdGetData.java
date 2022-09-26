package com.dataart.subcontractorstool.apitests.responseentities.project;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ProjectSubContractorIdGetData {
    String id;
    Integer pmId;
    String name;
    Integer projectGroupId;
    Integer projectGroupPmId;
    String projectGroupName;
    Integer projectManagerId;
    String projectManager;
    String startDate;
    String estimatedFinishDate;
    String finishDate;
    String invoiceApproveId;
    String invoiceApproveName;
    Integer statusId;
    String status;
}