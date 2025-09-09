<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsFluentValidations._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <asp:ValidationSummary ID="vs"
    runat="server"
    ValidationGroup="Register"
    DisplayMode="BulletList"
    ShowSummary="true" 
    CssClass="val-summary"
            />

       <div class="row">
  <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email" />
  <asp:TextBox runat="server" ID="txtEmail" />
  <asp:PlaceHolder runat="server" ID="phEmailError" />
</div>

<div class="row">
  <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Password" />
  <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
  <asp:PlaceHolder runat="server" ID="phPasswordError" />
</div>

<div class="row">
  <asp:Label runat="server" AssociatedControlID="txtRepeatPassword" Text="Repeat password" />
  <asp:TextBox runat="server" ID="txtRepeatPassword" TextMode="Password" />
  <asp:PlaceHolder runat="server" ID="phRepeatPasswordError" />
</div>

<asp:Button runat="server"
    ID="btnRegister"
    Text="Register"
    OnClick="btnRegister_Click"
    ValidationGroup="Register"
    CausesValidation="false" />
    </main>

</asp:Content>
