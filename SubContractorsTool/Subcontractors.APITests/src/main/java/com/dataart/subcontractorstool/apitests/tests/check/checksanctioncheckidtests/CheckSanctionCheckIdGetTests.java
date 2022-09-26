package com.dataart.subcontractorstool.apitests.tests.check.checksanctioncheckidtests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckSanctionCheckGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.check.checksanctionchecktests.CheckSanctionCheckTestsConstants;
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

public class CheckSanctionCheckIdGetTests {
    private SubContractorController subContractorController;
    private CheckController checkController;
    private StaffController staffController;
    private int subContractorSanctionCheckId;
    private int parentIdSubContractor;
    private int staffSanctionCheckId;
    private int parentIdStaff;
    private int approverId;

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
        checkController = new CheckController();
        subContractorController = new SubContractorController();

        parentIdSubContractor = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        parentIdStaff = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), parentIdSubContractor, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        approverId = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), parentIdSubContractor, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        subContractorSanctionCheckId = checkController.createSanctionCheck(CheckPayloads.createSanctionCheck(parentIdSubContractor, CheckSanctionCheckTestsConstants.PARENT_TYPE_SUBCONTRACTOR, approverId, CheckSanctionCheckTestsConstants.CHECK_STATUS_ID, CheckSanctionCheckTestsConstants.DATE, CheckSanctionCheckTestsConstants.COMMENT)).getData();

        staffSanctionCheckId = checkController.createSanctionCheck(CheckPayloads.createSanctionCheck(parentIdStaff, CheckSanctionCheckTestsConstants.PARENT_TYPE_STAFF, approverId, CheckSanctionCheckTestsConstants.CHECK_STATUS_ID, CheckSanctionCheckTestsConstants.DATE, CheckSanctionCheckTestsConstants.COMMENT)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Check/SanctionCheck/{Id} endpoint
     * AND ParentType is SubContractor
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getSanctionCheckParentTypeSubContractorTest() {
        CheckSanctionCheckGet getResponse = checkController.getSanctionCheck(subContractorSanctionCheckId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(getResponse.getData());

        Assert.assertEquals(getResponse.getData().getId(), subContractorSanctionCheckId);
        Assert.assertEquals(getResponse.getData().getCheckApproverId(), approverId);
        Assert.assertEquals(getResponse.getData().getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData().getCheckStatusId(), CheckSanctionCheckTestsConstants.CHECK_STATUS_ID);
        Assert.assertEquals(getResponse.getData().getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckSanctionCheckTestsConstants.CHECK_STATUS_ID));
        Assert.assertEquals(getResponse.getData().getDate(), CheckSanctionCheckTestsConstants.DATE);
        Assert.assertEquals(getResponse.getData().getComment(), CheckSanctionCheckTestsConstants.COMMENT);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Check/SanctionCheck/{Id} endpoint
     * AND ParentType is Staff
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getSanctionCheckParentTypeStaffTest() {
        CheckSanctionCheckGet getResponse = checkController.getSanctionCheck(staffSanctionCheckId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(getResponse.getData());

        Assert.assertEquals(getResponse.getData().getId(), staffSanctionCheckId);
        Assert.assertEquals(getResponse.getData().getCheckApproverId(), approverId);
        Assert.assertEquals(getResponse.getData().getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData().getCheckStatusId(), CheckSanctionCheckTestsConstants.CHECK_STATUS_ID);
        Assert.assertEquals(getResponse.getData().getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckSanctionCheckTestsConstants.CHECK_STATUS_ID));
        Assert.assertEquals(getResponse.getData().getDate(), CheckSanctionCheckTestsConstants.DATE);
        Assert.assertEquals(getResponse.getData().getComment(), CheckSanctionCheckTestsConstants.COMMENT);
    }
}