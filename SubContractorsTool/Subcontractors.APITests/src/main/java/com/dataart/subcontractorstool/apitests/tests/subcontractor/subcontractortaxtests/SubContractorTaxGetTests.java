package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxtests;

import com.dataart.subcontractorstool.apitests.controllers.CommonController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxGetValidationFailure;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorTaxGetTests {
    private SubContractorController subContractorController;
    private CommonController commonController;
    private int subContractorId;
    private int taxId;

    @BeforeClass
    public void setupTest() {
        commonController = new CommonController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        taxId = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Tax/{Id} endpoint
     * AND Tax ID value is valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void taxGetTest() {
        SubContractorTaxGet response = subContractorController.getTax(taxId);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(response.getData().getId(), taxId);
        Assert.assertEquals(response.getData().getSubContractorId(), subContractorId);
        Assert.assertEquals(response.getData().getTaxTypeId(), SubContractorTaxTestsConstants.TAX_TYPE_ID);
        Assert.assertEquals(response.getData().getName(), SubContractorTaxTestsConstants.TAX_NAME);
        Assert.assertEquals(response.getData().getTaxNumber(), SubContractorTaxTestsConstants.TAX_NUMBER);
        Assert.assertEquals(response.getData().getUrl(), SubContractorTaxTestsConstants.URL);
        Assert.assertEquals(response.getData().getDate(), SubContractorTaxTestsConstants.DATE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Tax/{Id} endpoint
     * AND Tax ID value is non-existent Tax id
     * THEN I should get Status Code of 404 and error message
     */
    @Test
    public void taxGetNonExistentTaxIdTest() {
        SubContractorTaxGet response = subContractorController.getTax(SubContractorTaxTestsConstants.TAX_ID_NON_EXISTENT);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(response.getMessage(), SubContractorTaxTestsConstants.ERROR_MESSAGE_INVALID_TAX_SECOND_VERSION + SubContractorTaxTestsConstants.TAX_ID_NON_EXISTENT);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Tax/{Id} endpoint
     * AND Tax ID value is negative
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxGetNegativeTaxIdTest() {
        SubContractorTaxGetValidationFailure response = subContractorController.getTaxValidationFailure(SubContractorTaxTestsConstants.TAX_ID_NEGATIVE);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);

        Assert.assertEquals(response.getErrors().getId()[0], CommonTestsConstants.ERROR_MESSAGE_MIN_VALUE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Tax/{Id} endpoint
     * AND Tax ID value is zero
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxGetZeroTaxIdTest() {
        SubContractorTaxGetValidationFailure response = subContractorController.getTaxValidationFailure(SubContractorTaxTestsConstants.TAX_ID_ZERO);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);

        Assert.assertEquals(response.getErrors().getId()[0], CommonTestsConstants.ERROR_MESSAGE_MIN_VALUE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Tax/{Id} endpoint
     * AND Tax ID value is text
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxGetTextTaxIdTest() {
        SubContractorTaxGetValidationFailure response = subContractorController.getTaxValidationFailure(SubContractorTaxTestsConstants.TAX_ID_TEXT);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Tax/{Id} endpoint
     * AND Tax ID value is special character
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxGetSpecCharTaxIdTest() {
        SubContractorTaxGetValidationFailure response = subContractorController.getTaxValidationFailure(SubContractorTaxTestsConstants.TAX_ID_SPEC_CHAR);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Tax/{Id} endpoint
     * AND Tax ID value is number + special character
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxGetNumberAndSpecCharTaxIdTest() {
        SubContractorTaxGetValidationFailure response = subContractorController.getTaxValidationFailure(SubContractorTaxTestsConstants.TAX_ID_NUMBER_AND_SPEC_CHAR);

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Tax/{Id} endpoint
     * AND Tax ID value is empty
     * THEN I should get Status Code of 415 and error message
     */
    @Test
    public void taxGetEmptyTaxId() {
        SubContractorTaxGetValidationFailure response = subContractorController.getTax();

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);

        Assert.assertEquals(response.getErrors().getId()[0], CommonTestsConstants.FIELD_IS_REQUIRED_MESSAGE);
    }
}