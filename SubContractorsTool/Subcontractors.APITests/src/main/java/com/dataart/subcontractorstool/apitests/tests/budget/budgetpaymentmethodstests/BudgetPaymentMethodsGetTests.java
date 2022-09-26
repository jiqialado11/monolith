package com.dataart.subcontractorstool.apitests.tests.budget.budgetpaymentmethodstests;

import com.dataart.subcontractorstool.apitests.controllers.BudgetController;
import com.dataart.subcontractorstool.apitests.responseentities.budget.BudgetPaymentMethodsGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class BudgetPaymentMethodsGetTests {
    private BudgetController budgetController;

    @BeforeClass
    public void setupTest() {
        budgetController = new BudgetController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Budget/PaymentMethods endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getPaymentMethodsTest() {
        BudgetPaymentMethodsGet getResponse = budgetController.getPaymentMethods();

        Map<Integer, String> responsePaymentMethodsList = new HashMap<>();
        Arrays.stream(getResponse.getData()).forEach(data -> responsePaymentMethodsList.put(data.getId(), data.getName()));

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertTrue(responsePaymentMethodsList.containsValue(BudgetPaymentMethodsTestsConstants.PAYMENT_METHODS_LIST.get(1)));
        Assert.assertTrue(responsePaymentMethodsList.containsValue(BudgetPaymentMethodsTestsConstants.PAYMENT_METHODS_LIST.get(2)));
        Assert.assertTrue(responsePaymentMethodsList.containsValue(BudgetPaymentMethodsTestsConstants.PAYMENT_METHODS_LIST.get(3)));
        Assert.assertTrue(responsePaymentMethodsList.containsValue(BudgetPaymentMethodsTestsConstants.PAYMENT_METHODS_LIST.get(4)));
        Assert.assertTrue(responsePaymentMethodsList.containsValue(BudgetPaymentMethodsTestsConstants.PAYMENT_METHODS_LIST.get(5)));
        Assert.assertTrue(responsePaymentMethodsList.containsValue(BudgetPaymentMethodsTestsConstants.PAYMENT_METHODS_LIST.get(6)));
    }
}