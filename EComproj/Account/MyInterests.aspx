<%@ Page Title="My Interests" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyInterests.aspx.cs" Inherits="EComproj.Account.MyInterests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Interests</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

    <div class="row">
        <div class="col-md-8">
            <asp:Panel ID="pnlInterests" runat="server" Visible="false" CssClass="card card-body">
                <asp:CheckBoxList ID="cblInterests" runat="server" RepeatColumns="3" CssClass="mb-3"></asp:CheckBoxList>
                <asp:Button ID="btnSave" runat="server" Text="Save Interests" CssClass="btn btn-success" OnClick="btnSave_Click" />
            </asp:Panel>
        </div>
    </div>
</asp:Content>