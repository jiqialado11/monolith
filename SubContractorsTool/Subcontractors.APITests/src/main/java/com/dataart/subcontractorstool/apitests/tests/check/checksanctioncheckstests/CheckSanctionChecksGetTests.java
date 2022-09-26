package com.dataart.subcontractorstool.apitests.tests.check.checksanctioncheckstests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckSanctionCheckCreate;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckSanctionChecksGet;
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

public class CheckSanctionChecksGetTests {
    private SubContractorController subContractorController;
    private CheckController checkController;
    private StaffController staffController;
    private int parentIdSubContractor;
    private int parentIdStaff;
    private int approverId;
    private int sanctionChecksQuantity = 5;
    private int[] subContractorSanctionChecksIds = new int[sanctionChecksQuantity];
    private int[] staffSanctionChecksIds = new int[sanctionChecksQuantity];

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
        checkController = new CheckController();
        subContractorController = new SubContractorController();

        parentIdSubContractor = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        parentIdStaff = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), parentIdSubContractor, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        approverId = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), parentIdSubContractor, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();

        for(int i = 0; i<sanctionChecksQuantity; i++){
            CheckSanctionCheckCreate createResponse = checkController.createSanctionCheck(CheckPayloads.createSanctionCheck(parentIdSubContractor, CheckSanctionChecksTestsConstants.PARENT_TYPE_SUBCONTRACTOR, approverId, CheckSanctionCheckTestsConstants.CHECK_STATUS_ID, CheckSanctionCheckTestsConstants.DATE, CheckSanctionCheckTestsConstants.COMMENT));
            subContractorSanctionChecksIds[i] = createResponse.getData();
        }

        for(int i = 0; i<sanctionChecksQuantity; i++){
            CheckSanctionCheckCreate createResponse = checkController.createSanctionCheck(CheckPayloads.createSanctionCheck(parentIdStaff, CheckSanctionChecksTestsConstants.PARENT_TYPE_STAFF, approverId, CheckSanctionCheckTestsConstants.CHECK_STATUS_ID, CheckSanctionCheckTestsConstants.DATE, CheckSanctionCheckTestsConstants.COMMENT));
            staffSanctionChecksIds[i] = createResponse.getData();
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Check/SanctionChecks endpoint
     * AND ParentType is SubContractor
     * AND all values are valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getSanctionChecksParentTypeSubContractorTest() {
        CheckSanctionChecksGet getResponse = checkController.getSanctionChecks(parentIdSubContractor, CheckSanctionChecksTestsConstants.PARENT_TYPE_SUBCONTRACTOR);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(getResponse.getData().size(), sanctionChecksQuantity);

        for(int i = 0; i<sanctionChecksQuantity; i++){
            Assert.assertEquals(getResponse.getData().get(i).getId(), subContractorSanctionChecksIds[i]);
            Assert.assertEquals(getResponse.getData().get(i).getCheckApproverId(), approverId);
            Assert.assertEquals(getResponse.getData().get(i).getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
            Assert.assertEquals(getResponse.getData().get(i).getCheckStatusId(), CheckSanctionCheckTestsConstants.CHECK_STATUS_ID);
            Assert.assertEquals(getResponse.getData().get(i).getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckSanctionCheckTestsConstants.CHECK_STATUS_ID));
            Assert.assertEquals(getResponse.getData().get(i).getDate(), CheckSanctionCheckTestsConstants.DATE);
            Assert.assertEquals(getResponse.getData().get(i).getComment(), CheckSanctionCheckTestsConstants.COMMENT);
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Check/SanctionChecks endpoint
     * AND ParentType is Staff
     * AND all values are valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getSanctionChecksParentTypeStaffTest() {
        CheckSanctionChecksGet getResponse = checkController.getSanctionChecks(parentIdStaff, CheckSanctionChecksTestsConstants.PARENT_TYPE_STAFF);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(getResponse.getData().size(), sanctionChecksQuantity);

        for(int i = 0; i<sanctionChecksQuantity; i++){
            Assert.assertEquals(getResponse.getData().get(i).getId(), staffSanctionChecksIds[i]);
            Assert.assertEquals(getResponse.getData().get(i).getCheckApproverId(), approverId);
            Assert.assertEquals(getResponse.getData().get(i).getCheckApprover(), StaffTestsConstants.FIRST_NAME + " " + StaffTestsConstants.LAST_NAME);
            Assert.assertEquals(getResponse.getData().get(i).getCheckStatusId(), CheckSanctionCheckTestsConstants.CHECK_STATUS_ID);
            Assert.assertEquals(getResponse.getData().get(i).getCheckStatus(), CheckStatusTestsConstants.CHECK_STATUSES_LIST.get(CheckSanctionCheckTestsConstants.CHECK_STATUS_ID));
            Assert.assertEquals(getResponse.getData().get(i).getDate(), CheckSanctionCheckTestsConstants.DATE);
            Assert.assertEquals(getResponse.getData().get(i).getComment(), CheckSanctionCheckTestsConstants.COMMENT);
        }
    }
}