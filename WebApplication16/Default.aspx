<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication16.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="main">
    <h2><%: Title %></h2>

        <div class="center" id="wikispotsearch" runat="server">
            <br />
            <br />
            
            <font size="10">WikiSpot
        </font>

            <br />
            <br />


        <asp:TextBox ID="TextBox1" runat="server" Height="34px" Width="50%"></asp:TextBox>
        
        <br />
            <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
            <br />
<asp:Button ID="Button1" runat="server" Height="31px" Text="Search" Width="139px" OnClick="Button1_Click" />
        </div>
    <div id="example" runat="server">
        <br />
    <div id="displayingDiv" runat="server">          
    
    <div class="center">
    <h1>
        <asp:Image ID="Image2" runat="server" ImageUrl="http://icons.iconarchive.com/icons/froyoshark/enkel/512/Spotify-icon.png" Height="50px" />
    &nbsp;Found on Spotify:</h1>
    </div>
        <br />
     
    <div class="center">

            <asp:Label ID="LblArtistName" runat="server" Text="" Font-Size="X-Large"></asp:Label>

        
        <table style="width: 100%; border-style: outset; border-color: #008000; font-family: Arial, Helvetica, sans-serif; text-align: left">
            <tr>
                <td style="width: 20%">
                    <asp:Image ID="Image1" runat="server" ImageUrl="https://i.scdn.co/image/2d702fd051657a679cf067f7609d79077692b06b" Width="80%"/>
                </td>
                <td style="width: 30%">
                    <B>Followers:</B>
                    <br />
                    <asp:Label ID="LblArtistFollowers" runat="server" Text=""></asp:Label><br />
                   

                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                    <b>Genre:<br />
                    </b><asp:Label ID="LblArtistGenre" runat="server" Text=""></asp:Label>
                    <br />
                    <b>Id number:<br />
                    </b><asp:Label ID="LblArtistID" runat="server" Text=""></asp:Label>

                    <br />
                </td>
                <td style="width: 30%">
                    <b>Albums: </b><br />
                    <asp:Label ID="LblArtistAlbums" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <br />

        <div class="center"> 
        <table style="width: 100%;">
            <tr>
            <td>
                                <h4> Most played songs on spotify: </h4>
                    <asp:Label ID="LblArtistTopSongs" runat="server" Text="Text"></asp:Label>
                </td>
                </tr>
        </table>
            </div>
        </div>
        
        <br />
        
        <br />

        <div class="center">
            <asp:Button ID="Button2" runat="server" Text="Get Wikipage" onClick="Button2_Click"/>
        <h1>Found on Wiki:</h1><asp:Label ID="Label7" runat="server" Text=""></asp:Label></div>
        <br />
        <br />
        
        <br />

        <div id="Wikidiv" runat="server" ></div>
    </div>
        </div>
        </div>
</asp:Content>
