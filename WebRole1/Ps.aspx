<%@ Page Title="Payment" Language="C#"  AutoEventWireup="true" CodeBehind="Ps.aspx.cs" Inherits="WebRole1.Ps" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment</title>
</head>
<body style="height: 460px; background-color: beige;">
    <form id="form1" runat="server">
        <div>
            <h1> Payment Service (PS)</h1>
            <br />
            <br />
            <label>Credit Card Number:</label>
            <br />
            <asp:TextBox ID="cardNbr" runat="server" Width="198px"></asp:TextBox>
            <br />
            <label>Name:</label> <br />
            <asp:TextBox ID="nameBox" style="width:200px" runat="server"></asp:TextBox>
            <br />
            <br />
            
            <label>Total Price:</label>
            <br />
            <asp:TextBox ID="totalAmount" runat="server" Width="200px"></asp:TextBox>
            <br />
            <br />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="backBtn" runat="server" Text="Back" Width="197px" Height="59px" OnClick="BtnBack_Click" style="margin-top: 0px" BackColor="#FF3300"></asp:Button>            
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="PayBtn" runat="server" Text="Pay" Width="200px" Height="64px" OnClick="BtnPay_Click" style="margin-top: 0px" BackColor="Lime"></asp:Button>
        </div>
    </form>
</body>
</html>

