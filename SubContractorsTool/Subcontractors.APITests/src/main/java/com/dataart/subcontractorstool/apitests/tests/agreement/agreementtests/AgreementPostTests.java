package com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests;

import com.dataart.subcontractorstool.apitests.controllers.AgreementController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementCreate;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.AgreementPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class AgreementPostTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private int subContractorId;

    @BeforeClass
    public void setupTest() {
        agreementController = new AgreementController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/Agreement/Agreement endpoint
     * AND all values are valid
     * THEN I should get Status Code of 201 and success message
     */
    @Test
    public void createAgreementTest() {
        AgreementCreate createResponse = agreementController.createAgreement(AgreementPayloads.createAgreement(AgreementTestsConstants.TITLE, subContractorId, TestsUtils.getLegalEntityId(), AgreementTestsConstants.START_DATE, AgreementTestsConstants.END_DATE, TestsUtils.getLocationId(), AgreementTestsConstants.CONDITIONS, TestsUtils.getPaymentMethodId(), AgreementTestsConstants.AGREEMENT_URL));

        Assert.assertTrue(createResponse.getIsSuccess());
        Assert.assertEquals(createResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_201);
        Assert.assertEquals(createResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(createResponse.getData());

        int agreementId = createResponse.getData();

        AgreementGet getResponse = agreementController.getAgreement(agreementId);

        Assert.assertEquals(getResponse.getData().getId(), agreementId);
        Assert.assertEquals(getResponse.getData().getTitle(), AgreementTestsConstants.TITLE);
        Assert.assertEquals(getResponse.getData().getLegalEntityId(), TestsUtils.getLegalEntityId());
        Assert.assertEquals(getResponse.getData().getStartDate(), AgreementTestsConstants.START_DATE);
        Assert.assertEquals(getResponse.getData().getEndDate(), AgreementTestsConstants.END_DATE);
        Assert.assertEquals(getResponse.getData().getLocationId(), TestsUtils.getLocationId());
        Assert.assertEquals(getResponse.getData().getConditions(), AgreementTestsConstants.CONDITIONS);
        Assert.assertEquals(getResponse.getData().getPaymentMethodId(), TestsUtils.getPaymentMethodId());
        Assert.assertEquals(getResponse.getData().getUrl(), AgreementTestsConstants.AGREEMENT_URL);
    }
}