package com.dataart.subcontractorstool.apitests.tests.check.checksanctioncheckidtests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckSanctionCheckDelete;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckSanctionCheckGet;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckSanctionChecksGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.check.checksanctioncheckstests.CheckSanctionChecksTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.check.checksanctionchecktests.CheckSanctionCheckTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.stafftests.StaffTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.CheckPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class CheckSanctionCheckIdDeleteTests {
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
     * WHEN I send a DELETE to api/Check/SanctionCheck/{Id} endpoint
     * AND ParentType is SubContractor
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void deleteSanctionCheckParentTypeSubContractorTest() {
        CheckSanctionCheckDelete deleteResponse = checkController.deleteSanctionCheck(subContractorSanctionCheckId);
        Assert.assertTrue(deleteResponse.getIsSuccess());
        Assert.assertEquals(deleteResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(deleteResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        CheckSanctionCheckGet getResponse = checkController.getSanctionCheck(subContractorSanctionCheckId);
        Assert.assertEquals(getResponse.getMessage(), CheckSanctionCheckIdTestsConstants.SANCTION_CHECK_NOT_FOUND_MESSAGE + subContractorSanctionCheckId);

        CheckSanctionChecksGet getChecksResponse = checkController.getSanctionChecks(parentIdSubContractor, CheckSanctionCheckTestsConstants.PARENT_TYPE_SUBCONTRACTOR);
        Assert.assertEquals(getChecksResponse.getMessage(), "SubContractor" + CheckSanctionChecksTestsConstants.SANCTION_CHECKS_NOT_FOUND_MESSAGE_PART_1 + parentIdSubContractor + CheckSanctionChecksTestsConstants.SANCTION_CHECKS_NOT_FOUND_MESSAGE_PART_2);
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/Check/SanctionCheck/{Id} endpoint
     * AND ParentType is Staff
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void deleteSanctionCheckParentTypeStaffTest() {
        CheckSanctionCheckDelete deleteResponse = checkController.deleteSanctionCheck(staffSanctionCheckId);
        Assert.assertTrue(deleteResponse.getIsSuccess());
        Assert.assertEquals(deleteResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(deleteResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        CheckSanctionCheckGet getCheckResponse = checkController.getSanctionCheck(staffSanctionCheckId);
        Assert.assertEquals(getCheckResponse.getMessage(), CheckSanctionCheckIdTestsConstants.SANCTION_CHECK_NOT_FOUND_MESSAGE + staffSanctionCheckId);

        CheckSanctionChecksGet getChecksResponse = checkController.getSanctionChecks(parentIdStaff, CheckSanctionCheckTestsConstants.PARENT_TYPE_STAFF);
        Assert.assertEquals(getChecksResponse.getMessage(), "Staff" + CheckSanctionChecksTestsConstants.SANCTION_CHECKS_NOT_FOUND_MESSAGE_PART_1 + parentIdStaff + CheckSanctionChecksTestsConstants.SANCTION_CHECKS_NOT_FOUND_MESSAGE_PART_2);
    }
}