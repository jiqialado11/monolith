package com.dataart.subcontractorstool.apitests.responseentities.check;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CheckBackgroundCheckGetData {
    Integer id;
    Integer checkApproverId;
    String checkApprover;
    Integer checkStatusId;
    String checkStatus;
    String date;
    String link;
}