package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

import java.math.BigDecimal;

@Getter
@AllArgsConstructor
public class InvoiceMilestonesGetData {
    Integer pmId;
    String name;
    BigDecimal amount;
    String toDate;
}