package com.dataart.subcontractorstool.apitests.tests.staff.staffsubcontractoridtests;

import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffSubContractorIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.staffstatustests.StaffStatusTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.stafftests.StaffTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.IOException;

public class StaffSubContractorIdGetTests {
    private SubContractorController subContractorController;
    private StaffController staffController;
    private int subContractorId;
    private int pmStaffId;
    private int staffId;

    @BeforeClass
    public void setupTest() throws IOException {
        staffController = new StaffController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        pmStaffId = TestsUtils.getUniquePmStaffId();

        staffId = staffController.createStaff(StaffPayloads.createStaff(pmStaffId, subContractorId, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Staff/{SubContractorId} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getStaffTest() {
        StaffSubContractorIdGet getResponse = staffController.getStaffListBySubContractorID(subContractorId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(getResponse.getData()[0].getId(), staffId);
        Assert.assertEquals(getResponse.getData()[0].getPmId(), pmStaffId);
        Assert.assertEquals(getResponse.getData()[0].getFirstName(), StaffTestsConstants.FIRST_NAME);
        Assert.assertEquals(getResponse.getData()[0].getLastName(), StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData()[0].getPosition(), StaffTestsConstants.POSITION);
        Assert.assertEquals(getResponse.getData()[0].getIsNdaSigned(), StaffTestsConstants.IS_NDA_SIGNED);
        Assert.assertEquals(getResponse.getData()[0].getStatusId(), StaffTestsConstants.STATUS_ID);
        Assert.assertEquals(getResponse.getData()[0].getStatus(), StaffStatusTestsConstants.STAFF_STATUSES.get(StaffTestsConstants.STATUS_ID));
        Assert.assertEquals(getResponse.getData()[0].getExpirationDate(), StaffTestsConstants.END_DATE);
    }
}