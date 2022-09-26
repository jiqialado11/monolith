package com.dataart.subcontractorstool.apitests.tests.compliance.complianceidtests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceGet;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceIdDelete;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.compliancefiletests.ComplianceFileTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.compliancetests.ComplianceTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.CompliancePayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.File;
import java.io.IOException;

public class ComplianceIdDeleteTests {
    private SubContractorController subContractorController;
    private ComplianceController complianceController;
    private String complianceDocumentId;
    private File complianceDocument;
    private int subContractorId;
    private int complianceId;

    @BeforeClass
    public void setupTest() throws IOException {
        complianceController = new ComplianceController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        complianceDocument = TestsUtils.createMicrosoftWordDocument(ComplianceFileTestsConstants.PATH + ComplianceFileTestsConstants.NAME, ComplianceFileTestsConstants.COMPLIANCE_DOCUMENT_CONTENT);

        complianceDocumentId = complianceController.uploadFile(complianceDocument).getData().getId();

        complianceId = complianceController.createCompliance(CompliancePayloads.createCompliance(subContractorId, ComplianceTestsConstants.COMPLIANCE_TYPE_ID, ComplianceTestsConstants.COMPLIANCE_RATING_ID, ComplianceTestsConstants.EXPIRATION_DATE, ComplianceTestsConstants.COMMENT, complianceDocumentId)).getData();
    }

    @AfterClass
    public void setDownTest() {
        complianceDocument.delete();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/Compliance/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void deleteComplianceTest() {
        ComplianceIdDelete deleteResponse = complianceController.deleteCompliance(complianceId);

        Assert.assertTrue(deleteResponse.getIsSuccess());
        Assert.assertEquals(deleteResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(deleteResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        ComplianceIdGet getComplianceResponse = complianceController.getCompliance(complianceId);
        Assert.assertEquals(getComplianceResponse.getMessage(), ComplianceIdTestsConstants.COMPLIANCE_NOT_FOUND_MESSAGE + complianceId);

        ComplianceGet getComplianceListResponse = complianceController.getComplianceList(subContractorId);
        Assert.assertEquals(getComplianceListResponse.getMessage(), ComplianceTestsConstants.COMPLIANCES_NOT_FOUND_MESSAGE_PART_1 + subContractorId + ComplianceTestsConstants.COMPLIANCES_NOT_FOUND_MESSAGE_PART_2);
    }
}