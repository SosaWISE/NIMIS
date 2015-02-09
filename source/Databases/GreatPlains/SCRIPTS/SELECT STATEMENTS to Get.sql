/** Invoice Items. 
IV  -- These are inventory tables

*/
SELECT
	*
FROM
	IV00101 AS II WITH (NOLOCK);


/** Customers */
SELECT * FROM dbo.RM00101 AS CUST WITH (NOLOCK) ORDER BY CUSTNMBR;
/**
	102		-- Addresses
	103		-- Agging
	20101	-- History (Invoice / Pmt / Credit memos)
	20201	-- Way payments are applied to invoices
*/

/** 
SOP10100
SOP10200
*/

SELECT TOP 100 * FROM RM20101 AS HIST WITH (NOLOCK) ORDER BY CUSTNMBR;  -- All history by customer
SELECT TOP 100 * FROM RM20201 ORDER BY CUSTNMBR;  -- How payments, credit memos, and returns are applied to invoices

SELECT TOP 100 * FROM SOP30200 ORDER BY CUSTNMBR; -- Invoice Header (SOPNUMBE is PK)
SELECT TOP 100 * FROM SOP30300 ORDER BY SOPNUMBE; -- Invoice Items (Join by SOPNUMBE)

/** Warehouse header .*/
SELECT TOP 100 LOCNCODE FROM SOP30200 ORDER BY CUSTNMBR;
/** Warehouse on the invoice item line. */
SELECT TOP 100 LOCNCODE FROM SOP30300 ORDER BY SOPNUMBE;

/** WAREHOUSE TABLES*/
SELECT * FROM IV00102;


/** This joins the items to the invoice header. */
SELECT
	HDr.*
FROM
	SOP30200 AS HDr WITH (NOLOCK)
	INNER JOIN SOP30300 AS LNE WITH (NOLOCK)
	ON
		(LNE.SOPNUMBE = HDr.SOPNUMBE);

/** Join an invoice to the History */
SELECT
	HDr.*
FROM
	SOP30200 AS HDr WITH (NOLOCK)
	INNER JOIN SOP30300 AS LNE WITH (NOLOCK)
	ON
		(LNE.SOPNUMBE = HDr.SOPNUMBE)
	INNER JOIN RM20101 AS HIST WITH (NOLOCK)
	ON
		(HIST.DOCNUMBR = HDr.SOPNUMBE);
	



/** LINKED STATEMENTS */
SELECT * FROM [DysneyDad].master.sys.databases AS II WITH (NOLOCK);

SELECT TOP 100 * FROM [DysneyDad].DFS.[dbo].IV00101 AS II WITH (NOLOCK);

SELECT TOP 100 * FROM [DYSNEYDAD].DFS.[dbo].RM00101;