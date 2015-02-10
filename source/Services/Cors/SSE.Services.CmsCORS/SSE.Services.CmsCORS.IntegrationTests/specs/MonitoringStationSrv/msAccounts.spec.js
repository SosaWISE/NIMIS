/** msAccounts.spec.js */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var modelDef = require("../modelDefinitions");
var dataSource = require("../dataSource");
var sessionId;
var authBody;

describe("msAccountLeadInfo.spec tests | ", function() {
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

  it("Onbourd System.", function(done) {
    // ** Create a premise address.
    dataSource.verifyAddress(request, {
      DealerId: 5001,
      SeasonId: 1,
      TeamLocationId: 1,
      SalesRepId: 'SOSA001',
      StreetAddress: '1128 E 3300 S',
      City: 'Salt Lake City',
      StateId: 'UT',
      PostalCode: '84106',
      PhoneNumber: '(801) 486-8251'

    }, function(addrValue) {
      // console.log("Address Value: ", addrValue);
      // ** Run Credit and create lead.
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
        FirstName: 'Demitri', // *REQUIRED
        MiddleName: 'S', // *OPTIONAL
        LastName: 'Mionovick', // *REQUIRED
        Suffix: 'II', // *OPTIONAL
        Gender: 'Male', // *REQUIORED
        SSN: '333-33-3333', // *CONDITIONAL:  Either SSN or DOB has to be present or both.
        DOB: '12/14/1968', // *CONDITIONAL:  Either SSN or DOB has to be present or both.
        Dl: '2342423423', // *OPTIONAL
        DlStateId: 'CA', // *OPTIONAL
        Email: 'Mionovicky@sam.com', // *REQUIORED
        PhoneHome: '(801) 654-9877', // *OPTIONAL
        PhoneWork: null, // *OPTIONAL
        PhoneMobile: '(909) 987-6790' // *OPTIONAL
      }, function(leadValue) {
        // console.log("Run Credit: ", leadValue);
        dataSource.createMsAccount(request, {
          leadId: leadValue.LeadId
        }, function(msValue) {
          // console.log("msValue: ", msValue);
          /** Create EMC's. */
          dataSource.createEmc(request, {
            CustomerId: msValue.CustomerId,
            AccountId: msValue.AccountID,
            RelationshipId: 86,
            OrderNumber: 1,
            Allergies: 'Pepper, pollen, sleeping',
            MedicalConditions: "Diabeties, High blood pressure",
            HasKey: true,
            DOB: '12/11/1933',
            FirstName: 'Justina',
            LastName: 'Rowe',
            Email: 'justina.rowe@give.com',
            'Password': '23432kdjfs',
            Phone1: '8889097878',
            Phone1TypeId: 4
          }, function(emcValue) {
            // console.log("EMC 1: ", emcValue);
            modelDef.msEmergencyContact(emcValue);
            /** Create EMC 2. */
            dataSource.createEmc(request, {
              CustomerId: msValue.CustomerId,
              AccountId: msValue.AccountID,
              RelationshipId: 87,
              OrderNumber: 2,
              Allergies: 'Pepper, pollen, sleeping',
              MedicalConditions: "Diabeties, High blood pressure",
              HasKey: false,
              DOB: '12/11/1933',
              FirstName: 'Justina',
              LastName: 'Rowe',
              Email: 'justina.rowe@give.com',
              'Password': '23432kdjfs',
              Phone1: '8889097878',
              Phone1TypeId: 3
            }, function(emcValue2) {
              // console.log("EMC 2: ", emcValue);
              modelDef.msEmergencyContact(emcValue2);

              /** Add equipment to the system. */
              dataSource.equipmentLookupByPartnumber(request, {
                AccountID: msValue.AccountID,
                PartNumber: 'GEC-60362N103195'
              }, function(partValue1) {
                console.log("Part1: ", partValue1);
                expect(partValue1.ItemId).toBe('EQPM_INVT112');

                dataSource.equipmentLookupByPartnumber(request, {
                  AccountID: msValue.AccountID,
                  PartNumber: 'GEC-6065295'
                }, function(partValue2) {
                  console.log("Part2: ", partValue2);
                  expect(partValue2.ItemId).toBe('EQPM_INVT111');

                  /** Generate Industry Account for Primary System. */
                  /** Generate Industry Account for Secondary System. */

                  /** Onboard Account */
                  request.post({
                    url: config.SseServicesCmsCORS + 'MonitoringStationSrv/MsAccounts/',
                    form: {
                      AccountId: msValue.AccountID
                    },
                    json: true
                  }, function(error, response, body) {
                    // console.log('Body of Accounts Submission: ', body);
                    // console.log('Error: ', error);
                    expect(error).toBeNull("The error object should be null.");
                    expect(body).toBeDefined("Body is not defined");
                    expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
                    expect(body.Value).not.toBeNull("No value was returned.");

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

  it("Get Signal Hisotry.", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + 'MonitoringStationSrv/MsAccounts/130532/SignalHistory',
      json: true
    }, function(error, response, body) {
      console.log('Signal History body: ', body);
      console.log('Error: ', error);
      expect(error).toBeNull("The error object should be null.");
      expect(body).toBeDefined("Body is not defined");
      expect(body.Code).toBe(0, "The response returned an error: " + body.Message);
      expect(body.Value).not.toBeNull("No value was returned.");

      done();
    });
  });
});