package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorsGetDataItem {
    Integer id;
    String name;
    String type;
    String description;
    String location;
    String rating;
    String status;
    String comment;
    String skills;
    Boolean isNdaSigned;
    String contact;
    String materials;
    String lastInteractionDate;
    SubContractorsGetDataItemBudgetLocation[] budgetLocations;
    SubContractorsGetDataItemPaymentTerms[] paymentTerms;
    SubContractorsGetDataItemLegalEntities[] legalEntities;
    SubContractorsGetDataItemProjectGroups[] projectGroups;
    SubContractorsGetDataItemOffices[] offices;
    SubContractorsGetDataItemMarkets[] markets;
}