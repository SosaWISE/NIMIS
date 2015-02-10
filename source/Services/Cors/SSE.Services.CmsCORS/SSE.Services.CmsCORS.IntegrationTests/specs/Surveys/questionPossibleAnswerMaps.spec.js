/** QuestionPossibleAnswerMaps spec. */
/** questionMeaningTokenMaps.spec */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
//var config = require("../config");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("QuestionPossibleAnswerMaps spec | ", function() {
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
  it("QuestionPossibleAnswerMaps ", function(done) {
    // ** Create Survey Type
    dataSource.createSurveyType(request, "PosAnswers Test type", function(surveyTypeObj) {
      // ** Create Survey
      dataSource.createSurvey(request, surveyTypeObj.SurveyTypeID, "PossibleTS", function(surveyObj) {
        // ** Create Question Meaning
        dataSource.createQuestionMeaning(request, surveyTypeObj.SurveyTypeID, "QM PosAns Test", function(questionMeaningObj) {
          // ** Create Question
          dataSource.createQuestion(request, surveyObj.SurveyID, questionMeaningObj.QuestionMeaningID, null, 1, null, function(questionObj) {
            // ** Possible Answers
            dataSource.createPossibleAnswers(request, "possibleAnswer Map test", function(possibleAnswerObj) {
              // ** Create the map
              var expands = true;
              dataSource.createQuestionPossibleAnswerMaps(request, questionObj.QuestionID, possibleAnswerObj.PossibleAnswerID, expands, function(objectMap) {
                expect(objectMap).not.toBeNull();
                expect(objectMap.Expands).toBe(true, "`Expands` should be set to `true`");
                // ** Update the map
                expands = false;
                dataSource.createQuestionPossibleAnswerMaps(request, objectMap.QuestionId, objectMap.PossibleAnswerId, expands, function(objectMap) {
                  expect(objectMap).not.toBeNull();
                  expect(objectMap.Expands).toBe(false, "`Expands` should be set to `false`");
                  done();
                });
              });
            });
          });
        });
      });
    });
  });
});
