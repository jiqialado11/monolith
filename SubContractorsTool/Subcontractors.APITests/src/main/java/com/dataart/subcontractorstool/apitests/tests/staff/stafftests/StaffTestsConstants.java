package com.dataart.subcontractorstool.apitests.tests.staff.stafftests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class StaffTestsConstants {
    public static final String FIRST_NAME = "Marlena";

    public static final String LAST_NAME = "Hestrop";

    public static final String EMAIL = "dataart@dataart.com";

    public static final String SKYPE = "skype";

    public static final String SKYPE_UPDATE = "skype update";

    public static final String POSITION = "position";

    public static final String START_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));;

    public static final String END_DATE = LocalDateTime.now().plusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String QUALIFICATIONS = "qualifications";

    public static final String REAL_LOCATION = "New York";

    public static final String CELL_PHONE = "+63 (742) 981-5777";

    public static final Boolean IS_NDA_SIGNED = true;

    public static final String DEPARTMENT_NAME = "department name";

    public static final String DOMAIN_LOGIN = "universe\\\\dataart";

    public static final int STATUS_ID = 1;
}