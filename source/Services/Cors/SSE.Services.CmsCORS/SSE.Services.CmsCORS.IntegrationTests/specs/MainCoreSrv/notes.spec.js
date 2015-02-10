/** notes.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("Notes.spec tests | ", function() {
  // ** Initialize
  var noteId;

  function assertsFx(error, body) {
    //console.log("Body: ", body);
    expect(error).toBeNull();
    expect(body.Code).toBe(0);
    expect(body.Value.length).toBeGreaterThan(0);
    // ** Check for the columns
    if (body.Value.length > 0) {
      body.Value.map(function(item) {
        modelDef.mcNoteFull(item);
      });
    }
  }

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

  it("Create a note, then update it", function(done) {
    // ** Get all tokens.
    request.post({
      url: config.SseServicesCmsCORS + "MainCoreSrv/Notes",
      form: {
        NoteTypeId: 'AUTO_GEN',
        CustomerMasterFileId: 3000023,
        CustomerId: 100172,
        LeadId: 1000046,
        NoteCategory1Id: 1,
        NoteCategory2Id: 1,
        Note: 'Account was accessed via a search from this integration test.'
      },
      json: true
    }, function(error, response, body) {
      //console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);
      // ** Get the noteid 
      noteId = body.Value.NoteID;
      //console.log("NoteID: ", noteId);

      // ** Update the note
      request.post({
        url: config.SseServicesCmsCORS + "MainCoreSrv/Notes/" + noteId,
        form: {
          NoteID: noteId,
          NoteTypeId: 'STANDARD',
          CustomerMasterFileId: 3000023,
          CustomerId: 100172,
          LeadId: 1000046,
          NoteCategory1Id: 3,
          NoteCategory2Id: 6,
          Note: 'Account was accessed via a search from this integration test.  And we updated the note'
        },
        json: true
      }, function(error, response, body) {
        //console.log("Body: ", body);
        expect(error).toBeNull();
        expect(body.Code).toBe(0);
        // ** Get the noteid 
        noteId = body.Value.NoteID;
        //console.log("NoteID: ", noteId);

        done();
      });
    });
  });

  it("Get notes for a given CMFID", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "maincoresrv/notes/" + 3000023 + "/cmfid?pageSize=10&pageNumber=1",
      // form: {
      //   CustomerMasterFileId: 3000023,
      //   PageSize: 10,
      //   PageNumber: 1
      // },
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      assertsFx(error, body);
      done();
    });
  });

  it("Get notes for a given CustomerId", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "MainCoreSrv/Notes/" + 100172 + "/CustomerId?pageSize=10&PageNumber=1",
      json: true
    }, function(error, response, body) {
      assertsFx(error, body);
      done();
    });
  });

  it("Get notes for a given LeadId", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "MainCoreSrv/Notes/" + 1000004 + "/LeadId?pageSize=10&PageNumber=1",
      json: true
    }, function(error, response, body) {
      assertsFx(error, body);
      done();
    });
  });

  it("Create a generic auto note.", function(done) {
    request.post({
      url: config.SseServicesCmsCORS + "MainCoreSrv/Notes",
      form: {
        NoteTypeId: "AUTO_GEN",
        CustomerMasterFileId: 3000001,
        Note: '',
        NoteCategory1Id: 1
      },
      json: true
    }, function(error, response, body) {
      //console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);
      done();
    });
  });
});