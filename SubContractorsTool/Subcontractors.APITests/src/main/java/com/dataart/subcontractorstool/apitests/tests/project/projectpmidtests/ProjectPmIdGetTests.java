package com.dataart.subcontractorstool.apitests.tests.project.projectpmidtests;

import com.dataart.subcontractorstool.apitests.controllers.ProjectController;
import com.dataart.subcontractorstool.apitests.responseentities.project.ProjectPmIdGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class ProjectPmIdGetTests {
    private ProjectController projectController;

    @BeforeClass
    public void setupTest() {
        projectController = new ProjectController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Project/PM/{Id} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void pmProjectGetTest() {
        ProjectPmIdGet response = projectController.getProjectFromPM(ProjectPmIdTestsConstants.PROJECT_PM_ID);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData());

        Assert.assertEquals(response.getData().getProjectPmId(), ProjectPmIdTestsConstants.PROJECT_PM_ID);
        Assert.assertEquals(response.getData().getProjectGroupId(), ProjectPmIdTestsConstants.PROJECT_GROUP_ID);
        Assert.assertEquals(response.getData().getProjectGroup(), ProjectPmIdTestsConstants.PROJECT_GROUP);
        Assert.assertEquals(response.getData().getProjectManagerId(), ProjectPmIdTestsConstants.PROJECT_MANAGER_ID);
        Assert.assertEquals(response.getData().getProjectManager(), ProjectPmIdTestsConstants.PROJECT_MANAGER);
        Assert.assertEquals(response.getData().getStartDate(), ProjectPmIdTestsConstants.START_DATE);
        Assert.assertEquals(response.getData().getEstimatedFinishDate(), ProjectPmIdTestsConstants.ESTIMATED_FINISH_DATE);
        Assert.assertEquals(response.getData().getFinishDate(), ProjectPmIdTestsConstants.FINISH_DATE);
        Assert.assertEquals(response.getData().getStatusId(), ProjectPmIdTestsConstants.STATUS_ID);
        Assert.assertEquals(response.getData().getStatus(), ProjectPmIdTestsConstants.STATUS);
    }
}