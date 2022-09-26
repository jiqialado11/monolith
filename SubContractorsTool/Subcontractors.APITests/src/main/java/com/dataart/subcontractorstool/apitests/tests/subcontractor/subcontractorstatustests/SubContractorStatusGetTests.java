package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractorstatustests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.status.SubContractorStatusGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class SubContractorStatusGetTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Status endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorStatusGetTest() {
        SubContractorStatusGet response = subContractorController.getSubContractorStatuses();

        Map<Integer, String> responseSubContractorStatusesList = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseSubContractorStatusesList.put(data.getId(), data.getName()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseSubContractorStatusesList, SubContractorStatusTestsConstants.SUBCONTRACTOR_STATUSES_LIST);
    }
}