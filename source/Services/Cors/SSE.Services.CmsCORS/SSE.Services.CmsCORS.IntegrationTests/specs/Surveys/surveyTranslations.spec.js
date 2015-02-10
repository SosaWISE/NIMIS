/** Survey Translations */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("Questions.spec tests | ", function() {
  // ** InitiLize
  var stObj1, stObj2;
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody.Code).toBe(0);
      done();
    });
  });

            // **   END BEFORE EACH.
  it("Read all. SurveySrv/SurveyTranslations/", function(done) {
    // ** Create data
    dataSource.createSurveyType(request, "Survey Type [Survey Translation] Test.", function (surveyType){
      dataSource.createSurvey(request, surveyType.SurveyTypeID, "1.1", function (survey1){
        dataSource.createSurvey(request, surveyType.SurveyTypeID, "1.2", function (survey2){
          dataSource.createSurveyTranslation(request, survey1.SurveyID, "en-us", function (surveyTran1){
            stObj1 = surveyTran1;
            dataSource.createSurveyTranslation(request, survey2.SurveyID, "sp-us", function (surveyTran2){
              stObj2 = surveyTran2;
              // ** Get all tokens.
              request.get({
                url: config.SseServicesCmsCORS + "SurveySrv/SurveyTranslations/",
                json: true
              }, function(error, response, body) {
                expect(error).toBeNull();
                expect(body.Code).toBe(0);

                done();
              });
            });
          });
        });
      });
    });
  });

  it("Read one.  SurveySrv/SurveyTranslations/{id1}", function(done) {
    // ** Create data
    dataSource.createSurveyType(request, "Survey Type [Survey Translation] Test.", function (surveyType){
      dataSource.createSurvey(request, surveyType.SurveyTypeID, "1.1", function (survey1){
        dataSource.createSurveyTranslation(request, survey1.SurveyID, "en-us", function (surveyTran1){
          // ** Get a token.
          request.get({
            url: config.SseServicesCmsCORS + "SurveySrv/SurveyTranslations/" + surveyTran1.SurveyTranslationID,
            json: true
          }, function(error, response, body) {
            expect(error).toBeNull();
            expect(body).not.toBeNull();
            if (body.Code !== 0) {
              console.log("SurveySrv/SurveyTranslations/1 | Body:", body);
            }
            expect(body.Code).toBe(0, "SurveySrv/SurveyTranslations/" + surveyTran1.SurveyTranslationID + ": Code should be 0.");
            expect(body.Value.LocalizationCode).toBe("en-us");

            done();
          });
        });
      });
    });
  });

  it("Read one.  SurveySrv/SurveyTranslations/{id2}", function(done) {
    dataSource.createSurveyType(request, "Survey Type [Survey Translation] Test.", function (surveyType){
      dataSource.createSurvey(request, surveyType.SurveyTypeID, "1.2", function (survey2){
        dataSource.createSurveyTranslation(request, survey2.SurveyID, "sp-us", function (surveyTran2){
          // ** Get a token.
          request.get({
            url: config.SseServicesCmsCORS + "SurveySrv/SurveyTranslations/" + surveyTran2.SurveyTranslationID,
            json: true
          }, function(error, response, body) {
            expect(error).toBeNull();
            expect(body).not.toBeNull();
            if (body.Code !== 0) {
              console.log("SurveySrv/SurveyTranslations/1 | Body:", body);
            }
            expect(body.Code).toBe(0, "SurveySrv/SurveyTranslations/" + surveyTran2.SurveyTranslationID + ": Code should be 0.");
            expect(body.Value.LocalizationCode).toBe("sp-us");

            done();
          });
        });
      });
    });
  });


  it("Read one.  SurveySrv/SurveyTranslations/1/QuestionTranslations", function(done) {
    // ** Get /questions/(QuestionID)/questionPossibleAnswerMaps
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTranslations/1/QuestionTranslations",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("SurveySrv/SurveyTranslations/1/QuestionTranslations | Body:", body);
      }
      expect(body.Code).toBe(0, "surverysrv/Questions/1000/QuestionPossibleAnswerMaps: Code should be 0");

      //console.log(body);
      done();
    });
  });
});

describe("Create Update SurveyTranslations | ", function() {
  // ** Initialize
  var oldSurveyTran;
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody.Code).toBe(0);
      done();
    });
  });
  // **   END BEFORE EACH.
  it("Create One.", function(done) {
    // ** Create SurveyType First
    dataSource.createSurveyType(request, "ST for SurvTran", function(valueObj1) {
      // ** Create Survey
      dataSource.createSurvey(request, valueObj1.SurveyTypeID, "Version ST", function(surveyObject1) {
        // ** Create a Survey Translation
        dataSource.createSurveyTranslation(request, surveyObject1.SurveyID, "en-us", function(surveyTranslation) {
          expect(surveyTranslation).not.toBeNull();
          expect(surveyTranslation.SurveyTranslationID).not.toBe(0, "SurveyTranslationID should not be 0 or null.");
          expect(surveyTranslation.SurveyId).toBe(surveyObject1.SurveyID);
          expect(surveyTranslation.LocalizationCode).toBe("en-us");
          oldSurveyTran = surveyTranslation;
          done();
        });
      });
    });
  });
  it("Update one.", function(done) {
    expect(oldSurveyTran).not.toBeNull("oldSurveyTran: ", oldSurveyTran);
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTranslations/" + oldSurveyTran.SurveyTranslationID,
      form: {
        SurveyId: oldSurveyTran.SurveyId,
        LocalizationCode: "sp-ar"
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull("Error was not null", error);
      expect(body.Code).toBe(0, "There was an error generated from the action.  Body: ", body);
      expect(body.Value.LocalizationCode).toBe("sp-ar", "Update failed to work.");
      done();
    });
  });
  it("Update one.", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTranslations/",
      form: {
        SurveyTranslationID: oldSurveyTran.SurveyTranslationID,
        SurveyId: oldSurveyTran.SurveyId,
        LocalizationCode: "en"
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull("Error was not null", error);
      expect(body.Code).toBe(0, "There was an error generated from the action.  Body: ", body);
      expect(body.Value.LocalizationCode).toBe("en", "Update failed to work.");
      done();
    });
  });
});