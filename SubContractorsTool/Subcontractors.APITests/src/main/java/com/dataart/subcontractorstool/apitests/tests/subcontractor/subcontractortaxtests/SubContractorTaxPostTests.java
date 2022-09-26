package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxtests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxCreate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxCreateValidationError;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorTaxPostTests {
    private SubContractorController subContractorController;
    private int subContractorId;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND all values are valid
     * THEN I should get Status Code of 201 and success message
     */
    @Test
    public void taxCreateTest() {
        SubContractorTaxCreate createResponse = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertTrue(createResponse.getIsSuccess());
        Assert.assertEquals(createResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_201);
        Assert.assertEquals(createResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(createResponse.getData());

        int taxId = createResponse.getData();

        SubContractorTaxGet getResponse = subContractorController.getTax(taxId);

        Assert.assertEquals(getResponse.getData().getId(), taxId);
        Assert.assertEquals(getResponse.getData().getSubContractorId(), subContractorId);
        Assert.assertEquals(getResponse.getData().getTaxTypeId(), SubContractorTaxTestsConstants.TAX_TYPE_ID);
        Assert.assertEquals(getResponse.getData().getName(), SubContractorTaxTestsConstants.TAX_NAME);
        Assert.assertEquals(getResponse.getData().getTaxNumber(), SubContractorTaxTestsConstants.TAX_NUMBER);
        Assert.assertEquals(getResponse.getData().getUrl(), SubContractorTaxTestsConstants.URL);
        Assert.assertEquals(getResponse.getData().getDate(), SubContractorTaxTestsConstants.DATE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND SubContractor ID value is non-existent SubContractor ID
     * THEN I should get Status Code of 404 and error message
     */
    @Test
    public void taxCreateNonExistentSubContractorIdTest() {
        SubContractorTaxCreate createResponse = subContractorController.createTax(SubContractorPayloads.createTax(SubContractorTaxTestsConstants.SUBCONTRACTOR_ID_NON_EXISTENT, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(createResponse.getIsSuccess());
        Assert.assertEquals(createResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(createResponse.getMessage(), SubContractorTaxTestsConstants.ERROR_MESSAGE_INVALID_SUBCONTRACTOR + SubContractorTaxTestsConstants.SUBCONTRACTOR_ID_NON_EXISTENT);

        SubContractorTaxesGet getResponse = subContractorController.getTaxes(SubContractorTaxTestsConstants.SUBCONTRACTOR_ID_NON_EXISTENT);

        Assert.assertFalse(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(getResponse.getMessage(), SubContractorTaxTestsConstants.ERROR_MESSAGE_INVALID_SUBCONTRACTOR_SECOND_VERSION + SubContractorTaxTestsConstants.SUBCONTRACTOR_ID_NON_EXISTENT);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is non-existent Tax Type ID
     * THEN I should get Status Code of 404 and error message
     */
    @Test
    public void taxCreateNonExistentTaxTypeIdTest() {
        SubContractorTaxCreate createResponse = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID_NON_EXISTENT, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(createResponse.getIsSuccess());
        Assert.assertEquals(createResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(createResponse.getMessage(), SubContractorTaxTestsConstants.ERROR_MESSAGE_INVALID_TAX_TYPE + SubContractorTaxTestsConstants.TAX_TYPE_ID_NON_EXISTENT);

        SubContractorTaxesGet getResponse = subContractorController.getTaxes(subContractorId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertEquals(getResponse.getData().size(), 1);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND SubContractor ID value is empty
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateEmptySubContractorIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTaxSubContractorIdEmpty(SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND SubContractor ID value is negative
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateNegativeSubContractorIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(SubContractorTaxTestsConstants.SUBCONTRACTOR_ID_NEGATIVE, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND SubContractor ID value is zero
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateZeroSubContractorIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(SubContractorTaxTestsConstants.SUBCONTRACTOR_ID_ZERO, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND SubContractor ID value is longer than Integer
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateLongerThanIntegerSubContractorIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(SubContractorTaxTestsConstants.SUBCONTRACTOR_ID_LONGER_THAN_INTEGER, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND SubContractor ID value is text
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateTextSubContractorIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(SubContractorTaxTestsConstants.SUBCONTRACTOR_ID_TEXT, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND SubContractor ID value is number + special character in the end
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateSpecCharSubContractorIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTaxSubContractorIdSpecChar(SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is empty
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateEmptyTaxTypeIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTaxTaxTypeIdEmpty(subContractorId, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is negative
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateNegativeTaxTypeIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID_NEGATIVE, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is zero
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateZeroTaxTypeIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID_ZERO, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is longer than Integer
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateLongerThanIntegerTaxTypeIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID_LONGER_THAN_INTEGER, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is text
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateTextTaxTypeIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID_TEXT, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is number + special character in the end
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateSpecCharTaxTypeIdTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTaxTaxTypeIdSpecChar(subContractorId, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Name value is number
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateNumberNameTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME_AS, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Tax Number value is number
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateNumberTaxNumberTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND URL value is number
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateNumberUrlTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL_NUMBER, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND Date value is number
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateNumberDateTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTax(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE_NUMBER));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND payload is huge
     * THEN I should get Status Code of 201 and success message
     */
    @Test
    public void taxCreateHugePayloadTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTaxHugePayload(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_201);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND payload is empty
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxCreateEmptyPayloadTest() {
        SubContractorTaxCreateValidationError response = subContractorController.createTaxValidationError(SubContractorPayloads.emptyPayload());

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);

        Assert.assertEquals(response.getErrors().getSubContractorId()[0], CommonTestsConstants.FIELD_IS_REQUIRED_MESSAGE);
        Assert.assertEquals(response.getErrors().getTaxTypeId()[0], CommonTestsConstants.FIELD_IS_REQUIRED_MESSAGE);
        Assert.assertEquals(response.getErrors().getName()[0], CommonTestsConstants.FIELD_IS_REQUIRED_MESSAGE);
        Assert.assertEquals(response.getErrors().getTaxNumber()[0], CommonTestsConstants.FIELD_IS_REQUIRED_MESSAGE);
        Assert.assertEquals(response.getErrors().getUrl()[0], CommonTestsConstants.FIELD_IS_REQUIRED_MESSAGE);
        Assert.assertEquals(response.getErrors().getDate()[0], CommonTestsConstants.FIELD_IS_REQUIRED_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND no payload
     * THEN I should get Status Code of 400 and error message
     */
    @Test
    public void taxCreateNoPayloadTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.noPayload());

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND JSON schema is broken - no delimiter between key and value
     * THEN I should get Status Code of 400 and error message
     */
    @Test
    public void taxCreateBrokenJSONSchemaTest() {
        SubContractorTaxCreate response = subContractorController.createTax(SubContractorPayloads.createTaxBrokenJSONSchema(subContractorId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }
}