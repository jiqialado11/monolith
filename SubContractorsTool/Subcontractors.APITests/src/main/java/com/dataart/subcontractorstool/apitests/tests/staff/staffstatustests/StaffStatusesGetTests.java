package com.dataart.subcontractorstool.apitests.tests.staff.staffstatustests;

import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffStatusesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class StaffStatusesGetTests {
    private StaffController staffController;

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Staff/Status endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void staffStatusGetTest() {
        StaffStatusesGet response = staffController.getStaffStatuses();

        Map<Integer, String> responseStaffStatuses = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseStaffStatuses.put(data.getId(), data.getName()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseStaffStatuses, StaffStatusTestsConstants.STAFF_STATUSES);
    }
}