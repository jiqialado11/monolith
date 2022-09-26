package com.dataart.subcontractorstool.apitests.responseentities.agreement;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class AgreementAddendaGetData {
    Integer id;
    String title;
    AgreementAddendumGetDataProjects[] projects;
    Integer legalEntityId;
    String legalEntity;
    String startDate;
    String endDate;
    String docUrl;
    String parentDocUrl;
    String comment;
}