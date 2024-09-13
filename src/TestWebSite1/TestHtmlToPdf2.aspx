<%@ Page Title="TestHtmlToPdf2" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="TestHtmlToPdf2.aspx.cs" Inherits="TestHtmlToPdf2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2><%: Title %></h2>
  <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
