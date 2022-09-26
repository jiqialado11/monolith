package com.dataart.subcontractorstool.apitests.tests.compliance.compliancetests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceCreate;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.compliancefiletests.ComplianceFileTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.complianceratingstests.ComplianceRatingsTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.compliancetypestests.ComplianceTypesTestsConstants;
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

public class CompliancePostTests {
    private SubContractorController subContractorController;
    private ComplianceController complianceController;
    private String complianceDocumentId;
    private File complianceDocument;
    private int subContractorId;

    @BeforeClass
    public void setupTest() throws IOException {
        complianceController = new ComplianceController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        complianceDocument = TestsUtils.createMicrosoftWordDocument(ComplianceFileTestsConstants.PATH + ComplianceFileTestsConstants.NAME, ComplianceFileTestsConstants.COMPLIANCE_DOCUMENT_CONTENT);

        complianceDocumentId = complianceController.uploadFile(complianceDocument).getData().getId();
    }

    @AfterClass
    public void setDownTest() {
        complianceDocument.delete();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/Compliance endpoint
     * THEN I should get Status Code of 201 and success message
     */
    @Test
    public void createComplianceTest() {
        ComplianceCreate createResponse = complianceController.createCompliance(CompliancePayloads.createCompliance(subContractorId, ComplianceTestsConstants.COMPLIANCE_TYPE_ID, ComplianceTestsConstants.COMPLIANCE_RATING_ID, ComplianceTestsConstants.EXPIRATION_DATE, ComplianceTestsConstants.COMMENT, complianceDocumentId));

        Assert.assertTrue(createResponse.getIsSuccess());
        Assert.assertEquals(createResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_201);
        Assert.assertEquals(createResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(createResponse.getData());

        int complianceId = createResponse.getData();

        ComplianceIdGet response = complianceController.getCompliance(complianceId);

        Assert.assertEquals(response.getData().getId(), complianceId);
        Assert.assertEquals(response.getData().getSubcontractorId(), subContractorId);
        Assert.assertEquals(response.getData().getFile().getId(), complianceDocumentId);
        Assert.assertEquals(response.getData().getFile().getFilename(), ComplianceFileTestsConstants.NAME);
        Assert.assertEquals(response.getData().getComplianceTypeId(), ComplianceTestsConstants.COMPLIANCE_TYPE_ID);
        Assert.assertEquals(response.getData().getComplianceType(), ComplianceTypesTestsConstants.TYPES.get(ComplianceTestsConstants.COMPLIANCE_TYPE_ID));
        Assert.assertEquals(response.getData().getComplianceRatingId(), ComplianceTestsConstants.COMPLIANCE_RATING_ID);
        Assert.assertEquals(response.getData().getComplianceRating(), ComplianceRatingsTestsConstants.RATINGS.get(ComplianceTestsConstants.COMPLIANCE_RATING_ID));
        Assert.assertEquals(response.getData().getExpirationDate(), ComplianceTestsConstants.EXPIRATION_DATE);
        Assert.assertEquals(response.getData().getComment(), ComplianceTestsConstants.COMMENT);
    }
}