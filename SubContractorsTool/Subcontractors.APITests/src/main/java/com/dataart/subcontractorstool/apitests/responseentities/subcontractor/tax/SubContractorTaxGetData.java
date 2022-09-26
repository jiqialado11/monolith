package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorTaxGetData {
    Integer id;
    Integer subContractorId;
    Integer taxTypeId;
    String name;
    String taxNumber;
    String url;
    String date;
}