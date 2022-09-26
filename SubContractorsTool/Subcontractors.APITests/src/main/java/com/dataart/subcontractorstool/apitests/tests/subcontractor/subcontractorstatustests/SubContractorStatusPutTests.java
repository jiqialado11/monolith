package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractorstatustests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.status.SubContractorStatusUpdate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorStatusPutTests {
    private SubContractorController subContractorController;
    private int subContractorId;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor/Status endpoint
     * AND all values are valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorStatusUpdateTest() {
        SubContractorStatusUpdate updateResponse = subContractorController.updateSubContractorStatus(SubContractorPayloads.updateSubContractorStatus(subContractorId, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_UPDATE));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        SubContractorGet getResponse = subContractorController.getSubContractor(subContractorId);

        Assert.assertEquals(getResponse.getData().getSubContractorStatus().getId(), SubContractorTestsConstants.SUBCONTRACTOR_STATUS_UPDATE);
    }
}