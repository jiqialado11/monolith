package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class InvoiceStatusUpdate {
    Boolean isSuccess;
    Integer statusCode;
    String message;
}