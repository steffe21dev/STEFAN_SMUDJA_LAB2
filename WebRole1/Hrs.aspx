<%@ Page Title="Hotel Reservation Service" Language="C#"  AutoEventWireup="true" CodeBehind="Hrs.aspx.cs" Inherits="WebRole1.Hrs" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel Reservation Service (HRS)</title>
</head>
<body style="height: 500px; background-color: beige;">
    <form id="form1" runat="server">
        <div>
     <h1>Hotel Reservation Service (HRS)</h1>


            <img src="https://www.kindpng.com/picc/m/284-2844300_hotel-building-clipart-png-hotel-clipart-png-transparent.png" width ="240" height ="240" />
            </br>
            </br>
	
       
             <asp:Label ID="Label1" runat="server" Text= "How many travelers?:"></asp:Label> 
           
            <br />
           
            
            
            <asp:TextBox ID="Textbox1" runat="server"></asp:TextBox>
            
            
            <br />
            
           


            <asp:Label ID="Label2" runat="server" Text= "How many nights do you wish to stay?: "></asp:Label>  
            <br />
            
            <asp:TextBox ID="Textbox2" runat="server"></asp:TextBox>
            <br />
            
            <asp:Label ID="Label3" runat="server" Text= "How many seniors are there in your company?: "></asp:Label>  
            <br />
            <asp:TextBox ID="Textbox3" runat="server"></asp:TextBox>
           
            <br />
             <asp:Label ID="Label4" runat="server" Text= "Name of the primary guest: "></asp:Label>  
            <br />
             <asp:TextBox ID="Textbox4" runat="server"></asp:TextBox>


                
                <br />
            <br />
            
             <asp:Label ID="Label5" runat="server" Text= "Please select desired room type: "></asp:Label>  <br />
           <asp:CheckBox ID="singleRoom" runat="server" Text="Single"></asp:CheckBox>
            &nbsp;&nbsp;
            
            <asp:CheckBox ID="doubleRoom" runat="server" Text="Double"></asp:CheckBox>

            <br />
           <br />
     

             <asp:Button ID ="Button3" runat="server" Text="Check price" OnClick="BtnCheck_Click" ForeColor="Red" Height="64px" Width="173px" BackColor="Cyan" />
           
                        &nbsp;&nbsp;&nbsp; Hotel price:

            <asp:TextBox ID="Textbox5" runat="server" Width="106px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             

            
           <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="BtnCancel_Click" ForeColor="White" Height="64px" Width="174px" BackColor="Red" />
            

           &nbsp; &nbsp; &nbsp;<asp:Button ID="Button1" runat="server" Text="Continue" OnClick="BtnNext_Click" ForeColor="Red" Height="64px" Width="174px" BackColor="Cyan" />
            
            <br />
            
            </div>
    </form>
</body>
</html>





