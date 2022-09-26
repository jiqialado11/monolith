package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class InvoiceMilestonesGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    InvoiceMilestonesGetData[] data;
}