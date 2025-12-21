<%@ Page Title="My Interests" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyInterests.aspx.cs" Inherits="EComproj.Account.MyInterests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Interests</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

    <asp:Panel ID="pnlInterests" runat="server" Visible="false">
        <asp:CheckBoxList ID="cblInterests" runat="server" RepeatColumns="3"></asp:CheckBoxList>
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save Interests" OnClick="btnSave_Click" />
    </asp:Panel>
</asp:Content>