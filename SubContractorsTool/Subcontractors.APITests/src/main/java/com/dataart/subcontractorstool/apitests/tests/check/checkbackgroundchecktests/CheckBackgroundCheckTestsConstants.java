package com.dataart.subcontractorstool.apitests.tests.check.checkbackgroundchecktests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class CheckBackgroundCheckTestsConstants {
    public static final int CHECK_STATUS_ID = 1;

    public static final String DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String CURRENT_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String LINK = "https:/link.com";
}