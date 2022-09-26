package com.dataart.subcontractorstool.apitests.tests.invoice.invoicestatusestests;

import com.dataart.subcontractorstool.apitests.controllers.InvoiceController;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceStatusesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class InvoiceStatusesGetTests {
    private InvoiceController invoiceController;

    @BeforeClass
    public void setupTest() {
        invoiceController = new InvoiceController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Invoice/Status endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void invoiceStatusesGetTest() {
        InvoiceStatusesGet response = invoiceController.getInvoiceStatuses();

        Map<Integer, String> responseInvoiceStatuses = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseInvoiceStatuses.put(data.getId(), data.getValue()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseInvoiceStatuses, InvoiceStatusesTestsConstants.INVOICE_STATUSES);
    }
}