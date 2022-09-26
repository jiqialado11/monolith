package com.dataart.subcontractorstool.apitests.tests.budget.budgetpaymenttermstests;

import com.dataart.subcontractorstool.apitests.controllers.BudgetController;
import com.dataart.subcontractorstool.apitests.responseentities.budget.BudgetPaymentTermsGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class BudgetPaymentTermsGetTests {
    private BudgetController budgetController;

    @BeforeClass
    public void setupTest() {
        budgetController = new BudgetController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Budget/PaymentTerms endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getPaymentTermsTest() {
        BudgetPaymentTermsGet getResponse = budgetController.getPaymentTerms();

        Map<Integer, String> responsePaymentTermsList = new HashMap<>();
        Arrays.stream(getResponse.getData()).forEach(data -> responsePaymentTermsList.put(data.getId(), data.getValue()));

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responsePaymentTermsList, BudgetPaymentTermsTestsConstants.PAYMENT_TERMS_LIST);
    }
}