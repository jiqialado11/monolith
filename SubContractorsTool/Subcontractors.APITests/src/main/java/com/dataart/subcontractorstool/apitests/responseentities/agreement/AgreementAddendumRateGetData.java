package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementAddendumRateGetData {
    Integer addendumId;
    Integer id;
    String title;
    Integer staffId;
    String staff;
    Integer rateUnitId;
    String rateUnit;
    String fromDate;
    String toDate;
    String description;
    Integer rateValue;
}