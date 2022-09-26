package com.dataart.subcontractorstool.apitests.tests.invoice.invoicefilestests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class InvoiceFilesTestsConstants {
    public static final String CURRENT_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String FILES_PATH = "src/main/java/com/dataart/subcontractorstool/apitests/tests/invoice/invoicefilestests/";

    public static final String FILE_NAME = "Document.docx";

    public static final String[] FILES_NAMES = {"Document_1.docx", "Document_2.docx", "Document_3.docx", "Document_4.docx", "Document_5.docx"};

    public static final String FILE_CONTENT = "Document date " + CURRENT_DATE;
}