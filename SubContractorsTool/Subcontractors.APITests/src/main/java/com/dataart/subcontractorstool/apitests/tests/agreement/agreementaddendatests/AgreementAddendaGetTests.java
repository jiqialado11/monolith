package com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendatests;

import com.dataart.subcontractorstool.apitests.controllers.*;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendaGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumtests.AgreementAddendumTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests.AgreementTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.legalentity.legalentitytests.LegalEntityTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projecttests.ProjectTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.stafftests.StaffTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.AgreementPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.ProjectPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class AgreementAddendaGetTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private ProjectController projectController;
    private StaffController staffController;
    private int[] addendumIds = new int[5];
    private int subContractorId;
    private String projectId;
    private int agreementId;
    private int staffId;

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
        projectController = new ProjectController();
        agreementController = new AgreementController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        projectId = projectController.assignProjectToSubContractor(ProjectPayloads.assignProjectToSubContractor(ProjectTestsConstants.PROJECT_PM_ID, subContractorId, ProjectTestsConstants.PROJECT_NAME, ProjectTestsConstants.PROJECT_GROUP_PM_ID, ProjectTestsConstants.PROJECT_GROUP, ProjectTestsConstants.PROJECT_MANAGER_ID, ProjectTestsConstants.START_DATE, ProjectTestsConstants.ESTIMATED_FINISH_DATE, ProjectTestsConstants.FINISH_DATE, ProjectTestsConstants.PROJECT_STATUS_ID)).getData();

        staffId = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), subContractorId, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        agreementId = agreementController.createAgreement(AgreementPayloads.createAgreement(AgreementTestsConstants.TITLE, subContractorId, TestsUtils.getLegalEntityId(), AgreementTestsConstants.START_DATE, AgreementTestsConstants.END_DATE, TestsUtils.getLocationId(), AgreementTestsConstants.CONDITIONS, TestsUtils.getPaymentMethodId(), AgreementTestsConstants.AGREEMENT_URL)).getData();

        for (int i = 0; i < addendumIds.length; i++){
            addendumIds[i] = agreementController.createAddendum(AgreementPayloads.createAddendum(AgreementAddendumTestsConstants.TITLE, agreementId, projectId, AgreementAddendumTestsConstants.START_DATE, AgreementAddendumTestsConstants.END_DATE, AgreementAddendumTestsConstants.COMMENT, TestsUtils.getPaymentTermId(), AgreementAddendumTestsConstants.PAYMENT_TERM_IN_DAYS, TestsUtils.getCurrencyId(), AgreementAddendumTestsConstants.ADDENDUM_URL, AgreementAddendumTestsConstants.IS_FOR_NON_BILLABLE_PROJECTS)).getData();
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Agreement/Addenda/{SubContractorId} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getAddendaTest() {
        AgreementAddendaGet getResponse = agreementController.getAddenda(subContractorId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertEquals(getResponse.getData().length, addendumIds.length);

        for (int i = 0; i < addendumIds.length; i++){
            Assert.assertEquals(getResponse.getData()[i].getId(), addendumIds[i]);
            Assert.assertEquals(getResponse.getData()[i].getTitle(), AgreementAddendumTestsConstants.TITLE);
            Assert.assertEquals(getResponse.getData()[i].getProjects()[0].getId(), projectId);
            Assert.assertEquals(getResponse.getData()[i].getLegalEntityId(), TestsUtils.getLegalEntityId());
            Assert.assertEquals(getResponse.getData()[i].getLegalEntity(), LegalEntityTestsConstants.LEGAL_ENTITIES_LIST.get(TestsUtils.getLegalEntityId()));
            Assert.assertEquals(getResponse.getData()[i].getStartDate(), AgreementAddendumTestsConstants.START_DATE);
            Assert.assertEquals(getResponse.getData()[i].getEndDate(), AgreementAddendumTestsConstants.END_DATE);
            Assert.assertEquals(getResponse.getData()[i].getDocUrl(), AgreementAddendumTestsConstants.ADDENDUM_URL);
            Assert.assertEquals(getResponse.getData()[i].getComment(), AgreementAddendumTestsConstants.COMMENT);
        }
    }
}