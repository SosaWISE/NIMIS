/** invoice.spec.js. */
var request = require("request").defaults({
  jar: true
});
var auth = require("../auth");
var config = require("../config");
var dataSource = require("../dataSource");
var modelDef = require("../modelDefinitions");
var sessionId;
var authBody;

describe("Invoice.spec tests | ", function() {
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
  it("Get invoice by its id", function(done) {
    request.get({
      url: config.SseServicesCmsCORS + "InvoiceSrv/Invoices/10000021",
      json: true
    }, function(error, response, body) {
      // console.log("Body: ", body);
      expect(error).toBeNull();
      expect(body.Code).toBe(0);

      done();
    });
  });

  /** Create a minimal invoice for a first time install. */
  it("Create invoice minimal.", function(done) {
    // ** Init
    // var accountId = 100203;
    dataSource.verifyAddress(request, {
      DealerId: config.DealerId,
      SeasonId: config.SeasonId,
      TeamLocationId: config.TeamLocationId,
      SalesRepId: config.SalesRepId,
      StreetAddress: '722 E Technology Ave',
      City: 'OREM',
      StateId: 'UT',
      PostalCode: '84097',
      PhoneNumber: '3853758088'
    }, function(addrData) {
      // console.log("Addr Data: ", addrData);
      dataSource.runCredit(request, {
        AddressID: addrData.AddressID,
        DealerId: config.DealerId, // *REQUIRED:  This is which dealer is getting credit for the lead.  5000: Is NexSense.
        TeamLocationId: config.TeamLocationId, // *REQUIRED:  This is the office that credit is being ran from.  1: Would be default which means corporate.  For summer sales we would need to pass the correct office.
        SeasonId: config.SeasonId, // *REQUIRED:  This is the season that it was ran.  1: Would be the default for year round.
        SalesRepId: config.SalesRepId, // *REQUIRED:  This is the Company ID of a rep.
        LocalizationId: 'en-us', // *REQUIRED:  This is the default language that they speak.
        LeadSourceId: '7', // *REQUIRED:  This is the web.
        LeadDispositionId: 9, // *REQUIRED:  This is in what state is the lead.  9: Means "Ran Credit"
        FirstName: 'Kirby', // *REQUIRED
        LastName: 'Sales', // *REQUIRED
        Gender: 'Male', // *REQUIORED
        SSN: '333-33-3333', // *CONDITIONAL:  Either SSN or DOB has to be present or both.
        DOB: '12/14/1968', // *CONDITIONAL:  Either SSN or DOB has to be present or both.
        Dl: '2342423423', // *OPTIONAL
        DlStateId: 'UT', // *OPTIONAL
        Email: 'sammy@sam.com', // *REQUIORED
        PhoneHome: '(801) 654-9877', // *OPTIONAL
        PhoneWork: null, // *OPTIONAL
        PhoneMobile: '(909) 987-6790' // *OPTIONAL
      }, function(crData) {
        // console.log("CR Data: ", crData);
        dataSource.createMsAccount(request, {
          leadId: crData.LeadId
        }, function(acctData) {
          // console.log("Account Data: ", acctData);
          request.post({
            url: config.SseServicesCmsCORS + "InvoiceSrv/Invoices",
            form: {
              AccountId: acctData.AccountID,
              InvoiceTypeId: 'INSTALL'
            },
            json: true
          }, function(error, response, body) {
            // console.log("Post Invoice Body: ", body);
            expect(error).toBeNull();
            expect(body.Code).toBe(0);

            /** Check the returned fields. */
            expect(body.Value.InvoiceID).not.toBeNull("The InvoiceID should have been returned.");
            expect(body.Value.AccountId).toBe(acctData.AccountID, "The AccountId did not match what was passed.");
            expect(body.Value.InvoiceTypeId).toBe("INSTALL", "The InvoiceTypeId did not match what was passed.");

            // Create or Add an InvoiceItem
            var itemId = "EQPM_INVT109",
              itemSku = "GEC-6065295",
              barcodeId = "TESTSIM5000",
              existingSku = "EEQ-DOOR";
            request.post({
              url: config.SseServicesCmsCORS + "InvoiceSrv/InvoiceItems",
              form: {
                InvoiceId: body.Value.InvoiceID,
                ItemId: itemId,
                Qty: 1,
                //SalesmanID: 'PRIVT001', // A SalesmanID or a technician ID must be passed not both at the same time.
                TechnicianID: 'SAMM001'
              },
              json: true
            }, function(error, response, bodyIn1) {
              // console.log("Get Invoice Item Body: ", bodyIn1);
              expect(error).toBeNull();
              expect(bodyIn1.Code).toBe(0);

              /** Check the returned fields. */
              modelDef.aeInvoiceItem(bodyIn1.Value, body.Value.InvoiceID, itemId, null, 1);

              // ** Delete / Remove Invoice Item
              request.del({
                url: config.SseServicesCmsCORS + "InvoiceSrv/InvoiceItems/" + bodyIn1.Value.InvoiceItemID,
                json: true
              }, function(error, response, bodyIn2) {
                // console.log("Delete Invoice Item Body: ", bodyIn2);
                expect(error).toBeNull();
                expect(bodyIn2.Code).toBe(0);

                /** Check the returned fields. */
                expect(bodyIn2.Value).toBe(true, "This should be true if it is returned successfully");

                /** Add by partnumber. */
                request.post({
                  url: config.SseServicesCmsCORS + "InvoiceSrv/InvoiceItems/" + body.Value.InvoiceID + "/AddByPartNumber",
                  form: {
                    InvoiceID: body.Value.InvoiceID,
                    ItemSku: itemSku,
                    Qty: 1,
                    SalesmanID: 'PRIVT001', // A SalesmanID or a technician ID must be passed not both at the same time.
                    //TechnicianID: 'SAMM001'
                  },
                  json: true
                }, function(error, response, bodyByPartNumber) {
                  // console.log("Body Add byPartNumber: ", bodyByPartNumber);
                  expect(bodyByPartNumber.Code).toBe(0, "There was an error adding by PartNumber.");
                  expect(bodyByPartNumber.Value).not.toBeNull("There is no value returned.");
                  expect(error).toBeNull();

                  modelDef.aeInvoice(bodyByPartNumber.Value, body.Value.InvoiceID, {
                    salesmanIdIsRequired: true,
                    salesmanId: 'PRIVT001'
                  });

                  /** Show items. */
                  // console.log("Invice Items: ", bodyByPartNumber.Value.Items);

                  /** Check to see if we can add by Barcode. */
                  request.post({
                    url: config.SseServicesCmsCORS + "InvoiceSrv/InvoiceItems/" + body.Value.InvoiceID + "/AddByBarcode",
                    form: {
                      InvoiceID: body.Value.InvoiceID, // Not required
                      BarcodeId: barcodeId,
                      Qty: 1,
                      // SalesmanID: 'PRIVT001', // A SalesmanID or a technician ID must be passed not both at the same time.
                      TechnicianID: 'SAMM001'
                    },
                    json: true
                  }, function(error, response, bodyByBarcode) {
                    // console.log("Body by Barcode: ", bodyByBarcode);
                    expect(bodyByBarcode.Code).toBe(0, "There was an error adding by Barcode.");
                    expect(bodyByBarcode.Value).not.toBeNull("There is no value returned.");
                    expect(error).toBeNull();

                    modelDef.aeInvoice(bodyByBarcode.Value, body.Value.InvoiceID);

                    /** Check ProductBarcodeId. */

                    expect(bodyByBarcode.Value.Items[1].ProductBarcodeId).toBe(barcodeId, "BarcodeId did not return the correct one.");

                    /** NEXT TEST is AddExistingEquipment */
                    request.post({
                      url: config.SseServicesCmsCORS + "InvoiceSrv/InvoiceItems/" + body.Value.InvoiceID + "/AddExistingEquipment",
                      form: {
                        InvoiceID: body.Value.InvoiceID, // Not required
                        ItemSku: existingSku,
                        Qty: 1,
                        SalesmanID: 'PRIVT001' // A SalesmanID or a technician ID must be passed not both at the same time.
                        // TechnicianID: 'SAMM001'
                      },
                      json: true
                    }, function(error, response, bodyByExisting) {
                      // console.log("Body by Barcode: ", bodyByExisting);
                      expect(bodyByExisting.Code).toBe(0, "There was an error adding by Existing Equipment.");
                      expect(bodyByExisting.Value).not.toBeNull("There is no value returned.");
                      expect(error).toBeNull();

                      modelDef.aeInvoice(bodyByExisting.Value, body.Value.InvoiceID);

                      /** Check ProductBarcodeId. */
                      // console.log("Invoice Item: ", bodyByExisting.Value.Items[3]);
                      expect(bodyByExisting.Value.Items[3].ItemSKU).toBe(existingSku, "Sku's do not match.");

                      /** Updat Existing invoiceItem. */
                      request.post({
                        url: config.SseServicesCmsCORS + "InvoiceSrv/InvoiceItems/" + bodyByExisting.Value.Items[2].InvoiceItemID + "/UpdateEquipment",
                        form: {
                          RetailPrice: 1000,
                          SystemPoints: 8,
                          Qty: 2,
                          SalesmanID: 'PRIVT001' // A SalesmanID or a technician ID must be passed not both at the same time.
                          // TechnicianID: 'SAMM001'
                        },
                        json: true
                      }, function(error, response, bodyUpdateEq) {
                        // console.log("Body update EQ: ", bodyUpdateEq);
                        expect(bodyUpdateEq.Code).toBe(0, "There was an error adding by Existing Equipment.");
                        expect(bodyUpdateEq.Value).not.toBeNull("There is no value returned.");
                        expect(error).toBeNull();

                        /** Check Data. */
                        modelDef.aeInvoiceItem(bodyUpdateEq.Value, bodyUpdateEq.Value.InvoiceId, bodyUpdateEq.Value.ItemId, bodyUpdateEq.Value.ItemSKU, 2);

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
    });
  });
});