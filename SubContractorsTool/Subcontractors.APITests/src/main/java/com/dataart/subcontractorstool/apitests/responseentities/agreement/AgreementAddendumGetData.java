package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementAddendumGetData {
    Integer id;
    String title;
    Integer subContractorId;
    Integer agreementId;
    AgreementAddendumGetDataProjects[] projects;
    String startDate;
    String endDate;
    String comment;
    Integer paymentTermId;
    String paymentTerm;
    Integer paymentTermInDays;
    Integer currencyId;
    String currency;
    String docUrl;
    String parentDocUrl;
    Boolean isForNonBillableProjects;
}