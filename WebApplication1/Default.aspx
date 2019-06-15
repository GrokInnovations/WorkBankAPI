<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.WebForm1" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div class="jumbotron">
        <h1>World Bank API Usage in c#.net</h1>
        <p class="lead">This project  is for anyone willing to use data provided by World Bank and use in bespoke software </p>
        <div class="col-md-6"><a target="_blank" href="https://datahelpdesk.worldbank.org/knowledgebase/articles/889392-api-documentation" class="btn btn-primary btn-lg">API Documentation &raquo;</a>
            </div>
        <div class="col-md-6">
           <a target="_blank" href="https://api.worldbank.org/v2/en/country/all/indicator/NY.GDP.MKTP.CD?format=json&per_page=500&date=2017&MRNEV=1" class="btn btn-prinmary btn-lg">GDP data in JSON Format &raquo;</a>
            </div>
     <br />   
    </div>

    <div class="row">
        <div class="col-md-12">
           
            <h3>Demo</h3><h5>To see GDP of TOP <asp:TextBox MaxLength="3" Width="40" runat="server" AutoPostBack="false" ID="txtTop" Text="10" TextMode="Number"/> Countries, click <asp:Button runat="server" OnClick="OnClick" Text="HERE"/></h5>

            <asp:GridView ID="GDPGridView" runat="server"  datakeynames="CountryName" PageSize="10" ShowFooter="true" AutoGenerateColumns="False" CellPadding="5" ForeColor="#333333" GridLines="Horizontal" CellSpacing="2" EmptyDataText="No Data Found, check worldbank data live"  >
                 <EditRowStyle BackColor="#999999" />
                 <HeaderStyle BackColor="#7e6428" Font-Bold="True" ForeColor="White" />
                 <FooterStyle BackColor="#ffffff" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle HorizontalAlign="Left" VerticalAlign="Top" BackColor="#F7F6F3" ForeColor="#333333" BorderColor="#7e6428"  BorderStyle="Solid" BorderWidth="1px"/>
                  <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                 <Columns>
                         <asp:BoundField DataField="CountryName" HeaderText="Country" />
                         <asp:BoundField DataField="Value" HeaderText="GDP" SortExpression="Value"  DataFormatString="{0:F2}"/>
                     </Columns>

            </asp:GridView>
            <h4>Notes</h4>
           
               <ul> 
                   <li>Solution is developed using Visual Studio Community 2015 edition with .net version 4.6.1. Project Template used ASP.net webform to keep it simple to be understood</li>
                   <li>To define objects and JSONArray in JSON, Strong typed Classes added to "WorldBankAPI.cs" to represent c# notation</li>  
                   <li>JSON feed is loaded in stream reader asyncronously on click of button then deserialised to show top X countries in grid</li>
                   <li>This code also uses my previous article <a target="_blank" href="https://www.c-sharpcorner.com/article/reading-and-display-source-of-web-pages/">here</a> to request json and stream  response to stream reader without downloading and saving it physically on server. This article has very high number of reads.  Advantage of use the data on the fly. </li>
                   <li> Click of button calls asyncronous click method and it internally uses the asyncronous call to load JSON and deserialise it and then binds it with Grid </li>
                   <li>Lamda expressions used to sort and filter data for country
                
                       </li>
                   <li>NewtonSoft Library is also used for JSON operations</li>
                   </ul>
            
        </div>
    
    </div>
    </form>
</body>
</html>
