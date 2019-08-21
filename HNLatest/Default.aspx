<%@ Page Title="Home Page" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HNTest._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="1">
        <h1>Hacker News</h1>
    </div>

    <div>
        <asp:ListView ID="ListView1" runat="server">
            <LayoutTemplate> 
                <table id="Table1" runat="server" class="TableCSS"> 
                    <tr id="Tr1" runat="server" class="TableHeader"> 
                        <td id="Td1" runat="server" width="80%"><h1>Title</h1></td> 
                        <td id="Td2" runat="server" width="20%"><H1>Author</H1></td> 
                    </tr> 
                    <tr id="ItemPlaceholder" runat="server"> 
                    </tr> 
                </table> 
            </LayoutTemplate>
            <ItemTemplate> 
                <tr class="TableData"> 
                    <td> 
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("URL")%>'><%# Eval("Title")%></asp:HyperLink> 
                    </td> 
                    <td> 
                        <asp:Label  
                            ID="Label2" 
                            runat="server" 
                            Text='<%# Eval("User")%>'> 
                        </asp:Label> 
                    </td>
                </tr>                 
            </ItemTemplate> 
        </asp:ListView>
    </div>

</asp:Content>
