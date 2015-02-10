/** Integration test for Surveys. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("Surveys.spec tests | ", function() {
  // ** Init
  var surveyTypeId, surveyId, surveyId2;
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody.Code).toBe(0);

      request.post({
        url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
        form: {
          Name: 'Survey Type Test Get'
        },
        json: true
      }, function(error, response, body) {
        expect(error).toBeNull();
        expect(body.Code).toBe(0, "Error on creating surveyType | Body: ", body);
        if (body.Code === 0) {
          surveyTypeId = body.Value.SurveyTypeID;
        }
        // ** START Create Survey
        request.post({
          url: config.SseServicesCmsCORS + "SurveySrv/Surveys",
          form: {
            SurveyTypeId: surveyTypeId,
            Version: "Some Ver 1"
          },
          json: true
        }, function(error, response, body) {
          expect(body.Code).toBe(0, "Error : " + body.Message);
          expect(error).toBeNull("There was an error creating a Survey.");
          if (body.Code === 0) {
            surveyId = body.Value.SurveyID;

            // ** START Create Another Survey.
            request.post({
              url: config.SseServicesCmsCORS + "SurveySrv/Surveys",
              form: {
                SurveyTypeId: surveyTypeId,
                Version: "Some Ver 1"
              },
              json: true
            }, function(error, response, body) {
              expect(body.Code).toBe(0, "Error : " + body.Message);
              expect(error).toBeNull("There was an error creating a Survey.");
              if (body.Code === 0) {
                surveyId2 = body.Value.SurveyID;
              }
              done();
            });
            // **   END Create Another Survey.
          }
        });
        // **   END Create Survey
      });
    });
  });
  // **   END BEFORE EACH.
  it("Read all.  ''", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/Surveys",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0, "SurveySrv/Surveys GET | Body: ", body);
      //console.log("SurveySrv/SurveyTypes GET | Body: ", body);
      expect(body.Value.length).toBeGreaterThan(1);
      done();
    });
  });

  it("Read one. SurveySrv/Surveys/" + surveyId, function(done) {
    // ** Get a token.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/Surveys/" + surveyId,
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        // console.log("SurveySrv/Survey/" + surveyId + " | Body:", body);
      }
      expect(body.Code).toBe(0, "SurveySrv/Surveys/" + surveyId + ": Code should be 0.");
      //expect(body.Value).not.toBeUndefined();

      //console.log("Body: ", body);
      done();
    });
  });

  it("Read one. SurveySrv/Surveys/" + surveyId2, function(done) {
    // ** Get a token.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/Surveys/" + surveyId2,
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("SurveySrv/Survey/" + surveyId2 + " | Body:", body);
      }
      expect(body.Code).toBe(0, "SurveySrv/Surveys/" + surveyId2 + ": Code should be 0.");
      //expect(body.Value).not.toBeUndefined();

      //console.log("Body: ", body);
      done();
    });
  });
});

describe("Survey.specs  Create and Update | ", function() {
  // ** Init
  var surveyTypeId;
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody.Code).toBe(0);

      request.post({
        url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
        form: {
          Name: 'Survey Type Test'
        },
        json: true
      }, function(error, response, body) {
        expect(error).toBeNull();
        expect(body.Code).toBe(0, "Error on creating surveyType | Body: ", body);
        if (body.Code === 0) {
          surveyTypeId = body.Value.SurveyTypeID;
        }
        done();
      });
    });
  });
  // **   END BEFORE EACH.
  it("Create. SurveySrv/Surveys", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/Surveys",
      form: {
        SurveyTypeId: surveyTypeId,
        Version: '1.1'
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0, "SurveySrv/Surveys | Body: " + body);
      done();
    });
  });
  it("Update.  SurveySrv/Surveys/" + surveyTypeId, function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/Surveys/" + surveyTypeId,
      form: {
        SurveyTypeId: surveyTypeId,
        Version: '1.12'
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0, "SurveySrv/Surveys/" + surveyTypeId + " | Body: " + body);
      done();
    });
  });
});

describe("Retrieve a survey type by a SurveyID | ", function() {
  // ** Init
  var surveyTypeId;
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

  it("Get the survey type from a survey.", function(done) {
    dataSource.createSurveyType(request, "Get by SurveyID test", function(surveyTypeObj) {
      surveyTypeId = surveyTypeObj.SurveyTypeID;
      dataSource.createSurvey(request, surveyTypeId, "Ver 10", function(surveyObj) {
        // ** Start test
        request.get({
          url: config.SseServicesCmsCORS + "SurveySrv/Surveys/" + surveyObj.SurveyID + "/SurveyType",
          json: true
        }, function(error, response, body) {
          expect(error).toBeNull();
          expect(body).not.toBeNull();
          expect(body.Value).toBeDefined();
          expect(typeof(body.Value)).toBe("object", "Value should be an object");
          expect(Array.isArray(body.Value)).toBe(false, "Value should be an object, not an array.");
          expect(body.Code).toBe(0, "Error SurveySrv/Surveys/" + surveyObj.SurveyID + "/SurveyType");
          if (body.Code !== 0) {
            console.log("Error Boby: ", body);
          }
          done();
        });
      });
    });
  });

  it("Get questions from a survey id.", function(done) {
    dataSource.createSurveyType(request, "Get by SurveyID Test", function(surveyTypeObj) {
      dataSource.createSurvey(request, surveyTypeObj.SurveyTypeID, "Ver 11", function(surveyObj) {
        dataSource.createQuestionMeaning(request, surveyTypeObj.SurveyTypeID, "Question Meaning test get Questions by survey id.", function(questionMeaningObj) {
          dataSource.createQuestion(request, surveyObj.SurveyID, questionMeaningObj.QuestionMeaningID, null, 12, null, function(questionObj) {
            request.get({
              url: config.SseServicesCmsCORS + "SurveySrv/Surveys/" + surveyObj.SurveyID + "/Questions",
              json: true
            }, function(error, response, body) {
              expect(error).toBeNull();
              expect(body).not.toBeNull();
              expect(body.Code).toBe(0, "Error SurveySrv/Surveys/" + surveyObj.SurveyID + "/Questions");
              expect(body.Value.length).toBe(1, "This should be a list of one");
              expect(body.Value[0].QuestionID).toBe(questionObj.QuestionID, "The ID's should match");
              if (body.Code !== 0) {
                console.log("Error Boby: ", body);
              }
              done();
            });
          });
        });
      });
    });
  });

  it("Get survey translations from a survey id.", function(done) {
    dataSource.createSurveyType(request, "Get by SurveyID Test", function(surveyTypeObj) {
      dataSource.createSurvey(request, surveyTypeObj.SurveyTypeID, "Ver 11", function(surveyObj) {
        dataSource.createSurveyTranslation(request, surveyObj.SurveyID, "en-us", function() {
          dataSource.createSurveyTranslation(request, surveyObj.SurveyID, "sp-us", function() {
            dataSource.createSurveyTranslation(request, surveyObj.SurveyID, "sp", function() {
              request.get({
                url: config.SseServicesCmsCORS + "SurveySrv/Surveys/" + surveyObj.SurveyID + "/SurveyTranslations",
                json: true
              }, function(error, response, body) {
                expect(error).toBeNull();
                expect(body).not.toBeNull();
                expect(body.Code).toBe(0, "Error SurveySrv/Surveys/" + surveyObj.SurveyID + "/SurveyTranslations");
                expect(body.Value.length).toBe(3, "This should be a list of three.");
                if (body.Code !== 0) {
                  console.log("Error Boby: ", body);
                }

                done();
              });
            });
          });
        });
      });
    });
  });
});