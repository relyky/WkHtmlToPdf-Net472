<%@ Page Title="TestHtmlToPdf" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="TestHtmlToPdf.aspx.cs" Inherits="TestHtmlToPdf" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h2><%: Title %></h2>
  <asp:Button ID="Button1" runat="server" Text="測試 Html to PDF" OnClick="Button1_Click" />
  <asp:Label ID="Label1" runat="server" Text="Message"></asp:Label>
</asp:Content>
