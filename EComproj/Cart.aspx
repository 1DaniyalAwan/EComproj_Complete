<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="EComproj.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Your Cart</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductId"
                  CssClass="table table-striped table-bordered" OnRowCommand="gvCart_RowCommand" Width="100%">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Product" />
            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Quantity" HeaderText="Qty" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton CommandName="Remove" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CssClass="btn btn-outline-danger btn-sm" Text="Remove" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div class="mt-2">
        Total: <span class="fw-bold"><asp:Label ID="lblTotal" runat="server" /></span>
    </div>

    <div class="mt-3">
        <a class="btn btn-secondary" href="<%= ResolveUrl("~/Shop/Catalog.aspx") %>">Continue Shopping</a>
        <a class="btn btn-primary ms-2" href="<%= ResolveUrl("~/Checkout.aspx") %>">Checkout</a>
    </div>
</asp:Content>