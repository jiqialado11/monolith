package com.dataart.subcontractorstool.apitests.responseentities.check;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CheckSanctionCheckGetData {
    Integer id;
    Integer checkApproverId;
    String checkApprover;
    Integer checkStatusId;
    String checkStatus;
    String date;
    String comment;
}