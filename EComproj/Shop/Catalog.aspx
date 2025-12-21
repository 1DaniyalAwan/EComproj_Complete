<%@ Page Title="Catalog" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="EComproj.Shop.Catalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Catalog</h2>

    <div class="row mb-3">
        <div class="col-md-4">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search products..." />
        </div>
        <div class="col-md-3">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        </div>
    </div>

    <asp:Panel ID="pnlRecommendations" runat="server" Visible="false" CssClass="mb-3">
        <h3>Recommended for you</h3>
        <asp:Repeater ID="rptRecommendations" runat="server">
            <ItemTemplate>
                <div class="card me-2 mb-3" style="width: 14rem;">
                    <img class="card-img-top" src='<%# Eval("ImagePath") ?? "" %>' alt="img" style="height:140px;object-fit:cover;" />
                    <div class="card-body">
                        <h6 class="card-title"><%# Eval("Name") %></h6>
                        <p class="card-text">Price: <%# String.Format("{0:C}", Eval("Price")) %></p>
                        <a class="btn btn-outline-primary btn-sm" href='<%# ResolveUrl("~/Shop/ProductDetails.aspx?id=" + Eval("Id")) %>'>View</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>

    <h3>All Products</h3>
    <div class="d-flex flex-wrap">
        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <div class="card me-2 mb-3" style="width: 14rem;">
                    <img class="card-img-top" src='<%# Eval("ImagePath") ?? "" %>' alt="img" style="height:140px;object-fit:cover;" />
                    <div class="card-body">
                        <h6 class="card-title"><%# Eval("Name") %></h6>
                        <p class="card-text">Price: <%# String.Format("{0:C}", Eval("Price")) %></p>
                        <a class="btn btn-outline-primary btn-sm" href='<%# ResolveUrl("~/Shop/ProductDetails.aspx?id=" + Eval("Id")) %>'>View</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <div class="mt-2">
        <asp:Label ID="lblPageInfo" runat="server" CssClass="me-2" />
        <asp:HyperLink ID="lnkPrev" runat="server" CssClass="btn btn-secondary btn-sm me-1" Text="Prev" />
        <asp:HyperLink ID="lnkNext" runat="server" CssClass="btn btn-secondary btn-sm" Text="Next" />
    </div>
</asp:Content>