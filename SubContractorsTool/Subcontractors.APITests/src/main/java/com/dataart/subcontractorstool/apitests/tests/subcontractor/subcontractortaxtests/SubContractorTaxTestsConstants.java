package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxtests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class SubContractorTaxTestsConstants {
    public static final String DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String CURRENT_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final int DATE_NUMBER = 2022;

    public static final String SUBCONTRACTOR_NAME = "SubContractor " + CURRENT_DATE;

    public static final String ERROR_MESSAGE_INVALID_SUBCONTRACTOR = "SubContractor wasn't found in database with provided identifier ";

    public static final String ERROR_MESSAGE_INVALID_SUBCONTRACTOR_SECOND_VERSION = "Couldn't find subContractor with provided identifier ";

    public static final String ERROR_MESSAGE_INVALID_TAX_TYPE = "Tax type wasn't found in database with provided identifier ";

    public static final String ERROR_MESSAGE_INVALID_TAX = "Tax wasn't found in database with provided identifier ";

    public static final String ERROR_MESSAGE_INVALID_TAX_SECOND_VERSION = "Couldn't find tax with provided identifier ";

    public static final int SUBCONTRACTOR_ID_NON_EXISTENT = 2147483647;

    public static final long SUBCONTRACTOR_ID_LONGER_THAN_INTEGER = 2147483648L;

    public static final int SUBCONTRACTOR_ID_NEGATIVE = -1;

    public static final int SUBCONTRACTOR_ID_ZERO = 0;

    public static final String SUBCONTRACTOR_ID_TEXT = "subcontractorId";

    public static final int TAX_TYPE_ID = 1;

    public static final int TAX_TYPE_ID_NON_EXISTENT = 7;

    public static final long TAX_TYPE_ID_LONGER_THAN_INTEGER = 2147483648L;

    public static final int TAX_TYPE_ID_NEGATIVE = -1;

    public static final int TAX_TYPE_ID_ZERO = 0;

    public static final String TAX_TYPE_ID_TEXT = "taxTypeId";

    public static final int TAX_ID_NON_EXISTENT = 2147483647;

    public static final int TAX_ID_NEGATIVE = -1;

    public static final int TAX_ID_ZERO = 0;

    public static final long TAX_ID_LONGER_THAN_INTEGER = 2147483648L;

    public static final String TAX_ID_TEXT = "taxId";

    public static final String TAX_ID_SPEC_CHAR = "$";

    public static final String TAX_ID_NUMBER_AND_SPEC_CHAR = "214$";

    public static final String TAX_NAME = "taxName";

    public static final int TAX_NAME_AS = 215;

    public static final String TAX_NUMBER = "taxNumber";

    public static final int TAX_NUMBER_NUMBER = 215;

    public static final String URL = "dataart.sharepoint.com";

    public static final int URL_NUMBER = 215;
}