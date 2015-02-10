/** questionMeaningTokenMaps.spec */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
//var config = require("../config");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("QuestionMeaningTokenMaps spec | ", function() {
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
  it("QuestionMeaningTokenMaps ", function(done) {
    // ** Create Survey Type
    dataSource.createSurveyType(request, "QMMap Test type name", function(surveyTypeObj) {
      // ** Create Question Meaning
      dataSource.createQuestionMeaning(request, surveyTypeObj.SurveyTypeID, "Some QM Name", function(qmObject) {
        // ** Create token
        dataSource.createToken(request, "QMMap test token name", function(tokenObj) {
          // ** Create the map
          dataSource.createQuestionMeaningTokenMaps(request, qmObject.QuestionMeaningID, tokenObj.TokenID, function(objectMap) {
            expect(objectMap).not.toBeNull();
            done();
          });
        });
      });
    });
  });
});