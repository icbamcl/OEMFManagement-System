<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportFundPosition.aspx.cs" Inherits="UI_UnitReportFundPosition" Title=" Unit Fund Position (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
            
	function  fnReset()
	{
	   	    
	     return false;
	}
	function fnCheqInput()
	{
	 
	     if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
                
            }   
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
	
	
	function fnOnChangeText(textObject)
	{
	    
	     if(textObject.id.indexOf("fromDateTextBox")!=-1)
	    {
	        document.getElementById("<%=toDateTextBox.ClientID%>").value =document.getElementById("<%=fromDateTextBox.ClientID%>").value ;
	        document.getElementById("<%=toDateTextBox.ClientID%>").focus();
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
                Unit Fund Position Form <span id="spanFundName" runat="server"></span>
                            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
  <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>  
<table width="1000" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="190"></colgroup>
<colgroup width="75"></colgroup>
<colgroup width="100"></colgroup>
<colgroup width="75"></colgroup>

    
     <tr >
        <td align="right">  &nbsp;<b>Fund Name :</b></td>
        
        <td align="left"   colspan="4">          
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="1"></asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
     <tr >
        <td align="left"  style="text-align: right"  ><strong>Branch Name </strong>:</td>
        
        <td align="left"   colspan="4"> &nbsp;<asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="2"></asp:DropDownList>
        </td>
       
    </tr>
   
    
    <tr >
        <td align="left" style="text-align: right"  ><strong>Date Range :</strong></td>
        
        <td align="left"   colspan="4"> 
            &nbsp;<asp:TextBox ID="fromDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="fromDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" onBlur="Javascript:fnOnChangeText(this);"
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
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">
            <asp:Button ID="findButton" runat="server" AccessKey="f" CssClass="buttonmid" 
                meta:resourcekey="findButtonResource1" onclick="findButton_Click" 
                onclientclick="return fnCheqInput();" TabIndex="7" Text="Find" 
                Width="128px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="PrintButton" runat="server" CssClass="buttonmid" meta:resourcekey="findButtonResource1"  onclientclick="return fnCheqInput();" TabIndex="7" Text="Print" Width="128px" OnClick="PrintButton_Click" />
&nbsp; </td>
    </tr>
</table>
<br />
  <table align="center"  cellpadding="0" cellspacing="0" border="0" 
            style="width: 1000px">
                 <tr>
                 <td> 
                     <div id="dvLedger" runat="server" >
                   

                       <asp:DataGrid ID="dgFundPosition" runat="server" AutoGenerateColumns="False"  
                             Width="850px" BorderColor="Black" CellPadding="1" CellSpacing="1" meta:resourcekey="dgLedgerResource1"
                            >
                           <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                            <ItemStyle CssClass="TableText"></ItemStyle>
                            <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                            <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                            <Columns>
                                                                                                                         
                                <asp:BoundColumn DataField="SI" HeaderText="SI">
                                    <HeaderStyle  Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                        Width="50px" />
                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                                                                                                         
                            <asp:BoundColumn DataField="TRANS_TYPE" HeaderText="Transaction Type">
                                <HeaderStyle Width="150px" Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>                                                                                              
                            <asp:BoundColumn  DataField="TOTAL_UNIT" HeaderText="Total Units">
                                <HeaderStyle Width="150px" Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                </asp:BoundColumn>    
                            <asp:BoundColumn DataField="TOTAL_AMT" HeaderText="Total Amount">
                                <HeaderStyle Width="150px" Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                </asp:BoundColumn>                                                                                              
                            <asp:BoundColumn DataField="TOTAL_HOLD" HeaderText="Total No. of Holding">
                                <HeaderStyle Width="150px" Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Right" />                                
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />                                                               
                                </asp:BoundColumn> 
                                <asp:BoundColumn DataField="TOTAL_TRANS" HeaderText="Total No. of Transaction">
                                <HeaderStyle  Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Right" />                                
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />                                                               
                                </asp:BoundColumn>                                                                                              
                                                           
                        </Columns>
                    </asp:DataGrid>              
                        </div>          
                   </td>
                 </tr>
            </table>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="fundNameDropDownList" EventName="SelectedIndexChanged" />
    <asp:AsyncPostBackTrigger ControlID="findButton" EventName="Click" />     
</Triggers>
</asp:UpdatePanel>
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

