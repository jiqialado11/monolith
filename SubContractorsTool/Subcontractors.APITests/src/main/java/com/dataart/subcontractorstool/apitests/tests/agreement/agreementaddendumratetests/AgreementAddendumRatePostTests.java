package com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumratetests;

import com.dataart.subcontractorstool.apitests.controllers.*;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendumRateCreate;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendumRateGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumtests.AgreementAddendumTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests.AgreementTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projecttests.ProjectTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.staffrateunitstests.StaffRateUnitsTestsConstants;
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

import static com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumratetests.AgreementAddendumRateTestsConstants.RATE_UNIT_ID;

public class AgreementAddendumRatePostTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private ProjectController projectController;
    private StaffController staffController;
    private int subContractorId;
    private String projectId;
    private int agreementId;
    private int addendumId;
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

        addendumId = agreementController.createAddendum(AgreementPayloads.createAddendum(AgreementAddendumTestsConstants.TITLE, agreementId, projectId, AgreementAddendumTestsConstants.START_DATE, AgreementAddendumTestsConstants.END_DATE, AgreementAddendumTestsConstants.COMMENT, TestsUtils.getPaymentTermId(), AgreementAddendumTestsConstants.PAYMENT_TERM_IN_DAYS, TestsUtils.getCurrencyId(), AgreementAddendumTestsConstants.ADDENDUM_URL, AgreementAddendumTestsConstants.IS_FOR_NON_BILLABLE_PROJECTS)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/Agreement/Addendum/Rate endpoint
     * AND all values are valid
     * THEN I should get Status Code of 201 and success message
     */
    @Test
    public void createRateTest() {
        AgreementAddendumRateCreate createResponse = agreementController.createRate(AgreementPayloads.createRate(addendumId, staffId, AgreementAddendumRateTestsConstants.NAME, AgreementAddendumRateTestsConstants.RATE, RATE_UNIT_ID, AgreementAddendumRateTestsConstants.FROM_DATE, AgreementAddendumRateTestsConstants.TO_DATE, AgreementAddendumRateTestsConstants.DESCRIPTION));

        Assert.assertTrue(createResponse.getIsSuccess());
        Assert.assertEquals(createResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_201);
        Assert.assertEquals(createResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(createResponse.getData());

        int rateId = createResponse.getData();

        AgreementAddendumRateGet getResponse = agreementController.getRate(addendumId, rateId);

        Assert.assertEquals(getResponse.getData().getAddendumId(), addendumId);
        Assert.assertEquals(getResponse.getData().getId(), rateId);
        Assert.assertEquals(getResponse.getData().getTitle(), AgreementAddendumRateTestsConstants.NAME);
        Assert.assertEquals(getResponse.getData().getStaffId(), staffId);
        Assert.assertEquals(getResponse.getData().getStaff(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData().getRateUnitId(), RATE_UNIT_ID);
        Assert.assertEquals(getResponse.getData().getRateUnit(), StaffRateUnitsTestsConstants.RATE_UNITS_LIST.get(RATE_UNIT_ID));
        Assert.assertEquals(getResponse.getData().getFromDate(), AgreementAddendumRateTestsConstants.FROM_DATE);
        Assert.assertEquals(getResponse.getData().getToDate(), AgreementAddendumRateTestsConstants.TO_DATE);
        Assert.assertEquals(getResponse.getData().getDescription(), AgreementAddendumRateTestsConstants.DESCRIPTION);
        Assert.assertEquals(getResponse.getData().getRateValue(), AgreementAddendumRateTestsConstants.RATE);
    }
}