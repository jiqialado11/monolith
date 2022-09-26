package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementAddendumRatesGetData {
    Integer addendumId;
    Integer rateId;
    String title;
    Integer rateValue;
    Integer rateUnitId;
    String rateUnit;
    String fromDate;
    String toDate;
    String description;
    Integer staffId;
    String staff;
}