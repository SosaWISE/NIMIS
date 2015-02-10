/** Question Translations tests. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("Create QuestionTranslation |", function() {
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

  it("Read All, SurveySrv/QuestionTranslations.", function(done) {
    dataSource.createSurveyType(request, "Get All 1", function(valueObj1) {
      if (valueObj1 !== null) {
        // ** Creae a second survey type.
        dataSource.createSurveyType(request, "Get All 2", function(valueObj2) {
          //console.log("SurveyType 2:", valueObj2);
          if (valueObj2 !== null) {
            dataSource.createSurvey(request, valueObj1.SurveyTypeID, "Version 1", function(surveyObject1) {
              //console.log("Survy 1:", surveyObject1);
              dataSource.createSurveyTranslation(request, surveyObject1.SurveyID, "en-us", function(surveyTranslation) {
                dataSource.createQuestionMeaning(request, valueObj2.SurveyTypeID, "Some qm meaning...", function(qm) {
                  dataSource.createQuestion(request, surveyObject1.SurveyID, qm.QuestionMeaningID, null, 1, null, function(questionObj) {
                    // ** START ACTUAL TEST
                    dataSource.createQuestionTranslation(request, surveyTranslation.SurveyTranslationID, questionObj.QuestionID, "Some textFormat", function(resultObj) {
                      expect(resultObj).not.toBeNull("Error occurred creating QuestionTranslation.");
                      expect(resultObj.QuestionTranslationID).toBeGreaterThan(0, "QuestionTranslation identity column should be set.");
                      request.get({
                        url: config.SseServicesCmsCORS + "SurveySrv/QuestionTranslations",
                        json: true
                      }, function(error, response, body) {
                        // ** Check results
                        expect(error).toBeNull("There was an error retrieving all QuestionTranslations.");
                        expect(body).not.toBeNull("Body returned as null");
                        expect(body.Code).toBe(0, "There was an error retrieving all QuestionTranslations. | Body: ", body);
                        expect(body.Value.length).toBeGreaterThan(0, "This should have at least returned 1 question translation.");

                        // ** Finished test
                        done();
                      });
                    });
                    // **   END ACTUAL TEST
                  });
                });
              });
            });
          }
        });
      }
    });
  });
});
