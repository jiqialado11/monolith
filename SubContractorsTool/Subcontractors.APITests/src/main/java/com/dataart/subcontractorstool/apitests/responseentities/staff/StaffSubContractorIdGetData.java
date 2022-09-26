package com.dataart.subcontractorstool.apitests.responseentities.staff;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class StaffSubContractorIdGetData {
    Integer id;
    Integer pmId;
    String firstName;
    String lastName;
    String position;
    Boolean isNdaSigned;
    Integer statusId;
    String status;
    String expirationDate;
}