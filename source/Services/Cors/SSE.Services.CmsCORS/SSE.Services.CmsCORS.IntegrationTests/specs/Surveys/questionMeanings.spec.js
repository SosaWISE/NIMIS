// ** Question meaning calls
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var sessionId;
var authBody;

describe("QuestionMeanings.spec Create and Update | ", function() {
  // ** Initialize
  var surveyTypeId1, surveyTypeId2, qmID;
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody.Code).toBe(0);

      // ** START Create Survey Type 1
      request.post({
        url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
        form: {
          SurveyTypeID: 0,
          Name: 'Survey Type Test QuestionMeanings 1 C-U'
        },
        json: true
      }, function(error, response, body) {
        expect(error).toBeNull();
        expect(body.Code).toBe(0);
        surveyTypeId1 = body.Value.SurveyTypeID;

        request.post({
          url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
          form: {
            SurveyTypeID: 0,
            Name: 'Survey Type Test QuestionMeanings 2 C-U'
          },
          json: true
        }, function(error, response, body) {
          expect(error).toBeNull();
          expect(body.Code).toBe(0);
          surveyTypeId2 = body.Value.SurveyTypeID;

          done();
        });
      });
      // **   END Create Survey Type 1
    });
  });
  // **   END BEFORE EACH.
  it("Create ", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings",
      form: {
        SurveyTypeId: surveyTypeId1,
        Name: "Create Test..."
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0);
      qmID = body.Value.QuestionMeaningID;
      done();
    });
  });

  it("Update ", function(done) {
    var newName = "Create Test... Changes";
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings/" + qmID,
      form: {
        SurveyTypeId: surveyTypeId1,
        Name: newName
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0);
      expect(body.Value.Name).toEqual(newName);
      done();
    });
  });
});
describe("QuestionMeanings.spec tests", function() {
  // ** Initialize
  var surveyTypeId1, surveyTypeId2;
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody.Code).toBe(0);

      // ** START Create Survey Type 1
      request.post({
        url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
        form: {
          SurveyTypeID: 0,
          Name: 'Survey Type Test QuestionMeanings 1 Get'
        },
        json: true
      }, function(error, response, body) {
        expect(error).toBeNull();
        expect(body.Code).toBe(0);
        surveyTypeId1 = body.Value.SurveyTypeID;
        // ** START Create Survey Type 2
        request.post({
          url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
          form: {
            SurveyTypeID: 0,
            Name: 'Survey Type Test QuestionMeanings 2'
          },
          json: true
        }, function(error, response, body) {
          expect(function() {
            expect(error).toBeNull();
            expect(body.Code).toBe(0);
            surveyTypeId2 = body.Value.SurveyTypeID;

            done();
          }).not.toThrow();
        });
        // **   END Create Survey Type 2
      });
      // **   END Create Survey Type 1
    });
  });
  // **   END BEFORE EACH.

  it("Read all. SurveySrv/QuestionMeanings/", function(done) {
    // ** Get all tokens.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings/",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      //console.log("surveysrv/QuestionMeanings/ | Body: ", body);
      done();
    });
  });

  it("Get one. SurveySrv/QuestionMeanings/" + surveyTypeId1, function(done) {
    // ** Get a token.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings/" + surveyTypeId1,
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("surveysrv/QuestionMeanings/" + surveyTypeId1 + " | Body:", body);
      }
      expect(body.Code).toBe(0, "surveysrv/QuestionMeanings/" + surveyTypeId1 + ": Code should be 0.");
      //expect(body.Value).not.toBeUndefined();

      //console.log("Body: ", body);
      //console.log("QuestionMeanings: ", body.Value);
      done();
    });
  });

  it("Get one. SurveySrv/QuestionMeanings/" + surveyTypeId2, function(done) {
    // ** Get a token.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings/" + surveyTypeId2,
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("surveysrv/QuestionMeanings/" + surveyTypeId2 + " | Body:", body);
      }
      expect(body.Code).toBe(0, "surveysrv/QuestionMeanings/" + surveyTypeId2 + ": Code should be 0.");
      //expect(body.Value).not.toBeUndefined();

      //console.log("Body: ", body);
      //console.log("QuestionMeanings: ", body.Value);
      done();
    });
  });

  it("Get many. SurveySrv/QuestionMeanings/10000/QuestionMeaningTokenMaps", function(done) {
    // ** Get TokenMap
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/QuestionMeanings/10000/QuestionMeaningTokenMaps",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("Message: ", body.Message);
      }
      expect(body.Code).toBe(0, "surveysrv/QuestionMeanings/10000/QuestionMeaningTokenMaps: Code should be 0.");
      //expect(body.Value).not.toBeUndefined();

      expect(body.Value).not.toBeUndefined();
      expect(body.Value).not.toBeNull();
      if (body.Value === null || body.Value === undefined) {
        console.log("QuestionMeaningTokenMaps Value: ", body.Value);
      }
      done();
    });
  });
});