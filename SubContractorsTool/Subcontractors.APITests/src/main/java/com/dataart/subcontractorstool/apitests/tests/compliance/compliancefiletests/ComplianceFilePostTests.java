package com.dataart.subcontractorstool.apitests.tests.compliance.compliancefiletests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceFilePost;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.File;
import java.io.IOException;

public class ComplianceFilePostTests {
    private ComplianceController complianceController;
    private File complianceDocument;

    @BeforeClass
    public void setupTest() throws IOException {
        complianceController = new ComplianceController();

        complianceDocument = TestsUtils.createMicrosoftWordDocument(ComplianceFileTestsConstants.PATH + ComplianceFileTestsConstants.NAME, ComplianceFileTestsConstants.COMPLIANCE_DOCUMENT_CONTENT);
    }

    @AfterClass
    public void setDownTest() {
        complianceDocument.delete();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/Compliance/File endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void uploadFileTest() {
        ComplianceFilePost response = complianceController.uploadFile(complianceDocument);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData().getId());
        Assert.assertNotNull(response.getData().getFilename());
    }
}