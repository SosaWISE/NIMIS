select 
--	*
 rtrim(a.ITEMNMBR) as [SKU]
, rtrim(a.ITEMDESC) as [Item_Desc]
, rtrim(a.ITMGEDSC) as [Item_Type]
, rtrim(a.ITMCLSCD) as [Item_Class]
, rtrim(a.UOMSCHDL) as [Item_UOM]
, rtrim(a.PriceGroup) as [Item_PriceGroup]
, rtrim(a.USCATVLS_1) as [Vendor]
, rtrim(a.USCATVLS_2) as [Points]
, a.STNDCOST
--, a.*
from DYSNEYDAD.NEX.dbo.IV00101 AS a
order by
 a.USCATVLS_1
, a.ITEMNMBR
