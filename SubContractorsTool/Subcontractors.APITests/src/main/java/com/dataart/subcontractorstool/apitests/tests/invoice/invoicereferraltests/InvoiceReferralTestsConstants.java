package com.dataart.subcontractorstool.apitests.tests.invoice.invoicereferraltests;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

public class InvoiceReferralTestsConstants {
    public static final LocalDate PROBATION_START_DATE = LocalDate.parse(LocalDate.now().minusDays(90).format(DateTimeFormatter.ofPattern("yyyy-MM-dd")));

    public static final int PAYMENT_NUMBER_1 = 1;

    public static final int PAYMENT_NUMBER_2 = 2;

    public static final int CURRENT_PAGE = 1;

    public static final int RESULTS_PER_PAGE = 1000000;
}