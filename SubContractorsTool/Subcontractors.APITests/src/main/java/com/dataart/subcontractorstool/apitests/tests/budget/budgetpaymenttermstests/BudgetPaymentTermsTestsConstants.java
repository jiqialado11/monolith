package com.dataart.subcontractorstool.apitests.tests.budget.budgetpaymenttermstests;

import java.util.Map;

public class BudgetPaymentTermsTestsConstants {
    public static final Map<Integer, String> PAYMENT_TERMS_LIST = Map.of(
            1,"NoRestriction",
            2,"AfterClientPayOnly",
            3,"AfterClientPayOnlyOrExpirationDate"
    );
}