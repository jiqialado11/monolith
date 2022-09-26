package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractoralltests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.all.SubContractorAllGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;

public class SubContractorAllGetTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/All endpoint
     * THEN I should get Status Code of 200 and success message
     * AND all SubContractors in the returned list have status "Active"
     */
    @Test
    public void subContractorsAllGetTest() {
        SubContractorAllGet response = subContractorController.getSubContractorsAll();

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Arrays.stream(response.getData()).forEach(data -> {
            SubContractorGet subContractorGetResponse = subContractorController.getSubContractor(data.getId());
            Assert.assertEquals(subContractorGetResponse.getData().getSubContractorStatus().getId(), SubContractorTestsConstants.SUBCONTRACTOR_STATUS_ACTIVE);
        });
    }
}