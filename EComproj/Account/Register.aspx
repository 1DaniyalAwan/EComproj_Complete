<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EComproj.Account.Register" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Register</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

    <div class="row">
        <div class="col-md-6">
            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="Email" CssClass="form-label">Email</asp:Label>
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="Password" CssClass="form-label">Password</asp:Label>
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="form-label">Confirm password</asp:Label>
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <asp:Label runat="server" CssClass="form-label">Select Role</asp:Label>
                <asp:RadioButtonList ID="rblRole" runat="server" RepeatDirection="Horizontal" CssClass="form-check">
                    <asp:ListItem Text="Customer" Value="Customer" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Seller" Value="Seller"></asp:ListItem>
                </asp:RadioButtonList>
            </div>

            <asp:Panel ID="pnlInterests" runat="server" Visible="true" CssClass="mb-3">
                <strong>Select Your Interests</strong>
                <asp:CheckBoxList ID="cblInterests" runat="server" RepeatColumns="3" CssClass="mt-2"></asp:CheckBoxList>
                <div class="form-text">You can update these later.</div>
            </asp:Panel>

            <asp:Button ID="CreateUser" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="CreateUser_Click" />
        </div>
    </div>

    <script runat="server">
        protected void rblRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlInterests.Visible = (rblRole.SelectedValue == "Customer");
        }
    </script>
</asp:Content>