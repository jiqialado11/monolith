package com.dataart.subcontractorstool.apitests.utils.testentities;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class Office {
    Integer id;
    String name;
    String code;
    Currency currency;
}