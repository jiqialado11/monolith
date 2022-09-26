package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementGetData {
    Integer id;
    String title;
    Integer subContractorId;
    String subContractor;
    Integer legalEntityId;
    String legalEntity;
    String startDate;
    String endDate;
    Integer locationId;
    String location;
    String conditions;
    Integer paymentMethodId;
    String paymentMethod;
    String url;
}