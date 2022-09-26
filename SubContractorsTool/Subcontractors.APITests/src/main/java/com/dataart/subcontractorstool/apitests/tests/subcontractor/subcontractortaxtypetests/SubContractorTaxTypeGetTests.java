package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxtypetests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxTypeGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorTaxTypeGetTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() { subContractorController = new SubContractorController(); }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/TaxType endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subcontractorTaxTypeGetTest() {
        SubContractorTaxTypeGet response = subContractorController.getTaxType();

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(response.getData().size(), SubContractorTaxTypeTestsConstants.TAX_TYPE_LIST_SIZE);
        Assert.assertEquals(response.getData().get(0).getId(), SubContractorTaxTypeTestsConstants.W9_TAX_FORM_ID);
        Assert.assertEquals(response.getData().get(0).getName(), SubContractorTaxTypeTestsConstants.W9_TAX_FORM_NAME);
        Assert.assertEquals(response.getData().get(1).getId(), SubContractorTaxTypeTestsConstants.W8BEN_TAX_FORM_ID);
        Assert.assertEquals(response.getData().get(1).getName(), SubContractorTaxTypeTestsConstants.W8BEN_TAX_FORM_NAME);
    }
}