package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorCreate;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorUpdate;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class SubContractorPutTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/SubContractor endpoint
     * AND all values are valid
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void subContractorUpdateTest() {
        SubContractorCreate createResponse = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID));

        int subContractorId = createResponse.getData();

        SubContractorUpdate updateResponse = subContractorController.updateSubContractor(SubContractorPayloads.updateSubContractor(subContractorId, SubContractorTestsConstants.SUBCONTRACTOR_NAME_UPDATE, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_UPDATE, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_UPDATE, TestsUtils.getLocationId() + 1, SubContractorTestsConstants.SKILLS_UPDATE, SubContractorTestsConstants.COMMENT_UPDATE, SubContractorTestsConstants.CONTACT_UPDATE, SubContractorTestsConstants.LAST_INTERACTION_DATE_UPDATE, SubContractorTestsConstants.IS_NDA_SIGNED_UPDATE, SubContractorTestsConstants.SALES_OFFICE_ID_UPDATE, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID_UPDATE, SubContractorTestsConstants.COMPANY_SITE_UPDATE, SubContractorTestsConstants.MATERIALS_UPDATE, SubContractorTestsConstants.MARKET_ID_UPDATE, SubContractorTestsConstants.ACCOUNT_MANAGER_ID_UPDATE));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        SubContractorGet getResponse = subContractorController.getSubContractor(subContractorId);

        Assert.assertEquals(getResponse.getData().getName(), SubContractorTestsConstants.SUBCONTRACTOR_NAME_UPDATE);
        Assert.assertEquals(getResponse.getData().getSubContractorType().getId(), SubContractorTestsConstants.SUBCONTRACTOR_TYPE_UPDATE);
        Assert.assertEquals(getResponse.getData().getSubContractorStatus().getId(), SubContractorTestsConstants.SUBCONTRACTOR_STATUS_UPDATE);
        Assert.assertEquals(getResponse.getData().getLocationId(), TestsUtils.getLocationId() + 1);
        Assert.assertEquals(getResponse.getData().getSkills(), SubContractorTestsConstants.SKILLS_UPDATE);
        Assert.assertEquals(getResponse.getData().getComment(), SubContractorTestsConstants.COMMENT_UPDATE);
        Assert.assertEquals(getResponse.getData().getContact(), SubContractorTestsConstants.CONTACT_UPDATE);
        Assert.assertEquals(getResponse.getData().getLastInteractionDate(), SubContractorTestsConstants.LAST_INTERACTION_DATE_UPDATE);
        Assert.assertEquals(getResponse.getData().getIsNdaSigned(), SubContractorTestsConstants.IS_NDA_SIGNED_UPDATE);
        Assert.assertEquals(getResponse.getData().getSalesOffices()[0].getId(), SubContractorTestsConstants.SALES_OFFICE_ID_UPDATE);
        Assert.assertEquals(getResponse.getData().getDevelopmentOffices()[0].getId(), SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID_UPDATE);
        Assert.assertEquals(getResponse.getData().getCompanySite(), SubContractorTestsConstants.COMPANY_SITE_UPDATE);
        Assert.assertEquals(getResponse.getData().getMaterials(), SubContractorTestsConstants.MATERIALS_UPDATE);
        Assert.assertEquals(getResponse.getData().getMarkets()[0].getId(), SubContractorTestsConstants.MARKET_ID_UPDATE);
    }
}