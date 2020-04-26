<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Trs.aspx.cs" Inherits="WebRole1.Trs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head><title>Travel Reservation Service</title></head>
<body style="height: 435px; background-color: beige;">
    <form id="form1" runat="server">

     <h1>Travel Reservation Service (TRS)</h1>

        <img src="https://i.pinimg.com/736x/ee/3d/b1/ee3db19f00bb390aa6543d7a02df846a.jpg" width ="240" height="240" style="margin-left: 87px" /><div style="height: 407px">
           
            <br />

         <br />
            <br />
         
            &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
            <asp:Button ID="Button1" runat="server" Text="Book Travel!" OnClick="BtnPost_Click" ForeColor="Red" Height="64px" Width="174px" BackColor="Cyan" /> <br /> <br /> &nbsp; &nbsp; 
         &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;   <asp:CheckBox ID="Checkbox" runat="server" Text="Hotel included:"></asp:CheckBox>
           &nbsp; &nbsp;&nbsp; &nbsp; <br />
            
            </div>
    &nbsp;</form>
</body>
</html>



