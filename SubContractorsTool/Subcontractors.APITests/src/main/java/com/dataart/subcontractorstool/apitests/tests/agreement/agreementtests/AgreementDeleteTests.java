package com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests;

import com.dataart.subcontractorstool.apitests.controllers.AgreementController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementDelete;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementGet;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementsGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementagreementssubcontractoridtests.AgreementAgreementsSubContractorIdTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.AgreementPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class AgreementDeleteTests {
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
     * WHEN I send a DELETE to api/Agreement/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void deleteAgreementTest() {
        AgreementDelete deleteResponse = agreementController.deleteAgreement(agreementId);

        Assert.assertTrue(deleteResponse.getIsSuccess());
        Assert.assertEquals(deleteResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(deleteResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        AgreementGet getAgreementResponse = agreementController.getAgreement(agreementId);

        Assert.assertFalse(getAgreementResponse.getIsSuccess());
        Assert.assertEquals(getAgreementResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(getAgreementResponse.getMessage(), AgreementTestsConstants.AGREEMENT_NOT_FOUND_MESSAGE + agreementId);

        AgreementsGet getAgreementsResponse = agreementController.getAgreements(subContractorId);

        Assert.assertFalse(getAgreementsResponse.getIsSuccess());
        Assert.assertEquals(getAgreementsResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(getAgreementsResponse.getMessage(), AgreementAgreementsSubContractorIdTestsConstants.AGREEMENTS_ADDENDA_NOT_FOUND_MESSAGE_PART_1 + subContractorId + AgreementAgreementsSubContractorIdTestsConstants.AGREEMENTS_ADDENDA_NOT_FOUND_MESSAGE_PART_2);
    }
}