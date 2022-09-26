package com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class AgreementTestsConstants {
    public static final String TITLE = "Title";

    public static final String TITLE_UPDATE = "Title update";

    public static final String START_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));;

    public static final String END_DATE = LocalDateTime.now().plusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));;

    public static final String CONDITIONS = "Conditions";

    public static final String AGREEMENT_URL = "dataart.sharepoint.com/agreement";

    public static final String AGREEMENT_NOT_FOUND_MESSAGE = "Couldn't find agreement with provided identifier ";
}