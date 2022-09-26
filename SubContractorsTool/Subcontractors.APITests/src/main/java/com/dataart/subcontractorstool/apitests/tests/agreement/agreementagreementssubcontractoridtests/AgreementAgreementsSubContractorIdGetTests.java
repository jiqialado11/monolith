package com.dataart.subcontractorstool.apitests.tests.agreement.agreementagreementssubcontractoridtests;

import com.dataart.subcontractorstool.apitests.controllers.*;
import com.dataart.subcontractorstool.apitests.responseentities.agreement.AgreementsGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementaddendumtests.AgreementAddendumTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.agreement.agreementtests.AgreementTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.budget.budgetpaymentmethodstests.BudgetPaymentMethodsTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.budget.budgetpaymenttermstests.BudgetPaymentTermsTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.legalentity.legalentitytests.LegalEntityTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.project.projecttests.ProjectTestsConstants;
import com.dataart.subcontractorstool.apitests.tests.subcontractor.subcontractortests.SubContractorTestsConstants;
import com.dataart.subcontractorstool.apitests.utils.TestsUtils;
import com.dataart.subcontractorstool.apitests.utils.payloads.AgreementPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.ProjectPayloads;
import com.dataart.subcontractorstool.apitests.utils.payloads.SubContractorPayloads;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

public class AgreementAgreementsSubContractorIdGetTests {
    private SubContractorController subContractorController;
    private AgreementController agreementController;
    private ProjectController projectController;
    private int[] agreementIds = new int[5];
    private int subContractorId;
    private String projectId;

    @BeforeClass
    public void setupTest() {
        projectController = new ProjectController();
        agreementController = new AgreementController();
        subContractorController = new SubContractorController();

        subContractorId = subContractorController.createSubContractor(SubContractorPayloads.createSubContractor(SubContractorTestsConstants.SUBCONTRACTOR_NAME, SubContractorTestsConstants.SUBCONTRACTOR_TYPE_ID, SubContractorTestsConstants.SUBCONTRACTOR_STATUS_INACTIVE, TestsUtils.getLocationId(), SubContractorTestsConstants.SKILLS, SubContractorTestsConstants.COMMENT, SubContractorTestsConstants.CONTACT, SubContractorTestsConstants.LAST_INTERACTION_DATE, SubContractorTestsConstants.IS_NDA_SIGNED, SubContractorTestsConstants.SALES_OFFICE_ID, SubContractorTestsConstants.DEVELOPMENT_OFFICE_ID, SubContractorTestsConstants.COMPANY_SITE, SubContractorTestsConstants.MATERIALS, SubContractorTestsConstants.MARKET_ID)).getData();

        projectId = projectController.assignProjectToSubContractor(ProjectPayloads.assignProjectToSubContractor(ProjectTestsConstants.PROJECT_PM_ID, subContractorId, ProjectTestsConstants.PROJECT_NAME, ProjectTestsConstants.PROJECT_GROUP_PM_ID, ProjectTestsConstants.PROJECT_GROUP, ProjectTestsConstants.PROJECT_MANAGER_ID, ProjectTestsConstants.START_DATE, ProjectTestsConstants.ESTIMATED_FINISH_DATE, ProjectTestsConstants.FINISH_DATE, ProjectTestsConstants.PROJECT_STATUS_ID)).getData();

        for (int i = 0; i < agreementIds.length; i++){
            agreementIds[i] = agreementController.createAgreement(AgreementPayloads.createAgreement(AgreementTestsConstants.TITLE, subContractorId, TestsUtils.getLegalEntityId(), AgreementTestsConstants.START_DATE, AgreementTestsConstants.END_DATE, TestsUtils.getLocationId(), AgreementTestsConstants.CONDITIONS, TestsUtils.getPaymentMethodId(), AgreementTestsConstants.AGREEMENT_URL)).getData();
            agreementController.createAddendum(AgreementPayloads.createAddendum(AgreementAddendumTestsConstants.TITLE, agreementIds[i], projectId, AgreementAddendumTestsConstants.START_DATE, AgreementAddendumTestsConstants.END_DATE, AgreementAddendumTestsConstants.COMMENT, TestsUtils.getPaymentTermId(), AgreementAddendumTestsConstants.PAYMENT_TERM_IN_DAYS, TestsUtils.getCurrencyId(), AgreementAddendumTestsConstants.ADDENDUM_URL, AgreementAddendumTestsConstants.IS_FOR_NON_BILLABLE_PROJECTS)).getData();
        }
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Agreement/Agreements/{SubContractorId} endpoint
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getAgreementsTest() {
        AgreementsGet getResponse = agreementController.getAgreements(subContractorId);

        Assert.assertTrue(getResponse.getIsSuccess());
        Assert.assertEquals(getResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);
        Assert.assertEquals(getResponse.getData().length, agreementIds.length);

        for (int i = 0; i < agreementIds.length; i++){
            Assert.assertEquals(getResponse.getData()[i].getId(), agreementIds[i]);
            Assert.assertEquals(getResponse.getData()[i].getTitle(), AgreementTestsConstants.TITLE);
            Assert.assertEquals(getResponse.getData()[i].getConditions(), AgreementTestsConstants.CONDITIONS);
            Assert.assertEquals(getResponse.getData()[i].getLegalEntityId(), TestsUtils.getLegalEntityId());
            Assert.assertEquals(getResponse.getData()[i].getLegalEntity(), LegalEntityTestsConstants.LEGAL_ENTITIES_LIST.get(TestsUtils.getLegalEntityId()));
            Assert.assertEquals(getResponse.getData()[i].getUrl(), AgreementTestsConstants.AGREEMENT_URL);
            Assert.assertEquals(getResponse.getData()[i].getLocationId(), TestsUtils.getLocationId());
            Assert.assertEquals(getResponse.getData()[i].getPaymentMethodId(), TestsUtils.getPaymentMethodId());
            Assert.assertEquals(getResponse.getData()[i].getPaymentMethod(), BudgetPaymentMethodsTestsConstants.PAYMENT_METHODS_LIST.get(TestsUtils.getPaymentMethodId()));
            Assert.assertEquals(getResponse.getData()[i].getPaymentTerms()[0].getId(), TestsUtils.getPaymentTermId());
            Assert.assertEquals(getResponse.getData()[i].getPaymentTerms()[0].getName(), BudgetPaymentTermsTestsConstants.PAYMENT_TERMS_LIST.get(TestsUtils.getPaymentTermId()));
            Assert.assertEquals(getResponse.getData()[i].getStartDate(), AgreementTestsConstants.START_DATE);
            Assert.assertEquals(getResponse.getData()[i].getEndDate(), AgreementTestsConstants.END_DATE);
        }
    }
}