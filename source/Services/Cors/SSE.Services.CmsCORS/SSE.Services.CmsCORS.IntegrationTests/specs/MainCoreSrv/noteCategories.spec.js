/** noteCategories.spec.js. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var sessionId;
var authBody;

/** This is a brittle test because we can't create categories and/or departments. */

describe("NoteCategories.spec tests | ", function() {
  // ** Initialize 
  var departmentId = 'DENTRY';

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

  it("Get Primary categories by department ID.", function(done) {
    // ** Get all tokens.
    request.get({
      url: config.SseServicesCmsCORS + "MainCoreSrv/NoteCategory1/" + departmentId + "/DepartmentID",
      json: true
    }, function(error, response, body) {
      //console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      done();
    });
  });

  //** Inititalize
  it("Get Secondary categories by primary cat ID.", function(done) {
    // ** Get all tokens.
    request.get({
      url: config.SseServicesCmsCORS + "MainCoreSrv/NoteCategory2/" + 2 + "/Category1Id",
      json: true
    }, function(error, response, body) {
      //console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      done();
    });
  });
});