package com.dataart.subcontractorstool.apitests.tests.compliance.complianceidtests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.compliancefiletests.ComplianceFileTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.complianceratingstests.ComplianceRatingsTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.compliance.compliancetests.ComplianceTestsConstants;
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

public class ComplianceIdGetTests {
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
     * WHEN I send a GET to api/Compliance/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getComplianceTest() {
        ComplianceIdGet response = complianceController.getCompliance(complianceId);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData());

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