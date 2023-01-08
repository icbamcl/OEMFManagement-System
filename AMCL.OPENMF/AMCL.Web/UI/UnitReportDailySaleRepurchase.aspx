<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportDailySaleRepurchase.aspx.cs" Inherits="UI_UnitReportDailySaleRepurchase" Title=" Unit Fund Sale Repurchase Position (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
            
	function  fnReset()
	{
	   	    
	     return false;
	}
	function fnCheqInput()
	{
	 
	      
            if(document.getElementById("<%=fromDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=fromDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=fromDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }  
            if(document.getElementById("<%=toDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=toDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=toDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }            
	    
	}
	
	
	
 </script>
    <style type="text/css">
        .style5
        {
            height: 30px;
        }
        .style6
        {
         border:solid 1px #A8ACAF;
        text-align: left;
    }
        .style9
        {
            text-align: right;
            height: 24px;
        }
        .style10
        {
            font-size: small;
            font-weight: bold;
        }
        .style11
        {
            color: #660033;
        }
        .style12
        {
            height: 24px;
        }
        .auto-style1 {
            text-align: right;
        }
        .auto-style2 {
            font-size: small;
        }
        .auto-style3 {
            text-align: right;
            width: 806px;
            height: 24px;
        }
    </style>
    
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    
<br />
<br />
 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Unit Daily Sale Repurchase Position Form <span class="auto-style2">(Head Office Only)</span> <span id="spanFundName" runat="server"></span>
                            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
 <%-- <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>  --%>
<table width="1200" align="center" cellpadding="0" cellspacing="0" >
<%--<colgroup width="190"></colgroup>
<colgroup width="75"></colgroup>
<colgroup width="100"></colgroup>--%>


    
     <tr >
        <td  class="style10" style="text-align: right"  ><b>Fund Name :</b></td>
        
        <td align="left"   colspan="4" class="style12"> &nbsp;&nbsp;          
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="1"></asp:DropDownList>
            </td>
       
    </tr>
     <tr >
        <td  class="style10" style="text-align: right"  >Branch Name :</td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="2" Enabled="False"></asp:DropDownList>
        </td>
       
    </tr>
   
    
    <tr >
        <td align="left" class="style10" style="text-align: right"  >Date Range :</td>
        
        <td align="left"   colspan="4"> 
            &nbsp; &nbsp;<asp:TextBox ID="fromDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="fromDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="4" />
            <b><span style="font-weight:bold; height:100px;">&nbsp;&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="5"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="toRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="toDateTextBox" 
                PopupButtonID="toRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="toRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="6" />  
        </td>
       
    </tr>
  
    <tr>
        <td align="center" colspan="5">
            &nbsp;</td>
    </tr>
   
    <tr>
        <td align="center" colspan="5">
            <asp:Button ID="findButton" runat="server" AccessKey="f" CssClass="buttonmid" 
                meta:resourcekey="findButtonResource1" onclick="findButton_Click" 
                onclientclick="return fnCheqInput();" TabIndex="7" Text="Find" 
                Width="128px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
</table>
<br />
  <table align="center"  cellpadding="0" cellspacing="0" border="0" 
            style="width:1000px">
                 <tr>
                 <td> 
                     <div id="dvLedger" runat="server" 
                         style="text-align: center; display: block; overflow: auto; width:1190px; ">

                         <asp:GridView ID="SurrenderListGridView" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="3" Font-Size="Large">
                             <EditRowStyle Font-Bold="True" Font-Size="Larger" />
                             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                             <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                             <AlternatingRowStyle Font-Bold="True" Font-Size="Smaller" />
                             <Columns>                                 
                                 <asp:BoundField DataField="FUND_NM" HeaderText="Name of The Fund" />
                                 <asp:BoundField DataField="SL_CASH_AMT" HeaderText="Cash Sale" />
                                 <asp:BoundField DataField="SL_CHQ_AMT" HeaderText="Cheque Sale" />
                                 <asp:BoundField DataField="TOTAL_SL_AMT" HeaderText="Total Sale Taka" />
                                  <asp:BoundField DataField="TOTAL_SL_UNIT" HeaderText="Total Sale Unit" />
                                 <asp:BoundField DataField="REP_EFT_AMT" HeaderText="Rep. EFT" />
                                 <asp:BoundField DataField="REP_CHQ_AMT" HeaderText="Rep. Cheque" />
                                 <asp:BoundField DataField="TOTAL_REP_AMT" HeaderText="Total Rep. Taka" />
                                  <asp:BoundField DataField="TOTAL_REP_UNIT" HeaderText="Total Rep. Unit " />
                                  <asp:BoundField DataField="NET_AMOUNT" HeaderText="Net Taka" />
                                  <asp:BoundField DataField="NET_UNIT" HeaderText="Net Unit" />
                             </Columns>
                         </asp:GridView>

                        </div>          
                   </td>
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
</asp:Content>

