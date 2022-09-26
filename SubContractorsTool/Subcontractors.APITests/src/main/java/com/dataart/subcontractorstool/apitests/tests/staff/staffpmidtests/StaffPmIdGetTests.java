package com.dataart.subcontractorstool.apitests.tests.staff.staffpmidtests;

import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffPmIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class StaffPmIdGetTests {
    private StaffController staffController;
    private int pmStaffId;
    private String firstname;
    private String lastname;

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();

        pmStaffId = staffController.getStaffListFromPM().getData()[0].getPmId();
        firstname = staffController.getStaffListFromPM().getData()[0].getFirstname();
        lastname = staffController.getStaffListFromPM().getData()[0].getLastname();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Staff/PM/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void pmStaffGetByIdTest() {
        StaffPmIdGet response = staffController.getStaffFromPMById(pmStaffId);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData());

        Assert.assertEquals(response.getData().getPmId(), pmStaffId);
        Assert.assertEquals(response.getData().getFirstname(), firstname);
        Assert.assertEquals(response.getData().getLastname(), lastname);
    }
}