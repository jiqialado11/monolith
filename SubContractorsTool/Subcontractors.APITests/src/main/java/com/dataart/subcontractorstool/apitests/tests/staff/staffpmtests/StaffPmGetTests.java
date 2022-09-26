package com.dataart.subcontractorstool.apitests.tests.staff.staffpmtests;

import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffPmGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class StaffPmGetTests {
    private StaffController staffController;

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Staff/PM endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void pmStaffListGetTest() {
        StaffPmGet response = staffController.getStaffListFromPM();

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertNotNull(response.getData()[0].getPmId());
        Assert.assertNotNull(response.getData()[0].getFirstname());
        Assert.assertNotNull(response.getData()[0].getLastname());
    }
}