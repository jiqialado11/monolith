package com.dataart.subcontractorstool.apitests.tests.project.projectpmtests;

import com.dataart.subcontractorstool.apitests.controllers.ProjectController;
import com.dataart.subcontractorstool.apitests.responseentities.project.ProjectPmGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class ProjectPmGetTests {
    private ProjectController projectController;

    @BeforeClass
    public void setupTest() {
        projectController = new ProjectController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Project/PM endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void pmProjectsListGetTest() {
        ProjectPmGet response = projectController.getProjectsListFromPM();

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertNotNull(response.getData());
    }
}