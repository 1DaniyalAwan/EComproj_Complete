<%@ Page Title="Add Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="EComproj.Seller.AddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add Product</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
    <div class="row">
        <div class="col-md-8">
            <div class="mb-3">
                <asp:Label AssociatedControlID="txtName" runat="server" CssClass="form-label">Name</asp:Label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label AssociatedControlID="txtDescription" runat="server" CssClass="form-label">Description</asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="row">
                <div class="col-md-4 mb-3">
                    <asp:Label AssociatedControlID="txtPrice" runat="server" CssClass="form-label">Price</asp:Label>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-3">
                    <asp:Label AssociatedControlID="txtStock" runat="server" CssClass="form-label">Stock</asp:Label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="mb-3">
                <asp:Label AssociatedControlID="ddlCategory" runat="server" CssClass="form-label">Category</asp:Label>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select"></asp:DropDownList>
            </div>

            <div class="mb-3">
                <asp:Label runat="server" CssClass="form-label">Image</asp:Label>
                <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
            </div>

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </div>
    </div>
</asp:Content>