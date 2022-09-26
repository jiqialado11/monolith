package com.dataart.subcontractorstool.apitests.tests.invoice.invoicereferraltests;

import com.dataart.subcontractorstool.apitests.controllers.InvoiceController;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoicePagesAllGet;
import com.dataart.subcontractorstool.apitests.responseentities.invoice.InvoiceReferralGet;
import com.dataart.subcontractorstool.apitests.tests.CommonTestsConstants;
import org.testng.Assert;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import java.io.IOException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.*;

import static org.testng.Assert.fail;

public class InvoiceReferralGetTests {
    private InvoiceController invoiceController;

    @BeforeClass
    public void setupTest() throws IOException {
        invoiceController = new InvoiceController();
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Invoice/Referral endpoint
     * AND PaymentNumber equals to "1"
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getReferralsWithPaymentNumberOne() {
        InvoiceReferralGet response = invoiceController.getReferrals(InvoiceReferralTestsConstants.PAYMENT_NUMBER_1);

        Assert.assertTrue(response.getIsSuccess());
        Assert.assertEquals(response.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(response.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);

        Map<LocalDate, Integer> startDates = new HashMap<>();
        Arrays
                .stream(response.getData())
                .forEach(staff ->
                        startDates.put(LocalDate.parse(staff.getStartDate(), DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss")), staff.getReferralId()));

        startDates
                .forEach( (startDate, userId) -> {
                    if (startDate.isBefore(InvoiceReferralTestsConstants.PROBATION_START_DATE)) {
                        fail("Probationary period is over for Staff with PM identifier " + userId + ". Payment number must be equal to 2");
                    }
                });
    }

    /**
     * GIVEN As a Vendor manager
     * WHEN I send a GET to api/Invoice/Referral endpoint
     * AND PaymentNumber equals to "2"
     * THEN I should get Status Code of 200 and success message
     */
    @Test
    public void getReferralsWithPaymentNumberTwo() {
        InvoiceReferralGet getReferralsResponse = invoiceController.getReferrals(InvoiceReferralTestsConstants.PAYMENT_NUMBER_2);
        InvoicePagesAllGet getInvoicesResponse = invoiceController.getInvoices(InvoiceReferralTestsConstants.CURRENT_PAGE, InvoiceReferralTestsConstants.RESULTS_PER_PAGE);
        List<Integer> actualReferralsIds = new ArrayList<>();
        List<Integer> expectedReferralsIds = new ArrayList<>();
        List<Integer> referralsIdsWithPaymentNumberTwo = new ArrayList<>();

        Assert.assertTrue(getReferralsResponse.getIsSuccess());
        Assert.assertEquals(getReferralsResponse.getStatusCode(), CommonTestsConstants.STATUS_CODE_200);
        Assert.assertEquals(getReferralsResponse.getMessage(), CommonTestsConstants.SUCCESS_MESSAGE);


        // Get referrals who have invoices with Payment Number = 1 but don`t have invoices with Payment Number = 2
        Arrays.stream(getReferralsResponse.getData()).forEach(referral -> actualReferralsIds.add(referral.getReferralId()));

        // Get referrals who have invoices with Payment Number = 1
        Arrays.stream(getInvoicesResponse.getData().getItems()).forEach(invoice -> {
                if (invoice.getPaymentNumber() == 1) { expectedReferralsIds.add(invoice.getReferralId()); }
        });

        // Get referrals who have invoices with Payment Number = 2
        Arrays.stream(getInvoicesResponse.getData().getItems()).forEach(invoice -> {
            if (invoice.getPaymentNumber() == 2) { referralsIdsWithPaymentNumberTwo.add(invoice.getReferralId()); }
        });

        // Delete referrals who have invoices with Payment Number = 2
        expectedReferralsIds.removeAll(referralsIdsWithPaymentNumberTwo);

        // Sort referrals lists in order to compare them
        Collections.sort(actualReferralsIds);
        Collections.sort(expectedReferralsIds);

        // Compare lists
        Assert.assertEquals(actualReferralsIds, expectedReferralsIds);
    }
}