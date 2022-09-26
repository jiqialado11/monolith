package com.dataart.subcontractorstool.apitests.tests.invoice.invoicetests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class InvoiceTestsConstants {
    public static final String START_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String END_DATE = LocalDateTime.now().plusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String INVOICE_DATE = LocalDateTime.now().plusDays(5).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final Integer PAYMENT_NUMBER = null;

    public static final Integer REFERRAL_ID = null;

    public static final int AMOUNT = 1000;

    public static final int AMOUNT_UPDATE = 5000;

    public static final String INVOICE_NUMBER = "Invoice number " + START_DATE;

    public static final String NOT_PACKAGE_INVOICE_NUMBER = "Not package invoice number " + START_DATE;

    public static final int RATE = 1000;

    public static final byte TAX_RATE = 20;

    public static final int TAX_AMOUNT = AMOUNT * TAX_RATE/100 ;

    public static final String COMMENT = "Comment";

    public static final int INVOICE_NEW_STATUS_ID = 1;

    public static final int INVOICE_REVIEWED_STATUS_ID = 2;

    public static final int INVOICE_APPROVED_STATUS_ID = 3;

    public static final int INVOICE_REJECTED_STATUS_ID = 4;

    public static final int INVOICE_SENTTOPAY_STATUS_ID = 5;
}