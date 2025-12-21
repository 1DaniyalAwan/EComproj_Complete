<%@ Page Title="Edit Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="EComproj.Seller.EditProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Product</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

    <asp:Panel runat="server" ID="pnlForm" Visible="false">
        <div>Status: <asp:Label ID="lblStatus" runat="server" /></div>
        <br />

        <asp:Label AssociatedControlID="txtName" runat="server" Text="Name"></asp:Label><br />
        <asp:TextBox ID="txtName" runat="server" Width="400"></asp:TextBox><br /><br />

        <asp:Label AssociatedControlID="txtDescription" runat="server" Text="Description"></asp:Label><br />
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Width="600"></asp:TextBox><br /><br />

        <asp:Label AssociatedControlID="txtPrice" runat="server" Text="Price"></asp:Label><br />
        <asp:TextBox ID="txtPrice" runat="server" Width="200"></asp:TextBox><br /><br />

        <asp:Label AssociatedControlID="txtStock" runat="server" Text="Stock"></asp:Label><br />
        <asp:TextBox ID="txtStock" runat="server" Width="200"></asp:TextBox><br /><br />

        <asp:Label AssociatedControlID="ddlCategory" runat="server" Text="Category"></asp:Label><br />
        <asp:DropDownList ID="ddlCategory" runat="server" Width="300"></asp:DropDownList><br /><br />

        <div>
            Current Image:<br />
            <asp:Image ID="imgCurrent" runat="server" Width="120" />
        </div>
        <br />
        <asp:Label runat="server" Text="Replace Image (optional)"></asp:Label><br />
        <asp:FileUpload ID="fuImage" runat="server" /><br /><br />

        <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
    </asp:Panel>
</asp:Content>