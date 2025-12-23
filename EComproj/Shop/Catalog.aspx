<%@ Page Title="Catalog" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalog.aspx.cs" Inherits="EComproj.Shop.Catalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Catalog</h2>

    <!-- Compact single-row filter toolbar -->
    <div class="row g-2 align-items-end mb-3">
        <div class="col-md-3 col-lg-3">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-sm" placeholder="Search products..." />
        </div>

        <div class="col-md-2 col-lg-2">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select form-select-sm"></asp:DropDownList>
        </div>

        <div class="col-md-2 col-lg-2">
            <div class="input-group input-group-sm">
                <span class="input-group-text">Min</span>
                <asp:TextBox ID="txtMinPrice" runat="server" CssClass="form-control" placeholder="0" />
            </div>
        </div>

        <div class="col-md-2 col-lg-2">
            <div class="input-group input-group-sm">
                <span class="input-group-text">Max</span>
                <asp:TextBox ID="txtMaxPrice" runat="server" CssClass="form-control" placeholder="9999" />
            </div>
        </div>

        <div class="col-md-2 col-lg-2">
            <asp:DropDownList ID="ddlSort" runat="server" CssClass="form-select form-select-sm">
                <asp:ListItem Text="Popular" Value="popular" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Price: Low to High" Value="price_asc"></asp:ListItem>
                <asp:ListItem Text="Price: High to Low" Value="price_desc"></asp:ListItem>
                <asp:ListItem Text="Newest" Value="newest"></asp:ListItem>
                <asp:ListItem Text="Name A–Z" Value="name"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="col-md-1 col-lg-1">
            <asp:Button ID="btnSearch" runat="server" Text="Apply" CssClass="btn btn-primary btn-sm w-100" OnClick="btnSearch_Click" />
        </div>
    </div>

    <asp:Panel ID="pnlRecommendations" runat="server" Visible="false" CssClass="mb-3">
        <h3>Recommended for you</h3>
        <div class="d-flex flex-wrap">
            <asp:Repeater ID="rptRecommendations" runat="server">
                <ItemTemplate>
                    <div class="card me-2 mb-3" style="width: 14rem;">
                        <asp:Image runat="server" CssClass="card-img-top"
                                   ImageUrl='<%# string.IsNullOrEmpty(Eval("ImagePath") as string) ? ResolveUrl("~/Content/no-image.svg") : ResolveUrl(Eval("ImagePath") as string) %>' AlternateText="img" />
                        <div class="card-body">
                            <h6 class="card-title"><%# Eval("Name") %></h6>
                            <p class="card-text">Price: <%# String.Format("{0:C}", Eval("Price")) %></p>
                            <a class="btn btn-outline-primary btn-sm" href='<%# ResolveUrl("~/Shop/ProductDetails.aspx?id=" + Eval("Id")) %>'>View</a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>

    <h3>All Products</h3>
    <div class="d-flex flex-wrap">
        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <div class="card me-2 mb-3" style="width: 14rem;">
                    <asp:Image runat="server" CssClass="card-img-top"
                               ImageUrl='<%# string.IsNullOrEmpty(Eval("ImagePath") as string) ? ResolveUrl("~/Content/no-image.svg") : ResolveUrl(Eval("ImagePath") as string) %>' AlternateText="img" />
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