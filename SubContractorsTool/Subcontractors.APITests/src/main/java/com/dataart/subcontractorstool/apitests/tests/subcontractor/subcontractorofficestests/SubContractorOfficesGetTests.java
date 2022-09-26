package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractorofficestests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.offices.SubContractorOfficesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class SubContractorOfficesGetTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Offices endpoint
     * AND OfficeTypeId value equals to 1 - Sales offices
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorSalesOfficesGetTest() {
        SubContractorOfficesGet response = subContractorController.getSubContractorOffices(SubContractorOfficesTestsConstants.SALES_OFFICE_TYPE_ID);

        Map<Integer, Map<String, String>> responseSubContractorOfficesList = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> {
                    Map<String, String> officesData = new HashMap<>();
                    officesData.put(data.getName(), data.getOfficeType());
                    responseSubContractorOfficesList.put(data.getId(), officesData);
                }
        );

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseSubContractorOfficesList, SubContractorOfficesTestsConstants.SALES_OFFICES_LIST);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Offices endpoint
     * AND OfficeTypeId value equals to 2 - Development offices
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorDevelopmentOfficesGetTest() {
        SubContractorOfficesGet response = subContractorController.getSubContractorOffices(SubContractorOfficesTestsConstants.DEVELOPMENT_OFFICE_TYPE_ID);

        Map<Integer, Map<String, String>> responseSubContractorOfficesList = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> {
                Map<String, String> officesData = new HashMap<>();
                officesData.put(data.getName(), data.getOfficeType());
                responseSubContractorOfficesList.put(data.getId(), officesData);
            }
        );

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseSubContractorOfficesList, SubContractorOfficesTestsConstants.DEVELOPMENT_OFFICES_LIST);
    }
}