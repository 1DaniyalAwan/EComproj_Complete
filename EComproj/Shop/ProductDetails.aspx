<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="EComproj.Shop.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <asp:Panel ID="pnlDetails" runat="server" Visible="false">
        <div style="display:flex;gap:20px;">
            <img id="imgMain" runat="server" style="width:300px;height:220px;object-fit:cover;border:1px solid #ccc;" />
            <div>
                <h2><asp:Label ID="lblName" runat="server" /></h2>
                <div>Price: <asp:Label ID="lblPrice" runat="server" /></div>
                <div>Stock: <asp:Label ID="lblStock" runat="server" /></div>
                <div style="margin-top:10px;"><asp:Label ID="lblDescription" runat="server" /></div>

                <div style="margin-top:15px;">
                    Quantity:
                    <asp:TextBox ID="txtQty" runat="server" Text="1" Width="60" />
                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>