package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class SubContractorTestsConstants {
    public static final String CURRENT_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String SUBCONTRACTOR_NAME = "SubContractor " + CURRENT_DATE;

    public static final int SUBCONTRACTOR_TYPE_ID = 2;

    public static final String SKILLS = "Portfolio management";

    public static final String COMMENT = "Comment";

    public static final String CONTACT = "+62 (483) 828-7689";

    public static final String LAST_INTERACTION_DATE = LocalDateTime.now().minusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final Boolean IS_NDA_SIGNED = true;

    public static final int SALES_OFFICE_ID = 1;

    public static final int DEVELOPMENT_OFFICE_ID = 2;

    public static final String COMPANY_SITE = "companysite.com";

    public static final String MATERIALS = "materials";

    public static final int MARKET_ID = 3;

    public static final String SUBCONTRACTOR_NAME_UPDATE = "SubContractor Update" + CURRENT_DATE;

    public static final int SUBCONTRACTOR_TYPE_UPDATE = 1;

    public static final int SUBCONTRACTOR_STATUS_UPDATE = 3;

    public static final String SKILLS_UPDATE = "Portfolio management Update";

    public static final String COMMENT_UPDATE = "Comment Update";

    public static final String CONTACT_UPDATE = "+73 (295) 623-1701";

    public static final String LAST_INTERACTION_DATE_UPDATE = LocalDateTime.now().minusMonths(1).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final Boolean IS_NDA_SIGNED_UPDATE = false;

    public static final int SALES_OFFICE_ID_UPDATE = 1;

    public static final int DEVELOPMENT_OFFICE_ID_UPDATE = 2;

    public static final String COMPANY_SITE_UPDATE = "companysite.com Update";

    public static final String MATERIALS_UPDATE = "materials Update";

    public static final int MARKET_ID_UPDATE = 2;

    public static final int ACCOUNT_MANAGER_ID_UPDATE = 57;

    public static final byte QUERY_TYPE_LIBRARY = 1;

    public static final byte QUERY_TYPE_LIST = 0;

    public static final byte SUBCONTRACTOR_STATUS_ACTIVE = 1;

    public static final byte SUBCONTRACTOR_STATUS_INACTIVE = 2;

    public static final byte SUBCONTRACTOR_STATUS_TENTATIVE = 3;

    public static final int RESULTS_QUANTITY = 1000000;
}