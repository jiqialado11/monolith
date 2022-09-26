package com.dataart.subcontractorstool.apitests.tests.compliance.compliancetests;

import com.dataart.subcontractorstool.apitests.controllers.ComplianceController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.compliance.ComplianceGet;
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

public class ComplianceGetTests {
    private SubContractorController subContractorController;
    private ComplianceController complianceController;
    private int subContractorId;
    private byte complianceQuantity = 5;
    private int[] compliancesIds = new int[complianceQuantity];
    private String[] complianceDocumentsIds = new String[complianceQuantity];
    private File[] complianceDocuments = new File[complianceQuantity];

    @BeforeClass
    public void setupTest() throws IOException {
        complianceController = new ComplianceController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        for(int i = 0; i<complianceDocumentsIds.length; i++){
            complianceDocuments[i] = TestsUtils.createMicrosoftWordDocument(ComplianceFileTestsConstants.PATH + ComplianceFileTestsConstants.NAMES[i], ComplianceFileTestsConstants.COMPLIANCE_DOCUMENT_CONTENT);
            complianceDocumentsIds[i] = complianceController.uploadFile(complianceDocuments[i]).getData().getId();
        }

        for(int i = 0; i<compliancesIds.length; i++){
            compliancesIds[i] = complianceController.createCompliance(CompliancePayloads.createCompliance(subContractorId, ComplianceTestsConstants.COMPLIANCE_TYPE_ID, ComplianceTestsConstants.COMPLIANCE_RATING_ID, ComplianceTestsConstants.EXPIRATION_DATE, ComplianceTestsConstants.COMMENT, complianceDocumentsIds[i])).getData();
        }
    }

    @AfterClass
    public void setDownTest() {
        for ( File complianceDocument : complianceDocuments ) {
            complianceDocument.delete();
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Compliance endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getComplianceListTest() {
        ComplianceGet response = complianceController.getComplianceList(subContractorId);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertEquals(response.getData().length, compliancesIds.length);

        for(int i = 0; i<compliancesIds.length; i++){
            Assert.assertEquals(response.getData()[i].getId(), compliancesIds[i]);
            Assert.assertEquals(response.getData()[i].getSubcontractorId(), subContractorId);
            Assert.assertEquals(response.getData()[i].getFile().getId(), complianceDocumentsIds[i]);
            Assert.assertEquals(response.getData()[i].getFile().getFilename(), ComplianceFileTestsConstants.NAMES[i]);
            Assert.assertEquals(response.getData()[i].getComplianceTypeId(), ComplianceTestsConstants.COMPLIANCE_TYPE_ID);
            Assert.assertEquals(response.getData()[i].getComplianceType(), ComplianceTypesTestsConstants.TYPES.get(ComplianceTestsConstants.COMPLIANCE_TYPE_ID));
            Assert.assertEquals(response.getData()[i].getComplianceRatingId(), ComplianceTestsConstants.COMPLIANCE_RATING_ID);
            Assert.assertEquals(response.getData()[i].getComplianceRating(), ComplianceRatingsTestsConstants.RATINGS.get(ComplianceTestsConstants.COMPLIANCE_RATING_ID));
            Assert.assertEquals(response.getData()[i].getExpirationDate(), ComplianceTestsConstants.EXPIRATION_DATE);
            Assert.assertEquals(response.getData()[i].getComment(), ComplianceTestsConstants.COMMENT);
        }
    }
}