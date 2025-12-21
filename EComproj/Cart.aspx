<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="EComproj.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Your Cart</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCart_RowCommand" DataKeyNames="ProductId" Width="100%">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Product" />
            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Quantity" HeaderText="Qty" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button CommandName="Remove" CommandArgument="<%# Container.DataItemIndex %>" runat="server" Text="Remove" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div style="margin-top:10px;">
        Total: <asp:Label ID="lblTotal" runat="server" />
    </div>

    <div style="margin-top:15px;">
        <a href="<%= ResolveUrl("~/Shop/Catalog.aspx") %>">Continue Shopping</a>
        |
        <a href="<%= ResolveUrl("~/Checkout.aspx") %>">Checkout</a>
    </div>
</asp:Content>