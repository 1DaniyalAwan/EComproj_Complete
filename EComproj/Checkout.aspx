<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="EComproj.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Checkout</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <p>Dummy payment: click 'Place Order' to confirm.</p>
    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" OnClick="btnPlaceOrder_Click" />
</asp:Content>