package com.dataart.subcontractorstool.apitests.tests.check.checkbackgroundchecktests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckBackgroundCheckCreate;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckBackgroundChecksGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.check.checkstatustests.CheckStatusTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.stafftests.StaffTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.CheckPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class CheckBackgroundCheckPostTests {
    private SubContractorController subContractorController;
    private StaffController staffController;
    private CheckController checkController;
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
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/Check/BackgroundCheck endpoint
     * AND all values are valid
     * THEN I should get Status Code of 201 and success message
     */
    @Test
    public void createBackgroundCheckTest() {
        CheckBackgroundCheckCreate createResponse = checkController.createBackgroundCheck(CheckPayloads.createBackgroundCheck(staffId, approverId, CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID, CheckBackgroundCheckTestsConstants.DATE, CheckBackgroundCheckTestsConstants.LINK));

        Assert.assertTrue(createResponse.getIsSuccess());
        Assert.assertEquals(createResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_201);
        Assert.assertEquals(createResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(createResponse.getData());

        backgroundCheckId = createResponse.getData();

        CheckBackgroundChecksGet getResponse = checkController.getBackgroundChecks(staffId);

        Assert.assertEquals(getResponse.getData().get(0).getId(), backgroundCheckId);
        Assert.assertEquals(getResponse.getData().get(0).getCheckApproverId(), approverId);
        Assert.assertEquals(getResponse.getData().get(0).getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData().get(0).getCheckStatusId(), CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID);
        Assert.assertEquals(getResponse.getData().get(0).getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID));
        Assert.assertEquals(getResponse.getData().get(0).getDate(), CheckBackgroundCheckTestsConstants.DATE);
        Assert.assertEquals(getResponse.getData().get(0).getLink(), CheckBackgroundCheckTestsConstants.LINK);
    }
}