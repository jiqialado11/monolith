package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.staff.*;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import static io.restassured.RestAssured.given;

public class StaffController {
    private final String staffPath = "/api/Staff";
    private final String pmStaffPath = "/api/Staff/PM";
    private final String staffListPath = "/api/Staff/List";
    private final String staffProjectPath = "/api/Staff/Project";
    private final String staffStatusesPath = "/api/Staff/Status";
    private final String staffRateUnitsPath = "/api/Staff/RateUnits";

    public StaffCreate createStaff(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(staffPath)
                .then().extract().as(StaffCreate.class);
    }

    public StaffUpdate updateStaff(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(staffPath)
                .then().extract().as(StaffUpdate.class);
    }

    public StaffSubContractorIdGet getStaffListBySubContractorID(Integer subContractorId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(staffPath + "?SubContractorId=" + subContractorId)
                .then().extract().as(StaffSubContractorIdGet.class);
    }

    public StaffListGet getStaffList() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(staffListPath)
                .then().extract().as(StaffListGet.class);
    }

    public StaffIdGet getStaff(int staffId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(staffPath + "/" + staffId)
                .then().extract().as(StaffIdGet.class);
    }

    public StaffPmGet getStaffListFromPM() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(pmStaffPath)
                .then().extract().as(StaffPmGet.class);
    }

    public StaffPmIdGet getStaffFromPMById(Integer pmStaffId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(pmStaffPath + "/" + pmStaffId)
                .then().extract().as(StaffPmIdGet.class);
    }

    public StaffRateUnitsGet getRateUnits() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(staffRateUnitsPath)
                .then().extract().as(StaffRateUnitsGet.class);
    }

    public StaffStatusesGet getStaffStatuses() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(staffStatusesPath)
                .then().extract().as(StaffStatusesGet.class);
    }

    public StaffProjectCreate assignProjectToStaff(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(staffProjectPath)
                .then().extract().as(StaffProjectCreate.class);
    }

    public StaffProjectDelete unassignProjectToStaff(Integer staffId, String projectId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(staffProjectPath + "?StaffId=" + staffId + "&ProjectId=" + projectId)
                .then().extract().as(StaffProjectDelete.class);
    }
}