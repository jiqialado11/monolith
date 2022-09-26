package com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests;

import com.dataart.subcontractorstool.apitests.controllers.AgreementController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementGet;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementUpdate;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.AgreementPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class AgreementPutTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private int subContractorId;
    private int agreementId;

    @BeforeClass
    public void setupTest() {
        agreementController = new AgreementController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        agreementId = agreementController.createAgreement(AgreementPayloads.createAgreement(AgreementTestsConstants.TITLE, subContractorId, TestsUtils.getLegalEntityId(), AgreementTestsConstants.START_DATE, AgreementTestsConstants.END_DATE, TestsUtils.getLocationId(), AgreementTestsConstants.CONDITIONS, TestsUtils.getPaymentMethodId(), AgreementTestsConstants.AGREEMENT_URL)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/Agreement endpoint
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void updateAgreementTest() {
        AgreementUpdate updateResponse = agreementController.updateAgreement(AgreementPayloads.updateAgreement(agreementId, AgreementTestsConstants.TITLE_UPDATE, TestsUtils.getLegalEntityId(), AgreementTestsConstants.START_DATE, AgreementTestsConstants.END_DATE, TestsUtils.getLocationId(), AgreementTestsConstants.CONDITIONS, TestsUtils.getPaymentMethodId(), AgreementTestsConstants.AGREEMENT_URL));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        AgreementGet getResponse = agreementController.getAgreement(agreementId);

        Assert.assertEquals(getResponse.getData().getId(), agreementId);
        Assert.assertEquals(getResponse.getData().getTitle(), AgreementTestsConstants.TITLE_UPDATE);
        Assert.assertEquals(getResponse.getData().getLegalEntityId(), TestsUtils.getLegalEntityId());
        Assert.assertEquals(getResponse.getData().getStartDate(), AgreementTestsConstants.START_DATE);
        Assert.assertEquals(getResponse.getData().getEndDate(), AgreementTestsConstants.END_DATE);
        Assert.assertEquals(getResponse.getData().getLocationId(), TestsUtils.getLocationId());
        Assert.assertEquals(getResponse.getData().getConditions(), AgreementTestsConstants.CONDITIONS);
        Assert.assertEquals(getResponse.getData().getPaymentMethodId(), TestsUtils.getPaymentMethodId());
        Assert.assertEquals(getResponse.getData().getUrl(), AgreementTestsConstants.AGREEMENT_URL);
    }
}