package com.dataart.subcontractorstool.apitests.tests.invoice.invoicepagesalltests;

import com.dataart.subcontractorstool.apitests.controllers.InvoiceController;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoicePagesAllGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.IOException;

public class InvoicePagesAllGetTests {
    private InvoiceController invoiceController;

    @BeforeClass
    public void setupTest() throws IOException {
        invoiceController = new InvoiceController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Invoice/Pages/All endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getInvoices() {
        InvoicePagesAllGet response = invoiceController.getInvoices(null, null);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData().getItems());

        int resultsPerPage = response.getData().getTotalResults();

        InvoicePagesAllGet resultsResponse = invoiceController.getInvoices(InvoicePagesAllTestsConstants.CURRENT_PAGE, resultsPerPage);

        Assert.assertEquals(resultsResponse.getData().getItems().length, resultsPerPage);
    }
}