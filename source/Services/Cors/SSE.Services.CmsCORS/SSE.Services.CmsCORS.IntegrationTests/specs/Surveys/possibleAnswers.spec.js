/** Possible Answers. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("Possible Answers Test | ", function() {
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
  it("Read all. SurveySrv/PossibleAnswers/", function(done) {
    // ** Create possible answers to test.
    dataSource.createPossibleAnswers(request, "Get Test 1", function () {
      dataSource.createPossibleAnswers(request, "Get Test 2", function (){
        // ** Get all tokens.
        request.get({
          url: config.SseServicesCmsCORS + "SurveySrv/PossibleAnswers/",
          json: true
        }, function(error, response, body) {
          expect(error).toBeNull();
          expect(body.Code).toBe(0, "SurveySrv/PossibleAnswers/");
          done();
          /*if (body.Code !== 0) {
            console.log("SurveySrv/PossibleAnswers/ | BODY: ", body);
          }*/
        });
      });
    });
  });

  it("Read one.  SurveySrv/PossibleAnswers/{id}", function(done) {
    // ** Create possible answers to test.
    dataSource.createPossibleAnswers(request, "Get Test 1", function (possibleAns1) {
      // ** Get a token.
      request.get({
        url: config.SseServicesCmsCORS + "SurveySrv/PossibleAnswers/" + possibleAns1.PossibleAnswerID,
        json: true
      }, function(error, response, body) {
        expect(error).toBeNull();
        expect(body.Code).toBe(0);
        //expect(body.Value).not.toBeUndefined();

        // console.log("PossibleAnswers Body: ", body);
        expect(body.Value).not.toBeNull();
        //console.log("PossibleAnswers: ", body.Value);
        done();
      });
    });
  });
});