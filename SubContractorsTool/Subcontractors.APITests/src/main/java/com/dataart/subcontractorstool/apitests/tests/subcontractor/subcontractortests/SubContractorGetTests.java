package com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests;

import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorsGet;
import com.dataart.subcontractorstool.apitests.responseentities.subcontractor.subcontractor.SubContractorsGetDataItem;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractorstatustests.SubContractorStatusTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Objects;

import static org.testng.Assert.fail;

public class SubContractorGetTests {
    private SubContractorController subContractorController;

    @BeforeClass
    public void setupTest() {
        subContractorController = new SubContractorController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor endpoint
     * AND QueryType equals to "Library"
     * THEN I should get Status Code of 200 and success message
     * AND all SubContractors in the returned list have status "Active", "Inactive" or "Tentative"
     */
    @Test
    public void subContractorsGetQueryTypeLibraryTest() {
        SubContractorsGet response = subContractorController.getSubContractors(SubContractorTestsConstants.QUERY_TYPE_LIBRARY, SubContractorTestsConstants.RESULTS_QUANTITY);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        for (SubContractorsGetDataItem item : response.getData().getItems()) {
            if (!Objects.equals(item.getStatus(), SubContractorStatusTestsConstants.SUBCONTRACTOR_STATUSES_LIST.get(1)) & !Objects.equals(item.getStatus(), SubContractorStatusTestsConstants.SUBCONTRACTOR_STATUSES_LIST.get(2)) & !Objects.equals(item.getStatus(), SubContractorStatusTestsConstants.SUBCONTRACTOR_STATUSES_LIST.get(3))) {
                fail("Endpoint /api/SubContractor with QueryType=Library should contain SubContractors only with statuses Active, Inactive or Tentative. " + "Status \"" + item.getStatus() + "\" is not allowed. " + "SubContractor ID: " + item.getId());
            }
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/SubContractor endpoint
     * AND QueryType equals to "List"
     * THEN I should get Status Code of 200 and success message
     * AND all SubContractors in the returned list have status "Active" or "Inactive"
     */
    @Test
    public void subContractorsGetQueryTypeListTest() {
        SubContractorsGet response = subContractorController.getSubContractors(SubContractorTestsConstants.QUERY_TYPE_LIST, SubContractorTestsConstants.RESULTS_QUANTITY);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        for (SubContractorsGetDataItem item : response.getData().getItems()) {
            if (!Objects.equals(item.getStatus(), SubContractorStatusTestsConstants.SUBCONTRACTOR_STATUSES_LIST.get(1)) & !Objects.equals(item.getStatus(), SubContractorStatusTestsConstants.SUBCONTRACTOR_STATUSES_LIST.get(2))) {
                fail("Endpoint /api/SubContractor with QueryType=List should contain SubContractors only with statuses Active or Inactive. " + "Status \"" + item.getStatus() + "\" is not allowed. " + "SubContractor ID: " + item.getId());
            }
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I create SubContractor with Tentative status
     * THEN SubContractor should be present in Vendor Library and not present in Vendor List
     * WHEN I change SubContractor status to Active
     * THEN SubContractor should be present in Vendor Library and Vendor List
     * WHEN I change SubContractor status to Inactive
     * THEN SubContractor should be present in Vendor Library and Vendor List
     * WHEN I change SubContractor status to Tentative
     * THEN SubContractor should be present in Vendor Library and not present in Vendor List
     */
    @Test
    public void subContractorsGetChangeStatusesTest() {
        // Create SubContractor with Tentative status
        int subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_TENTATIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        // Check if SubContractor present in Vendor Library and not present in Vendor List
        TestsUtils.checkSubContractorPresenceInVendorLibrary(subContractorId);
        TestsUtils.checkSubContractorAbsenceInVendorList(subContractorId);

        // Change SubContractor status to Active
        subContractorController.updateSubContractorStatus(SubContractorPayloads.updateSubContractorStatus(subContractorId, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_ACTIVE));

        // Check if SubContractor present in Vendor Library and Vendor List
        TestsUtils.checkSubContractorPresenceInVendorLibrary(subContractorId);
        TestsUtils.checkSubContractorPresenceInVendorList(subContractorId);

        // Change SubContractor status to Inactive
        subContractorController.updateSubContractorStatus(SubContractorPayloads.updateSubContractorStatus(subContractorId, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE));

        // Check if SubContractor present in Vendor Library and Vendor List
        TestsUtils.checkSubContractorPresenceInVendorLibrary(subContractorId);
        TestsUtils.checkSubContractorPresenceInVendorList(subContractorId);

        // Change SubContractor status to Tentative
        subContractorController.updateSubContractorStatus(SubContractorPayloads.updateSubContractorStatus(subContractorId, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_TENTATIVE));

        // Check if SubContractor present in Vendor Library and not present in Vendor List
        TestsUtils.checkSubContractorPresenceInVendorLibrary(subContractorId);
        TestsUtils.checkSubContractorAbsenceInVendorList(subContractorId);
    }
}