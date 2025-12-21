<%@ Page Title="My Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="EComproj.Orders.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Orders</h2>
    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Order #" />
            <asp:BoundField DataField="CreatedAt" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="TotalAmount" HeaderText="Total" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>

    <h3>Items</h3>
    <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="OrderId" HeaderText="Order #" />
            <asp:BoundField DataField="ProductName" HeaderText="Product" />
            <asp:BoundField DataField="Quantity" HeaderText="Qty" />
            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="LineTotal" HeaderText="Line Total" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
</asp:Content>