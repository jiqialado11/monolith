package com.dataart.subcontractorstool.apitests.utils;

import io.restassured.RestAssured;
import io.restassured.builder.RequestSpecBuilder;
import io.restassured.builder.ResponseSpecBuilder;
import io.restassured.http.ContentType;
import io.restassured.specification.RequestSpecification;
import io.restassured.specification.ResponseSpecification;

public class Specifications {
    private final static String baseUri = "https://subc-dev.dataart.com/";
    private final static int port = 8001;

    public static RequestSpecification requestSpec() {
        return new RequestSpecBuilder()
                .setBaseUri(baseUri)
                .setPort(port)
                .setContentType(ContentType.JSON)
                .build();
    }

    public static RequestSpecification requestSpecUploadFile() {
        return new RequestSpecBuilder()
                .setBaseUri(baseUri)
                .setPort(port)
                .setContentType("multipart/form-data")
                .setAccept("text/plain")
                .build();
    }

    public static ResponseSpecification responseSpecOk200() {
        return new ResponseSpecBuilder()
                .expectStatusCode(200)
                .build();
    }

    public static ResponseSpecification responseSpecMethodNotAllowed405() {
        return new ResponseSpecBuilder()
                .expectStatusCode(405)
                .build();
    }

    public static void run(RequestSpecification requestSpec, ResponseSpecification responseSpec) {
        RestAssured.requestSpecification = requestSpec;
        RestAssured.responseSpecification = responseSpec;
    }
}