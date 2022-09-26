package com.dataart.subcontractorstool.apitests.tests.compliance.compliancetypestests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceTypesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class ComplianceTypesGetTests {
    private ComplianceController complianceController;

    @BeforeClass
    public void setupTest() {
        complianceController = new ComplianceController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Compliance/Types endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void complianceTypesGetTest() {
        ComplianceTypesGet response = complianceController.getTypes();

        Map<Integer, String> responseTypes = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseTypes.put(data.getId(), data.getValue()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseTypes, ComplianceTypesTestsConstants.TYPES);
    }
}