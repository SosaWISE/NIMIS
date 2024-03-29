/** EquipmentAdd.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var dataSource = require("../dataSource");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("EquipmentAdd.spec tests. | ", function() {
  // ** START BEFORE EACH.
  beforeEach(function(done) {
    auth.authScript(request, function(aSessionId, aBody) {
      sessionId = aSessionId;
      authBody = aBody;
      expect(authBody).toBeDefined("AuthBody is not defined.");
      expect(authBody.Code).toBe(0);
      done();
    });
  });
  // **   END BEFORE EACH.

  /** Initialize. */
  var equipment1 = 'GEC-TX1510011'; // DOOR/WINDOW

  /** Lookup by Part Number. */
  it("Validate getting Equipment Add", function(done) {
    dataSource.createMsAccountScratch(request, {
      DealerId: 5001,
      SeasonId: 1,
      TeamLocationId: 1,
      SalesRepId: 'SOSA001',
      StreetAddress: '1128 E 3300 S',
      City: 'Salt Lake City',
      StateId: 'UT',
      PostalCode: '84106',
      PhoneNumber: '(801) 486-8251',
      LocalizationId: 'en-us', // *REQUIRED:  This is the default language that they speak.
      LeadSourceId: '7', // *REQUIRED:  This is the web.
      LeadDispositionId: 9, // *REQUIRED:  This is in what state is the lead.  9: Means "Ran Credit"
      Salutation: 'Mr.', // *OPTIONAL
      FirstName: 'Kirby', // *REQUIRED
      MiddleName: null, // *OPTIONAL
      LastName: 'Vacuum', // *REQUIRED
      Suffix: 'II', // *OPTIONAL
      Gender: 'Male', // *REQUIORED
      SSN: '333-33-3333', // *CONDITIONAL:  Either SSN or DOB has to be present or both.
      DOB: '12/14/1968', // *CONDITIONAL:  Either SSN or DOB has to be present or both.
      Dl: '2342423423', // *OPTIONAL
      DlStateId: 'UT', // *OPTIONAL
      Email: 'sammy@sam.com', // *REQUIORED
      PhoneHome: '(801) 654-9877', // *OPTIONAL
      PhoneWork: null, // *OPTIONAL
      PhoneMobile: '(909) 987-6790' // *OPTIONAL

    }, function(accountValue) {
      // console.log("Account Value: ", accountValue);
      dataSource.equipmentLookupByPartnumber(request, {
        PartNumber: equipment1,
        AccountID: accountValue.AccountID,
        TechID: 'SAMMY001'
      }, function (zoneItem){
        // console.log("Zone Item: ", zoneItem);
      request.post({
          url: config.SseServicesCmsCORS + "MsAccountSetupSrv/Equipments/",
            form:{
              AccountZoneAssignmentID: zoneItem.AccountZoneAssignmentID,
              AccountEquipmentID: zoneItem.AccountEquipmentID,
              AccountId: zoneItem.AccountId,
              ItemId: zoneItem.ItemId,
              ItemDesc: zoneItem.ItemDesc,
              // Zone: zoneItem.Zone,
              Zone: '099',
              AccountZoneTypeId: zoneItem.AccountZoneTypeId,
              EquipmentLocationId: 10, //Game Room
              GPEmployeeId: zoneItem.GPEmployeeId,
              AccountEquipmentUpgradeTypeId: zoneItem.AccountEquipmentUpgradeTypeId,
              Price: zoneItem.Price,
              IsExistingWiring: zoneItem.IsExistingWiring,
              IsExisting: zoneItem.IsExisting,
              IsMainPanel: zoneItem.IsMainPanel
            },
          json: true
        },
        function(error, response, body) {
          // console.log("Body: ", body);
          expect(error).toBeNull();
          expect(body).not.toBeNull();
          expect(body.Code).toBe(0, "Error on call.  Message: " + body.Message);

          /** Check for modified items. */
          expect(body.Value.EquipmentLocationId).toBe(10, "EquipmentLocationId did not update correctly.");
          expect(body.Value.Zone).toBe('099', "Zone did not update correctly.");

          modelDef.msAccountEquipment(body.Value);
          done();
        });

      });
    });
  });
});