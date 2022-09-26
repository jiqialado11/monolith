package com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumratetests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class AgreementAddendumRateTestsConstants {
    public static final String NAME = "Rate name";

    public static final String NAME_UPDATE = "Rate name update";

    public static final int RATE = 100;

    public static final int RATE_UNIT_ID = 1;

    public static final String FROM_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String TO_DATE = LocalDateTime.now().plusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));;

    public static final String DESCRIPTION = "description";
}