package com.dataart.subcontractorstool.apitests.tests.compliance.compliancetests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class ComplianceTestsConstants {
    public static final int COMPLIANCE_TYPE_ID = 1;

    public static final int COMPLIANCE_TYPE_ID_UPDATE = 2;

    public static final int COMPLIANCE_RATING_ID = 1;

    public static final String EXPIRATION_DATE = LocalDateTime.now().plusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String COMMENT = "comment";

    public static final String COMPLIANCES_NOT_FOUND_MESSAGE_PART_1 = "SubContractor with identifier ";

    public static final String COMPLIANCES_NOT_FOUND_MESSAGE_PART_2 = " doesn't have compliances";
}