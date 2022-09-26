package com.dataart.subcontractorstool.apitests.tests.staff.staffrateunitstests;

import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffRateUnitsGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class StaffRateUnitsGetTests {
    private StaffController staffController;

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Staff/RateUnits endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void rateUnitsGetTest() {
        StaffRateUnitsGet response = staffController.getRateUnits();

        Map<Integer, String> responseRateUnitsList = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseRateUnitsList.put(data.getId(), data.getValue()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseRateUnitsList, StaffRateUnitsTestsConstants.RATE_UNITS_LIST);
    }
}