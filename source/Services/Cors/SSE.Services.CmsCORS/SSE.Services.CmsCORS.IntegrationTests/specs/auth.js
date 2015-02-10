/** Authentication scripts. */
var config = require("./config");
module.exports.authScript = function(request, cb) {
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
    // ** Get the session ID value.
    var sessionId = body.Value.SessionId;

    // ** Print values.
    //console.log(body.Value, body.Value.SessionId);
    // ** Execute Authentication.
    request.post({
      url: config.SseServicesCmsCORS + "AuthSrv/UserAuth",
      form: {
        Username: 'DevUser',
        'Password': 'NexSense',
        SessionID: sessionId
      },
      json: true
    }, function(error, response, body) {
      // ** Check for error
      if (error !== null) {
        console.log("Error Object: ", error);
      }
      //console.log("Authentication Body:", body);
      cb(sessionId, body);
    });
  });
};