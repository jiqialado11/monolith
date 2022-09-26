package com.dataart.subcontractorstool.apitests.tests.compliance.compliancefiletests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class ComplianceFileTestsConstants {
    public static final String CURRENT_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String PATH = "src/main/java/com/dataart/subcontractorstool/apitests/tests/compliance/compliancefiletests/";

    public static final String NAME = "ComplianceDocument.docx";

    public static final String[] NAMES = {"ComplianceDocument_1.docx", "ComplianceDocument_2.docx", "ComplianceDocument_3.docx", "ComplianceDocument_4.docx", "ComplianceDocument_5.docx"};

    public static final String COMPLIANCE_DOCUMENT_CONTENT = "Compliance date " + CURRENT_DATE;
}