package com.dataart.subcontractorstool.apitests.tests.project.projecttests;

import com.dataart.subcontractorstool.apitests.controllers.ProjectController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.project.ProjectDelete;
import com.dataart.subcontractorstool.apitests.responseentities.project.ProjectSubContractorIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.ProjectPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class ProjectDeleteTests {
    private SubContractorController subContractorController;
    private ProjectController projectController;
    private StaffController staffController;
    private int subContractorId;
    private String projectId;

    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
        projectController = new ProjectController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        projectId = projectController.assignProjectToSubContractor(ProjectPayloads.assignProjectToSubContractor(ProjectTestsConstants.PROJECT_PM_ID, subContractorId, ProjectTestsConstants.PROJECT_NAME, ProjectTestsConstants.PROJECT_GROUP_PM_ID, ProjectTestsConstants.PROJECT_GROUP, ProjectTestsConstants.PROJECT_MANAGER_ID, ProjectTestsConstants.START_DATE, ProjectTestsConstants.ESTIMATED_FINISH_DATE, ProjectTestsConstants.FINISH_DATE, ProjectTestsConstants.PROJECT_STATUS_ID)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a DELETE to /api/Project endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void unassignProjectFromSubContractorTest() {
        ProjectDelete deleteResponse = projectController.unassignProjectFromSubContractor(subContractorId, projectId);

        Assert.assertTrue(deleteResponse.getIsSuccess());
        Assert.assertEquals(deleteResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(deleteResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        ProjectSubContractorIdGet getResponse = projectController.getProjectsBySubContractorId(subContractorId);

        Assert.assertEquals(getResponse.getMessage(), ProjectTestsConstants.NO_PROJECTS_MESSAGE_PART_1 + subContractorId + ProjectTestsConstants.NO_PROJECTS_MESSAGE_PART_2);
    }
}