<%@ Page Title="Product Approvals" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductApprovals.aspx.cs" Inherits="EComproj.Admin.ProductApprovals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Pending Product Approvals</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
    <asp:GridView ID="gvPending" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="Id" OnRowCommand="gvPending_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="img" runat="server" ImageUrl='<%# Eval("ImagePath") %>' Width="80" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Approve" Text="Approve" />
            <asp:ButtonField CommandName="Reject" Text="Reject" />
        </Columns>
    </asp:GridView>
</asp:Content>