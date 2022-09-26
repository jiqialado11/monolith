package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.common.CommonCurrenciesGet;
import com.dataart.subcontractorstool.apitests.responseentities.common.CommonLocationsGet;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import static io.restassured.RestAssured.given;

public class CommonController {
    private final String commonLocationsPath = "/api/Common/Locations";
    private final String commonCurrenciesPath = "/api/Common/Currencies";

    public CommonLocationsGet getLocations() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(commonLocationsPath)
                .then().extract().as(CommonLocationsGet.class);
    }

    public CommonCurrenciesGet getCurrencies() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(commonCurrenciesPath)
                .then().extract().as(CommonCurrenciesGet.class);
    }
}