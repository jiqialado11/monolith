package com.dataart.subcontractorstool.apitests.tests.check.checkbackgroundcheckIdtests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckBackgroundCheckDelete;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckBackgroundCheckGet;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckBackgroundChecksGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.check.checkbackgroundcheckstests.CheckBackgroundChecksTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.check.checkbackgroundchecktests.CheckBackgroundCheckTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.stafftests.StaffTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.CheckPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class CheckBackgroundCheckIdDeleteTests {
    private SubContractorController subContractorController;
    private CheckController checkController;
    private StaffController staffController;
    private int backgroundCheckId;
    private int subContractorId;
    private int staffId;
    private int approverId;

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
        checkController = new CheckController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        staffId = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), subContractorId, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        approverId = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), subContractorId, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        backgroundCheckId = checkController.createBackgroundCheck(CheckPayloads.createBackgroundCheck(staffId, approverId, CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID, CheckBackgroundCheckTestsConstants.DATE, CheckBackgroundCheckTestsConstants.LINK)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/Check/BackgroundCheck/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void deleteBackgroundCheckTest() {
        CheckBackgroundCheckDelete deleteResponse = checkController.deleteBackgroundCheck(backgroundCheckId);

        Assert.assertTrue(deleteResponse.getIsSuccess());
        Assert.assertEquals(deleteResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(deleteResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        CheckBackgroundCheckGet getCheckResponse = checkController.getBackgroundCheck(backgroundCheckId);
        Assert.assertEquals(getCheckResponse.getMessage(), CheckBackgroundCheckIdTestsConstants.BACKGROUND_CHECK_NOT_FOUND_MESSAGE + backgroundCheckId);

        CheckBackgroundChecksGet getChecksResponse = checkController.getBackgroundChecks(staffId);
        Assert.assertEquals(getChecksResponse.getMessage(), CheckBackgroundChecksTestsConstants.BACKGROUND_CHECKS_NOT_FOUND_MESSAGE_PART_1 + staffId + CheckBackgroundChecksTestsConstants.BACKGROUND_CHECKS_NOT_FOUND_MESSAGE_PART_2);
    }
}