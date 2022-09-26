package com.dataart.subcontractorstool.apitests.tests.check.checkstatustests;

import com.dataart.subcontractorstool.apitests.controllers.CheckController;
import com.dataart.subcontractorstool.apitests.responseentities.check.CheckStatusGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractorstatustests.SubContractorStatusTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class CheckStatusGetTests {
    private CheckController checkController;

    @BeforeClass
    public void setupTest() {
        checkController = new CheckController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Check/Status endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getBackgroundChecksTest() {
        CheckStatusGet response = checkController.getCheckStatuses();

        Map<Integer, String> responseCheckStatusesList = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseCheckStatusesList.put(data.getId(), data.getValue()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseCheckStatusesList, CheckStatusTestsConstants.CHECK_STATUSES_LIST);
    }
}