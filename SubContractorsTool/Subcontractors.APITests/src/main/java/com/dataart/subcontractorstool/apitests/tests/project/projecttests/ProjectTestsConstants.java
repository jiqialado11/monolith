package com.dataart.subcontractorstool.apitests.tests.project.projecttests;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class ProjectTestsConstants {
    public static final int PROJECT_PM_ID = 118877;

    public static final String PROJECT_NAME = "Education -> World, Me and Feedback training";

    public static final int PROJECT_GROUP_PM_ID = 30;

    public static final String PROJECT_GROUP = "Education";

    public static final int PROJECT_MANAGER_ID = 88688;

    public static final String START_DATE = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String ESTIMATED_FINISH_DATE = LocalDateTime.now().plusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final String FINISH_DATE = LocalDateTime.now().plusMonths(12).format(DateTimeFormatter.ofPattern("yyyy-MM-dd'T'hh:mm:ss"));

    public static final int PROJECT_STATUS_ID = 1;

    public static final String NO_PROJECTS_MESSAGE_PART_1 = "SubContractor with identifier ";

    public static final String NO_PROJECTS_MESSAGE_PART_2 = " doesn't have projects";
}