package com.dataart.subcontractorstool.apitests.responseentities.staff;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class StaffPmIdGetData {
    Integer pmId;
    String firstname;
    String lastname;
    String email;
    String skype;
    String position;
    String cannotLoginBefore;
    String cannotLoginAfter;
    Integer locationId;
    String location;
    String realLocation;
    String cellPhone;
    Boolean isNdaSigned;
    String departmentName;
    String domainLogin;
    String qualification;
}