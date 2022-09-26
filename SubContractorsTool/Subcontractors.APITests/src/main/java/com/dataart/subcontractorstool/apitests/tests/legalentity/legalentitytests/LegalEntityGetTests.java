package com.dataart.subcontractorstool.apitests.tests.legalentity.legalentitytests;

import com.dataart.subcontractorstool.apitests.controllers.LegalEntityController;
import com.dataart.subcontractorstool.apitests.responseentities.legalentity.LegalEntityGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class LegalEntityGetTests {
    private LegalEntityController legalEntityController;

    @BeforeClass
    public void setupTest() {
        legalEntityController = new LegalEntityController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/LegalEntity endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorStatusGetTest() {
        LegalEntityGet response = legalEntityController.getLegalEntity();

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData());
    }
}