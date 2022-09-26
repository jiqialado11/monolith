package com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SubContractorTaxesGetData {
    Integer id;
    Integer subContractorId;
    Integer taxTypeId;
    String name;
    String number;
    String url;
    String date;
}