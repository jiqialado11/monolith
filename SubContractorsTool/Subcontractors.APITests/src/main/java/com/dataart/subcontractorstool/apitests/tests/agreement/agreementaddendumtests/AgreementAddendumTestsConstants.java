package com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumtests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class AgreementAddendumTestsConstants {
    public static final String TITLE = "Title";

    public static final String TITLE_UPDATE = "Title update";

    public static final String START_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String END_DATE = LocalDateTime.now().plusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));;

    public static final String COMMENT = "comment";

    public static final int PAYMENT_TERM_IN_DAYS = 5;

    public static final String ADDENDUM_URL = "dataart.sharepoint.com/addendum";

    public static final Boolean IS_FOR_NON_BILLABLE_PROJECTS = false;

    public static final String ADDENDUM_NOT_FOUND_MESSAGE = "Couldn't find addendum with provided identifier ";
}