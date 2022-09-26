package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortaxtypetests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorTaxTypeDeleteTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to api/SubContractor/TaxType endpoint
     * AND DELETE request is not allowed for api/SubContractor/TaxType endpoint
     * THEN I should get Status Code of 405 and error message
     */
    @Test
    public void subContractorTaxTypeDeleteTest() {
        subContractorController.deleteTaxType();
    }
}