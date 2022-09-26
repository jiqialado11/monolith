package com.dataart.subcontractorstool.apitests.tests.staff.staffidtests;

import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.stafftests.StaffTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.IOException;

public class StaffIdGetTests {
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
     * WHEN I send a GET to api/Staff/{StaffId} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getStaffTest() {
        StaffIdGet response = staffController.getStaff(staffId);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(response.getData().getId(), staffId);
        Assert.assertEquals(response.getData().getPmId(), pmStaffId);
        Assert.assertEquals(response.getData().getFirstname(), StaffTestsConstants.FIRST_NAME);
        Assert.assertEquals(response.getData().getLastname(), StaffTestsConstants.LAST_NAME);
        Assert.assertEquals(response.getData().getEmail(), StaffTestsConstants.EMAIL);
        Assert.assertEquals(response.getData().getSkype(), StaffTestsConstants.SKYPE);
        Assert.assertEquals(response.getData().getPosition(), StaffTestsConstants.POSITION);
        Assert.assertEquals(response.getData().getCannotLoginBefore(), StaffTestsConstants.START_DATE);
        Assert.assertEquals(response.getData().getCannotLoginAfter(), StaffTestsConstants.END_DATE);
        Assert.assertEquals(response.getData().getRealLocation(), StaffTestsConstants.REAL_LOCATION);
        Assert.assertEquals(response.getData().getCellPhone(), StaffTestsConstants.CELL_PHONE);
        Assert.assertEquals(response.getData().getIsNdaSigned(), StaffTestsConstants.IS_NDA_SIGNED);
        Assert.assertEquals(response.getData().getDepartmentName(), StaffTestsConstants.DEPARTMENT_NAME);
        Assert.assertEquals(TestsUtils.addSlashToDomainLogin(response.getData().getDomainLogin()), StaffTestsConstants.DOMAIN_LOGIN);
        Assert.assertEquals(response.getData().getQualification(), StaffTestsConstants.QUALIFICATIONS);
    }
}