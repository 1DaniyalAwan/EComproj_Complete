<%@ Page Title="Product Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="EComproj.Shop.ProductDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
    <asp:Panel ID="pnlDetails" runat="server" Visible="false" CssClass="row">
        <div class="col-md-5">
            <img id="imgMain" runat="server" class="img-fluid img-thumbnail" style="height:300px;object-fit:cover;" />
        </div>
        <div class="col-md-7">
            <h2><asp:Label ID="lblName" runat="server" /></h2>
            <div>Price: <span class="fw-bold"><asp:Label ID="lblPrice" runat="server" /></span></div>
            <div>Stock: <asp:Label ID="lblStock" runat="server" /></div>
            <div class="mt-2"><asp:Label ID="lblDescription" runat="server" /></div>

            <div class="mt-3">
                <div class="input-group" style="max-width:200px;">
                    <span class="input-group-text">Qty</span>
                    <asp:TextBox ID="txtQty" runat="server" Text="1" CssClass="form-control" />
                </div>
                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CssClass="btn btn-primary mt-2" OnClick="btnAddToCart_Click" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>