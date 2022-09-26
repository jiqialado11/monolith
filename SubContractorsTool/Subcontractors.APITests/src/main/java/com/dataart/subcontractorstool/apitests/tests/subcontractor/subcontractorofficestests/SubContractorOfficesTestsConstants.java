package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractorofficestests;

import java.util.Map;

public class SubContractorOfficesTestsConstants {
    public static final Map<Integer, Map<String, String>> DEVELOPMENT_OFFICES_LIST = Map.of(
            2, Map.of("sample development office", "DevelopmentOffice")
    );

    public static final Map<Integer, Map<String, String>> SALES_OFFICES_LIST = Map.of(
            1, Map.of("sample sales office", "SalesOffice")
    );

    public static final byte DEVELOPMENT_OFFICE_TYPE_ID = 2;

    public static final byte SALES_OFFICE_TYPE_ID = 1;
}