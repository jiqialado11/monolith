package com.dataart.subcontractorstool.apitests.tests.compliance.complianceratingstests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceRatingsGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class ComplianceRatingsGetTests {
    private ComplianceController complianceController;

    @BeforeClass
    public void setupTest() {
        complianceController = new ComplianceController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Compliance/Ratings endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void complianceRatingsGetTest() {
        ComplianceRatingsGet response = complianceController.getRatings();

        Map<Integer, String> responseRatings = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseRatings.put(data.getId(), data.getName()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseRatings, ComplianceRatingsTestsConstants.RATINGS);
    }
}