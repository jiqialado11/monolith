package com.dataart.subcontractorstool.apitests.tests.compliance.compliancefileidtests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceFileIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.compliancefiletests.ComplianceFileTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.File;
import java.io.IOException;

public class ComplianceFileIdGetTests {
    private ComplianceController complianceController;
    private File complianceDocument;
    private String complianceDocumentId;
    private File responseComplianceDocument;

    @BeforeClass
    public void setupTest() throws IOException {
        complianceController = new ComplianceController();

        complianceDocument = TestsUtils.createMicrosoftWordDocument(ComplianceFileTestsConstants.PATH + ComplianceFileTestsConstants.NAME, ComplianceFileTestsConstants.COMPLIANCE_DOCUMENT_CONTENT);

        complianceDocumentId = complianceController.uploadFile(complianceDocument).getData().getId();
    }

    @AfterClass
    public void setDownTest() {
        complianceDocument.delete();
        responseComplianceDocument.delete();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Compliance/File/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getComplianceFileTest() throws IOException {
        ComplianceFileIdGet response = complianceController.getFile(complianceDocumentId);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData().getContent());

        responseComplianceDocument = TestsUtils.convertBase64StringToFile(ComplianceFileIdTestsConstants.PATH + ComplianceFileIdTestsConstants.NAME, response.getData().getContent());
    }
}