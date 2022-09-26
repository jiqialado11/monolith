package com.dataart.subcontractorstool.apitests.tests.check.checkbackgroundchecktests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckBackgroundCheckUpdate;
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

public class CheckBackgroundCheckPutTests {
    private SubContractorController subContractorController;
    private CheckController checkController;
    private StaffController staffController;
    private int backgroundCheckId;
    private int subContractorId;
    private int approverId;
    private int staffId;



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
     * WHEN I send a PUT to api/Check/BackgroundCheck endpoint
     * AND all values are valid
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void updateBackgroundCheckTest() {
        CheckBackgroundCheckUpdate updateResponse = checkController.updateBackgroundCheck(CheckPayloads.updateBackgroundCheck(staffId, backgroundCheckId, approverId, CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID, CheckBackgroundCheckTestsConstants.CURRENT_DATE, CheckBackgroundCheckTestsConstants.LINK));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        CheckBackgroundChecksGet getResponse = checkController.getBackgroundChecks(staffId);

        Assert.assertEquals(getResponse.getData().get(0).getId(), backgroundCheckId);
        Assert.assertEquals(getResponse.getData().get(0).getCheckApproverId(), approverId);
        Assert.assertEquals(getResponse.getData().get(0).getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData().get(0).getCheckStatusId(), CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID);
        Assert.assertEquals(getResponse.getData().get(0).getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID));
        Assert.assertEquals(getResponse.getData().get(0).getDate(), CheckBackgroundCheckTestsConstants.CURRENT_DATE);
        Assert.assertEquals(getResponse.getData().get(0).getLink(), CheckBackgroundCheckTestsConstants.LINK);
    }
}