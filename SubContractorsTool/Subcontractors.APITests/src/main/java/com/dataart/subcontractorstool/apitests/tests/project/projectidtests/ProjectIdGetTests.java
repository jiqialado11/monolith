package com.dataart.subcontractorstool.apitests.tests.project.projectidtests;

import com.dataart.subcontractorstool.apitests.controllers.ProjectController;
import com.dataart.subcontractorstool.apitests.controllers.StaffController;
import com.dataart.subcontractorstool.apitests.controllers.SubContractorController;
import com.dataart.subcontractorstool.apitests.responseentities.project.ProjectIdGet;
import com.dataart.subcontractorstool.apitests.responseentities.staff.StaffIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projectstatustests.ProjectStatusTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projecttests.ProjectTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.staff.stafftests.StaffTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.ProjectPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.StaffPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class ProjectIdGetTests {
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

        projectId = projectController.assignProjectToSubContractor(ProjectPayloads.assignProjectToSubContractor(ProjectTestsConstants.PROJECT_PM_ID, subContractorId, ProjectTestsConstants.PROJECT_NAME, ProjectTestsConstants.PROJECT_GROUP_PM_ID, ProjectTestsConstants.PROJECT_GROUP, projectManagerId, ProjectTestsConstants.START_DATE, ProjectTestsConstants.ESTIMATED_FINISH_DATE, ProjectTestsConstants.FINISH_DATE, ProjectTestsConstants.PROJECT_STATUS_ID)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Project/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getProjectTest() {
        ProjectIdGet projectResponse = projectController.getProject(projectId);
        StaffIdGet staffResponse = staffController.getStaff(projectManagerId);

        firstname = staffResponse.getData().getFirstname();
        lastname = staffResponse.getData().getLastname();

        Assert.assertTrue(projectResponse.getIsSuccess());
        Assert.assertEquals(projectResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(projectResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(projectResponse.getData().getId(), projectId);
        Assert.assertEquals(projectResponse.getData().getPmId(), ProjectTestsConstants.PROJECT_PM_ID);
        Assert.assertEquals(projectResponse.getData().getProjectName(), ProjectTestsConstants.PROJECT_NAME);
        Assert.assertEquals(projectResponse.getData().getProjectGroupPmId(), ProjectTestsConstants.PROJECT_GROUP_PM_ID);
        Assert.assertEquals(projectResponse.getData().getProjectGroup(), ProjectTestsConstants.PROJECT_GROUP);
        Assert.assertEquals(projectResponse.getData().getProjectManagerId(), projectManagerId);
        Assert.assertEquals(projectResponse.getData().getProjectManager(), firstname + " " + lastname);
        Assert.assertEquals(projectResponse.getData().getStartDate(), ProjectTestsConstants.START_DATE);
        Assert.assertEquals(projectResponse.getData().getEstimatedFinishDate(), ProjectTestsConstants.ESTIMATED_FINISH_DATE);
        Assert.assertEquals(projectResponse.getData().getFinishDate(), ProjectTestsConstants.FINISH_DATE);
        Assert.assertEquals(projectResponse.getData().getProjectStatusId(), ProjectTestsConstants.PROJECT_STATUS_ID);
        Assert.assertEquals(projectResponse.getData().getProjectStatus(), ProjectStatusTestsConstants.PROJECT_STATUSES.get(ProjectTestsConstants.PROJECT_STATUS_ID));
    }
}