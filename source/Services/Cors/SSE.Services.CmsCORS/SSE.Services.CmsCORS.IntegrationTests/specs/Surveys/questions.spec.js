// ** Questions. 
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var sessionId;
var authBody;

describe("Questions.spec tests | ", function() {
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

  it("Read all. SurveySrv/Questions/", function(done) {
    // ** Get all tokens.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/Questions/",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0);
      done();
    });
  });

  it("Read one.  SurveySrv/Questions/1000", function(done) {
    // ** Get a token.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/Questions/1000",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("SurveySrv/Questions/1000 | Body:", body);
      }
      expect(body.Code).toBe(0, "SurveySrv/Questions/1000: Code should be 0.");
      //expect(body.Value).not.toBeUndefined();

      //console.log("Body: ", body);
      //console.log("QuestionMeanings: ", body.Value);
      done();
    });
  });

  it("Read many.  SurveySrv/Questions/1000/QuestionPossibleAnswerMaps", function(done) {
    // ** Get /questions/(QuestionID)/questionPossibleAnswerMaps
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/Questions/1000/QuestionPossibleAnswerMaps",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("SurverySrv/Questions/1000/QuestionPossibleAnswerMaps | Body:", body);
      }
      expect(body.Code).toBe(0, "SurverySrv/Questions/1000/QuestionPossibleAnswerMaps: Code should be 0");
      done();
    });
  });
});

describe("Question.spec Test Create and Update | ", function() {
  // ** Initialize. 
  var surveyId, surveyTypeId, questionMeaningId, questionID;
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody.Code).toBe(0);

      // ** Create environment
      // ** ** Create a Survey Type
      request.post({
        url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
        form: {
          Name: 'Some Test for create question'
        },
        json: true
      }, function(error, response, body) {
        expect(error).toBeNull("There was an error creating Survey Type.");
        expect(body.Code).toBe(0, "Error : " + body.Message);
        if (body.Code === 0) {
          surveyTypeId = body.Value.SurveyTypeID;
          //console.log("Made it to the SurveyTypeID..... SurveyTypeID: " + surveyTypeId);

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
              //console.log("Made it to the SurveyID..... SurveyID: " + surveyId, body);

              // ** START CREATE Question Meaning
              request.post({
                url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings",
                form: {
                  SurveyTypeId: surveyTypeId,
                  Name: "Question meaning for a test"
                },
                json: true
              }, function(error, response, body) {
                expect(error).toBeNull("Error was not null.");
                expect(body.Code).toBe(0, "Error : " + body.Message);
                if (body.Code === 0) {
                  questionMeaningId = body.Value.QuestionMeaningID;
                  //console.log("Made it to the QuestionMeaningID..... QuestionMeaningID: " + questionMeaningId);
                }
                // ** Finish
                done();
              });
              // **   END CREATE Question Meaning
            }
          });
          // **   END Create Survey
        }
      });
    });
  });
  // **   END BEFORE EACH.

  it("Create Question.  SurveySrv/Questions", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/Questions",
      form: {
        SurveyId: surveyId,
        QuestionMeaningId: questionMeaningId,
        GroupOrder: 1
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("SurverySrv/Questions | Body:", body);
      }
      expect(body.Code).toBe(0, "SurverySrv/Questions: Code should be 0");

      // ** Get Question ID
      questionID = body.Value.QuestionID;

      done();
    });
  });

  it("Update Question.  SurveySrv/Questions/" + questionID, function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/Questions/" + questionID,
      form: {
        SurveyId: surveyId,
        QuestionMeaningId: questionMeaningId,
        GroupOrder: 5
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("SurverySrv/Questions/" + questionID + " | Body:", body);
      }
      expect(body.Code).toBe(0, "SurverySrv/Questions/1000: Code should be 0");
      done();
    });
  });
});