package com.dataart.subcontractorstool.apitests.responseentities.compliance;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class ComplianceIdGetData {
    Integer id;
    Integer subcontractorId;
    ComplianceIdGetDataFile file;
    Integer complianceTypeId;
    String complianceType;
    Integer complianceRatingId;
    String complianceRating;
    String expirationDate;
    String comment;
}