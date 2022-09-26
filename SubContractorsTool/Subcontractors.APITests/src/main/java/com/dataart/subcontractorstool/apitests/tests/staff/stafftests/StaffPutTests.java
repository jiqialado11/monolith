package com.dataart.subcontractorstool.apitests.tests.staff.stafftests;

import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffIdGet;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffUpdate;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.IOException;

public class StaffPutTests {
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
     * WHEN I send a PUT to api/Staff endpoint
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void updateStaffTest() {
        StaffUpdate updateResponse = staffController.updateStaff(StaffPayloads.updateStaff(staffId, pmStaffId, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE_UPDATE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        StaffIdGet getResponse = staffController.getStaff(staffId);

        Assert.assertEquals(getResponse.getData().getId(), staffId);
        Assert.assertEquals(getResponse.getData().getPmId(), pmStaffId);
        Assert.assertEquals(getResponse.getData().getFirstname(), StaffTestsConstants.FIRST_NAME);
        Assert.assertEquals(getResponse.getData().getLastname(), StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(getResponse.getData().getEmail(), StaffTestsConstants.EMAIL);
        Assert.assertEquals(getResponse.getData().getSkype(), StaffTestsConstants.SKYPE_UPDATE);
        Assert.assertEquals(getResponse.getData().getPosition(), StaffTestsConstants.POSITION);
        Assert.assertEquals(getResponse.getData().getCannotLoginBefore(), StaffTestsConstants.START_DATE);
        Assert.assertEquals(getResponse.getData().getCannotLoginAfter(), StaffTestsConstants.END_DATE);
        Assert.assertEquals(getResponse.getData().getQualification(), StaffTestsConstants.QUALIFICATIONS);
        Assert.assertEquals(getResponse.getData().getRealLocation(), StaffTestsConstants.REAL_LOCATION);
        Assert.assertEquals(getResponse.getData().getCellPhone(), StaffTestsConstants.CELL_PHONE);
        Assert.assertEquals(getResponse.getData().getIsNdaSigned(), StaffTestsConstants.IS_NDA_SIGNED);
        Assert.assertEquals(getResponse.getData().getDepartmentName(), StaffTestsConstants.DEPARTMENT_NAME);
        Assert.assertEquals(TestsUtils.addSlashToDomainLogin(getResponse.getData().getDomainLogin()), StaffTestsConstants.DOMAIN_LOGIN);
    }
}