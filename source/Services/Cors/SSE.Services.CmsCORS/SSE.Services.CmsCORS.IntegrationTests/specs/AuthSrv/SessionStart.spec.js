var request = require("request").defaults({
  jar: true
});
//var auth = require("../auth");
var config = require("../config");
var sessionId;
//var authBody;

describe("Get Sales Rep Info using Company ID and SessionId.", function() {
  // ** START BEFORE EACH.
  // beforeEach(function(done) {
  //   auth.authScript(request, function(aSessionId, aBody) {
  //     sessionId = aSessionId;
  //     authBody = aBody;
  //     expect(authBody.Code).toBe(0);
  //     done();
  //   });
  // });

  it("Test SalesRepRead with error", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "AuthSrv/SalesRepRead",
      form: {
        CompanyID: 'SOSA001',
        SessionId: 123232
      },
      json: true
    }, function(error, response, body) {
      expect(body.Code).toBe(10040); // This means that the user is not authenticated.
      done();
    });
  });
});

describe("Get Session Info first.", function() {
  var d = new Date();
  it("Test create a new session.", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "AuthSrv/SessionStart",
      form: {
        AppToken: 'SSE_CMS_CORS',
        TimeZoneOffset: d.getTimezoneOffset()
      },
      json: true
    }, function(error, response, body) {
      if (error !== null) {
        console.log("Error: ", error);
      }
      expect(error).toBeNull();
      expect(body.Code).toBe(0);
      expect(body.Value.SessionId).not.toBeNull();
      expect(body.Value.AuthUser).toBeNull();

      // ** Get the session ID value.
      sessionId = body.Value.SessionId;

      // ** Print values.
      //console.log(body.Value, body.Value.SessionId);
      done();
    });
  });
});

describe("Authenticate User.", function() {
  it("Successfull login test.", function(done) {
    // ** Print the session ID.
    //console.log("Print SessionID: ", sessionId);

    // ** Execute post.
    request.post({
      url: config.SseServicesCmsCORS + "AuthSrv/UserAuth",
      form: {
        Username: 'PrivetteANDY',
        'Password': 'Freedom!SOS',
        SessionID: sessionId
      },
      json: true
    }, function(error, response, body) {
      // ** Check for error
      if (error !== null) {
        console.log("Error Object: ", error);
      }

      expect(body.Code).toBe(0);

      // ** Print body.
      //console.log("Authentication Body:", body);

      /** After authenticating check to see if we recall the session if it will auto Authenticate it. */
      request.post({
        url: config.SseServicesCmsCORS + "AuthSrv/SessionStart",
        form: {
          AppToken: 'SSE_CMS_CORS'
        },
        json: true
      }, function(error, response, body) {
        if (error !== null) {
          console.log("Error: ", error);
        }
        expect(error).toBeNull();
        expect(body.Code).toBe(0);
        expect(body.Value.SessionId).not.toBeNull();
        expect(body.Value.AuthUser).not.toBeNull("This should have returned a AuthUser object. ");

        /** These are the fields that are returned from the AuthUser. */
        expect(body.Value.AuthUser.UserID).not.toBeNull("The UserID must allways be present.");
        expect(body.Value.AuthUser.Ssid).toBeDefined("This can be null but it must be defined.");
        expect(body.Value.AuthUser.SessionID).not.toBeNull("Session ID must always be present.");
        expect(body.Value.AuthUser.Username).not.toBeNull("Username must always be present.");
        expect(body.Value.AuthUser.Firstname).not.toBeNull("Firstname must always be present.");
        expect(body.Value.AuthUser.Lastname).not.toBeNull("Lastname must always be present.");
        expect(body.Value.AuthUser.GPEmployeeID).not.toBeNull("GPEmployeeID must always be present.");

        done();
      });

    });
  });
});