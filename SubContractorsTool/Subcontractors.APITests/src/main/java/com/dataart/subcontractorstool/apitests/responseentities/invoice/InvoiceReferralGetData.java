package com.dataart.subcontractorstool.apitests.responseentities.invoice;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class InvoiceReferralGetData {
    Integer referralId;
    String firstName;
    String lastName;
    String startDate;
}