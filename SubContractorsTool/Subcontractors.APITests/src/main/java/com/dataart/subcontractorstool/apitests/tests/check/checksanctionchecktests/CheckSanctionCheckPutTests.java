package com.dataart.subcontractorstool.apitests.tests.check.checksanctionchecktests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckSanctionCheckUpdate;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckSanctionChecksGet;
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

public class CheckSanctionCheckPutTests {
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
     * WHEN I send a PUT to api/Check/SanctionCheck endpoint
     * AND ParentType is SubContractor
     * AND all values are valid
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void updateSanctionCheckParentTypeSubContractorTest() {
        CheckSanctionCheckUpdate updateResponse = checkController.updateSanctionCheck(CheckPayloads.updateSanctionCheck(parentIdSubContractor, CheckSanctionCheckTestsConstants.PARENT_TYPE_SUBCONTRACTOR, subContractorSanctionCheckId, approverId, CheckSanctionCheckTestsConstants.CHECK_STATUS_ID, CheckSanctionCheckTestsConstants.CURRENT_DATE, CheckSanctionCheckTestsConstants.COMMENT));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        CheckSanctionChecksGet getResponse = checkController.getSanctionChecks(parentIdSubContractor, CheckSanctionCheckTestsConstants.PARENT_TYPE_SUBCONTRACTOR);

        Assert.assertEquals(getResponse.getData().get(0).getId(), subContractorSanctionCheckId);
        Assert.assertEquals(getResponse.getData().get(0).getCheckApproverId(), approverId);
        Assert.assertEquals(getResponse.getData().get(0).getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData().get(0).getCheckStatusId(), CheckSanctionCheckTestsConstants.CHECK_STATUS_ID);
        Assert.assertEquals(getResponse.getData().get(0).getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckSanctionCheckTestsConstants.CHECK_STATUS_ID));
        Assert.assertEquals(getResponse.getData().get(0).getDate(), CheckSanctionCheckTestsConstants.CURRENT_DATE);
        Assert.assertEquals(getResponse.getData().get(0).getComment(), CheckSanctionCheckTestsConstants.COMMENT);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/Check/SanctionCheck endpoint
     * AND ParentType is Staff
     * AND all values are valid
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void updateSanctionCheckParentTypeStaffTest() {
        CheckSanctionCheckUpdate updateResponse = checkController.updateSanctionCheck(CheckPayloads.updateSanctionCheck(parentIdStaff, CheckSanctionCheckTestsConstants.PARENT_TYPE_STAFF, staffSanctionCheckId, approverId, CheckSanctionCheckTestsConstants.CHECK_STATUS_ID, CheckSanctionCheckTestsConstants.CURRENT_DATE, CheckSanctionCheckTestsConstants.COMMENT));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        CheckSanctionChecksGet getResponse = checkController.getSanctionChecks(parentIdStaff, CheckSanctionCheckTestsConstants.PARENT_TYPE_STAFF);

        Assert.assertEquals(getResponse.getData().get(0).getId(), staffSanctionCheckId);
        Assert.assertEquals(getResponse.getData().get(0).getCheckApproverId(), approverId);
        Assert.assertEquals(getResponse.getData().get(0).getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData().get(0).getCheckStatusId(), CheckSanctionCheckTestsConstants.CHECK_STATUS_ID);
        Assert.assertEquals(getResponse.getData().get(0).getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckSanctionCheckTestsConstants.CHECK_STATUS_ID));
        Assert.assertEquals(getResponse.getData().get(0).getDate(), CheckSanctionCheckTestsConstants.CURRENT_DATE);
        Assert.assertEquals(getResponse.getData().get(0).getComment(), CheckSanctionCheckTestsConstants.COMMENT);
    }
}