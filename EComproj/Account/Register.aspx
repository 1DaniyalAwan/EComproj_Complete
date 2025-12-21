<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EComproj.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2>Register</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

    <div>
        <asp:Label runat="server" AssociatedControlID="Email">Email</asp:Label><br />
        <asp:TextBox runat="server" ID="Email" Width="350" />
    </div>
    <div style="margin-top:10px;">
        <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label><br />
        <asp:TextBox runat="server" ID="Password" TextMode="Password" Width="350" />
    </div>
    <div style="margin-top:10px;">
        <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label><br />
        <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" Width="350" />
    </div>

    <div style="margin-top:15px;">
        <strong>Select Role</strong><br />
        <asp:RadioButtonList ID="rblRole" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Text="Customer" Value="Customer" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Seller" Value="Seller"></asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <asp:Panel ID="pnlInterests" runat="server" Visible="true" Style="margin-top:15px;">
        <strong>Select Your Interests</strong><br />
        <asp:CheckBoxList ID="cblInterests" runat="server" RepeatColumns="3"></asp:CheckBoxList>
        <div style="font-size:12px;color:#555;">You can update these later.</div>
    </asp:Panel>

    <div style="margin-top:20px;">
        <asp:Button ID="CreateUser" runat="server" Text="Register" OnClick="CreateUser_Click" />
    </div>

    <script runat="server">
        protected void rblRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlInterests.Visible = (rblRole.SelectedValue == "Customer");
        }
    </script>
</asp:Content>