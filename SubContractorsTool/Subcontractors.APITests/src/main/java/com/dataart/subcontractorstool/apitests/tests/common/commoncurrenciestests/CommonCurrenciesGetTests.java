package com.dataart.subcontractorstool.apitests.tests.common.commoncurrenciestests;

import com.dataart.subcontractorstool.apitests.controllers.CommonController;
import com.dataart.subcontractorstool.apitests.responseentities.common.CommonCurrenciesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class CommonCurrenciesGetTests {
    private CommonController commonController;

    @BeforeClass
    public void setupTest() {
        commonController = new CommonController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Common/Currencies endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getCurrenciesTest() {
        CommonCurrenciesGet response = commonController.getCurrencies();

        Map<Integer, String> responseCurrenciesList = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseCurrenciesList.put(data.getId(), data.getCode()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertTrue(responseCurrenciesList.containsValue(CommonCurrenciesTestsConstants.CURRENCIES_LIST.get(1)));
        Assert.assertTrue(responseCurrenciesList.containsValue(CommonCurrenciesTestsConstants.CURRENCIES_LIST.get(3)));
    }
}