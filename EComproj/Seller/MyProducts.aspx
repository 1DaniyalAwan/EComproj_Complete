<%@ Page Title="My Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyProducts.aspx.cs" Inherits="EComproj.Seller.MyProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>My Products</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
    <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" Width="100%"
                  CssClass="table table-striped table-bordered" DataKeyNames="Id" OnRowCommand="gvProducts_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:BoundField DataField="ApprovalStatus" HeaderText="Status" />
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image runat="server" ImageUrl='<%# ResolveUrl(Eval("ImagePath") as string) %>' Width="80" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <a class="btn btn-outline-primary btn-sm" href='<%# ResolveUrl("~/Seller/EditProduct.aspx?id=" + Eval("Id")) %>'>Edit</a>
                    <asp:LinkButton runat="server" CommandName="DeleteProduct" CommandArgument="<%# Container.DisplayIndex %>"
                                    CssClass="btn btn-outline-danger btn-sm ms-1" Text="Delete" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>