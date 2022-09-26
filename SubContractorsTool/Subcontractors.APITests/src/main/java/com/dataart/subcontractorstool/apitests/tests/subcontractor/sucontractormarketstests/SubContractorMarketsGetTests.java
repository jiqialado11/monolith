package com.dataart.subcontractorstool.apitests.tests.subcontractor.sucontractormarketstests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.markets.SubContractorMarketsGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class SubContractorMarketsGetTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/Markets endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorMarketsGetTest() {
        SubContractorMarketsGet response = subContractorController.getSubContractorMarkets();

        Map<Integer, String> responseSubContractorMarketsList = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseSubContractorMarketsList.put(data.getId(), data.getName()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseSubContractorMarketsList, SubContractorMarketsTestsConstants.SUBCONTRACTOR_MARKETS_LIST);
    }
}