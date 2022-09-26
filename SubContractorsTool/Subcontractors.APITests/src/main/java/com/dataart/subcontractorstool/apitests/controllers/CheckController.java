package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.check.*;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import static io.restassured.RestAssured.given;

public class CheckController {
    private final String sanctionChecksPath = "/api/Check/SanctionChecks";
    private final String sanctionCheckPath = "/api/Check/SanctionCheck";
    private final String backgroundChecksPath = "/api/Check/BackgroundChecks";
    private final String backgroundCheckPath = "/api/Check/BackgroundCheck";
    private final String checkStatusesPath = "/api/Check/Status";

    public CheckSanctionChecksGet getSanctionChecks(int parentId, int parentType) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(sanctionChecksPath + "?" + "ParentId=" + parentId + "&" + "ParentType=" + parentType)
                .then().extract().as(CheckSanctionChecksGet.class);
    }

    public CheckSanctionCheckGet getSanctionCheck(int sanctionCheckId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(sanctionCheckPath + "/" + sanctionCheckId)
                .then().extract().as(CheckSanctionCheckGet.class);
    }

    public CheckSanctionCheckCreate createSanctionCheck(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(sanctionCheckPath)
                .then().extract().as(CheckSanctionCheckCreate.class);
    }

    public CheckSanctionCheckUpdate updateSanctionCheck(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(sanctionCheckPath)
                .then().extract().as(CheckSanctionCheckUpdate.class);
    }

    public CheckSanctionCheckDelete deleteSanctionCheck(int sanctionCheckId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(sanctionCheckPath + "/" + sanctionCheckId)
                .then().extract().as(CheckSanctionCheckDelete.class);
    }

    public CheckBackgroundChecksGet getBackgroundChecks(int staffId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(backgroundChecksPath + "?" + "StaffId=" + staffId)
                .then().extract().as(CheckBackgroundChecksGet.class);
    }

    public CheckBackgroundCheckGet getBackgroundCheck(int backgroundCheckId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(backgroundCheckPath + "/" + backgroundCheckId)
                .then().extract().as(CheckBackgroundCheckGet.class);
    }

    public CheckBackgroundCheckCreate createBackgroundCheck(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().post(backgroundCheckPath)
                .then().extract().as(CheckBackgroundCheckCreate.class);
    }

    public CheckBackgroundCheckUpdate updateBackgroundCheck(String payload) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .body(payload)
                .when().put(backgroundCheckPath)
                .then().extract().as(CheckBackgroundCheckUpdate.class);
    }

    public CheckBackgroundCheckDelete deleteBackgroundCheck(int backgroundCheckId) {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().delete(backgroundCheckPath + "/" + backgroundCheckId)
                .then().extract().as(CheckBackgroundCheckDelete.class);
    }

    public CheckStatusGet getCheckStatuses() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(checkStatusesPath)
                .then().extract().as(CheckStatusGet.class);
    }
}