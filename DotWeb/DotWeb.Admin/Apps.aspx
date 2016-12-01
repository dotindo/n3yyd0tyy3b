<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Apps.aspx.cs" Inherits="DotWeb.Admin.Apps" %>
<asp:Content ID="pageTitle" ContentPlaceHolderID="PageTitle" runat="server">Applications</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="MainContent" runat="server">
	<dx:ASPxGridView ID="gridView" runat="server" AutoGenerateColumns="true" DataSourceID="entityDataSource" ClientInstanceName="gridView"
		KeyFieldName="Id" OnCustomColumnDisplayText="gridView_CustomColumnDisplayText" CssClass="gridView">
		<Columns>
			<dx:GridViewCommandColumn ShowDeleteButton="true" ShowEditButton="true" ShowNewButtonInHeader="true" VisibleIndex="0"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="Id" Caption="Id">
                <PropertiesTextEdit>
                    <ValidationSettings RequiredField-IsRequired="true" />
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name">
                <PropertiesTextEdit>
                    <ValidationSettings RequiredField-IsRequired="true" />
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Description" Caption="Description">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="GridTextColumnMaxLength" Caption="Grid Text Column Max Length">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PageSize" Caption="Page Size">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DefaultGroupName" Caption="Default Group Name">
            </dx:GridViewDataTextColumn>
		</Columns>
		<SettingsPager PageSize="32" />
		<Settings ShowGroupPanel="false" />
		<Paddings Padding="0px" />
		<Border BorderWidth="0px" />
		<BorderBottom BorderWidth="1px" />
	</dx:ASPxGridView>

    <ef:EntityDataSource ID="entityDataSource" runat="server" ContextTypeName="DotWeb.DotWebDb" EntitySetName="Apps"
        EnableInsert="true" EnableUpdate="true" EnableDelete="true" />
</asp:Content>
