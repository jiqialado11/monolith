package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxtests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxUpdate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.tax.SubContractorTaxUpdateValidationError;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorTaxPutTests {
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
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND all values are valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void taxUpdateTest() {
        SubContractorTaxUpdate updateResponse = subContractorController.updateTax(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.CURRENT_DATE));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        SubContractorTaxGet getResponse = subContractorController.getTax(taxId);

        Assert.assertEquals(getResponse.getData().getId(), taxId);
        Assert.assertEquals(getResponse.getData().getSubContractorId(), subContractorId);
        Assert.assertEquals(getResponse.getData().getTaxTypeId(), SubContractorTaxTestsConstants.TAX_TYPE_ID);
        Assert.assertEquals(getResponse.getData().getName(), SubContractorTaxTestsConstants.TAX_NAME);
        Assert.assertEquals(getResponse.getData().getTaxNumber(), SubContractorTaxTestsConstants.TAX_NUMBER);
        Assert.assertEquals(getResponse.getData().getUrl(), SubContractorTaxTestsConstants.URL);
        Assert.assertEquals(getResponse.getData().getDate(), SubContractorTaxTestsConstants.CURRENT_DATE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax ID value is non-existent Tax ID
     * THEN I should get Status Code of 404 and error message
     */
    @Test
    public void taxUpdateNonExistentTaxIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(SubContractorTaxTestsConstants.TAX_ID_NON_EXISTENT, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.CURRENT_DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(response.getMessage(), SubContractorTaxTestsConstants.ERROR_MESSAGE_INVALID_TAX + SubContractorTaxTestsConstants.TAX_ID_NON_EXISTENT);
}

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is non-existent Tax Type ID
     * THEN I should get Status Code of 404 and error message
     */
    @Test
    public void taxUpdateNonExistentTaxTypeIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID_NON_EXISTENT, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.CURRENT_DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(response.getMessage(), SubContractorTaxTestsConstants.ERROR_MESSAGE_INVALID_TAX_TYPE + SubContractorTaxTestsConstants.TAX_TYPE_ID_NON_EXISTENT);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax ID value is empty
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxUpdateEmptyTaxIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTaxTaxIdEmpty(SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax ID value is negative
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxUpdateNegativeTaxIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(SubContractorTaxTestsConstants.TAX_ID_NEGATIVE, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax ID value is zero
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxUpdateZeroTaxIdTest() {
        SubContractorTaxUpdateValidationError response = subContractorController.updateTaxValidationError(SubContractorPayloads.updateTax(SubContractorTaxTestsConstants.TAX_ID_ZERO, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);

        Assert.assertEquals(response.getErrors().getId()[0], CommonTestsConstants.ERROR_MESSAGE_MIN_VALUE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax ID value is leading zero value
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateLeadingZeroTaxIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTaxTaxIdLeadingZero(SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax ID value is longer than Integer
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxUpdateLongerThanIntegerTaxId() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(SubContractorTaxTestsConstants.TAX_ID_LONGER_THAN_INTEGER, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax ID value is text
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateTextTaxIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(SubContractorTaxTestsConstants.TAX_ID_TEXT, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax ID value is number + special character in the end
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateSpecCharTaxIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTaxTaxIdSpecChar(SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is empty
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateEmptyTaxTypeIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTaxTaxTypeIdEmpty(taxId, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is negative
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxUpdateNegativeTaxTypeIdTest() {
        SubContractorTaxUpdateValidationError response = subContractorController.updateTaxValidationError(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID_NEGATIVE, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);

        Assert.assertEquals(response.getErrors().getTaxTypeId()[0], CommonTestsConstants.ERROR_MESSAGE_MIN_VALUE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is zero
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxUpdateZeroTaxTypeIdTest() {
        SubContractorTaxUpdateValidationError response = subContractorController.updateTaxValidationError(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID_ZERO, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);

        Assert.assertEquals(response.getErrors().getTaxTypeId()[0], CommonTestsConstants.ERROR_MESSAGE_MIN_VALUE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is leading zero
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateLeadingZeroTaxTypeIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTaxTaxTypeIdLeadingZero(taxId, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is longer than Integer
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxUpdateLongerThanIntegerTaxTypeIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID_LONGER_THAN_INTEGER, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is text
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateTextTaxTypeIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID_TEXT, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Type ID value is number + special character in the end
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateSpecCharTaxTypeIdTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTaxTaxTypeIdSpecChar(taxId, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Name value is number
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateNumberNameTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME_AS, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Tax Number value is number
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateNumberTaxNumberTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND URL value is number
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateNumberUrlTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL_NUMBER, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND Date value is number
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateNumberDateTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTax(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE_NUMBER));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND payload is huge
     * THEN I should get Status Code of 201 and success message
     */
    @Test
    public void taxUpdateHugePayloadTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTaxHugePayload(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Tax endpoint
     * AND payload is empty
     * THEN I should get Status Code of 415 and validation error message
     */
    @Test
    public void taxUpdateEmptyPayloadTest() {
        SubContractorTaxUpdateValidationError response = subContractorController.updateTaxValidationError(SubContractorPayloads.emptyPayload());

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_415);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.VALIDATION_FAILURE_MESSAGE);

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
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateNoPayloadTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.noPayload());

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/SubContractor/Tax endpoint
     * AND JSON schema is broken - no delimiter between key and value
     * THEN I should get Status Code of 400 and bad request message
     */
    @Test
    public void taxUpdateBrokenJSONSchemaTest() {
        SubContractorTaxUpdate response = subContractorController.updateTax(SubContractorPayloads.updateTaxBrokenJSONSchema(taxId, SubContractorTaxTestsConstants.TAX_TYPE_ID, SubContractorTaxTestsConstants.TAX_NAME, SubContractorTaxTestsConstants.TAX_NUMBER, SubContractorTaxTestsConstants.URL, SubContractorTaxTestsConstants.DATE));

        Assert.assertFalse(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_400);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.BAD_REQUEST_MESSAGE);
    }
}