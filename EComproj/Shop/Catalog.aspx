<%@ Page Title="Catalog" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="EComproj.Shop.Catalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Catalog</h2>

    <div style="margin-bottom:10px;">
        <asp:TextBox ID="txtSearch" runat="server" Width="300" Placeholder="Search products..." />
        <asp:DropDownList ID="ddlCategory" runat="server" Width="200"></asp:DropDownList>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
    </div>

    <asp:Panel ID="pnlRecommendations" runat="server" Visible="false">
        <h3>Recommended for you</h3>
        <asp:Repeater ID="rptRecommendations" runat="server">
            <ItemTemplate>
                <div style="border:1px solid #ddd; padding:10px; margin:5px; display:inline-block; width:220px;">
                    <img src='<%# Eval("ImagePath") ?? "" %>' alt="img" style="width:200px;height:140px;object-fit:cover;" />
                    <div><strong><%# Eval("Name") %></strong></div>
                    <div>Price: <%# String.Format("{0:C}", Eval("Price")) %></div>
                    <a href='<%# ResolveUrl("~/Shop/ProductDetails.aspx?id=" + Eval("Id")) %>'>View</a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>

    <h3>All Products</h3>
    <asp:Repeater ID="rptProducts" runat="server">
        <ItemTemplate>
            <div style="border:1px solid #ddd; padding:10px; margin:5px; display:inline-block; width:220px;">
                <img src='<%# Eval("ImagePath") ?? "" %>' alt="img" style="width:200px;height:140px;object-fit:cover;" />
                <div><strong><%# Eval("Name") %></strong></div>
                <div>Price: <%# String.Format("{0:C}", Eval("Price")) %></div>
                <a href='<%# ResolveUrl("~/Shop/ProductDetails.aspx?id=" + Eval("Id")) %>'>View</a>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <div style="margin-top:10px;">
        <asp:Label ID="lblPageInfo" runat="server" />
        <asp:HyperLink ID="lnkPrev" runat="server" Text="Prev" />
        |
        <asp:HyperLink ID="lnkNext" runat="server" Text="Next" />
    </div>
</asp:Content>