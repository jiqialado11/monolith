package com.dataart.subcontractorstool.apitests.tests.invoice.invoicestatusestests;

import java.util.Map;

public class InvoiceStatusesTestsConstants {
    public static final Map<Integer, String> INVOICE_STATUSES = Map.of(
            1,"New",
            2,"Reviewed",
            3,"Approved",
            4,"Rejected",
            5,"Sent to pay"
    );
}