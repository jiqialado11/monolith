package com.dataart.subcontractorstool.apitests.tests.compliance.compliancetests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceIdGet;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceUpdate;
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

public class CompliancePutTests {
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
     * WHEN I send a PUT to api/Compliance endpoint
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void updateComplianceTest() {
        ComplianceUpdate updateResponse = complianceController.updateCompliance(CompliancePayloads.updateCompliance(complianceId, ComplianceTestsConstants.COMPLIANCE_TYPE_ID_UPDATE, ComplianceTestsConstants.COMPLIANCE_RATING_ID, ComplianceTestsConstants.EXPIRATION_DATE, ComplianceTestsConstants.COMMENT, complianceDocumentId));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        ComplianceIdGet getResponse = complianceController.getCompliance(complianceId);

        Assert.assertEquals(getResponse.getData().getId(), complianceId);
        Assert.assertEquals(getResponse.getData().getSubcontractorId(), subContractorId);
        Assert.assertEquals(getResponse.getData().getFile().getId(), complianceDocumentId);
        Assert.assertEquals(getResponse.getData().getFile().getFilename(), ComplianceFileTestsConstants.NAME);
        Assert.assertEquals(getResponse.getData().getComplianceTypeId(), ComplianceTestsConstants.COMPLIANCE_TYPE_ID_UPDATE);
        Assert.assertEquals(getResponse.getData().getComplianceType(), ComplianceTypesTestsConstants.TYPES.get(ComplianceTestsConstants.COMPLIANCE_TYPE_ID_UPDATE));
        Assert.assertEquals(getResponse.getData().getComplianceRatingId(), ComplianceTestsConstants.COMPLIANCE_RATING_ID);
        Assert.assertEquals(getResponse.getData().getComplianceRating(), ComplianceRatingsTestsConstants.RATINGS.get(ComplianceTestsConstants.COMPLIANCE_RATING_ID));
        Assert.assertEquals(getResponse.getData().getExpirationDate(), ComplianceTestsConstants.EXPIRATION_DATE);
        Assert.assertEquals(getResponse.getData().getComment(), ComplianceTestsConstants.COMMENT);
    }
}