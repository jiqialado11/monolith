package com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumtests;

import com.dataart.subcontractorstool.apitests.controllers.*;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendumDelete;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendumGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests.AgreementTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projecttests.ProjectTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.AgreementPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.ProjectPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class AgreementAddendumDeleteTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private ProjectController projectController;
    private int subContractorId;
    private String projectId;
    private int agreementId;
    private int addendumId;

    @BeforeClass
    public void setupTest() {
        projectController = new ProjectController();
        agreementController = new AgreementController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        projectId = projectController.assignProjectToSubContractor(ProjectPayloads.assignProjectToSubContractor(ProjectTestsConstants.PROJECT_PM_ID, subContractorId, ProjectTestsConstants.PROJECT_NAME, ProjectTestsConstants.PROJECT_GROUP_PM_ID, ProjectTestsConstants.PROJECT_GROUP, ProjectTestsConstants.PROJECT_MANAGER_ID, ProjectTestsConstants.START_DATE, ProjectTestsConstants.ESTIMATED_FINISH_DATE, ProjectTestsConstants.FINISH_DATE, ProjectTestsConstants.PROJECT_STATUS_ID)).getData();

        agreementId = agreementController.createAgreement(AgreementPayloads.createAgreement(AgreementTestsConstants.TITLE, subContractorId, TestsUtils.getLegalEntityId(), AgreementTestsConstants.START_DATE, AgreementTestsConstants.END_DATE, TestsUtils.getLocationId(), AgreementTestsConstants.CONDITIONS, TestsUtils.getPaymentMethodId(), AgreementTestsConstants.AGREEMENT_URL)).getData();

        addendumId = agreementController.createAddendum(AgreementPayloads.createAddendum(AgreementAddendumTestsConstants.TITLE, agreementId, projectId, AgreementAddendumTestsConstants.START_DATE, AgreementAddendumTestsConstants.END_DATE, AgreementAddendumTestsConstants.COMMENT, TestsUtils.getPaymentTermId(), AgreementAddendumTestsConstants.PAYMENT_TERM_IN_DAYS, TestsUtils.getCurrencyId(), AgreementAddendumTestsConstants.ADDENDUM_URL, AgreementAddendumTestsConstants.IS_FOR_NON_BILLABLE_PROJECTS)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/Agreement/Addendum/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void deleteAddendumTest() {
        AgreementAddendumDelete deleteResponse = agreementController.deleteAddendum(addendumId);

        Assert.assertTrue(deleteResponse.getIsSuccess());
        Assert.assertEquals(deleteResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(deleteResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        AgreementAddendumGet getResponse = agreementController.getAddendum(addendumId);

        Assert.assertFalse(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_404);
        Assert.assertEquals(getResponse.getMessage(), AgreementAddendumTestsConstants.ADDENDUM_NOT_FOUND_MESSAGE + addendumId);
    }
}