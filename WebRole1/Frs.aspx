<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frs.aspx.cs" Inherits="WebRole1.Frs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flight Reservation Service (FRS)</title>
    
</head>
<body style="height: 550px; background-color: beige">
    <form id="form1" runat="server">
        <div>
         <h1>Flight Reservation Service (FRS)</h1>

            <img src="https://www.aircraftcompare.com/wp-content/uploads/2019/12/airplane-sunset.jpg" width ="240" height ="240" />
      

       

              <br />
             <asp:Label ID="LabelFrom" runat="server" Text= "From: "></asp:Label>  
            <br /> 
             <asp:TextBox ID="frombox" runat="server" Width="363px"></asp:TextBox>

            
            <br />
            <h5>Destinations available, type in 'from' and 'to'</h5>
            <p>
      STO (Stockholm)
      CPH (Copenhaben)
      CDG (Paris)
      LHR (London) 
      FRA (Frankfurt)</p>
             

            <br />
                <asp:Label ID="Label1" runat="server" Text= "To: "></asp:Label>  
            <br />
             <asp:TextBox ID="tobox" runat="server" Width="363px"></asp:TextBox>
            
            <br />
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text= "Month: "></asp:Label>  
            <br />
            <asp:TextBox ID="monthbox" runat="server"></asp:TextBox>
          
           
           
            <br />
            <br />

            Infants&nbsp;&nbsp;&nbsp; Childs&nbsp;&nbsp; Adults&nbsp; Seniors<br />
            
            
            <asp:TextBox ID="Box1" runat="server" Width="35px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="Box2" runat="server" Width="35px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="Box3" runat="server" Width="35px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="Box4" runat="server" Width="35px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            
            <br />
            <br />
            <br />
            <br />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
             <asp:Button ID ="Button3" runat="server" Text="Check price" OnClick="BtnCheck_Click" ForeColor="Red" Height="64px" Width="173px" BackColor="Cyan" />
           
                        &nbsp;&nbsp;&nbsp; Your price:

            <asp:TextBox ID="Box5" runat="server" Width="106px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID ="Button1" runat="server" Text="Book -->" OnClick="BtnBook_Click" ForeColor="Red" Height="64px" Width="174px" BackColor="Cyan" />

            <br />
           
            </div>
    </form>
</body>
</html>





