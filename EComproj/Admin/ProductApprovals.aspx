<%@ Page Title="Product Approvals" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductApprovals.aspx.cs" Inherits="EComproj.Admin.ProductApprovals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Pending Product Approvals</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
    <asp:GridView ID="gvPending" runat="server" AutoGenerateColumns="False" Width="100%"
                  CssClass="table table-striped table-bordered" DataKeyNames="Id" OnRowCommand="gvPending_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <img src='<%# Eval("ImagePath") %>' style="width:80px;height:60px;object-fit:cover;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Approve" CommandArgument="<%# Container.DisplayIndex %>" CssClass="btn btn-success btn-sm" Text="Approve" />
                    <asp:LinkButton runat="server" CommandName="Reject" CommandArgument="<%# Container.DisplayIndex %>" CssClass="btn btn-danger btn-sm ms-1" Text="Reject" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>