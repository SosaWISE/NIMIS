


using System;
using System.ComponentModel;
using System.Linq;
using SubSonic;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using SOS.Data;

namespace NXS.Data.Connext
{
	/// <summary>
	/// Strongly-typed collection for the CX_Address class.
	/// </summary>
	[DataContract]
	public partial class CX_AddressCollection : ActiveList<CX_Address, CX_AddressCollection>
	{
		public static CX_AddressCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			CX_AddressCollection result = new CX_AddressCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (CX_Address item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the CX_Address table.
	/// </summary>
	[DataContract]
	public partial class CX_Address : ActiveRecord<CX_Address>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public CX_Address()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("CX_Address", TableType.Table, DataService.GetInstance("NxseConnextProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAddressID = new TableSchema.TableColumn(schema);
				colvarAddressID.ColumnName = "AddressID";
				colvarAddressID.DataType = DbType.Int64;
				colvarAddressID.MaxLength = 0;
				colvarAddressID.AutoIncrement = true;
				colvarAddressID.IsNullable = false;
				colvarAddressID.IsPrimaryKey = true;
				colvarAddressID.IsForeignKey = false;
				colvarAddressID.IsReadOnly = false;
				colvarAddressID.DefaultSetting = @"";
				colvarAddressID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddressID);

				TableSchema.TableColumn colvarDealerId = new TableSchema.TableColumn(schema);
				colvarDealerId.ColumnName = "DealerId";
				colvarDealerId.DataType = DbType.Int32;
				colvarDealerId.MaxLength = 0;
				colvarDealerId.AutoIncrement = false;
				colvarDealerId.IsNullable = false;
				colvarDealerId.IsPrimaryKey = false;
				colvarDealerId.IsForeignKey = false;
				colvarDealerId.IsReadOnly = false;
				colvarDealerId.DefaultSetting = @"((5000))";
				colvarDealerId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerId);

				TableSchema.TableColumn colvarValidationVendorId = new TableSchema.TableColumn(schema);
				colvarValidationVendorId.ColumnName = "ValidationVendorId";
				colvarValidationVendorId.DataType = DbType.AnsiString;
				colvarValidationVendorId.MaxLength = 20;
				colvarValidationVendorId.AutoIncrement = false;
				colvarValidationVendorId.IsNullable = false;
				colvarValidationVendorId.IsPrimaryKey = false;
				colvarValidationVendorId.IsForeignKey = false;
				colvarValidationVendorId.IsReadOnly = false;
				colvarValidationVendorId.DefaultSetting = @"";
				colvarValidationVendorId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarValidationVendorId);

				TableSchema.TableColumn colvarAddressValidationStateId = new TableSchema.TableColumn(schema);
				colvarAddressValidationStateId.ColumnName = "AddressValidationStateId";
				colvarAddressValidationStateId.DataType = DbType.AnsiString;
				colvarAddressValidationStateId.MaxLength = 5;
				colvarAddressValidationStateId.AutoIncrement = false;
				colvarAddressValidationStateId.IsNullable = false;
				colvarAddressValidationStateId.IsPrimaryKey = false;
				colvarAddressValidationStateId.IsForeignKey = false;
				colvarAddressValidationStateId.IsReadOnly = false;
				colvarAddressValidationStateId.DefaultSetting = @"";
				colvarAddressValidationStateId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddressValidationStateId);

				TableSchema.TableColumn colvarStateId = new TableSchema.TableColumn(schema);
				colvarStateId.ColumnName = "StateId";
				colvarStateId.DataType = DbType.AnsiString;
				colvarStateId.MaxLength = 4;
				colvarStateId.AutoIncrement = false;
				colvarStateId.IsNullable = false;
				colvarStateId.IsPrimaryKey = false;
				colvarStateId.IsForeignKey = false;
				colvarStateId.IsReadOnly = false;
				colvarStateId.DefaultSetting = @"";
				colvarStateId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateId);

				TableSchema.TableColumn colvarCountryId = new TableSchema.TableColumn(schema);
				colvarCountryId.ColumnName = "CountryId";
				colvarCountryId.DataType = DbType.String;
				colvarCountryId.MaxLength = 10;
				colvarCountryId.AutoIncrement = false;
				colvarCountryId.IsNullable = false;
				colvarCountryId.IsPrimaryKey = false;
				colvarCountryId.IsForeignKey = false;
				colvarCountryId.IsReadOnly = false;
				colvarCountryId.DefaultSetting = @"";
				colvarCountryId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountryId);

				TableSchema.TableColumn colvarTimeZoneId = new TableSchema.TableColumn(schema);
				colvarTimeZoneId.ColumnName = "TimeZoneId";
				colvarTimeZoneId.DataType = DbType.Int32;
				colvarTimeZoneId.MaxLength = 0;
				colvarTimeZoneId.AutoIncrement = false;
				colvarTimeZoneId.IsNullable = false;
				colvarTimeZoneId.IsPrimaryKey = false;
				colvarTimeZoneId.IsForeignKey = false;
				colvarTimeZoneId.IsReadOnly = false;
				colvarTimeZoneId.DefaultSetting = @"";
				colvarTimeZoneId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTimeZoneId);

				TableSchema.TableColumn colvarAddressTypeId = new TableSchema.TableColumn(schema);
				colvarAddressTypeId.ColumnName = "AddressTypeId";
				colvarAddressTypeId.DataType = DbType.AnsiString;
				colvarAddressTypeId.MaxLength = 10;
				colvarAddressTypeId.AutoIncrement = false;
				colvarAddressTypeId.IsNullable = false;
				colvarAddressTypeId.IsPrimaryKey = false;
				colvarAddressTypeId.IsForeignKey = false;
				colvarAddressTypeId.IsReadOnly = false;
				colvarAddressTypeId.DefaultSetting = @"";
				colvarAddressTypeId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddressTypeId);

				TableSchema.TableColumn colvarSeasonId = new TableSchema.TableColumn(schema);
				colvarSeasonId.ColumnName = "SeasonId";
				colvarSeasonId.DataType = DbType.Int32;
				colvarSeasonId.MaxLength = 0;
				colvarSeasonId.AutoIncrement = false;
				colvarSeasonId.IsNullable = false;
				colvarSeasonId.IsPrimaryKey = false;
				colvarSeasonId.IsForeignKey = false;
				colvarSeasonId.IsReadOnly = false;
				colvarSeasonId.DefaultSetting = @"";
				colvarSeasonId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonId);

				TableSchema.TableColumn colvarTeamLocationId = new TableSchema.TableColumn(schema);
				colvarTeamLocationId.ColumnName = "TeamLocationId";
				colvarTeamLocationId.DataType = DbType.Int32;
				colvarTeamLocationId.MaxLength = 0;
				colvarTeamLocationId.AutoIncrement = false;
				colvarTeamLocationId.IsNullable = false;
				colvarTeamLocationId.IsPrimaryKey = false;
				colvarTeamLocationId.IsForeignKey = false;
				colvarTeamLocationId.IsReadOnly = false;
				colvarTeamLocationId.DefaultSetting = @"";
				colvarTeamLocationId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationId);

				TableSchema.TableColumn colvarSalesRepId = new TableSchema.TableColumn(schema);
				colvarSalesRepId.ColumnName = "SalesRepId";
				colvarSalesRepId.DataType = DbType.String;
				colvarSalesRepId.MaxLength = 25;
				colvarSalesRepId.AutoIncrement = false;
				colvarSalesRepId.IsNullable = false;
				colvarSalesRepId.IsPrimaryKey = false;
				colvarSalesRepId.IsForeignKey = false;
				colvarSalesRepId.IsReadOnly = false;
				colvarSalesRepId.DefaultSetting = @"";
				colvarSalesRepId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesRepId);

				TableSchema.TableColumn colvarStreetAddress = new TableSchema.TableColumn(schema);
				colvarStreetAddress.ColumnName = "StreetAddress";
				colvarStreetAddress.DataType = DbType.String;
				colvarStreetAddress.MaxLength = 50;
				colvarStreetAddress.AutoIncrement = false;
				colvarStreetAddress.IsNullable = false;
				colvarStreetAddress.IsPrimaryKey = false;
				colvarStreetAddress.IsForeignKey = false;
				colvarStreetAddress.IsReadOnly = false;
				colvarStreetAddress.DefaultSetting = @"";
				colvarStreetAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetAddress);

				TableSchema.TableColumn colvarStreetAddress2 = new TableSchema.TableColumn(schema);
				colvarStreetAddress2.ColumnName = "StreetAddress2";
				colvarStreetAddress2.DataType = DbType.String;
				colvarStreetAddress2.MaxLength = 50;
				colvarStreetAddress2.AutoIncrement = false;
				colvarStreetAddress2.IsNullable = true;
				colvarStreetAddress2.IsPrimaryKey = false;
				colvarStreetAddress2.IsForeignKey = false;
				colvarStreetAddress2.IsReadOnly = false;
				colvarStreetAddress2.DefaultSetting = @"";
				colvarStreetAddress2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetAddress2);

				TableSchema.TableColumn colvarStreetNumber = new TableSchema.TableColumn(schema);
				colvarStreetNumber.ColumnName = "StreetNumber";
				colvarStreetNumber.DataType = DbType.String;
				colvarStreetNumber.MaxLength = 40;
				colvarStreetNumber.AutoIncrement = false;
				colvarStreetNumber.IsNullable = true;
				colvarStreetNumber.IsPrimaryKey = false;
				colvarStreetNumber.IsForeignKey = false;
				colvarStreetNumber.IsReadOnly = false;
				colvarStreetNumber.DefaultSetting = @"";
				colvarStreetNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetNumber);

				TableSchema.TableColumn colvarStreetName = new TableSchema.TableColumn(schema);
				colvarStreetName.ColumnName = "StreetName";
				colvarStreetName.DataType = DbType.String;
				colvarStreetName.MaxLength = 50;
				colvarStreetName.AutoIncrement = false;
				colvarStreetName.IsNullable = true;
				colvarStreetName.IsPrimaryKey = false;
				colvarStreetName.IsForeignKey = false;
				colvarStreetName.IsReadOnly = false;
				colvarStreetName.DefaultSetting = @"";
				colvarStreetName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetName);

				TableSchema.TableColumn colvarStreetType = new TableSchema.TableColumn(schema);
				colvarStreetType.ColumnName = "StreetType";
				colvarStreetType.DataType = DbType.String;
				colvarStreetType.MaxLength = 20;
				colvarStreetType.AutoIncrement = false;
				colvarStreetType.IsNullable = true;
				colvarStreetType.IsPrimaryKey = false;
				colvarStreetType.IsForeignKey = false;
				colvarStreetType.IsReadOnly = false;
				colvarStreetType.DefaultSetting = @"";
				colvarStreetType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStreetType);

				TableSchema.TableColumn colvarPreDirectional = new TableSchema.TableColumn(schema);
				colvarPreDirectional.ColumnName = "PreDirectional";
				colvarPreDirectional.DataType = DbType.String;
				colvarPreDirectional.MaxLength = 20;
				colvarPreDirectional.AutoIncrement = false;
				colvarPreDirectional.IsNullable = true;
				colvarPreDirectional.IsPrimaryKey = false;
				colvarPreDirectional.IsForeignKey = false;
				colvarPreDirectional.IsReadOnly = false;
				colvarPreDirectional.DefaultSetting = @"";
				colvarPreDirectional.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPreDirectional);

				TableSchema.TableColumn colvarPostDirectional = new TableSchema.TableColumn(schema);
				colvarPostDirectional.ColumnName = "PostDirectional";
				colvarPostDirectional.DataType = DbType.String;
				colvarPostDirectional.MaxLength = 20;
				colvarPostDirectional.AutoIncrement = false;
				colvarPostDirectional.IsNullable = true;
				colvarPostDirectional.IsPrimaryKey = false;
				colvarPostDirectional.IsForeignKey = false;
				colvarPostDirectional.IsReadOnly = false;
				colvarPostDirectional.DefaultSetting = @"";
				colvarPostDirectional.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPostDirectional);

				TableSchema.TableColumn colvarExtension = new TableSchema.TableColumn(schema);
				colvarExtension.ColumnName = "Extension";
				colvarExtension.DataType = DbType.String;
				colvarExtension.MaxLength = 50;
				colvarExtension.AutoIncrement = false;
				colvarExtension.IsNullable = true;
				colvarExtension.IsPrimaryKey = false;
				colvarExtension.IsForeignKey = false;
				colvarExtension.IsReadOnly = false;
				colvarExtension.DefaultSetting = @"";
				colvarExtension.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExtension);

				TableSchema.TableColumn colvarExtensionNumber = new TableSchema.TableColumn(schema);
				colvarExtensionNumber.ColumnName = "ExtensionNumber";
				colvarExtensionNumber.DataType = DbType.String;
				colvarExtensionNumber.MaxLength = 50;
				colvarExtensionNumber.AutoIncrement = false;
				colvarExtensionNumber.IsNullable = true;
				colvarExtensionNumber.IsPrimaryKey = false;
				colvarExtensionNumber.IsForeignKey = false;
				colvarExtensionNumber.IsReadOnly = false;
				colvarExtensionNumber.DefaultSetting = @"";
				colvarExtensionNumber.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExtensionNumber);

				TableSchema.TableColumn colvarCounty = new TableSchema.TableColumn(schema);
				colvarCounty.ColumnName = "County";
				colvarCounty.DataType = DbType.String;
				colvarCounty.MaxLength = 50;
				colvarCounty.AutoIncrement = false;
				colvarCounty.IsNullable = true;
				colvarCounty.IsPrimaryKey = false;
				colvarCounty.IsForeignKey = false;
				colvarCounty.IsReadOnly = false;
				colvarCounty.DefaultSetting = @"";
				colvarCounty.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCounty);

				TableSchema.TableColumn colvarCountyCode = new TableSchema.TableColumn(schema);
				colvarCountyCode.ColumnName = "CountyCode";
				colvarCountyCode.DataType = DbType.String;
				colvarCountyCode.MaxLength = 6;
				colvarCountyCode.AutoIncrement = false;
				colvarCountyCode.IsNullable = true;
				colvarCountyCode.IsPrimaryKey = false;
				colvarCountyCode.IsForeignKey = false;
				colvarCountyCode.IsReadOnly = false;
				colvarCountyCode.DefaultSetting = @"";
				colvarCountyCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountyCode);

				TableSchema.TableColumn colvarUrbanization = new TableSchema.TableColumn(schema);
				colvarUrbanization.ColumnName = "Urbanization";
				colvarUrbanization.DataType = DbType.String;
				colvarUrbanization.MaxLength = 50;
				colvarUrbanization.AutoIncrement = false;
				colvarUrbanization.IsNullable = true;
				colvarUrbanization.IsPrimaryKey = false;
				colvarUrbanization.IsForeignKey = false;
				colvarUrbanization.IsReadOnly = false;
				colvarUrbanization.DefaultSetting = @"";
				colvarUrbanization.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUrbanization);

				TableSchema.TableColumn colvarUrbanizationCode = new TableSchema.TableColumn(schema);
				colvarUrbanizationCode.ColumnName = "UrbanizationCode";
				colvarUrbanizationCode.DataType = DbType.String;
				colvarUrbanizationCode.MaxLength = 3;
				colvarUrbanizationCode.AutoIncrement = false;
				colvarUrbanizationCode.IsNullable = true;
				colvarUrbanizationCode.IsPrimaryKey = false;
				colvarUrbanizationCode.IsForeignKey = false;
				colvarUrbanizationCode.IsReadOnly = false;
				colvarUrbanizationCode.DefaultSetting = @"";
				colvarUrbanizationCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUrbanizationCode);

				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.String;
				colvarCity.MaxLength = 50;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = false;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);

				TableSchema.TableColumn colvarPostalCode = new TableSchema.TableColumn(schema);
				colvarPostalCode.ColumnName = "PostalCode";
				colvarPostalCode.DataType = DbType.String;
				colvarPostalCode.MaxLength = 5;
				colvarPostalCode.AutoIncrement = false;
				colvarPostalCode.IsNullable = false;
				colvarPostalCode.IsPrimaryKey = false;
				colvarPostalCode.IsForeignKey = false;
				colvarPostalCode.IsReadOnly = false;
				colvarPostalCode.DefaultSetting = @"";
				colvarPostalCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPostalCode);

				TableSchema.TableColumn colvarPlusFour = new TableSchema.TableColumn(schema);
				colvarPlusFour.ColumnName = "PlusFour";
				colvarPlusFour.DataType = DbType.String;
				colvarPlusFour.MaxLength = 4;
				colvarPlusFour.AutoIncrement = false;
				colvarPlusFour.IsNullable = true;
				colvarPlusFour.IsPrimaryKey = false;
				colvarPlusFour.IsForeignKey = false;
				colvarPlusFour.IsReadOnly = false;
				colvarPlusFour.DefaultSetting = @"";
				colvarPlusFour.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPlusFour);

				TableSchema.TableColumn colvarPostalCodeFull = new TableSchema.TableColumn(schema);
				colvarPostalCodeFull.ColumnName = "PostalCodeFull";
				colvarPostalCodeFull.DataType = DbType.AnsiString;
				colvarPostalCodeFull.MaxLength = 15;
				colvarPostalCodeFull.AutoIncrement = false;
				colvarPostalCodeFull.IsNullable = true;
				colvarPostalCodeFull.IsPrimaryKey = false;
				colvarPostalCodeFull.IsForeignKey = false;
				colvarPostalCodeFull.IsReadOnly = false;
				colvarPostalCodeFull.DefaultSetting = @"";
				colvarPostalCodeFull.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPostalCodeFull);

				TableSchema.TableColumn colvarPhone = new TableSchema.TableColumn(schema);
				colvarPhone.ColumnName = "Phone";
				colvarPhone.DataType = DbType.AnsiString;
				colvarPhone.MaxLength = 20;
				colvarPhone.AutoIncrement = false;
				colvarPhone.IsNullable = true;
				colvarPhone.IsPrimaryKey = false;
				colvarPhone.IsForeignKey = false;
				colvarPhone.IsReadOnly = false;
				colvarPhone.DefaultSetting = @"";
				colvarPhone.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhone);

				TableSchema.TableColumn colvarDeliveryPoint = new TableSchema.TableColumn(schema);
				colvarDeliveryPoint.ColumnName = "DeliveryPoint";
				colvarDeliveryPoint.DataType = DbType.String;
				colvarDeliveryPoint.MaxLength = 2;
				colvarDeliveryPoint.AutoIncrement = false;
				colvarDeliveryPoint.IsNullable = true;
				colvarDeliveryPoint.IsPrimaryKey = false;
				colvarDeliveryPoint.IsForeignKey = false;
				colvarDeliveryPoint.IsReadOnly = false;
				colvarDeliveryPoint.DefaultSetting = @"";
				colvarDeliveryPoint.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDeliveryPoint);

				TableSchema.TableColumn colvarCrossStreet = new TableSchema.TableColumn(schema);
				colvarCrossStreet.ColumnName = "CrossStreet";
				colvarCrossStreet.DataType = DbType.AnsiString;
				colvarCrossStreet.MaxLength = 50;
				colvarCrossStreet.AutoIncrement = false;
				colvarCrossStreet.IsNullable = true;
				colvarCrossStreet.IsPrimaryKey = false;
				colvarCrossStreet.IsForeignKey = false;
				colvarCrossStreet.IsReadOnly = false;
				colvarCrossStreet.DefaultSetting = @"";
				colvarCrossStreet.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCrossStreet);

				TableSchema.TableColumn colvarLatitude = new TableSchema.TableColumn(schema);
				colvarLatitude.ColumnName = "Latitude";
				colvarLatitude.DataType = DbType.Double;
				colvarLatitude.MaxLength = 0;
				colvarLatitude.AutoIncrement = false;
				colvarLatitude.IsNullable = false;
				colvarLatitude.IsPrimaryKey = false;
				colvarLatitude.IsForeignKey = false;
				colvarLatitude.IsReadOnly = false;
				colvarLatitude.DefaultSetting = @"";
				colvarLatitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLatitude);

				TableSchema.TableColumn colvarLongitude = new TableSchema.TableColumn(schema);
				colvarLongitude.ColumnName = "Longitude";
				colvarLongitude.DataType = DbType.Double;
				colvarLongitude.MaxLength = 0;
				colvarLongitude.AutoIncrement = false;
				colvarLongitude.IsNullable = false;
				colvarLongitude.IsPrimaryKey = false;
				colvarLongitude.IsForeignKey = false;
				colvarLongitude.IsReadOnly = false;
				colvarLongitude.DefaultSetting = @"";
				colvarLongitude.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLongitude);

				TableSchema.TableColumn colvarCongressionalDistric = new TableSchema.TableColumn(schema);
				colvarCongressionalDistric.ColumnName = "CongressionalDistric";
				colvarCongressionalDistric.DataType = DbType.Int32;
				colvarCongressionalDistric.MaxLength = 0;
				colvarCongressionalDistric.AutoIncrement = false;
				colvarCongressionalDistric.IsNullable = true;
				colvarCongressionalDistric.IsPrimaryKey = false;
				colvarCongressionalDistric.IsForeignKey = false;
				colvarCongressionalDistric.IsReadOnly = false;
				colvarCongressionalDistric.DefaultSetting = @"";
				colvarCongressionalDistric.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCongressionalDistric);

				TableSchema.TableColumn colvarDPV = new TableSchema.TableColumn(schema);
				colvarDPV.ColumnName = "DPV";
				colvarDPV.DataType = DbType.Boolean;
				colvarDPV.MaxLength = 0;
				colvarDPV.AutoIncrement = false;
				colvarDPV.IsNullable = false;
				colvarDPV.IsPrimaryKey = false;
				colvarDPV.IsForeignKey = false;
				colvarDPV.IsReadOnly = false;
				colvarDPV.DefaultSetting = @"";
				colvarDPV.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDPV);

				TableSchema.TableColumn colvarDPVResponse = new TableSchema.TableColumn(schema);
				colvarDPVResponse.ColumnName = "DPVResponse";
				colvarDPVResponse.DataType = DbType.AnsiStringFixedLength;
				colvarDPVResponse.MaxLength = 10;
				colvarDPVResponse.AutoIncrement = false;
				colvarDPVResponse.IsNullable = true;
				colvarDPVResponse.IsPrimaryKey = false;
				colvarDPVResponse.IsForeignKey = false;
				colvarDPVResponse.IsReadOnly = false;
				colvarDPVResponse.DefaultSetting = @"";
				colvarDPVResponse.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDPVResponse);

				TableSchema.TableColumn colvarDPVFootnote = new TableSchema.TableColumn(schema);
				colvarDPVFootnote.ColumnName = "DPVFootnote";
				colvarDPVFootnote.DataType = DbType.AnsiString;
				colvarDPVFootnote.MaxLength = 50;
				colvarDPVFootnote.AutoIncrement = false;
				colvarDPVFootnote.IsNullable = true;
				colvarDPVFootnote.IsPrimaryKey = false;
				colvarDPVFootnote.IsForeignKey = false;
				colvarDPVFootnote.IsReadOnly = false;
				colvarDPVFootnote.DefaultSetting = @"";
				colvarDPVFootnote.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDPVFootnote);

				TableSchema.TableColumn colvarCarrierRoute = new TableSchema.TableColumn(schema);
				colvarCarrierRoute.ColumnName = "CarrierRoute";
				colvarCarrierRoute.DataType = DbType.AnsiString;
				colvarCarrierRoute.MaxLength = 50;
				colvarCarrierRoute.AutoIncrement = false;
				colvarCarrierRoute.IsNullable = true;
				colvarCarrierRoute.IsPrimaryKey = false;
				colvarCarrierRoute.IsForeignKey = false;
				colvarCarrierRoute.IsReadOnly = false;
				colvarCarrierRoute.DefaultSetting = @"";
				colvarCarrierRoute.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCarrierRoute);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"((1))";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.AnsiString;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"('SYSTEM')";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				BaseSchema = schema;
				DataService.Providers["NxseConnextProvider"].AddSchema("CX_Address",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static CX_Address LoadFrom(CX_Address item)
		{
			CX_Address result = new CX_Address();
			if (item.AddressID != default(long)) {
				result.LoadByKey(item.AddressID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long AddressID {
			get { return GetColumnValue<long>(Columns.AddressID); }
			set {
				SetColumnValue(Columns.AddressID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AddressID));
			}
		}
		[DataMember]
		public int DealerId {
			get { return GetColumnValue<int>(Columns.DealerId); }
			set {
				SetColumnValue(Columns.DealerId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DealerId));
			}
		}
		[DataMember]
		public string ValidationVendorId {
			get { return GetColumnValue<string>(Columns.ValidationVendorId); }
			set {
				SetColumnValue(Columns.ValidationVendorId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ValidationVendorId));
			}
		}
		[DataMember]
		public string AddressValidationStateId {
			get { return GetColumnValue<string>(Columns.AddressValidationStateId); }
			set {
				SetColumnValue(Columns.AddressValidationStateId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AddressValidationStateId));
			}
		}
		[DataMember]
		public string StateId {
			get { return GetColumnValue<string>(Columns.StateId); }
			set {
				SetColumnValue(Columns.StateId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StateId));
			}
		}
		[DataMember]
		public string CountryId {
			get { return GetColumnValue<string>(Columns.CountryId); }
			set {
				SetColumnValue(Columns.CountryId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CountryId));
			}
		}
		[DataMember]
		public int TimeZoneId {
			get { return GetColumnValue<int>(Columns.TimeZoneId); }
			set {
				SetColumnValue(Columns.TimeZoneId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TimeZoneId));
			}
		}
		[DataMember]
		public string AddressTypeId {
			get { return GetColumnValue<string>(Columns.AddressTypeId); }
			set {
				SetColumnValue(Columns.AddressTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AddressTypeId));
			}
		}
		[DataMember]
		public int SeasonId {
			get { return GetColumnValue<int>(Columns.SeasonId); }
			set {
				SetColumnValue(Columns.SeasonId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SeasonId));
			}
		}
		[DataMember]
		public int TeamLocationId {
			get { return GetColumnValue<int>(Columns.TeamLocationId); }
			set {
				SetColumnValue(Columns.TeamLocationId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TeamLocationId));
			}
		}
		[DataMember]
		public string SalesRepId {
			get { return GetColumnValue<string>(Columns.SalesRepId); }
			set {
				SetColumnValue(Columns.SalesRepId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SalesRepId));
			}
		}
		[DataMember]
		public string StreetAddress {
			get { return GetColumnValue<string>(Columns.StreetAddress); }
			set {
				SetColumnValue(Columns.StreetAddress, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StreetAddress));
			}
		}
		[DataMember]
		public string StreetAddress2 {
			get { return GetColumnValue<string>(Columns.StreetAddress2); }
			set {
				SetColumnValue(Columns.StreetAddress2, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StreetAddress2));
			}
		}
		[DataMember]
		public string StreetNumber {
			get { return GetColumnValue<string>(Columns.StreetNumber); }
			set {
				SetColumnValue(Columns.StreetNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StreetNumber));
			}
		}
		[DataMember]
		public string StreetName {
			get { return GetColumnValue<string>(Columns.StreetName); }
			set {
				SetColumnValue(Columns.StreetName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StreetName));
			}
		}
		[DataMember]
		public string StreetType {
			get { return GetColumnValue<string>(Columns.StreetType); }
			set {
				SetColumnValue(Columns.StreetType, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.StreetType));
			}
		}
		[DataMember]
		public string PreDirectional {
			get { return GetColumnValue<string>(Columns.PreDirectional); }
			set {
				SetColumnValue(Columns.PreDirectional, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PreDirectional));
			}
		}
		[DataMember]
		public string PostDirectional {
			get { return GetColumnValue<string>(Columns.PostDirectional); }
			set {
				SetColumnValue(Columns.PostDirectional, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PostDirectional));
			}
		}
		[DataMember]
		public string Extension {
			get { return GetColumnValue<string>(Columns.Extension); }
			set {
				SetColumnValue(Columns.Extension, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Extension));
			}
		}
		[DataMember]
		public string ExtensionNumber {
			get { return GetColumnValue<string>(Columns.ExtensionNumber); }
			set {
				SetColumnValue(Columns.ExtensionNumber, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ExtensionNumber));
			}
		}
		[DataMember]
		public string County {
			get { return GetColumnValue<string>(Columns.County); }
			set {
				SetColumnValue(Columns.County, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.County));
			}
		}
		[DataMember]
		public string CountyCode {
			get { return GetColumnValue<string>(Columns.CountyCode); }
			set {
				SetColumnValue(Columns.CountyCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CountyCode));
			}
		}
		[DataMember]
		public string Urbanization {
			get { return GetColumnValue<string>(Columns.Urbanization); }
			set {
				SetColumnValue(Columns.Urbanization, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Urbanization));
			}
		}
		[DataMember]
		public string UrbanizationCode {
			get { return GetColumnValue<string>(Columns.UrbanizationCode); }
			set {
				SetColumnValue(Columns.UrbanizationCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.UrbanizationCode));
			}
		}
		[DataMember]
		public string City {
			get { return GetColumnValue<string>(Columns.City); }
			set {
				SetColumnValue(Columns.City, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.City));
			}
		}
		[DataMember]
		public string PostalCode {
			get { return GetColumnValue<string>(Columns.PostalCode); }
			set {
				SetColumnValue(Columns.PostalCode, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PostalCode));
			}
		}
		[DataMember]
		public string PlusFour {
			get { return GetColumnValue<string>(Columns.PlusFour); }
			set {
				SetColumnValue(Columns.PlusFour, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PlusFour));
			}
		}
		[DataMember]
		public string PostalCodeFull {
			get { return GetColumnValue<string>(Columns.PostalCodeFull); }
			set {
				SetColumnValue(Columns.PostalCodeFull, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PostalCodeFull));
			}
		}
		[DataMember]
		public string Phone {
			get { return GetColumnValue<string>(Columns.Phone); }
			set {
				SetColumnValue(Columns.Phone, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Phone));
			}
		}
		[DataMember]
		public string DeliveryPoint {
			get { return GetColumnValue<string>(Columns.DeliveryPoint); }
			set {
				SetColumnValue(Columns.DeliveryPoint, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DeliveryPoint));
			}
		}
		[DataMember]
		public string CrossStreet {
			get { return GetColumnValue<string>(Columns.CrossStreet); }
			set {
				SetColumnValue(Columns.CrossStreet, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CrossStreet));
			}
		}
		[DataMember]
		public double Latitude {
			get { return GetColumnValue<double>(Columns.Latitude); }
			set {
				SetColumnValue(Columns.Latitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Latitude));
			}
		}
		[DataMember]
		public double Longitude {
			get { return GetColumnValue<double>(Columns.Longitude); }
			set {
				SetColumnValue(Columns.Longitude, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Longitude));
			}
		}
		[DataMember]
		public int? CongressionalDistric {
			get { return GetColumnValue<int?>(Columns.CongressionalDistric); }
			set {
				SetColumnValue(Columns.CongressionalDistric, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CongressionalDistric));
			}
		}
		[DataMember]
		public bool DPV {
			get { return GetColumnValue<bool>(Columns.DPV); }
			set {
				SetColumnValue(Columns.DPV, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DPV));
			}
		}
		[DataMember]
		public string DPVResponse {
			get { return GetColumnValue<string>(Columns.DPVResponse); }
			set {
				SetColumnValue(Columns.DPVResponse, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DPVResponse));
			}
		}
		[DataMember]
		public string DPVFootnote {
			get { return GetColumnValue<string>(Columns.DPVFootnote); }
			set {
				SetColumnValue(Columns.DPVFootnote, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DPVFootnote));
			}
		}
		[DataMember]
		public string CarrierRoute {
			get { return GetColumnValue<string>(Columns.CarrierRoute); }
			set {
				SetColumnValue(Columns.CarrierRoute, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CarrierRoute));
			}
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
			}
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return AddressID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AddressIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn DealerIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn ValidationVendorIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn AddressValidationStateIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn StateIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CountryIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn TimeZoneIdColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn AddressTypeIdColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn SeasonIdColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn TeamLocationIdColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn SalesRepIdColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn StreetAddressColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn StreetAddress2Column
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn StreetNumberColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn StreetNameColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn StreetTypeColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn PreDirectionalColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn PostDirectionalColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn ExtensionColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn ExtensionNumberColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn CountyColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn CountyCodeColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn UrbanizationColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn UrbanizationCodeColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn CityColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn PostalCodeColumn
		{
			get { return Schema.Columns[25]; }
		}
		public static TableSchema.TableColumn PlusFourColumn
		{
			get { return Schema.Columns[26]; }
		}
		public static TableSchema.TableColumn PostalCodeFullColumn
		{
			get { return Schema.Columns[27]; }
		}
		public static TableSchema.TableColumn PhoneColumn
		{
			get { return Schema.Columns[28]; }
		}
		public static TableSchema.TableColumn DeliveryPointColumn
		{
			get { return Schema.Columns[29]; }
		}
		public static TableSchema.TableColumn CrossStreetColumn
		{
			get { return Schema.Columns[30]; }
		}
		public static TableSchema.TableColumn LatitudeColumn
		{
			get { return Schema.Columns[31]; }
		}
		public static TableSchema.TableColumn LongitudeColumn
		{
			get { return Schema.Columns[32]; }
		}
		public static TableSchema.TableColumn CongressionalDistricColumn
		{
			get { return Schema.Columns[33]; }
		}
		public static TableSchema.TableColumn DPVColumn
		{
			get { return Schema.Columns[34]; }
		}
		public static TableSchema.TableColumn DPVResponseColumn
		{
			get { return Schema.Columns[35]; }
		}
		public static TableSchema.TableColumn DPVFootnoteColumn
		{
			get { return Schema.Columns[36]; }
		}
		public static TableSchema.TableColumn CarrierRouteColumn
		{
			get { return Schema.Columns[37]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[38]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[39]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[40]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[41]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AddressID = @"AddressID";
			public static readonly string DealerId = @"DealerId";
			public static readonly string ValidationVendorId = @"ValidationVendorId";
			public static readonly string AddressValidationStateId = @"AddressValidationStateId";
			public static readonly string StateId = @"StateId";
			public static readonly string CountryId = @"CountryId";
			public static readonly string TimeZoneId = @"TimeZoneId";
			public static readonly string AddressTypeId = @"AddressTypeId";
			public static readonly string SeasonId = @"SeasonId";
			public static readonly string TeamLocationId = @"TeamLocationId";
			public static readonly string SalesRepId = @"SalesRepId";
			public static readonly string StreetAddress = @"StreetAddress";
			public static readonly string StreetAddress2 = @"StreetAddress2";
			public static readonly string StreetNumber = @"StreetNumber";
			public static readonly string StreetName = @"StreetName";
			public static readonly string StreetType = @"StreetType";
			public static readonly string PreDirectional = @"PreDirectional";
			public static readonly string PostDirectional = @"PostDirectional";
			public static readonly string Extension = @"Extension";
			public static readonly string ExtensionNumber = @"ExtensionNumber";
			public static readonly string County = @"County";
			public static readonly string CountyCode = @"CountyCode";
			public static readonly string Urbanization = @"Urbanization";
			public static readonly string UrbanizationCode = @"UrbanizationCode";
			public static readonly string City = @"City";
			public static readonly string PostalCode = @"PostalCode";
			public static readonly string PlusFour = @"PlusFour";
			public static readonly string PostalCodeFull = @"PostalCodeFull";
			public static readonly string Phone = @"Phone";
			public static readonly string DeliveryPoint = @"DeliveryPoint";
			public static readonly string CrossStreet = @"CrossStreet";
			public static readonly string Latitude = @"Latitude";
			public static readonly string Longitude = @"Longitude";
			public static readonly string CongressionalDistric = @"CongressionalDistric";
			public static readonly string DPV = @"DPV";
			public static readonly string DPVResponse = @"DPVResponse";
			public static readonly string DPVFootnote = @"DPVFootnote";
			public static readonly string CarrierRoute = @"CarrierRoute";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string CreatedOn = @"CreatedOn";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AddressID; }
		}
		*/

		#region Foreign Collections

		private CX_ContactCollection _CX_ContactsCol;
		//Relationship: FK_CX_Contacts_CX_Address
		public CX_ContactCollection CX_ContactsCol
		{
			get
			{
				if(_CX_ContactsCol == null) {
					_CX_ContactsCol = new CX_ContactCollection();
					_CX_ContactsCol.LoadAndCloseReader(CX_Contact.Query()
						.WHERE(CX_Contact.Columns.AddressId, AddressID).ExecuteReader());
				}
				return _CX_ContactsCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the CX_Appointment class.
	/// </summary>
	[DataContract]
	public partial class CX_AppointmentCollection : ActiveList<CX_Appointment, CX_AppointmentCollection>
	{
		public static CX_AppointmentCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			CX_AppointmentCollection result = new CX_AppointmentCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (CX_Appointment item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the CX_Appointments table.
	/// </summary>
	[DataContract]
	public partial class CX_Appointment : ActiveRecord<CX_Appointment>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public CX_Appointment()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("CX_Appointments", TableType.Table, DataService.GetInstance("NxseConnextProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarAppointmentID = new TableSchema.TableColumn(schema);
				colvarAppointmentID.ColumnName = "AppointmentID";
				colvarAppointmentID.DataType = DbType.Int64;
				colvarAppointmentID.MaxLength = 0;
				colvarAppointmentID.AutoIncrement = true;
				colvarAppointmentID.IsNullable = false;
				colvarAppointmentID.IsPrimaryKey = true;
				colvarAppointmentID.IsForeignKey = false;
				colvarAppointmentID.IsReadOnly = false;
				colvarAppointmentID.DefaultSetting = @"";
				colvarAppointmentID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAppointmentID);

				TableSchema.TableColumn colvarContactId = new TableSchema.TableColumn(schema);
				colvarContactId.ColumnName = "ContactId";
				colvarContactId.DataType = DbType.Int64;
				colvarContactId.MaxLength = 0;
				colvarContactId.AutoIncrement = false;
				colvarContactId.IsNullable = false;
				colvarContactId.IsPrimaryKey = false;
				colvarContactId.IsForeignKey = true;
				colvarContactId.IsReadOnly = false;
				colvarContactId.DefaultSetting = @"";
				colvarContactId.ForeignKeyTableName = "CX_Contacts";
				schema.Columns.Add(colvarContactId);

				TableSchema.TableColumn colvarAppointmentDate = new TableSchema.TableColumn(schema);
				colvarAppointmentDate.ColumnName = "AppointmentDate";
				colvarAppointmentDate.DataType = DbType.DateTime;
				colvarAppointmentDate.MaxLength = 0;
				colvarAppointmentDate.AutoIncrement = false;
				colvarAppointmentDate.IsNullable = false;
				colvarAppointmentDate.IsPrimaryKey = false;
				colvarAppointmentDate.IsForeignKey = false;
				colvarAppointmentDate.IsReadOnly = false;
				colvarAppointmentDate.DefaultSetting = @"(getutcdate())";
				colvarAppointmentDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAppointmentDate);

				TableSchema.TableColumn colvarNote = new TableSchema.TableColumn(schema);
				colvarNote.ColumnName = "Note";
				colvarNote.DataType = DbType.String;
				colvarNote.MaxLength = -1;
				colvarNote.AutoIncrement = false;
				colvarNote.IsNullable = false;
				colvarNote.IsPrimaryKey = false;
				colvarNote.IsForeignKey = false;
				colvarNote.IsReadOnly = false;
				colvarNote.DefaultSetting = @"";
				colvarNote.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNote);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"(N'SYSTEM')";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				BaseSchema = schema;
				DataService.Providers["NxseConnextProvider"].AddSchema("CX_Appointments",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static CX_Appointment LoadFrom(CX_Appointment item)
		{
			CX_Appointment result = new CX_Appointment();
			if (item.AppointmentID != default(long)) {
				result.LoadByKey(item.AppointmentID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long AppointmentID {
			get { return GetColumnValue<long>(Columns.AppointmentID); }
			set {
				SetColumnValue(Columns.AppointmentID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AppointmentID));
			}
		}
		[DataMember]
		public long ContactId {
			get { return GetColumnValue<long>(Columns.ContactId); }
			set {
				SetColumnValue(Columns.ContactId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactId));
			}
		}
		[DataMember]
		public DateTime AppointmentDate {
			get { return GetColumnValue<DateTime>(Columns.AppointmentDate); }
			set {
				SetColumnValue(Columns.AppointmentDate, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AppointmentDate));
			}
		}
		[DataMember]
		public string Note {
			get { return GetColumnValue<string>(Columns.Note); }
			set {
				SetColumnValue(Columns.Note, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Note));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private CX_Contact _Contact;
		//Relationship: FK_CX_Appointments_CX_Contacts
		public CX_Contact Contact
		{
			get
			{
				if(_Contact == null) {
					_Contact = CX_Contact.FetchByID(this.ContactId);
				}
				return _Contact;
			}
			set
			{
				SetColumnValue("ContactId", value.ContactID);
				_Contact = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return AppointmentID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn AppointmentIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ContactIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AppointmentDateColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn NoteColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[5]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string AppointmentID = @"AppointmentID";
			public static readonly string ContactId = @"ContactId";
			public static readonly string AppointmentDate = @"AppointmentDate";
			public static readonly string Note = @"Note";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return AppointmentID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the CX_ContactNote class.
	/// </summary>
	[DataContract]
	public partial class CX_ContactNoteCollection : ActiveList<CX_ContactNote, CX_ContactNoteCollection>
	{
		public static CX_ContactNoteCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			CX_ContactNoteCollection result = new CX_ContactNoteCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (CX_ContactNote item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the CX_ContactNotes table.
	/// </summary>
	[DataContract]
	public partial class CX_ContactNote : ActiveRecord<CX_ContactNote>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public CX_ContactNote()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("CX_ContactNotes", TableType.Table, DataService.GetInstance("NxseConnextProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarContactNoteID = new TableSchema.TableColumn(schema);
				colvarContactNoteID.ColumnName = "ContactNoteID";
				colvarContactNoteID.DataType = DbType.Int64;
				colvarContactNoteID.MaxLength = 0;
				colvarContactNoteID.AutoIncrement = true;
				colvarContactNoteID.IsNullable = false;
				colvarContactNoteID.IsPrimaryKey = true;
				colvarContactNoteID.IsForeignKey = false;
				colvarContactNoteID.IsReadOnly = false;
				colvarContactNoteID.DefaultSetting = @"";
				colvarContactNoteID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactNoteID);

				TableSchema.TableColumn colvarContactId = new TableSchema.TableColumn(schema);
				colvarContactId.ColumnName = "ContactId";
				colvarContactId.DataType = DbType.Int64;
				colvarContactId.MaxLength = 0;
				colvarContactId.AutoIncrement = false;
				colvarContactId.IsNullable = false;
				colvarContactId.IsPrimaryKey = false;
				colvarContactId.IsForeignKey = true;
				colvarContactId.IsReadOnly = false;
				colvarContactId.DefaultSetting = @"";
				colvarContactId.ForeignKeyTableName = "CX_Contacts";
				schema.Columns.Add(colvarContactId);

				TableSchema.TableColumn colvarNote = new TableSchema.TableColumn(schema);
				colvarNote.ColumnName = "Note";
				colvarNote.DataType = DbType.String;
				colvarNote.MaxLength = -1;
				colvarNote.AutoIncrement = false;
				colvarNote.IsNullable = false;
				colvarNote.IsPrimaryKey = false;
				colvarNote.IsForeignKey = false;
				colvarNote.IsReadOnly = false;
				colvarNote.DefaultSetting = @"";
				colvarNote.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNote);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"(N'SYSTEM')";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				BaseSchema = schema;
				DataService.Providers["NxseConnextProvider"].AddSchema("CX_ContactNotes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static CX_ContactNote LoadFrom(CX_ContactNote item)
		{
			CX_ContactNote result = new CX_ContactNote();
			if (item.ContactNoteID != default(long)) {
				result.LoadByKey(item.ContactNoteID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ContactNoteID {
			get { return GetColumnValue<long>(Columns.ContactNoteID); }
			set {
				SetColumnValue(Columns.ContactNoteID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactNoteID));
			}
		}
		[DataMember]
		public long ContactId {
			get { return GetColumnValue<long>(Columns.ContactId); }
			set {
				SetColumnValue(Columns.ContactId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactId));
			}
		}
		[DataMember]
		public string Note {
			get { return GetColumnValue<string>(Columns.Note); }
			set {
				SetColumnValue(Columns.Note, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Note));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private CX_Contact _Contact;
		//Relationship: FK_CX_ContactNotes_CX_Contacts
		public CX_Contact Contact
		{
			get
			{
				if(_Contact == null) {
					_Contact = CX_Contact.FetchByID(this.ContactId);
				}
				return _Contact;
			}
			set
			{
				SetColumnValue("ContactId", value.ContactID);
				_Contact = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ContactNoteID.ToString();
		}

		#region Typed Columns

		public static TableSchema.TableColumn ContactNoteIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ContactIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn NoteColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[4]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ContactNoteID = @"ContactNoteID";
			public static readonly string ContactId = @"ContactId";
			public static readonly string Note = @"Note";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ContactNoteID; }
		}
		*/


	}
	/// <summary>
	/// Strongly-typed collection for the CX_Contact class.
	/// </summary>
	[DataContract]
	public partial class CX_ContactCollection : ActiveList<CX_Contact, CX_ContactCollection>
	{
		public static CX_ContactCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			CX_ContactCollection result = new CX_ContactCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (CX_Contact item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the CX_Contacts table.
	/// </summary>
	[DataContract]
	public partial class CX_Contact : ActiveRecord<CX_Contact>, INotifyPropertyChanged
	{


		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public CX_Contact()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("CX_Contacts", TableType.Table, DataService.GetInstance("NxseConnextProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarContactID = new TableSchema.TableColumn(schema);
				colvarContactID.ColumnName = "ContactID";
				colvarContactID.DataType = DbType.Int64;
				colvarContactID.MaxLength = 0;
				colvarContactID.AutoIncrement = true;
				colvarContactID.IsNullable = false;
				colvarContactID.IsPrimaryKey = true;
				colvarContactID.IsForeignKey = false;
				colvarContactID.IsReadOnly = false;
				colvarContactID.DefaultSetting = @"";
				colvarContactID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactID);

				TableSchema.TableColumn colvarContactTypeId = new TableSchema.TableColumn(schema);
				colvarContactTypeId.ColumnName = "ContactTypeId";
				colvarContactTypeId.DataType = DbType.AnsiString;
				colvarContactTypeId.MaxLength = 10;
				colvarContactTypeId.AutoIncrement = false;
				colvarContactTypeId.IsNullable = false;
				colvarContactTypeId.IsPrimaryKey = false;
				colvarContactTypeId.IsForeignKey = true;
				colvarContactTypeId.IsReadOnly = false;
				colvarContactTypeId.DefaultSetting = @"";
				colvarContactTypeId.ForeignKeyTableName = "CX_ContactTypes";
				schema.Columns.Add(colvarContactTypeId);

				TableSchema.TableColumn colvarAddressId = new TableSchema.TableColumn(schema);
				colvarAddressId.ColumnName = "AddressId";
				colvarAddressId.DataType = DbType.Int64;
				colvarAddressId.MaxLength = 0;
				colvarAddressId.AutoIncrement = false;
				colvarAddressId.IsNullable = false;
				colvarAddressId.IsPrimaryKey = false;
				colvarAddressId.IsForeignKey = true;
				colvarAddressId.IsReadOnly = false;
				colvarAddressId.DefaultSetting = @"";
				colvarAddressId.ForeignKeyTableName = "CX_Address";
				schema.Columns.Add(colvarAddressId);

				TableSchema.TableColumn colvarDealerId = new TableSchema.TableColumn(schema);
				colvarDealerId.ColumnName = "DealerId";
				colvarDealerId.DataType = DbType.Int32;
				colvarDealerId.MaxLength = 0;
				colvarDealerId.AutoIncrement = false;
				colvarDealerId.IsNullable = false;
				colvarDealerId.IsPrimaryKey = false;
				colvarDealerId.IsForeignKey = false;
				colvarDealerId.IsReadOnly = false;
				colvarDealerId.DefaultSetting = @"((5000))";
				colvarDealerId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDealerId);

				TableSchema.TableColumn colvarLocalizationId = new TableSchema.TableColumn(schema);
				colvarLocalizationId.ColumnName = "LocalizationId";
				colvarLocalizationId.DataType = DbType.AnsiString;
				colvarLocalizationId.MaxLength = 20;
				colvarLocalizationId.AutoIncrement = false;
				colvarLocalizationId.IsNullable = false;
				colvarLocalizationId.IsPrimaryKey = false;
				colvarLocalizationId.IsForeignKey = false;
				colvarLocalizationId.IsReadOnly = false;
				colvarLocalizationId.DefaultSetting = @"";
				colvarLocalizationId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLocalizationId);

				TableSchema.TableColumn colvarTeamLocationId = new TableSchema.TableColumn(schema);
				colvarTeamLocationId.ColumnName = "TeamLocationId";
				colvarTeamLocationId.DataType = DbType.Int32;
				colvarTeamLocationId.MaxLength = 0;
				colvarTeamLocationId.AutoIncrement = false;
				colvarTeamLocationId.IsNullable = false;
				colvarTeamLocationId.IsPrimaryKey = false;
				colvarTeamLocationId.IsForeignKey = false;
				colvarTeamLocationId.IsReadOnly = false;
				colvarTeamLocationId.DefaultSetting = @"";
				colvarTeamLocationId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarTeamLocationId);

				TableSchema.TableColumn colvarSeasonId = new TableSchema.TableColumn(schema);
				colvarSeasonId.ColumnName = "SeasonId";
				colvarSeasonId.DataType = DbType.Int32;
				colvarSeasonId.MaxLength = 0;
				colvarSeasonId.AutoIncrement = false;
				colvarSeasonId.IsNullable = false;
				colvarSeasonId.IsPrimaryKey = false;
				colvarSeasonId.IsForeignKey = false;
				colvarSeasonId.IsReadOnly = false;
				colvarSeasonId.DefaultSetting = @"";
				colvarSeasonId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeasonId);

				TableSchema.TableColumn colvarSalesRepId = new TableSchema.TableColumn(schema);
				colvarSalesRepId.ColumnName = "SalesRepId";
				colvarSalesRepId.DataType = DbType.AnsiString;
				colvarSalesRepId.MaxLength = 10;
				colvarSalesRepId.AutoIncrement = false;
				colvarSalesRepId.IsNullable = false;
				colvarSalesRepId.IsPrimaryKey = false;
				colvarSalesRepId.IsForeignKey = false;
				colvarSalesRepId.IsReadOnly = false;
				colvarSalesRepId.DefaultSetting = @"";
				colvarSalesRepId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalesRepId);

				TableSchema.TableColumn colvarSalutation = new TableSchema.TableColumn(schema);
				colvarSalutation.ColumnName = "Salutation";
				colvarSalutation.DataType = DbType.String;
				colvarSalutation.MaxLength = 50;
				colvarSalutation.AutoIncrement = false;
				colvarSalutation.IsNullable = true;
				colvarSalutation.IsPrimaryKey = false;
				colvarSalutation.IsForeignKey = false;
				colvarSalutation.IsReadOnly = false;
				colvarSalutation.DefaultSetting = @"";
				colvarSalutation.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSalutation);

				TableSchema.TableColumn colvarFirstName = new TableSchema.TableColumn(schema);
				colvarFirstName.ColumnName = "FirstName";
				colvarFirstName.DataType = DbType.String;
				colvarFirstName.MaxLength = 50;
				colvarFirstName.AutoIncrement = false;
				colvarFirstName.IsNullable = false;
				colvarFirstName.IsPrimaryKey = false;
				colvarFirstName.IsForeignKey = false;
				colvarFirstName.IsReadOnly = false;
				colvarFirstName.DefaultSetting = @"";
				colvarFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstName);

				TableSchema.TableColumn colvarMiddleName = new TableSchema.TableColumn(schema);
				colvarMiddleName.ColumnName = "MiddleName";
				colvarMiddleName.DataType = DbType.String;
				colvarMiddleName.MaxLength = 50;
				colvarMiddleName.AutoIncrement = false;
				colvarMiddleName.IsNullable = true;
				colvarMiddleName.IsPrimaryKey = false;
				colvarMiddleName.IsForeignKey = false;
				colvarMiddleName.IsReadOnly = false;
				colvarMiddleName.DefaultSetting = @"";
				colvarMiddleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMiddleName);

				TableSchema.TableColumn colvarLastName = new TableSchema.TableColumn(schema);
				colvarLastName.ColumnName = "LastName";
				colvarLastName.DataType = DbType.String;
				colvarLastName.MaxLength = 50;
				colvarLastName.AutoIncrement = false;
				colvarLastName.IsNullable = false;
				colvarLastName.IsPrimaryKey = false;
				colvarLastName.IsForeignKey = false;
				colvarLastName.IsReadOnly = false;
				colvarLastName.DefaultSetting = @"";
				colvarLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastName);

				TableSchema.TableColumn colvarSuffix = new TableSchema.TableColumn(schema);
				colvarSuffix.ColumnName = "Suffix";
				colvarSuffix.DataType = DbType.String;
				colvarSuffix.MaxLength = 50;
				colvarSuffix.AutoIncrement = false;
				colvarSuffix.IsNullable = true;
				colvarSuffix.IsPrimaryKey = false;
				colvarSuffix.IsForeignKey = false;
				colvarSuffix.IsReadOnly = false;
				colvarSuffix.DefaultSetting = @"";
				colvarSuffix.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSuffix);

				TableSchema.TableColumn colvarGender = new TableSchema.TableColumn(schema);
				colvarGender.ColumnName = "Gender";
				colvarGender.DataType = DbType.String;
				colvarGender.MaxLength = 10;
				colvarGender.AutoIncrement = false;
				colvarGender.IsNullable = false;
				colvarGender.IsPrimaryKey = false;
				colvarGender.IsForeignKey = false;
				colvarGender.IsReadOnly = false;
				colvarGender.DefaultSetting = @"";
				colvarGender.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGender);

				TableSchema.TableColumn colvarSSN = new TableSchema.TableColumn(schema);
				colvarSSN.ColumnName = "SSN";
				colvarSSN.DataType = DbType.AnsiString;
				colvarSSN.MaxLength = 50;
				colvarSSN.AutoIncrement = false;
				colvarSSN.IsNullable = true;
				colvarSSN.IsPrimaryKey = false;
				colvarSSN.IsForeignKey = false;
				colvarSSN.IsReadOnly = false;
				colvarSSN.DefaultSetting = @"";
				colvarSSN.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSSN);

				TableSchema.TableColumn colvarDOB = new TableSchema.TableColumn(schema);
				colvarDOB.ColumnName = "DOB";
				colvarDOB.DataType = DbType.DateTime;
				colvarDOB.MaxLength = 0;
				colvarDOB.AutoIncrement = false;
				colvarDOB.IsNullable = true;
				colvarDOB.IsPrimaryKey = false;
				colvarDOB.IsForeignKey = false;
				colvarDOB.IsReadOnly = false;
				colvarDOB.DefaultSetting = @"";
				colvarDOB.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDOB);

				TableSchema.TableColumn colvarEmail = new TableSchema.TableColumn(schema);
				colvarEmail.ColumnName = "Email";
				colvarEmail.DataType = DbType.String;
				colvarEmail.MaxLength = 256;
				colvarEmail.AutoIncrement = false;
				colvarEmail.IsNullable = true;
				colvarEmail.IsPrimaryKey = false;
				colvarEmail.IsForeignKey = false;
				colvarEmail.IsReadOnly = false;
				colvarEmail.DefaultSetting = @"";
				colvarEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmail);

				TableSchema.TableColumn colvarPhoneHome = new TableSchema.TableColumn(schema);
				colvarPhoneHome.ColumnName = "PhoneHome";
				colvarPhoneHome.DataType = DbType.String;
				colvarPhoneHome.MaxLength = 20;
				colvarPhoneHome.AutoIncrement = false;
				colvarPhoneHome.IsNullable = true;
				colvarPhoneHome.IsPrimaryKey = false;
				colvarPhoneHome.IsForeignKey = false;
				colvarPhoneHome.IsReadOnly = false;
				colvarPhoneHome.DefaultSetting = @"";
				colvarPhoneHome.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneHome);

				TableSchema.TableColumn colvarPhoneWork = new TableSchema.TableColumn(schema);
				colvarPhoneWork.ColumnName = "PhoneWork";
				colvarPhoneWork.DataType = DbType.String;
				colvarPhoneWork.MaxLength = 30;
				colvarPhoneWork.AutoIncrement = false;
				colvarPhoneWork.IsNullable = true;
				colvarPhoneWork.IsPrimaryKey = false;
				colvarPhoneWork.IsForeignKey = false;
				colvarPhoneWork.IsReadOnly = false;
				colvarPhoneWork.DefaultSetting = @"";
				colvarPhoneWork.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneWork);

				TableSchema.TableColumn colvarPhoneMobile = new TableSchema.TableColumn(schema);
				colvarPhoneMobile.ColumnName = "PhoneMobile";
				colvarPhoneMobile.DataType = DbType.String;
				colvarPhoneMobile.MaxLength = 20;
				colvarPhoneMobile.AutoIncrement = false;
				colvarPhoneMobile.IsNullable = true;
				colvarPhoneMobile.IsPrimaryKey = false;
				colvarPhoneMobile.IsForeignKey = false;
				colvarPhoneMobile.IsReadOnly = false;
				colvarPhoneMobile.DefaultSetting = @"";
				colvarPhoneMobile.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneMobile);

				TableSchema.TableColumn colvarCurrentSystem = new TableSchema.TableColumn(schema);
				colvarCurrentSystem.ColumnName = "CurrentSystem";
				colvarCurrentSystem.DataType = DbType.AnsiString;
				colvarCurrentSystem.MaxLength = 50;
				colvarCurrentSystem.AutoIncrement = false;
				colvarCurrentSystem.IsNullable = true;
				colvarCurrentSystem.IsPrimaryKey = false;
				colvarCurrentSystem.IsForeignKey = false;
				colvarCurrentSystem.IsReadOnly = false;
				colvarCurrentSystem.DefaultSetting = @"";
				colvarCurrentSystem.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCurrentSystem);

				TableSchema.TableColumn colvarIsActive = new TableSchema.TableColumn(schema);
				colvarIsActive.ColumnName = "IsActive";
				colvarIsActive.DataType = DbType.Boolean;
				colvarIsActive.MaxLength = 0;
				colvarIsActive.AutoIncrement = false;
				colvarIsActive.IsNullable = false;
				colvarIsActive.IsPrimaryKey = false;
				colvarIsActive.IsForeignKey = false;
				colvarIsActive.IsReadOnly = false;
				colvarIsActive.DefaultSetting = @"((1))";
				colvarIsActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsActive);

				TableSchema.TableColumn colvarIsDeleted = new TableSchema.TableColumn(schema);
				colvarIsDeleted.ColumnName = "IsDeleted";
				colvarIsDeleted.DataType = DbType.Boolean;
				colvarIsDeleted.MaxLength = 0;
				colvarIsDeleted.AutoIncrement = false;
				colvarIsDeleted.IsNullable = false;
				colvarIsDeleted.IsPrimaryKey = false;
				colvarIsDeleted.IsForeignKey = false;
				colvarIsDeleted.IsReadOnly = false;
				colvarIsDeleted.DefaultSetting = @"((0))";
				colvarIsDeleted.ForeignKeyTableName = "";
				schema.Columns.Add(colvarIsDeleted);

				TableSchema.TableColumn colvarCreatedOn = new TableSchema.TableColumn(schema);
				colvarCreatedOn.ColumnName = "CreatedOn";
				colvarCreatedOn.DataType = DbType.DateTime;
				colvarCreatedOn.MaxLength = 0;
				colvarCreatedOn.AutoIncrement = false;
				colvarCreatedOn.IsNullable = false;
				colvarCreatedOn.IsPrimaryKey = false;
				colvarCreatedOn.IsForeignKey = false;
				colvarCreatedOn.IsReadOnly = false;
				colvarCreatedOn.DefaultSetting = @"(getutcdate())";
				colvarCreatedOn.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedOn);

				TableSchema.TableColumn colvarCreatedBy = new TableSchema.TableColumn(schema);
				colvarCreatedBy.ColumnName = "CreatedBy";
				colvarCreatedBy.DataType = DbType.String;
				colvarCreatedBy.MaxLength = 50;
				colvarCreatedBy.AutoIncrement = false;
				colvarCreatedBy.IsNullable = false;
				colvarCreatedBy.IsPrimaryKey = false;
				colvarCreatedBy.IsForeignKey = false;
				colvarCreatedBy.IsReadOnly = false;
				colvarCreatedBy.DefaultSetting = @"(N'SYSTEM')";
				colvarCreatedBy.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreatedBy);

				TableSchema.TableColumn colvarDEX_ROW_TS = new TableSchema.TableColumn(schema);
				colvarDEX_ROW_TS.ColumnName = "DEX_ROW_TS";
				colvarDEX_ROW_TS.DataType = DbType.DateTime;
				colvarDEX_ROW_TS.MaxLength = 0;
				colvarDEX_ROW_TS.AutoIncrement = false;
				colvarDEX_ROW_TS.IsNullable = false;
				colvarDEX_ROW_TS.IsPrimaryKey = false;
				colvarDEX_ROW_TS.IsForeignKey = false;
				colvarDEX_ROW_TS.IsReadOnly = false;
				colvarDEX_ROW_TS.DefaultSetting = @"(getutcdate())";
				colvarDEX_ROW_TS.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDEX_ROW_TS);

				BaseSchema = schema;
				DataService.Providers["NxseConnextProvider"].AddSchema("CX_Contacts",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static CX_Contact LoadFrom(CX_Contact item)
		{
			CX_Contact result = new CX_Contact();
			if (item.ContactID != default(long)) {
				result.LoadByKey(item.ContactID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public long ContactID {
			get { return GetColumnValue<long>(Columns.ContactID); }
			set {
				SetColumnValue(Columns.ContactID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactID));
			}
		}
		[DataMember]
		public string ContactTypeId {
			get { return GetColumnValue<string>(Columns.ContactTypeId); }
			set {
				SetColumnValue(Columns.ContactTypeId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactTypeId));
			}
		}
		[DataMember]
		public long AddressId {
			get { return GetColumnValue<long>(Columns.AddressId); }
			set {
				SetColumnValue(Columns.AddressId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.AddressId));
			}
		}
		[DataMember]
		public int DealerId {
			get { return GetColumnValue<int>(Columns.DealerId); }
			set {
				SetColumnValue(Columns.DealerId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DealerId));
			}
		}
		[DataMember]
		public string LocalizationId {
			get { return GetColumnValue<string>(Columns.LocalizationId); }
			set {
				SetColumnValue(Columns.LocalizationId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LocalizationId));
			}
		}
		[DataMember]
		public int TeamLocationId {
			get { return GetColumnValue<int>(Columns.TeamLocationId); }
			set {
				SetColumnValue(Columns.TeamLocationId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.TeamLocationId));
			}
		}
		[DataMember]
		public int SeasonId {
			get { return GetColumnValue<int>(Columns.SeasonId); }
			set {
				SetColumnValue(Columns.SeasonId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SeasonId));
			}
		}
		[DataMember]
		public string SalesRepId {
			get { return GetColumnValue<string>(Columns.SalesRepId); }
			set {
				SetColumnValue(Columns.SalesRepId, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SalesRepId));
			}
		}
		[DataMember]
		public string Salutation {
			get { return GetColumnValue<string>(Columns.Salutation); }
			set {
				SetColumnValue(Columns.Salutation, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Salutation));
			}
		}
		[DataMember]
		public string FirstName {
			get { return GetColumnValue<string>(Columns.FirstName); }
			set {
				SetColumnValue(Columns.FirstName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.FirstName));
			}
		}
		[DataMember]
		public string MiddleName {
			get { return GetColumnValue<string>(Columns.MiddleName); }
			set {
				SetColumnValue(Columns.MiddleName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.MiddleName));
			}
		}
		[DataMember]
		public string LastName {
			get { return GetColumnValue<string>(Columns.LastName); }
			set {
				SetColumnValue(Columns.LastName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.LastName));
			}
		}
		[DataMember]
		public string Suffix {
			get { return GetColumnValue<string>(Columns.Suffix); }
			set {
				SetColumnValue(Columns.Suffix, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Suffix));
			}
		}
		[DataMember]
		public string Gender {
			get { return GetColumnValue<string>(Columns.Gender); }
			set {
				SetColumnValue(Columns.Gender, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Gender));
			}
		}
		[DataMember]
		public string SSN {
			get { return GetColumnValue<string>(Columns.SSN); }
			set {
				SetColumnValue(Columns.SSN, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.SSN));
			}
		}
		[DataMember]
		public DateTime? DOB {
			get { return GetColumnValue<DateTime?>(Columns.DOB); }
			set {
				SetColumnValue(Columns.DOB, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DOB));
			}
		}
		[DataMember]
		public string Email {
			get { return GetColumnValue<string>(Columns.Email); }
			set {
				SetColumnValue(Columns.Email, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.Email));
			}
		}
		[DataMember]
		public string PhoneHome {
			get { return GetColumnValue<string>(Columns.PhoneHome); }
			set {
				SetColumnValue(Columns.PhoneHome, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PhoneHome));
			}
		}
		[DataMember]
		public string PhoneWork {
			get { return GetColumnValue<string>(Columns.PhoneWork); }
			set {
				SetColumnValue(Columns.PhoneWork, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PhoneWork));
			}
		}
		[DataMember]
		public string PhoneMobile {
			get { return GetColumnValue<string>(Columns.PhoneMobile); }
			set {
				SetColumnValue(Columns.PhoneMobile, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.PhoneMobile));
			}
		}
		[DataMember]
		public string CurrentSystem {
			get { return GetColumnValue<string>(Columns.CurrentSystem); }
			set {
				SetColumnValue(Columns.CurrentSystem, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CurrentSystem));
			}
		}
		[DataMember]
		public bool IsActive {
			get { return GetColumnValue<bool>(Columns.IsActive); }
			set {
				SetColumnValue(Columns.IsActive, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsActive));
			}
		}
		[DataMember]
		public bool IsDeleted {
			get { return GetColumnValue<bool>(Columns.IsDeleted); }
			set {
				SetColumnValue(Columns.IsDeleted, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.IsDeleted));
			}
		}
		[DataMember]
		public DateTime CreatedOn {
			get { return GetColumnValue<DateTime>(Columns.CreatedOn); }
			set {
				SetColumnValue(Columns.CreatedOn, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedOn));
			}
		}
		[DataMember]
		public string CreatedBy {
			get { return GetColumnValue<string>(Columns.CreatedBy); }
			set {
				SetColumnValue(Columns.CreatedBy, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.CreatedBy));
			}
		}
		[DataMember]
		public DateTime DEX_ROW_TS {
			get { return GetColumnValue<DateTime>(Columns.DEX_ROW_TS); }
			set {
				SetColumnValue(Columns.DEX_ROW_TS, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.DEX_ROW_TS));
			}
		}

		#endregion //Properties

		#region ForeignKey Properties

		private CX_Address _Address;
		//Relationship: FK_CX_Contacts_CX_Address
		public CX_Address Address
		{
			get
			{
				if(_Address == null) {
					_Address = CX_Address.FetchByID(this.AddressId);
				}
				return _Address;
			}
			set
			{
				SetColumnValue("AddressId", value.AddressID);
				_Address = value;
			}
		}

		private CX_ContactType _ContactType;
		//Relationship: FK_CX_Contacts_CX_ContactTypes
		public CX_ContactType ContactType
		{
			get
			{
				if(_ContactType == null) {
					_ContactType = CX_ContactType.FetchByID(this.ContactTypeId);
				}
				return _ContactType;
			}
			set
			{
				SetColumnValue("ContactTypeId", value.ContactTypeID);
				_ContactType = value;
			}
		}

		#endregion //ForeignKey Properties

		public override string ToString()
		{
			return ContactTypeId;
		}

		#region Typed Columns

		public static TableSchema.TableColumn ContactIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ContactTypeIdColumn
		{
			get { return Schema.Columns[1]; }
		}
		public static TableSchema.TableColumn AddressIdColumn
		{
			get { return Schema.Columns[2]; }
		}
		public static TableSchema.TableColumn DealerIdColumn
		{
			get { return Schema.Columns[3]; }
		}
		public static TableSchema.TableColumn LocalizationIdColumn
		{
			get { return Schema.Columns[4]; }
		}
		public static TableSchema.TableColumn TeamLocationIdColumn
		{
			get { return Schema.Columns[5]; }
		}
		public static TableSchema.TableColumn SeasonIdColumn
		{
			get { return Schema.Columns[6]; }
		}
		public static TableSchema.TableColumn SalesRepIdColumn
		{
			get { return Schema.Columns[7]; }
		}
		public static TableSchema.TableColumn SalutationColumn
		{
			get { return Schema.Columns[8]; }
		}
		public static TableSchema.TableColumn FirstNameColumn
		{
			get { return Schema.Columns[9]; }
		}
		public static TableSchema.TableColumn MiddleNameColumn
		{
			get { return Schema.Columns[10]; }
		}
		public static TableSchema.TableColumn LastNameColumn
		{
			get { return Schema.Columns[11]; }
		}
		public static TableSchema.TableColumn SuffixColumn
		{
			get { return Schema.Columns[12]; }
		}
		public static TableSchema.TableColumn GenderColumn
		{
			get { return Schema.Columns[13]; }
		}
		public static TableSchema.TableColumn SSNColumn
		{
			get { return Schema.Columns[14]; }
		}
		public static TableSchema.TableColumn DOBColumn
		{
			get { return Schema.Columns[15]; }
		}
		public static TableSchema.TableColumn EmailColumn
		{
			get { return Schema.Columns[16]; }
		}
		public static TableSchema.TableColumn PhoneHomeColumn
		{
			get { return Schema.Columns[17]; }
		}
		public static TableSchema.TableColumn PhoneWorkColumn
		{
			get { return Schema.Columns[18]; }
		}
		public static TableSchema.TableColumn PhoneMobileColumn
		{
			get { return Schema.Columns[19]; }
		}
		public static TableSchema.TableColumn CurrentSystemColumn
		{
			get { return Schema.Columns[20]; }
		}
		public static TableSchema.TableColumn IsActiveColumn
		{
			get { return Schema.Columns[21]; }
		}
		public static TableSchema.TableColumn IsDeletedColumn
		{
			get { return Schema.Columns[22]; }
		}
		public static TableSchema.TableColumn CreatedOnColumn
		{
			get { return Schema.Columns[23]; }
		}
		public static TableSchema.TableColumn CreatedByColumn
		{
			get { return Schema.Columns[24]; }
		}
		public static TableSchema.TableColumn DEX_ROW_TSColumn
		{
			get { return Schema.Columns[25]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ContactID = @"ContactID";
			public static readonly string ContactTypeId = @"ContactTypeId";
			public static readonly string AddressId = @"AddressId";
			public static readonly string DealerId = @"DealerId";
			public static readonly string LocalizationId = @"LocalizationId";
			public static readonly string TeamLocationId = @"TeamLocationId";
			public static readonly string SeasonId = @"SeasonId";
			public static readonly string SalesRepId = @"SalesRepId";
			public static readonly string Salutation = @"Salutation";
			public static readonly string FirstName = @"FirstName";
			public static readonly string MiddleName = @"MiddleName";
			public static readonly string LastName = @"LastName";
			public static readonly string Suffix = @"Suffix";
			public static readonly string Gender = @"Gender";
			public static readonly string SSN = @"SSN";
			public static readonly string DOB = @"DOB";
			public static readonly string Email = @"Email";
			public static readonly string PhoneHome = @"PhoneHome";
			public static readonly string PhoneWork = @"PhoneWork";
			public static readonly string PhoneMobile = @"PhoneMobile";
			public static readonly string CurrentSystem = @"CurrentSystem";
			public static readonly string IsActive = @"IsActive";
			public static readonly string IsDeleted = @"IsDeleted";
			public static readonly string CreatedOn = @"CreatedOn";
			public static readonly string CreatedBy = @"CreatedBy";
			public static readonly string DEX_ROW_TS = @"DEX_ROW_TS";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ContactID; }
		}
		*/

		#region Foreign Collections

		private CX_AppointmentCollection _CX_AppointmentsCol;
		//Relationship: FK_CX_Appointments_CX_Contacts
		public CX_AppointmentCollection CX_AppointmentsCol
		{
			get
			{
				if(_CX_AppointmentsCol == null) {
					_CX_AppointmentsCol = new CX_AppointmentCollection();
					_CX_AppointmentsCol.LoadAndCloseReader(CX_Appointment.Query()
						.WHERE(CX_Appointment.Columns.ContactId, ContactID).ExecuteReader());
				}
				return _CX_AppointmentsCol;
			}
		}

		private CX_ContactNoteCollection _CX_ContactNotesCol;
		//Relationship: FK_CX_ContactNotes_CX_Contacts
		public CX_ContactNoteCollection CX_ContactNotesCol
		{
			get
			{
				if(_CX_ContactNotesCol == null) {
					_CX_ContactNotesCol = new CX_ContactNoteCollection();
					_CX_ContactNotesCol.LoadAndCloseReader(CX_ContactNote.Query()
						.WHERE(CX_ContactNote.Columns.ContactId, ContactID).ExecuteReader());
				}
				return _CX_ContactNotesCol;
			}
		}

		#endregion Foreign Collections

	}
	/// <summary>
	/// Strongly-typed collection for the CX_ContactType class.
	/// </summary>
	[DataContract]
	public partial class CX_ContactTypeCollection : ActiveList<CX_ContactType, CX_ContactTypeCollection>
	{
		public static CX_ContactTypeCollection LoadByStoredProcedure(StoredProcedure sp)
		{
			CX_ContactTypeCollection result = new CX_ContactTypeCollection();
			result.LoadAndCloseReader(sp.GetReader());
			return result;
		}
		public string GetInList(string columnName)
		{
			return JoinColumnList(columnName, ",");
		}
		public string JoinColumnList(string columnName, string seperator)
		{
			return SOS.Lib.Util.StringHelper.Join(GetJoinColumnList(columnName), seperator);
		}
		public IEnumerable<object> GetJoinColumnList(string columnName)
		{
			foreach (CX_ContactType item in this) {
				object value = item.GetColumnValue<object>(columnName);
				if (value != null) {
					yield return value;
				}
			}
		}
	}

	/// <summary>
	/// This is an ActiveRecord class which wraps the CX_ContactTypes table.
	/// </summary>
	[DataContract]
	public partial class CX_ContactType : ActiveRecord<CX_ContactType>, INotifyPropertyChanged
	{

		#region MetaData
		[DataContract]
		public static class MetaData
		{
		}
		#endregion MetaData

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}
		#endregion Events

		#region .ctors and Default Settings

		public CX_ContactType()
		{
			SetSQLProps();InitSetDefaults();MarkNew();
		}
		private void InitSetDefaults() { SetDefaults(); }
		protected static void SetSQLProps() { GetTableSchema(); }

		#endregion

		#region Schema and Query Accessor
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get {
				if (BaseSchema == null) SetSQLProps();
				return BaseSchema;
			}
		}
		private static void GetTableSchema()
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("CX_ContactTypes", TableType.Table, DataService.GetInstance("NxseConnextProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns

				TableSchema.TableColumn colvarContactTypeID = new TableSchema.TableColumn(schema);
				colvarContactTypeID.ColumnName = "ContactTypeID";
				colvarContactTypeID.DataType = DbType.AnsiString;
				colvarContactTypeID.MaxLength = 10;
				colvarContactTypeID.AutoIncrement = false;
				colvarContactTypeID.IsNullable = false;
				colvarContactTypeID.IsPrimaryKey = true;
				colvarContactTypeID.IsForeignKey = false;
				colvarContactTypeID.IsReadOnly = false;
				colvarContactTypeID.DefaultSetting = @"";
				colvarContactTypeID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactTypeID);

				TableSchema.TableColumn colvarContactTypeName = new TableSchema.TableColumn(schema);
				colvarContactTypeName.ColumnName = "ContactTypeName";
				colvarContactTypeName.DataType = DbType.AnsiString;
				colvarContactTypeName.MaxLength = 50;
				colvarContactTypeName.AutoIncrement = false;
				colvarContactTypeName.IsNullable = false;
				colvarContactTypeName.IsPrimaryKey = false;
				colvarContactTypeName.IsForeignKey = false;
				colvarContactTypeName.IsReadOnly = false;
				colvarContactTypeName.DefaultSetting = @"";
				colvarContactTypeName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactTypeName);

				BaseSchema = schema;
				DataService.Providers["NxseConnextProvider"].AddSchema("CX_ContactTypes",schema);
			}
		}
		#endregion // Schema and Query Accessor

		public static CX_ContactType LoadFrom(CX_ContactType item)
		{
			CX_ContactType result = new CX_ContactType();
			if (item.ContactTypeID != default(string)) {
				result.LoadByKey(item.ContactTypeID);
			}
			result.CopyFrom(item);
			return result;
		}

		#region Properties
		[DataMember]
		public string ContactTypeID {
			get { return GetColumnValue<string>(Columns.ContactTypeID); }
			set {
				SetColumnValue(Columns.ContactTypeID, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactTypeID));
			}
		}
		[DataMember]
		public string ContactTypeName {
			get { return GetColumnValue<string>(Columns.ContactTypeName); }
			set {
				SetColumnValue(Columns.ContactTypeName, value);
				OnPropertyChanged(new PropertyChangedEventArgs(Columns.ContactTypeName));
			}
		}

		#endregion //Properties


		public override string ToString()
		{
			return ContactTypeName;
		}

		#region Typed Columns

		public static TableSchema.TableColumn ContactTypeIDColumn
		{
			get { return Schema.Columns[0]; }
		}
		public static TableSchema.TableColumn ContactTypeNameColumn
		{
			get { return Schema.Columns[1]; }
		}

		#endregion

		#region Columns Struct
		public struct Columns
		{
			public static readonly string ContactTypeID = @"ContactTypeID";
			public static readonly string ContactTypeName = @"ContactTypeName";
		}
		#endregion Columns Struct

		/*
		public override object PrimaryKeyValue
		{
			get { return ContactTypeID; }
		}
		*/

		#region Foreign Collections

		private CX_ContactCollection _CX_ContactsCol;
		//Relationship: FK_CX_Contacts_CX_ContactTypes
		public CX_ContactCollection CX_ContactsCol
		{
			get
			{
				if(_CX_ContactsCol == null) {
					_CX_ContactsCol = new CX_ContactCollection();
					_CX_ContactsCol.LoadAndCloseReader(CX_Contact.Query()
						.WHERE(CX_Contact.Columns.ContactTypeId, ContactTypeID).ExecuteReader());
				}
				return _CX_ContactsCol;
			}
		}

		#endregion Foreign Collections

	}
}
