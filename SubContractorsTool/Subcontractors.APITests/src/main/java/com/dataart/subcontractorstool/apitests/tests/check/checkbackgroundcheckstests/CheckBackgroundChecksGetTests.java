package com.dataart.subcontractorstool.apitests.tests.check.checkbackgroundcheckstests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckBackgroundCheckCreate;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckBackgroundChecksGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.check.checkbackgroundchecktests.CheckBackgroundCheckTestsConstants;
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

public class CheckBackgroundChecksGetTests {
    private SubContractorController subContractorController;
    private CheckController checkController;
    private StaffController staffController;
    private int subContractorId;
    private int staffId;
    private int approverId;
    private byte backgroundChecksQuantity = 5;
    private int[] backgroundChecksIds = new int[backgroundChecksQuantity];

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
        checkController = new CheckController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        staffId = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), subContractorId, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        approverId = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), subContractorId, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        for(int i = 0; i<backgroundChecksQuantity; i++){
            CheckBackgroundCheckCreate createResponse = checkController.createBackgroundCheck(CheckPayloads.createBackgroundCheck(staffId, approverId, CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID, CheckBackgroundCheckTestsConstants.DATE, CheckBackgroundCheckTestsConstants.LINK));
            backgroundChecksIds[i] = createResponse.getData();
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Check/BackgroundChecks endpoint
     * AND Staff ID value is valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getBackgroundChecksTest() {
        CheckBackgroundChecksGet getResponse = checkController.getBackgroundChecks(staffId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(getResponse.getData().size(), backgroundChecksQuantity);

        for(int i = 0; i<backgroundChecksQuantity; i++){
            Assert.assertEquals(getResponse.getData().get(i).getId(), backgroundChecksIds[i]);
            Assert.assertEquals(getResponse.getData().get(i).getCheckApproverId(), approverId);
            Assert.assertEquals(getResponse.getData().get(i).getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
            Assert.assertEquals(getResponse.getData().get(i).getCheckStatusId(), CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID);
            Assert.assertEquals(getResponse.getData().get(i).getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckBackgroundCheckTestsConstants.CHECK_STATUS_ID));
            Assert.assertEquals(getResponse.getData().get(i).getDate(), CheckBackgroundCheckTestsConstants.DATE);
            Assert.assertEquals(getResponse.getData().get(i).getLink(), CheckBackgroundCheckTestsConstants.LINK);
        }
    }
}