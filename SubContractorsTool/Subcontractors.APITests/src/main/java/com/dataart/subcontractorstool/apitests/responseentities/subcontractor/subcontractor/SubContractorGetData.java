package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorGetData {
    String name;
    String description;
    Integer locationId;
    String location;
    String comment;
    String lastInteractionDate;
    Boolean isNdaSigned;
    String companySite;
    String contact;
    String skills;
    String materials;
    SubContractorGetDataSubContractorType subContractorType;
    SubContractorGetDataSubContractorStatus subContractorStatus;
    SubContractorGetDataAccountManager accountManager;
    SubContractorGetDataSalesOffices[] salesOffices;
    SubContractorGetDataDevelopmentOffices[] developmentOffices;
    SubContractorGetDataMarkets[] markets;
}