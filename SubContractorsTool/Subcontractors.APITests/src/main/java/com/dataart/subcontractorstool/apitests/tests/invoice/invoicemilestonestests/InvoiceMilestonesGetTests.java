package com.dataart.subcontractorstool.apitests.tests.invoice.invoicemilestonestests;

import com.dataart.subcontractorstool.apitests.controllers.InvoiceController;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceMilestonesGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.IOException;

public class InvoiceMilestonesGetTests {
    private InvoiceController invoiceController;

    @BeforeClass
    public void setupTest() throws IOException {
        invoiceController = new InvoiceController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Invoice/Milestones endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getMilestonesTest() {
        InvoiceMilestonesGet response = invoiceController.getMilestones(InvoiceMilestonesTestsConstants.PM_PROJECT_ID);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
    }
}