package com.dataart.subcontractorstool.apitests.tests.check.checksanctionchecktests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class CheckSanctionCheckTestsConstants {
    public static final int PARENT_TYPE_SUBCONTRACTOR = 1;

    public static final int PARENT_TYPE_STAFF = 2;

    public static final int CHECK_STATUS_ID = 1;

    public static final String DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String CURRENT_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String COMMENT = "Comment";
}