package com.dataart.subcontractorstool.apitests.tests.invoice.invoicefilestests;

import com.dataart.subcontractorstool.apitests.controllers.InvoiceController;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceFilesCreate;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.File;
import java.io.IOException;

public class InvoiceFilesPostTests {
    private InvoiceController invoiceController;
    private byte invoiceDocumentQuantity = 5;
    private File[] invoiceDocuments = new File[invoiceDocumentQuantity];

    @BeforeClass
    public void setupTest() throws IOException {
        invoiceController = new InvoiceController();

        for(int i = 0; i<invoiceDocuments.length; i++){
            invoiceDocuments[i] = TestsUtils.createMicrosoftWordDocument(InvoiceFilesTestsConstants.FILES_PATH + InvoiceFilesTestsConstants.FILES_NAMES[i], InvoiceFilesTestsConstants.FILE_CONTENT);
        }
    }

    @AfterClass
    public void setDownTest() {
        for (File invoiceDocument : invoiceDocuments) { invoiceDocument.delete(); }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/Invoice/Files endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void uploadFilesTest() {
        InvoiceFilesCreate response = invoiceController.uploadFiles(invoiceDocuments);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData());
    }
}