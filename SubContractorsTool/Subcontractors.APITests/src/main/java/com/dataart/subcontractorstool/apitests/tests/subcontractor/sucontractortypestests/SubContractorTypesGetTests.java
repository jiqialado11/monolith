package com.dataart.subcontractorstool.apitests.tests.subcontractor.sucontractortypestests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.types.SubContractorTypesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class SubContractorTypesGetTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Types endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorTypesGetTest() {
        SubContractorTypesGet response = subContractorController.getSubContractorTypes();

        Map<Integer, String> responseSubContractorTypesList = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseSubContractorTypesList.put(data.getId(), data.getValue()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseSubContractorTypesList, SubContractorTypesTestsConstants.SUBCONTRACTOR_TYPES_LIST);
    }
}