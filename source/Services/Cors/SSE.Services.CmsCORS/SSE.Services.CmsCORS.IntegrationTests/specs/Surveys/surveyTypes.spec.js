/** Survey Types */
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
  it("Read all, SurveySrv/SurveyTypes.", function(done) {
    // ** Get all tokens.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body.Code).toBe(0, "SurveySrv/SurveyTypes GET | Body: ", body);
      //console.log("SurveySrv/SurveyTypes GET | Body: ", body);
      done();
    });
  });

  it("Read one. SurveySrv/SurveyTypes/1000", function(done) {
    // ** Get a token.
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes/1000",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("SurveySrv/SurveyTypes/1000 | Body:", body);
      }
      expect(body.Code).toBe(0, "SurveySrv/SurveyTypes/1000: Code should be 0.");
      //expect(body.Value).not.toBeUndefined();

      //console.log("Body: ", body);
      done();
    });
  });

  it("Read one. SurveySrv/SurveyTypes/1000/QuestionMeanings", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes/1000/QuestionMeanings",
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      if (body.Code !== 0) {
        console.log("SurveySrv/SurveyTypes/1000/QuestionMeanings | Body:", body);
      }
      expect(body.Code).toBe(0, "surverysrv/SurveyTypes/1000/QuestionMeanings: Code should be 0");

      done();
    });
  });

  it("Read all. SurveySrv/SurveyTypes", function(done) {
    // ** Post   /surveyTypes &&  /surveyTypes/(SurveyTypeID)
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
      form: {
        SurveyTypeID: 0,
        Name: 'Survey Type Test'
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).not.toBeUndefined();
      if (body.Code !== 0) {
        console.log("Post SurveySrv/SurveyTypes | Body: ", body);
      }
      expect(body.Code).toBe(0, "Post SurveySrv/SurveyTypes: Code should be 0.");
      done();
    });
  });

});

describe("SurveyTypes.spec Testing Create and Updates | ", function() {
  // ** Initialize
  var surveyTypeID;
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
  it("Create one. SurveySrv/SurveyTypes", function(done) {
    var currentDate = new Date();
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes",
      form: {
        SurveyTypeID: 0,
        Name: 'S ' + currentDate.getDay() + "/" + currentDate.getMinutes() + "/" + currentDate.getSeconds()
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).not.toBeUndefined();
      if (body.Code !== 0) {
        console.log("Post SurveySrv/SurveyTypes | Body: ", body);
      }
      expect(body.Code).toBe(0, "Post SurveySrv/SurveyTypes: Code should be 0.");
      // ** Get Survey TypeID to do the Update.
      surveyTypeID = body.Value.SurveyTypeID;
      done();
    });
  });

  it("Update one.  SurveySrv/SurveyTypes/" + surveyTypeID, function(done) {
    var currentDate = new Date();
    request.post({
      url: config.SseServicesCmsCORS + "SurveySrv/SurveyTypes/" + surveyTypeID,
      form: {
        Name: 'U ' + currentDate.getDay() + "/" + currentDate.getMinutes() + "/" + currentDate.getSeconds()
      },
      json: true
    }, function(error, response, body) {
      expect(error).toBeNull();
      expect(body).not.toBeNull();
      expect(body.Code).not.toBeUndefined();
      if (body.Code !== 0) {
        console.log("Post SurveySrv/SurveyTypes/" + surveyTypeID + " | Body: ", body);
      }
      expect(body.Code).toBe(0, "Post SurveySrv/SurveyTypes/" + surveyTypeID + ": Code should be 0.");
      expect(body.Value.SurveyTypeID).toBe(surveyTypeID);
      // ** Get Survey TypeID to do the Update.
      surveyTypeID = body.Value.SurveyTypeID;
      done();
    });
  });
});