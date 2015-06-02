<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<!--
SQL Data Compare
SQL Data Compare
Version:10.4.8.62-->
<Project version="2" type="SQLComparisonToolsProject">
  <DataSource1 version="3" type="LiveDatabaseSource">
    <ServerName>db1.dev.nexsense.com</ServerName>
    <DatabaseName>WISE_HumanResource</DatabaseName>
    <Username>sa</Username>
    <SavePassword>False</SavePassword>
    <Password />
    <ScriptFolderLocation />
    <MigrationsFolderLocation />
    <IntegratedSecurity>False</IntegratedSecurity>
  </DataSource1>
  <DataSource2 version="3" type="LiveDatabaseSource">
    <ServerName>db1.stg.nexsense.com</ServerName>
    <DatabaseName>WISE_HumanResource</DatabaseName>
    <Username>sa</Username>
    <SavePassword>True</SavePassword>
    <Password encrypted="1">lof8lOoJ2pAV+FE+QwydUQ==</Password>
    <ScriptFolderLocation />
    <MigrationsFolderLocation />
    <IntegratedSecurity>False</IntegratedSecurity>
  </DataSource2>
  <LastCompared>05/15/2015 18:24:00</LastCompared>
  <Options>Default, CaseSensitiveObjectDefinition, ForceColumnOrder</Options>
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
      <ExtendedProperty version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </ExtendedProperty>
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
      <SearchPropertyList version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </SearchPropertyList>
      <Sequence version="1">
        <Include>True</Include>
        <Expression>TRUE</Expression>
      </Sequence>
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
      <value type="Byte">0</value>
    </Grouping>
    <SelectAll>False</SelectAll>
  </SelectedSyncObjects>
  <SCGroupingStyle>0</SCGroupingStyle>
  <SQLOptions>10</SQLOptions>
  <MappingOptions>82</MappingOptions>
  <ComparisonOptions>0</ComparisonOptions>
  <TableActions type="ArrayList" version="1">
    <value version="1" type="SelectTableEvent">
      <action>DeselectAll</action>
    </value>
    <value version="1" type="SelectTableEvent">
      <action>SelectItem</action>
      <val>[dbo].[RU_Payscales]:[dbo].[RU_Payscales]</val>
    </value>
  </TableActions>
  <SessionSettings>15</SessionSettings>
  <DCGroupingStyle>0</DCGroupingStyle>
</Project>