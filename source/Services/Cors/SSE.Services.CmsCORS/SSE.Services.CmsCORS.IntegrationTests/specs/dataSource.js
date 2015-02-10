/** dataSource. */
var config = require("./config");
module.exports.createSurveyType = function(request, surveyTypeName, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
    form: {
      Name: surveyTypeName
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a SurveyType with name '" + surveyTypeName + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating SurveyType with name '" + surveyTypeName + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating SurveyType with name '" + surveyTypeName + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createSurvey = function(request, surveyTypeId, version, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/Surveys",
    form: {
      SurveyTypeId: surveyTypeId,
      Version: version
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a Survery with Version '" + version + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating Survery with Version '" + version + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating Survery with Version '" + version + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createQuestionMeaning = function(request, surveyTypeId, name, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings",
    form: {
      SurveyTypeId: surveyTypeId,
      Name: name
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a QuestionMeaning with Name '" + name + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating QuestionMeaning with Name '" + name + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating QuestionMeaning with Name '" + name + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createSurveyTranslation = function(request, surveyId, localCode, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/SurveyTranslations",
    form: {
      SurveyId: surveyId,
      LocalizationCode: localCode
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a SurveyTranslation with LocalizationCode '" + localCode + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating SurveyTranslation with LocalizationCode '" + localCode + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating SurveyTranslation with LocalizationCode '" + localCode + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createQuestion = function(request, surveyId, questionMeaningId, parentId, groupOrder, mapToTokenId, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/Questions",
    form: {
      SurveyId: surveyId,
      QuestionMeaningId: questionMeaningId,
      ParentId: parentId,
      GroupOrder: groupOrder,
      MapToTokenId: mapToTokenId
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a Question with SurveyID '" + surveyId + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating Question with SurveyID '" + surveyId + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating Question with SurveyID '" + surveyId + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createQuestionTranslation = function(request, surveyTranslationId, questionId, textFormat, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/QuestionTranslations",
    form: {
      SurveyTranslationId: surveyTranslationId,
      QuestionId: questionId,
      TextFormat: textFormat
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a QuestionTranslation with QuestionId '" + questionId + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating QuestionTranslation with QuestionId '" + questionId + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating QuestionTranslation with QuestionId '" + questionId + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createQuestionMeaning = function(request, surveyTypeId, name, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings",
    form: {
      SurveyTypeId: surveyTypeId,
      Name: name
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a QuestionMeaning with surveyTypeId '" + surveyTypeId + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating QuestionMeaning with surveyTypeId '" + surveyTypeId + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating QuestionMeaning with surveyTypeId '" + surveyTypeId + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createToken = function(request, name, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/Tokens",
    form: {
      Token: name
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a Token with Name '" + name + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating Token with Name '" + name + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating Token with Name '" + name + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createQuestionMeaningTokenMaps = function(request, questionMeaningId, tokenId, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeaningTokenMaps",
    form: {
      QuestionMeaningId: questionMeaningId,
      TokenId: tokenId
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a QuestionMeaningTokenMap with QuestionMeaningID/TokenID '" + questionMeaningId + "'/'" + tokenId + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating QuestionMeaningTokenMap with QuestionMeaningID/TokenID '" + questionMeaningId + "'/'" + tokenId + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating QuestionMeaningTokenMap with QuestionMeaningID/TokenID '" + questionMeaningId + "'/'" + tokenId + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createQuestionPossibleAnswerMaps = function(request, questionId, possibleAnswerId, expands, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/QuestionPossibleAnswerMaps",
    form: {
      QuestionId: questionId,
      PossibleAnswerId: possibleAnswerId,
      Expands: expands
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a QuestionPossibleAnswerMap with QuestionId/PossibleAnswerId/Expands '" + questionId + "'/'" + possibleAnswerId + "'/'" + expands + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating QuestionPossibleAnswerMap with QuestionId/PossibleAnswerId/Expands '" + questionId + "'/'" + possibleAnswerId + "'/'" + expands + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating QuestionPossibleAnswerMap with QuestionId/PossibleAnswerId/Expands '" + questionId + "'/'" + possibleAnswerId + "'/'" + expands + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.createPossibleAnswers = function(request, answerText, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "SurveySrv/PossibleAnswers",
    form: {
      AnswerText: answerText
    },
    json: true
  }, function(error, response, body) {
    //console.log("Body: ", body);
    if (error !== null) {
      console.log("Error creating a PossibleAnswers with Name '" + answerText + "': ", error);
      cb(null);
      return;
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error creating PossibleAnswers with Name '" + answerText + "'.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error creating PossibleAnswers with Name '" + answerText + "': ", body.Message);
      cb(null);
      return;
    }

    // ** Return the valid id. */
    cb(body.Value);
  });
};
module.exports.verifyAddress = function(request, params, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "QualifySrv/AddressValidation",
    form: {
      DealerId: params.DealerId,
      SeasonId: params.SeasonId,
      TeamLocationId: params.TeamLocationId,
      SalesRepId: params.SalesRepId,
      StreetAddress: params.StreetAddress,
      City: params.City,
      StateId: params.StateId,
      PostalCode: params.PostalCode,
      PhoneNumber: params.PhoneNumber
    },
    json: true
  }, function(error, response, body) {
    // console.log("VerifyAddress Body: ", body);
    if (error !== null) {
      console.log("Error validating address", error);
    }
    if (body.Code === null || body.Code === undefined) {
      console.log("Action returned an error validating address.");
      cb(null);
      return;
    }
    if (body.Code !== 0) {
      console.log("Action returned an error validating address.", body.Message);
      cb(null);
      return;
    }

    // ** Return the value
    cb(body.Value);
  });
};
module.exports.runCredit = function(request, params, cb) {
  // console.log("runCredit Params: ", params);
  request.post({
    url: config.SseServicesCmsCORS + "QualifySrv/runCredit",
    form: {
      AddressId: params.AddressID, // *REQUIRED:  This is the address that will be bounced against credit bureaus.
      DealerId: params.DealerId, // *REQUIRED:  This is which dealer is getting credit for the lead.  5000: Is NexSense.
      TeamLocationId: params.TeamLocationId, // *REQUIRED:  This is the office that credit is being ran from.  1: Would be default which means corporate.  For summer sales we would need to pass the correct office.
      SeasonId: params.SeasonId, // *REQUIRED:  This is the season that it was ran.  1: Would be the default for year round.
      SalesRepId: params.SalesRepId, // *REQUIRED:  This is the Company ID of a rep.
      LocalizationId: params.LocalizationId, // *REQUIRED:  This is the default language that they speak.
      LeadSourceId: params.LeadSourceId, // *REQUIRED:  This is the web.
      LeadDispositionId: params.LeadDispositionId, // *REQUIRED:  This is in what state is the lead.  9: Means "Ran Credit"
      Salutation: params.Salutation, // *OPTIONAL
      FirstName: params.FirstName, // *REQUIRED
      MiddleName: params.MiddleName, // *OPTIONAL
      LastName: params.LastName, // *REQUIRED
      Suffix: params.Suffix, // *OPTIONAL
      Gender: params.Gender, // *REQUIORED
      SSN: params.SSN, // *CONDITIONAL:  Either SSN or DOB has to be present or both.
      DOB: params.DOB, // *CONDITIONAL:  Either SSN or DOB has to be present or both.
      Dl: params.Dl, // *OPTIONAL
      DlStateId: params.DlStateId, // *OPTIONAL
      Email: params.Email, // *REQUIORED
      PhoneHome: params.PhoneHome, // *OPTIONAL
      PhoneWork: params.PhoneWork, // *OPTIONAL
      PhoneMobile: params.PhoneMobile // *OPTIONAL
    },
    json: true
  }, function(error, response, body) {
    // console.log("Lead Body: ", body);
    expect(error).toBeNull();
    expect(body).not.toBeNull();
    expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
    expect(body.Value).not.toBeNull("No Rep was returned.");

    cb(body.Value);
  });
};
module.exports.createMsAccount = function(request, params, cb) {
  request.post({
    url: config.SseServicesCmsCORS + 'MsAccountSetupSrv/Accounts',
    form: {
      LeadId: params.leadId
    },
    json: true
  }, function(error, response, body) {
    // console.log('Body of MsAccountLeadInfos: ', body);
    expect(error).toBeNull();
    expect(body).not.toBeNull();
    expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
    expect(body.Value).not.toBeNull("No value was returned.");

    cb(body.Value);
  });
};
module.exports.createEmc = function(request, params, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "MsAccountSetupSrv/EmergencyContacts",
    form: {
      CustomerId: params.CustomerId, // Not Required
      AccountId: params.AccountId,
      RelationshipId: params.RelationshipId, // In-law
      OrderNumber: params.OrderNumber,
      Allergies: params.Allergies, // Not Required
      MedicalConditions: params.MedicalConditions, // Not Required
      HasKey: params.HasKey,
      DOB: params.DOB,
      Prefix: params.Prefix, // Not Required
      FirstName: params.FirstName,
      MiddleName: params.MiddleName, // Not Required
      LastName: params.LastName,
      Postfix: params.Postfix, // Not Required
      Email: params.Email, // Not Required
      'Password': params.Password, // Not Required
      Phone1: params.Phone1,
      Phone1TypeId: params.Phone1TypeId, // Home.
      /** Values below here ia not required. */
      Phone2: params.Phone2,
      Phone2TypeId: params.Phone2TypeId,
      Phone3: params.Phone3,
      Phone3TypeId: params.Phone3TypeId,
      Comment1: params.Comment1
    },
    json: true
  }, function(error, response, body) {
    // console.log("Body: ", body);
    expect(error).toBeNull();
    expect(body.Code).toBe(0, "Code did not return a successfull code.");

    cb(body.Value);
  });
};
module.exports.createMsAccountScratch = function(request, params, cb) {
  module.exports.verifyAddress(request, {
    DealerId: params.DealerId,
    SeasonId: params.SeasonId,
    TeamLocationId: params.TeamLocationId,
    SalesRepId: params.SalesRepId,
    StreetAddress: params.StreetAddress,
    City: params.City,
    StateId: params.StateId,
    PostalCode: params.PostalCode,
    PhoneNumber: params.PhoneNumber

  }, function(addrValue) {
    // console.log("I'm here address: ", addrValue)
    module.exports.runCredit(request, {
      AddressID: addrValue.AddressID,
      DealerId: params.DealerId,
      TeamLocationId: params.TeamLocationId,
      SeasonId: params.SeasonId,
      SalesRepId: params.SalesRepId,
      LocalizationId: params.LocalizationId,
      LeadSourceId: params.LeadSourceId,
      LeadDispositionId: params.LeadDispositionId,
      Salutation: params.Salutation,
      FirstName: params.FirstName,
      MiddleName: params.MiddleName,
      LastName: params.LastName,
      Suffix: params.Suffix,
      Gender: params.Gender,
      SSN: params.SSN,
      DOB: params.DOB,
      Dl: params.Dl,
      DlStateId: params.DlStateId,
      Email: params.Email,
      PhoneHome: params.PhoneHome,
      PhoneWork: params.PhoneWork,
      PhoneMobile: params.PhoneMobile

    }, function(leadValue) {
      // console.log("I'm here lead: ", leadValue);
      module.exports.createMsAccount(request, {
        leadId: leadValue.LeadId

      }, function(accountValue) {
        // console.log("I'm here account: ", accountValue);
        cb(accountValue);
      });
    });
  });
};
module.exports.saveMsAccountSystemDetails = function(request, params, cb) {
  request.post({
    url: config.SseServicesCmsCORS + "MsAccountSetupSrv/SystemDetails/" + params.AccountID,
    form: {
      AccountID: params.AccountID,
      SystemTypeId: params.SystemTypeId,
      CellularTypeId: params.CellularTypeId,
      PanelTypeId: params.PanelTypeId,
      AccountPassword: params.AccountPassword,
      DslSeizureId: params.DslSeizureId
    },
    json: true
  }, function(error, response, body) {
    expect(error).toBeNull();
    expect(body).not.toBeNull();
    expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);
    // console.log("Body on Save SystemDetails: ", body);

    cb(body.Value);
  });
};
module.exports.generateIndustryAccount = function(request, params, cb) {
  request.get({
      url: config.SseServicesCmsCORS + "MonitoringStationSrv/MsAccounts/" + params.accountID + "/GenerateIndustryAccount",
      json: true
    },
    function(error, response, body) {
      // console.log("Body for generated Industry Account: ", body);
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);

      cb(body.Value);
    });
};
module.exports.equipmentLookupByPartnumber = function(request, params, cb) {
  request.get({
      url: config.SseServicesCmsCORS + "MsAccountSetupSrv/Equipments/" + params.PartNumber + "/ByPartNumber?id=" + params.AccountID +
        "&tId=" + params.TechID,
      json: true
    },
    function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);

      cb(body.Value);
    });
};
module.exports.equipmentLookupByBarcode = function(request, params, cb) {
  request.get({
      url: config.SseServicesCmsCORS + "MsAccountSetupSrv/Equipments/" + params.Barcode + "/ByBarcode?id=" + params.AccountID +
        "&tId=" + params.TechID,
      json: true
    },
    function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);

      cb(body.Value);
    });
};