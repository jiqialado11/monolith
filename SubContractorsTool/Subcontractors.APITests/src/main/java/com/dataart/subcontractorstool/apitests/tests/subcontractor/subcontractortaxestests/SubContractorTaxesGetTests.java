package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxestests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxtests.SubContractorTaxTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorTaxesGetTests {
    private SubContractorController subContractorController;
    private int subContractorIdWithNoTaxes;
    private int[] taxesId = new int[5];
    private int subContractorId;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();
        subContractorIdWithNoTaxes = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        for (int i = 0; i < taxesId.length; i++){
            taxesId[i] = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE)).getData();
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/{SubcontractorId}/Tax endpoint
     * AND all values are valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorTaxesGetTest() {
        SubContractorTaxesGet response = subContractorController.getTaxes(subContractorId);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        for (int i = 0; i < taxesId.length; i++){
            Assert.assertEquals(response.getData().get(i).getId(), taxesId[i]);
            Assert.assertEquals(response.getData().get(i).getSubContractorId(), subContractorId);
            Assert.assertEquals(response.getData().get(i).getTaxTypeId(), SubContractorTaxTestsConstants.TAX_TYPE_ID);
            Assert.assertEquals(response.getData().get(i).getName(), SubContractorTaxTestsConstants.TAX_NAME);
            Assert.assertEquals(response.getData().get(i).getNumber(), SubContractorTaxTestsConstants.TAX_NUMBER);
            Assert.assertEquals(response.getData().get(i).getUrl(), SubContractorTaxTestsConstants.URL);
            Assert.assertEquals(response.getData().get(0).getDate(), SubContractorTaxTestsConstants.DATE);
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/{SubcontractorId}/Tax endpoint
     * AND SubContractor has no taxes
     * THEN I should get Status Code of 404 and error message
     */
    @Test
    public void subContractorTaxesGetSubContractorWithNoTaxesTest() {
        SubContractorTaxesGet response = subContractorController.getTaxes(subContractorIdWithNoTaxes);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(response.getMessage(), SubContractorTaxesTestsConstants.ERROR_MESSAGE_SUBCONTRACTOR_WITH_NO_TAXES_PART_ONE + subContractorIdWithNoTaxes + SubContractorTaxesTestsConstants.ERROR_MESSAGE_SUBCONTRACTOR_WITH_NO_TAXES_PART_TWO);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/{SubcontractorId}/Tax endpoint
     * AND SubContractor ID is non-existent
     * THEN I should get Status Code of 404 and error message
     */
    @Test
    public void subContractorTaxesGetNonExistentSubContractorIdTest() {
        SubContractorTaxesGet response = subContractorController.getTaxes(SubContractorTaxesTestsConstants.SUBCONTRACTOR_ID_NON_EXISTENT);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(response.getMessage(), SubContractorTaxesTestsConstants.ERROR_MESSAGE_SUBCONTRACTOR_ID_NON_EXISTENT + SubContractorTaxesTestsConstants.SUBCONTRACTOR_ID_NON_EXISTENT);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/{SubcontractorId}/Tax endpoint
     * AND SubContractor ID is text
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void subContractorTaxesGetSubContractorIdIsTextTest() {
        SubContractorTaxesGet response = subContractorController.getTaxes(SubContractorTaxesTestsConstants.SUBCONTRACTOR_ID_TEXT);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }
}