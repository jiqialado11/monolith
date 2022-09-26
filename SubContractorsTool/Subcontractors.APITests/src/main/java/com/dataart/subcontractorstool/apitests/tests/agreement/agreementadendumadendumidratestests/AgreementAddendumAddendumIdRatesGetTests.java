package com.dataart.subcontractorstool.apitests.tests.agreement.agreementadendumadendumidratestests;

import com.dataart.subcontractorstool.apitests.controllers.*;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendumRatesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumratetests.AgreementAddendumRateTestsConstants;
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

public class AgreementAddendumAddendumIdRatesGetTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private ProjectController projectController;
    private StaffController staffController;
    private int[] rateIds = new int[5];
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

        for (int i = 0; i < rateIds.length; i++){
            rateIds[i] = agreementController.createRate(AgreementPayloads.createRate(addendumId, staffId, AgreementAddendumRateTestsConstants.NAME, AgreementAddendumRateTestsConstants.RATE, RATE_UNIT_ID, AgreementAddendumRateTestsConstants.FROM_DATE, AgreementAddendumRateTestsConstants.TO_DATE, AgreementAddendumRateTestsConstants.DESCRIPTION)).getData();
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Agreement/Addendum/{AddendumId}/Rates endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getRatesTest() {
        AgreementAddendumRatesGet getResponse = agreementController.getRates(addendumId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertEquals(getResponse.getData().length, rateIds.length);

        for (int i = 0; i < rateIds.length; i++){
            Assert.assertEquals(getResponse.getData()[i].getAddendumId(), addendumId);
            Assert.assertEquals(getResponse.getData()[i].getRateId(), rateIds[i]);
            Assert.assertEquals(getResponse.getData()[i].getTitle(), AgreementAddendumRateTestsConstants.NAME);
            Assert.assertEquals(getResponse.getData()[i].getRateValue(), AgreementAddendumRateTestsConstants.RATE);
            Assert.assertEquals(getResponse.getData()[i].getRateUnitId(), RATE_UNIT_ID);
            Assert.assertEquals(getResponse.getData()[i].getRateUnit(), StaffRateUnitsTestsConstants.RATE_UNITS_LIST.get(RATE_UNIT_ID));
            Assert.assertEquals(getResponse.getData()[i].getFromDate(), AgreementAddendumRateTestsConstants.FROM_DATE);
            Assert.assertEquals(getResponse.getData()[i].getToDate(), AgreementAddendumRateTestsConstants.TO_DATE);
            Assert.assertEquals(getResponse.getData()[i].getDescription(), AgreementAddendumRateTestsConstants.DESCRIPTION);
            Assert.assertEquals(getResponse.getData()[i].getStaffId(), staffId);
            Assert.assertEquals(getResponse.getData()[i].getStaff(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
        }
    }
}