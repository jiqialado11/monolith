package com.dataart.subcontractorstool.apitests.tests.project.projecttests;

import com.dataart.subcontractorstool.apitests.controllers.ProjectController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.project.ProjectCreate;
import com.dataart.subcontractorstool.apitests.responseentities.project.ProjectIdGet;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projectstatustests.ProjectStatusTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.stafftests.StaffTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.ProjectPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class ProjectPostTests {
    private SubContractorController subContractorController;
    private ProjectController projectController;
    private StaffController staffController;
    private int projectManagerId;
    private int subContractorId;
    private String projectId;
    private String firstname;
    private String lastname;


    @BeforeClass
    public void setupTest() {
        staffController = new StaffController();
        projectController = new ProjectController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        projectManagerId = staffController.createStaff(StaffPayloads.createStaff(TestsUtils.getUniquePmStaffId(), subContractorId, StaffTestsConstants.FIRST_NAME, StaffTestsConstants.LAST_NAME, StaffTestsConstants.EMAIL, StaffTestsConstants.SKYPE, StaffTestsConstants.POSITION, StaffTestsConstants.START_DATE, StaffTestsConstants.END_DATE, StaffTestsConstants.QUALIFICATIONS, StaffTestsConstants.REAL_LOCATION, StaffTestsConstants.CELL_PHONE, StaffTestsConstants.IS_NDA_SIGNED, StaffTestsConstants.DEPARTMENT_NAME, StaffTestsConstants.DOMAIN_LOGIN)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a POST to api/Project endpoint
     * THEN I should get Status Code of 202 and success message
     */
    @Test
    public void createProjectTest() {
        ProjectCreate createProjectResponse = projectController.assignProjectToSubContractor(ProjectPayloads.assignProjectToSubContractor(ProjectTestsConstants.PROJECT_PM_ID, subContractorId, ProjectTestsConstants.PROJECT_NAME, ProjectTestsConstants.PROJECT_GROUP_PM_ID, ProjectTestsConstants.PROJECT_GROUP, projectManagerId, ProjectTestsConstants.START_DATE, ProjectTestsConstants.ESTIMATED_FINISH_DATE, ProjectTestsConstants.FINISH_DATE, ProjectTestsConstants.PROJECT_STATUS_ID));

        Assert.assertTrue(createProjectResponse.getIsSuccess());
        Assert.assertEquals(createProjectResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(createProjectResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(createProjectResponse.getData());

        projectId = createProjectResponse.getData();

        ProjectIdGet getProjectResponse = projectController.getProject(projectId);

        StaffIdGet getStaffResponse = staffController.getStaff(projectManagerId);

        firstname = getStaffResponse.getData().getFirstname();
        lastname = getStaffResponse.getData().getLastname();

        Assert.assertEquals(getProjectResponse.getData().getId(), projectId);
        Assert.assertEquals(getProjectResponse.getData().getPmId(), ProjectTestsConstants.PROJECT_PM_ID);
        Assert.assertEquals(getProjectResponse.getData().getProjectName(), ProjectTestsConstants.PROJECT_NAME);
        Assert.assertEquals(getProjectResponse.getData().getProjectGroupPmId(), ProjectTestsConstants.PROJECT_GROUP_PM_ID);
        Assert.assertEquals(getProjectResponse.getData().getProjectGroup(), ProjectTestsConstants.PROJECT_GROUP);
        Assert.assertEquals(getProjectResponse.getData().getProjectManagerId(), projectManagerId);
        Assert.assertEquals(getProjectResponse.getData().getProjectManager(), firstname + " " + lastname);
        Assert.assertEquals(getProjectResponse.getData().getStartDate(), ProjectTestsConstants.START_DATE);
        Assert.assertEquals(getProjectResponse.getData().getEstimatedFinishDate(), ProjectTestsConstants.ESTIMATED_FINISH_DATE);
        Assert.assertEquals(getProjectResponse.getData().getFinishDate(), ProjectTestsConstants.FINISH_DATE);
        Assert.assertEquals(getProjectResponse.getData().getProjectStatusId(), ProjectTestsConstants.PROJECT_STATUS_ID);
        Assert.assertEquals(getProjectResponse.getData().getProjectStatus(), ProjectStatusTestsConstants.PROJECT_STATUSES.get(ProjectTestsConstants.PROJECT_STATUS_ID));
    }
}