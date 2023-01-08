<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="UI_Home" Title=" ICB ASSET MANAGEMENT COMPANY LIMITED(Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript"> 
     function fnCheqeInput()
     {
      
          if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
            }
            if(document.getElementById("<%=branchNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=branchNameDropDownList.ClientID%>").focus();
                alert("Please Select  Branch");
                return false;
            }
           if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value !="0"&&document.getElementById("<%=branchNameDropDownList.ClientID%>").value !="0")
           {
             var fundName=document.getElementById("<%=fundNameDropDownList.ClientID%>")[document.getElementById("<%=fundNameDropDownList.ClientID%>").selectedIndex].innerHTML;
             var branchName=document.getElementById("<%=branchNameDropDownList.ClientID%>")[document.getElementById("<%=branchNameDropDownList.ClientID%>").selectedIndex].innerHTML;
             var IsConfirm=confirm("Are You Sure to Enter at"+" "+fundName+" "+"and"+" "+branchName);
             if(IsConfirm)
             {
                return true;
             }
             else
             {
                return false;
             }

           }
     }
    </script>
<br />
<br />
<table width="500" align="center" cellpadding="0" cellspacing="0" >
    <%--<tr>
    <td align="left" class="FormSubTitle">User Name:&nbsp;<div id="DivUser" runat="server"></div></td>
    <td align="left" class="FormSubTitle">Branch Name:&nbsp;<div id="DivBranch" runat="server"></div></td>
        
    </tr>--%>
    <tr><td><b>Please Select The Fund and Branch to Enter The System</b></td></tr>
</table>
<br />
<br />
<table width="400" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="100"></colgroup>

    <tr>
        <td align="left">Fund Name:</td>
        <td align="left"><asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="1"></asp:DropDownList></td>
        
    </tr>
    <tr>
       <td align="left">Branch Name:</td>
        <td align="left"><asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="2"></asp:DropDownList></td>
      
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="2"><asp:Button ID="OkButton" runat="server" Text="Ok" 
                CssClass="buttonmid" onclick="OkButton_Click" OnClientClick="return fnCheqeInput();"/></td>
    </tr>
</table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

