package com.dataart.subcontractorstool.apitests.tests.common.commonlocationstests;

import com.dataart.subcontractorstool.apitests.controllers.CommonController;
import com.dataart.subcontractorstool.apitests.responseentities.common.CommonLocationsGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class CommonLocationsGetTests {
    private CommonController commonController;

    @BeforeClass
    public void setupTest() {
        commonController = new CommonController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Common/Locations endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getLocationsTest() {
        CommonLocationsGet getResponse = commonController.getLocations();

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(getResponse.getData());
    }
}