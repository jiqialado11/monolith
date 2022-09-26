package com.dataart.subcontractorstool.apitests.tests.project.projectstatustests;

import com.dataart.subcontractorstool.apitests.controllers.ProjectController;
import com.dataart.subcontractorstool.apitests.responseentities.project.ProjectStatusGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

public class ProjectStatusGetTests {
    private ProjectController projectController;

    @BeforeClass
    public void setupTest() {
        projectController = new ProjectController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Project/Status endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void projectStatusesGetTest() {
        ProjectStatusGet response = projectController.getProjectStatuses();

        Map<Integer, String> responseProjectStatuses = new HashMap<>();
        Arrays.stream(response.getData()).forEach(data -> responseProjectStatuses.put(data.getId(), data.getName()));

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Assert.assertEquals(responseProjectStatuses, ProjectStatusTestsConstants.PROJECT_STATUSES);
    }
}