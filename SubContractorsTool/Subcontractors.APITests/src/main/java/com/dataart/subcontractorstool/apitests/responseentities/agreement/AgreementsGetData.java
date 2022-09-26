package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementsGetData {
    Integer id;
    String title;
    String conditions;
    Integer legalEntityId;
    String legalEntity;
    String url;
    Integer locationId;
    String location;
    Integer paymentMethodId;
    String paymentMethod;
    AgreementsGetDataPaymentTerms[] paymentTerms;
    String startDate;
    String endDate;
}