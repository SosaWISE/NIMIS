<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<!--
SQL Data Compare
SQL Data Compare 9
Version:9.0.0.117-->
<Project version="1" type="SQLComparisonToolsProject">
  <DataSource1 version="2" type="LiveDatabaseSource">
    <ServerName>(local)</ServerName>
    <DatabaseName>WISE_GPSTRACKING</DatabaseName>
    <Username />
    <SavePassword>False</SavePassword>
    <Password />
    <IntegratedSecurity>True</IntegratedSecurity>
  </DataSource1>
  <DataSource2 version="2" type="LiveDatabaseSource">
    <ServerName>WAUTDC01</ServerName>
    <DatabaseName>WISE_GPSTRACKING</DatabaseName>
    <Username>sa</Username>
    <SavePassword>True</SavePassword>
    <Password encrypted="1">miIIydXG4aY32EkYkJUMBg==</Password>
    <IntegratedSecurity>False</IntegratedSecurity>
  </DataSource2>
  <LastCompared>12/20/2012 14:29:34</LastCompared>
  <Options>317002946838538</Options>
  <InRecycleBin>False</InRecycleBin>
  <Direction>0</Direction>
  <ProjectFilter version="1" type="DifferenceFilter">
    <FilterCaseSensitive>False</FilterCaseSensitive>
    <Filters version="1">
      <None version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </None>
      <Assembly version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Assembly>
      <AsymmetricKey version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </AsymmetricKey>
      <Certificate version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Certificate>
      <Contract version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Contract>
      <DdlTrigger version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </DdlTrigger>
      <Default version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Default>
      <EventNotification version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </EventNotification>
      <FullTextCatalog version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </FullTextCatalog>
      <FullTextStoplist version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </FullTextStoplist>
      <Function version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Function>
      <MessageType version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </MessageType>
      <PartitionFunction version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </PartitionFunction>
      <PartitionScheme version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </PartitionScheme>
      <Queue version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Queue>
      <Role version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Role>
      <Route version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Route>
      <Rule version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Rule>
      <Schema version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Schema>
      <Service version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Service>
      <ServiceBinding version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </ServiceBinding>
      <StoredProcedure version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </StoredProcedure>
      <SymmetricKey version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </SymmetricKey>
      <Synonym version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Synonym>
      <Table version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Table>
      <User version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </User>
      <UserDefinedType version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </UserDefinedType>
      <View version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </View>
      <XmlSchemaCollection version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </XmlSchemaCollection>
    </Filters>
  </ProjectFilter>
  <ProjectFilterName />
  <UserNote />
  <SelectedSyncObjects version="1" type="SelectedSyncObjects">
    <Schemas type="ListString" version="2" />
    <Grouping type="ListByte" version="2">
      <value type="Byte">0</value>
      <value type="Byte">0</value>
      <value type="Byte">0</value>
      <value type="Byte">0</value>
      <value type="Byte">0</value>
    </Grouping>
    <SelectAll>False</SelectAll>
  </SelectedSyncObjects>
  <SCGroupingStyle>0</SCGroupingStyle>
  <SQLOptions>10</SQLOptions>
  <MappingOptions>90</MappingOptions>
  <ComparisonOptions>0</ComparisonOptions>
  <TableActions type="ArrayList" version="1">
    <value version="1" type="SelectTableEvent">
      <action>DeselectAll</action>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>SelectItem</action>
      <val>[dbo].[GS_AccountGeoFenceTypes]:[dbo].[GS_AccountGeoFenceTypes]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>SelectItem</action>
      <val>[dbo].[LP_CommandNames]:[dbo].[LP_CommandNames]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>SelectItem</action>
      <val>[dbo].[GS_EventTypes]:[dbo].[GS_EventTypes]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>SelectItem</action>
      <val>[dbo].[LP_AVCFGCodes]:[dbo].[LP_AVCFGCodes]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>SelectItem</action>
      <val>[dbo].[LP_CommandTypes]:[dbo].[LP_CommandTypes]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>SelectItem</action>
      <val>[dbo].[LP_RequestNames]:[dbo].[LP_RequestNames]</val>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>SelectItem</action>
      <val>[dbo].[GS_AccountGeoFenceReportModes]:[dbo].[GS_AccountGeoFenceReportModes]</val>
    </value>
  </TableActions>
  <SessionSettings>14</SessionSettings>
  <DCGroupingStyle>0</DCGroupingStyle>
</Project>