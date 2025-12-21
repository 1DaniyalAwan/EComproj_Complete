<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="EComproj.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Checkout</h2>
    <div class="alert alert-info">Dummy payment: click 'Place Order' to confirm.</div>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block"></asp:Label>
    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-success" OnClick="btnPlaceOrder_Click" />
</asp:Content>