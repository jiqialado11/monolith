package com.dataart.subcontractorstool.apitests.controllers;

import com.dataart.subcontractorstool.apitests.responseentities.budget.BudgetOfficeGet;
import com.dataart.subcontractorstool.apitests.responseentities.budget.BudgetPaymentMethodsGet;
import com.dataart.subcontractorstool.apitests.responseentities.budget.BudgetPaymentTermsGet;
import com.dataart.subcontractorstool.apitests.utils.Specifications;

import static io.restassured.RestAssured.given;

public class BudgetController {
    private final String budgetOfficePath = "/api/Budget/Office";
    private final String budgetPaymentTermsPath = "/api/Budget/PaymentTerms";
    private final String budgetPaymentMethodsPath = "/api/Budget/PaymentMethods";

    public BudgetOfficeGet getOffices() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(budgetOfficePath)
                .then().extract().as(BudgetOfficeGet.class);
    }

    public BudgetPaymentTermsGet getPaymentTerms() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(budgetPaymentTermsPath)
                .then().extract().as(BudgetPaymentTermsGet.class);
    }

    public BudgetPaymentMethodsGet getPaymentMethods() {
        Specifications.run(Specifications.requestSpec(), Specifications.responseSpecOk200());

        return given()
                .when().get(budgetPaymentMethodsPath)
                .then().extract().as(BudgetPaymentMethodsGet.class);
    }
}