package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractoridtests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorCreate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorIdGetTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor/{Id} endpoint
     * AND SubContractorId value is valid
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void subContractorGetTest() {
        SubContractorCreate createResponse = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID));

        int subContractorId = createResponse.getData();

        SubContractorGet getResponse = subContractorController.getSubContractor(subContractorId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(getResponse.getData().getName(), SubContractorTestsConstants.SUBCONTRACTOR_NAME);
        Assert.assertEquals(getResponse.getData().getSubContractorType().getId(), SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID);
        Assert.assertEquals(getResponse.getData().getSubContractorStatus().getId(), SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE);
        Assert.assertEquals(getResponse.getData().getLocationId(), TestsUtils.getLocationId());
        Assert.assertEquals(getResponse.getData().getSkills(), SubContractorTestsConstants.SKILLS);
        Assert.assertEquals(getResponse.getData().getComment(), SubContractorTestsConstants.COMMENT);
        Assert.assertEquals(getResponse.getData().getContact(), SubContractorTestsConstants.CONTACT);
        Assert.assertEquals(getResponse.getData().getLastInteractionDate(), SubContractorTestsConstants.LAST_INTERACTION_DATE);
        Assert.assertEquals(getResponse.getData().getIsNdaSigned(), SubContractorTestsConstants.IS_NDA_SIGNED);
        Assert.assertEquals(getResponse.getData().getSalesOffices()[0].getId(), SubContractorTestsConstants.SALES_OFFICE_ID);
        Assert.assertEquals(getResponse.getData().getDevelopmentOffices()[0].getId(), SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID);
        Assert.assertEquals(getResponse.getData().getCompanySite(), SubContractorTestsConstants.COMPANY_SITE);
        Assert.assertEquals(getResponse.getData().getMaterials(), SubContractorTestsConstants.MATERIALS);
        Assert.assertEquals(getResponse.getData().getMarkets()[0].getId(), SubContractorTestsConstants.MARKET_ID);
    }
}