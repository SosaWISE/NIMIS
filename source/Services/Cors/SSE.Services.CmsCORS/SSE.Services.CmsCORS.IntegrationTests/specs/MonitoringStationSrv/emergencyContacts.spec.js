/** Emergency Contacts. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("emergencyContacts.spec tests | ", function() {
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

  /**
	These are brittle tests */
  it("Create EMC", function(done) {
    dataSource.verifyAddress(request, {
      DealerId: 5001,
      SeasonId: 1,
      TeamLocationId: 1,
      SalesRepId: 'SOSA002',
      StreetAddress: '794 E 3950 N',
      City: 'Provo',
      StateId: 'UT',
      PostalCode: '84604',
      PhoneNumber: '(801) 224-2331'

    }, function(addrValue) {
      dataSource.runCredit(request, {
        AddressID: addrValue.AddressID,
        DealerId: 5000, // *REQUIRED:  This is which dealer is getting credit for the lead.  5000: Is NexSense.
        TeamLocationId: 1, // *REQUIRED:  This is the office that credit is being ran from.  1: Would be default which means corporate.  For summer sales we would need to pass the correct office.
        SeasonId: 1, // *REQUIRED:  This is the season that it was ran.  1: Would be the default for year round.
        SalesRepId: 'SOSA001', // *REQUIRED:  This is the Company ID of a rep.
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
      }, function(leadValue) {
        dataSource.createMsAccount(request, {
          leadId: leadValue.LeadId
        }, function(msValue) {
          request.post({
            url: config.SseServicesCmsCORS + "MsAccountSetupSrv/EmergencyContacts",
            form: {
              // CustomerId: 100186,
              AccountId: msValue.AccountID,
              RelationshipId: 103, // In-law
              OrderNumber: 1,
              Allergies: 'Alergic to integration tests.', // Not Required
              MedicalConditions: 'Well the condition is serious.', // Not Required
              HasKey: true,
              DOB: '12/14/1965',
              Prefix: 'Mr.', // Not Required
              FirstName: 'Pit',
              MiddleName: 'Efraim', // Not Required
              LastName: 'Bull',
              Postfix: 'III', // Not Required
              Email: 'email@email.com', // Not Required
              'Password': 'Sammy123', // Not Required
              Phone1: '8886549877',
              Phone1TypeId: 4, // Home.
              /** Values below here ia not required. */
              Phone2: '8086326541',
              Phone2TypeId: 5,
              Phone3: '2016546544',
              Phone3TypeId: 6,
              Comment1: 'This is a comment and not a required field.'
            },
            json: true
          }, function(error, response, body) {
            // console.log("Body: ", body);
            expect(error).toBeNull();
            expect(body.Code).toBe(0, "Code did not return a successfull code.");

            /** Data fields validation. */
            var item = body.Value;

            modelDef.msEmergencyContact(item);

            done();
          });
        });
      });
    });
  });

  it("Update EMC", function(done) {
    dataSource.verifyAddress(request, {
      DealerId: 5001,
      SeasonId: 1,
      TeamLocationId: 1,
      SalesRepId: 'SOSA002',
      StreetAddress: '794 E 3950 N',
      City: 'Provo',
      StateId: 'UT',
      PostalCode: '84604',
      PhoneNumber: '(801) 224-2331'
    }, function(addrValue) {
      dataSource.runCredit(request, {
        AddressID: addrValue.AddressID,
        DealerId: 5000, // *REQUIRED:  This is which dealer is getting credit for the lead.  5000: Is NexSense.
        TeamLocationId: 1, // *REQUIRED:  This is the office that credit is being ran from.  1: Would be default which means corporate.  For summer sales we would need to pass the correct office.
        SeasonId: 1, // *REQUIRED:  This is the season that it was ran.  1: Would be the default for year round.
        SalesRepId: 'SOSA001', // *REQUIRED:  This is the Company ID of a rep.
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
      }, function(leadValue) {
        dataSource.createMsAccount(request, {
          leadId: leadValue.LeadId
        }, function(msValue) {
          dataSource.createEmc(request, {
            // CustomerId: 100186,
            AccountId: 100185,
            RelationshipId: 103, // In-law
            OrderNumber: 1,
            Allergies: 'Alergic to integration tests.', // Not Required
            MedicalConditions: 'Well the condition is serious.', // Not Required
            HasKey: true,
            DOB: '12/14/1965',
            Prefix: 'Mr.', // Not Required
            FirstName: 'Pit',
            MiddleName: 'Efraim', // Not Required
            LastName: 'Bull',
            Postfix: 'III', // Not Required
            Email: 'email@email.com', // Not Required
            'Password': 'Sammy123', // Not Required
            Phone1: '8886549877',
            Phone1TypeId: 4, // Home.
            /** Values below here ia not required. */
            Phone2: '8086326541',
            Phone2TypeId: 5,
            Phone3: '2016546544',
            Phone3TypeId: 6,
            Comment1: 'This is a comment and not a required field.'
          }, function(resp) {
            modelDef.msEmergencyContact(resp);
            /** Update the emc here. */
            request.post({
              url: config.SseServicesCmsCORS + "MsAccountSetupSrv/EmergencyContacts/" + resp.EmergencyContactID,
              form: {
                //CustomerId: 100186,
                AccountId: msValue.AccountID,
                RelationshipId: 103, // In-law
                OrderNumber: 1,
                Allergies: 'Alergic to integration tests.', // Not Required
                MedicalConditions: 'Well the condition is serious.', // Not Required
                HasKey: true,
                DOB: '12/14/1965',
                Prefix: 'Mr.', // Not Required
                FirstName: '000',
                MiddleName: '000000', // Not Required
                LastName: '0000',
                Postfix: 'III', // Not Required
                Email: '00000@00000.com', // Not Required
                'Password': 'Sammy000', // Not Required
                Phone1: '0000000000',
                Phone1TypeId: 4, // Home.
                /** Values below here ia not required. */
                Phone2: '8086326541',
                Phone2TypeId: 5,
                Phone3: '2016546544',
                Phone3TypeId: 6,
                Comment1: '00000000000000000000000000000000000000.'
              },
              json: true
            }, function(error, response, body) {
              // console.log("Body: ", body);
              expect(error).toBeNull();
              expect(body.Code).toBe(0, "Code did not return a successfull code.");

              /** Check the data that was updated. */
              var emc = body.Value;
              expect(emc.EmergencyContactID).toBe(resp.EmergencyContactID, "This should be an update not a new EMC.");
              modelDef.msEmergencyContact(emc);
              expect(emc.FirstName).toBe("000", "Sorry FirstName did not update correctly.");
              expect(emc.MiddleName).toBe("000000", "Sorry MiddleName did not update correctly.");
              expect(emc.LastName).toBe("0000", "Sorry LastName did not update correctly.");
              expect(emc.Email).toBe("00000@00000.com", "Sorry Email did not update correctly.");
              expect(emc.Password).toBe("Sammy000", "Sorry Password did not update correctly.");
              expect(emc.Phone1).toBe("0000000000", "Sorry Phone1 did not update correctly.");

              /** Delete Contact. */
              request.del({
                url: config.SseServicesCmsCORS + "MsAccountSetupSrv/EmergencyContacts/" + resp.EmergencyContactID,
                json: true
              }, function(error, response, body) {
                // console.log("Body: ", body);
                expect(error).toBeNull();
                expect(body.Code).toBe(0, "Code did not return a successfull code.");

                /** To test the result we need to do a read. */
                request.get({
                  url: config.SseServicesCmsCORS + "MsAccountSetupSrv/EmergencyContacts/" + resp.EmergencyContactID,
                  json: true
                }, function(error, response, body) {
                  // console.log("Body: ", body);
                  expect(error).toBeNull();
                  expect(body.Code).toBe(30100, "Code was to be returned with a 30100 number.");

                  done();
                });
              });
            });
          });
        });
      });
    });
  });

  it("Delete EMC", function(done) {
    dataSource.verifyAddress(request, {
      DealerId: 5001,
      SeasonId: 1,
      TeamLocationId: 1,
      SalesRepId: 'SOSA002',
      StreetAddress: '794 E 3950 N',
      City: 'Provo',
      StateId: 'UT',
      PostalCode: '84604',
      PhoneNumber: '(801) 224-2331'
    }, function(addrValue) {
      dataSource.runCredit(request, {
        AddressID: addrValue.AddressID,
        DealerId: 5000, // *REQUIRED:  This is which dealer is getting credit for the lead.  5000: Is NexSense.
        TeamLocationId: 1, // *REQUIRED:  This is the office that credit is being ran from.  1: Would be default which means corporate.  For summer sales we would need to pass the correct office.
        SeasonId: 1, // *REQUIRED:  This is the season that it was ran.  1: Would be the default for year round.
        SalesRepId: 'SOSA001', // *REQUIRED:  This is the Company ID of a rep.
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
      }, function(leadValue) {
        dataSource.createMsAccount(request, {
          leadId: leadValue.LeadId
        }, function(msValue) {
          dataSource.createEmc(request, {
            //CustomerId: 100186,
            AccountId: msValue.AccountID,
            RelationshipId: 103, // In-law
            OrderNumber: 1,
            Allergies: 'Alergic to integration tests.', // Not Required
            MedicalConditions: 'Well the condition is serious.', // Not Required
            HasKey: true,
            DOB: '12/14/1965',
            Prefix: 'Mr.', // Not Required
            FirstName: 'Pit',
            MiddleName: 'Efraim', // Not Required
            LastName: 'Bull',
            Postfix: 'III', // Not Required
            Email: 'email@email.com', // Not Required
            'Password': 'Sammy123', // Not Required
            Phone1: '8886549877',
            Phone1TypeId: 4, // Home.
            /** Values below here ia not required. */
            Phone2: '8086326541',
            Phone2TypeId: 5,
            Phone3: '2016546544',
            Phone3TypeId: 6,
            Comment1: 'This is a comment and not a required field.'
          }, function(resp) {
            modelDef.msEmergencyContact(resp);
            //console.log('RESULT: ', resp);
            // console.log("request.delete", Object.keys(request));
            /** Update the emc here. */
            request.del({
              url: config.SseServicesCmsCORS + "MsAccountSetupSrv/EmergencyContacts/" + resp.EmergencyContactID,
              json: true
            }, function(error, response, body) {
              // console.log("Body: ", body);
              expect(error).toBeNull();
              expect(body.Code).toBe(0, "Code did not return a successfull code.");

              done();
            });
          });
        });
      });
    });
  });
});