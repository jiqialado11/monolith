package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxestests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorTaxesPutTests {
    private SubContractorController subContractorController;
    private int subContractorId;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/{SubcontractorId}/Tax endpoint
     * AND PUT request is not allowed for api/SubContractor/{SubcontractorId}/Tax endpoint
     * THEN I should get Status Code of 405 and error message
     */
    @Test
    public void subContractorTaxesUpdateTest() {
        subContractorController.updateTaxes(subContractorId);
    }
}