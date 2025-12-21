<%@ Page Title="Edit Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="EComproj.Seller.EditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Product</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

    <asp:Panel runat="server" ID="pnlForm" Visible="false" CssClass="row">
        <div class="col-md-8">
            <div class="mb-2">Status: <span class="badge bg-secondary"><asp:Label ID="lblStatus" runat="server" /></span></div>

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
                <label class="form-label">Current Image</label><br />
                <asp:Image ID="imgCurrent" runat="server" CssClass="img-thumbnail" Width="120" />
            </div>

            <div class="mb-3">
                <asp:Label runat="server" CssClass="form-label">Replace Image (optional)</asp:Label>
                <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
            </div>

            <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-success" OnClick="btnSave_Click" />
        </div>
    </asp:Panel>
</asp:Content>