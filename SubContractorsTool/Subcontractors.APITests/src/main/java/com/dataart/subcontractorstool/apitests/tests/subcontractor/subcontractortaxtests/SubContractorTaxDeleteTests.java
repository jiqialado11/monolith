package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxtests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxDelete;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorTaxDeleteTests {
    private SubContractorController subContractorController;
    private int subContractorId;
    private int taxId;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        taxId = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/Tax endpoint
     * AND Tax ID value is valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void taxDeleteTest() {
        SubContractorTaxDelete deleteResponse = subContractorController.deleteTax(taxId);

        Assert.assertTrue(deleteResponse.getIsSuccess());
        Assert.assertEquals(deleteResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(deleteResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        SubContractorTaxGet getResponse = subContractorController.getTax(taxId);

        Assert.assertFalse(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(getResponse.getMessage(), SubContractorTaxTestsConstants.ERROR_MESSAGE_INVALID_TAX_SECOND_VERSION + taxId);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/Tax endpoint
     * AND Tax ID value is non-existent Tax ID
     * THEN I should get Status Code of 404 and error message
     */
    @Test
    public void taxDeleteNonExistentTaxIdTest() {
        SubContractorTaxDelete response = subContractorController.deleteTax(SubContractorTaxTestsConstants.TAX_ID_NON_EXISTENT);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(response.getMessage(), SubContractorTaxTestsConstants.ERROR_MESSAGE_INVALID_TAX + SubContractorTaxTestsConstants.TAX_ID_NON_EXISTENT);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/Tax endpoint
     * AND Tax ID value is negative
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxDeleteNegativeTaxIdTest() {
        SubContractorTaxDelete response = subContractorController.deleteTax(SubContractorTaxTestsConstants.TAX_ID_NEGATIVE);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/Tax endpoint
     * AND Tax ID value is zero
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxDeleteZeroTaxIdTest() {
        SubContractorTaxDelete response = subContractorController.deleteTax(SubContractorTaxTestsConstants.TAX_ID_ZERO);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/Tax endpoint
     * AND Tax ID value is longer than Integer
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxDeleteLongerThanIntegerTaxIdTest() {
        SubContractorTaxDelete response = subContractorController.deleteTax(SubContractorTaxTestsConstants.TAX_ID_LONGER_THAN_INTEGER);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/Tax endpoint
     * AND Tax ID value is text
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxDeleteTextTaxIdTest() {
        SubContractorTaxDelete response = subContractorController.deleteTax(SubContractorTaxTestsConstants.TAX_ID_TEXT);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/Tax endpoint
     * AND Tax ID value is special character
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxDeleteSpecCharTaxIdTest() {
        SubContractorTaxDelete response = subContractorController.deleteTax(SubContractorTaxTestsConstants.TAX_ID_SPEC_CHAR);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/Tax endpoint
     * AND Tax ID value is empty
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxDeleteEmptyTaxIdTest() {
        subContractorController.deleteTax();
    }
}