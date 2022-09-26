package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class InvoicePagesAllGet {
    Boolean isSuccess;
    Integer statusCode;
    String message;
    InvoicePagesAllGetData data;
}