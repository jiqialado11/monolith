package com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumtests;

import com.dataart.subcontractorstool.apitests.controllers.*;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendumGet;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementAddendumUpdate;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests.AgreementTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.budget.budgetpaymenttermstests.BudgetPaymentTermsTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.common.commoncurrenciestests.CommonCurrenciesTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projecttests.ProjectTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.AgreementPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.ProjectPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class AgreementAddendumPutTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private ProjectController projectController;
    private int subContractorId;
    private String projectId;
    private int agreementId;
    private int addendumId;

    @BeforeClass
    public void setupTest() {
        projectController = new ProjectController();
        agreementController = new AgreementController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        projectId = projectController.assignProjectToSubContractor(ProjectPayloads.assignProjectToSubContractor(ProjectTestsConstants.PROJECT_PM_ID, subContractorId, ProjectTestsConstants.PROJECT_NAME, ProjectTestsConstants.PROJECT_GROUP_PM_ID, ProjectTestsConstants.PROJECT_GROUP, ProjectTestsConstants.PROJECT_MANAGER_ID, ProjectTestsConstants.START_DATE, ProjectTestsConstants.ESTIMATED_FINISH_DATE, ProjectTestsConstants.FINISH_DATE, ProjectTestsConstants.PROJECT_STATUS_ID)).getData();

        agreementId = agreementController.createAgreement(AgreementPayloads.createAgreement(AgreementTestsConstants.TITLE, subContractorId, TestsUtils.getLegalEntityId(), AgreementTestsConstants.START_DATE, AgreementTestsConstants.END_DATE, TestsUtils.getLocationId(), AgreementTestsConstants.CONDITIONS, TestsUtils.getPaymentMethodId(), AgreementTestsConstants.AGREEMENT_URL)).getData();

        addendumId = agreementController.createAddendum(AgreementPayloads.createAddendum(AgreementAddendumTestsConstants.TITLE, agreementId, projectId, AgreementAddendumTestsConstants.START_DATE, AgreementAddendumTestsConstants.END_DATE, AgreementAddendumTestsConstants.COMMENT, TestsUtils.getPaymentTermId(), AgreementAddendumTestsConstants.PAYMENT_TERM_IN_DAYS, TestsUtils.getCurrencyId(), AgreementAddendumTestsConstants.ADDENDUM_URL, AgreementAddendumTestsConstants.IS_FOR_NON_BILLABLE_PROJECTS)).getData();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a PUT to api/Agreement/Addendum endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void updateAddendumTest() {
        AgreementAddendumUpdate updateResponse = agreementController.updateAddendum(AgreementPayloads.updateAddendum(addendumId, AgreementAddendumTestsConstants.TITLE_UPDATE, agreementId, projectId, AgreementAddendumTestsConstants.START_DATE, AgreementAddendumTestsConstants.END_DATE, AgreementAddendumTestsConstants.COMMENT, TestsUtils.getPaymentTermId(), AgreementAddendumTestsConstants.PAYMENT_TERM_IN_DAYS, TestsUtils.getCurrencyId(), AgreementAddendumTestsConstants.ADDENDUM_URL, AgreementAddendumTestsConstants.IS_FOR_NON_BILLABLE_PROJECTS));

        Assert.assertTrue(updateResponse.getIsSuccess());
        Assert.assertEquals(updateResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_202);
        Assert.assertEquals(updateResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);


        AgreementAddendumGet getResponse = agreementController.getAddendum(addendumId);

        Assert.assertEquals(getResponse.getData().getId(), addendumId);
        Assert.assertEquals(getResponse.getData().getTitle(), AgreementAddendumTestsConstants.TITLE_UPDATE);
        Assert.assertEquals(getResponse.getData().getSubContractorId(), subContractorId);
        Assert.assertEquals(getResponse.getData().getAgreementId(), agreementId);
        Assert.assertEquals(getResponse.getData().getProjects()[0].getId(), projectId);
        Assert.assertEquals(getResponse.getData().getStartDate(), AgreementAddendumTestsConstants.START_DATE);
        Assert.assertEquals(getResponse.getData().getEndDate(), AgreementAddendumTestsConstants.END_DATE);
        Assert.assertEquals(getResponse.getData().getComment(), AgreementAddendumTestsConstants.COMMENT);
        Assert.assertEquals(getResponse.getData().getPaymentTermId(), TestsUtils.getPaymentTermId());
        Assert.assertEquals(getResponse.getData().getPaymentTerm(), BudgetPaymentTermsTestsConstants.PAYMENT_TERMS_LIST.get(TestsUtils.getPaymentTermId()));
        Assert.assertEquals(getResponse.getData().getPaymentTermInDays(), AgreementAddendumTestsConstants.PAYMENT_TERM_IN_DAYS);
        Assert.assertEquals(getResponse.getData().getCurrencyId(), TestsUtils.getCurrencyId());
        Assert.assertEquals(getResponse.getData().getCurrency(), CommonCurrenciesTestsConstants.CURRENCIES_LIST.get(TestsUtils.getCurrencyId()));
        Assert.assertEquals(getResponse.getData().getDocUrl(), AgreementAddendumTestsConstants.ADDENDUM_URL);
        Assert.assertEquals(getResponse.getData().getParentDocUrl(), AgreementTestsConstants.AGREEMENT_URL);
        Assert.assertEquals(getResponse.getData().getIsForNonBillableProjects(), AgreementAddendumTestsConstants.IS_FOR_NON_BILLABLE_PROJECTS);
    }
}