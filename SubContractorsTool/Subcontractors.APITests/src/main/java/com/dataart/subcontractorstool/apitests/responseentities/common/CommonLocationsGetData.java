package com.dataart.subcontractorstool.apitests.responseentities.common;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CommonLocationsGetData {
    Integer id;
    String name;
    String code;
    String code3;
    String email;
    String hrEmail;
    Boolean isVirtual;
}