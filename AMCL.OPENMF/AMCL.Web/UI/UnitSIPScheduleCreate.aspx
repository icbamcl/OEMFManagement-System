<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSIPScheduleCreate.aspx.cs" Inherits="UI_UnitSIPScheduleCreate" Title="Registration Entry Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Save");
        if(confirm)
        {                        
           document.getElementById("<%=sipInstallmentLabel.ClientID%>").value = "";
            document.getElementById("<%=NameLabel.ClientID%>").value ="";
            document.getElementById("<%=regNoLabel.ClientID%>").value = "";
            document.getElementById("<%=amountLabel.ClientID%>").value = "";
            document.getElementById("<%=payFreqLabel.ClientID%>").value = "";
            document.getElementById("<%=sipDayLabel.ClientID%>").value = "";
            document.getElementById("<%=EFTEndDateLabel.ClientID%>").value ="";
            document.getElementById("<%=EFTStartDateLabel.ClientID%>").value = ""; 
            
            document.getElementById("<%=durationLabel.ClientID%>").value = "";
            document.getElementById("<%=accountNoLabel.ClientID%>").value = "";
            document.getElementById("<%=bankInfoLabel.ClientID%>").value = "";
           
           
            return false;
        }
        else
        {
            return true;
            
        }
    }
    
  
	 
  	 
        function fnCheqInput()
        {
            if(document.getElementById("<%=sipNumberDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=sipNumberDropDownList.ClientID%>").focus();
                alert("Please Select Date ");
                return false;                
            }
            
           }

                
	    
</script>



    <style type="text/css">
    
       
        .style3
        {
            font-size: large;
        }
        A.TextLink:hover
        {
	        COLOR: #556677; 
	        font-weight:bold; 
	        font-size:large;
	        text-decoration:underline;
        	
        }
       
        .fontStyle
        {
            color: #FFFFFF;
            font-weight: bold;
        }
        tr .menuBarBottomSelected td a:hover
        {
        	background-color:#547DD3;
        }
       .menuBarBottomSelected
       {
       	         background-color: #666666;  
       }
        .style4
        {
            height: 25px;
            width: 196px;
        }
        .style5
        {
            height: 25px;
            width: 172px;
        }
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
        }
        .auto-style4 {
            height: 17px;
        }
        .auto-style7 {
            color: #339933;
            font-size: medium;
        }
        .auto-style8 {
            color: #339933;
            font-size: small;
        }
        .auto-style9 {
            height: 18px;
        }
        </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />        
        
      
      
 <table width="1100px" align="left" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="150"></colgroup>
<colgroup width="310"></colgroup>
<colgroup width="200"></colgroup>
     <tr>
         <td  colspan="4" align="center" class="auto-style2">
        <table align="center" runat="server" id="tableFundName">
        <tr>
            <td class="FormTitle" align="left">
                 Unit Holder SIP Schedule Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
         </td>
     </tr>

     <tr>
        <td colspan="4" align="left">
        <fieldset>
            <legend style="font-weight: 700"> ::Unit SIP Information::</legend>
            <br />
            <table width="100%" align="center" cellpadding="1" cellspacing="1" border="1" >
            <colgroup width="150"></colgroup>
            <colgroup width="150"></colgroup>
            <colgroup width="150"></colgroup>
            <colgroup width="200"></colgroup>
            <colgroup width="150"></colgroup>
             <tr>
        <td align="right" ><b>SIP Number:</b></td>
        <td align="left">
        
            <asp:DropDownList ID="sipNumberDropDownList" runat="server" 
                CssClass="DropDownList"       
                AutoPostBack="True" OnSelectedIndexChanged="sipNumberDropDownList_SelectedIndexChanged"  >
                                  
            </asp:DropDownList>                                                                                               
                                                   
                                </td>
                  <td align="right" ><b>Registration No:</b></td>
                  <td align="left" >
        
            <asp:Label ID="regNoLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                   
                                            
                                                   
                                </td>
        <td align="right"><b>Name of The&nbsp; Holder:</b></td>
        <td align="left">
          &nbsp;  <asp:Label ID="NameLabel" runat="server" Text="" 
                style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                    </td> 
       
    </tr>
             <tr>
        <td colspan="6" class="auto-style9"></td>
        
    </tr>
          
                     
             <tr>              
        <td align="right" class="auto-style4" >
            <b>Total Instllaments:</b></td>
       <td align="left" class="auto-style4">
        
            <asp:Label ID="sipInstallmentLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                   
                                            
                                                   
                                </td>
        <td align="right" class="auto-style4" ><b>Amount:</b></td>
        <td align="left" class="auto-style4">
            <asp:Label ID="amountLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                    </td>

                 
            <td align="right" class="auto-style4" ><b>Pay Frequency:</b></td>
                <td align="left" class="auto-style4">
                    <asp:Label ID="payFreqLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                    </td>
            </tr>
            
    
     <tr>
         <td align="right" class="auto-style2" ><b>&nbsp;Monthly SIP Day:</b></td>
        <td align="left" class="auto-style2">
            <asp:Label ID="sipDayLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                    </td>
          <td align="right" class="auto-style2" ><b>&nbsp;Debit Starts Date:</b></td>
        <td align="left" class="auto-style2">
            <asp:Label ID="EFTStartDateLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                    </td>
          <td align="right" class="auto-style2" ><b>Debit Ends Date:</b></td>
        <td align="left" class="auto-style2">
            <asp:Label ID="EFTEndDateLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                    </td>
    </tr>
   
    
    <tr>
       <td align="right" class="auto-style2" ><b>&nbsp;Duration in Month:</b></td>
        <td align="left" class="auto-style2">
            <asp:Label ID="durationLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                    </td>
        <td align="right" ><b>Account No:</b></td>
        <td align="left">
            <asp:Label ID="accountNoLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style7"></asp:Label>
                                                    </td>
         <td align="right" ><b>Bank Info:</b></td>
        <td align="left">
            <asp:Label ID="bankInfoLabel" runat="server" 
                    style="font-weight: 700; " CssClass="auto-style8"></asp:Label>
                                                    </td>      
     
    </tr>
                 
     
            
            
       
     
 </table>
        </fieldset>
        </td>                   
    </tr>
   <tr>
    <td colspan="6">

        &nbsp;</td>
</tr>     
<tr>
    <td align="center" colspan="6">

        <asp:Button ID="sipCreateScheduleButton" runat="server" Text="Create Schedule" CssClass="auto-style3" 
                  Width="140px" Height="22px" OnClick="sipCreateScheduleButton_Click" OnClientClick="return fnCheqInput();"
                />
        &nbsp;
        <asp:Button ID="sipSaveButton" runat="server" Text="Save Schedule"  OnClientClick="return fnCheqInput();"
                CssClass="auto-style3"    Width="140px" Height="22px" OnClick="sipSaveButton_Click" />
        &nbsp;
        
        <asp:Button ID="sipCreateScheduleButtonNew" runat="server" Text="Create Schedule New" CssClass="auto-style3" 
                  Width="140px" Height="22px"  OnClientClick="return fnCheqInput();" OnClick="sipCreateScheduleButtonNew_Click"
                />
        
    </td>
</tr>     
      <tr>
            <td align="center" colspan="6">
            
                <table align="left">
                    <tr>
                        <td align="center">
                         <div id="dvGridSurrender" runat="server"  >
                         
                                <asp:GridView ID="SaleListGridView" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                                         
                                <asp:BoundField DataField="REG_BK" HeaderText="Fund Code" />
                                <asp:BoundField DataField="REG_BR" HeaderText="Branch Code" />
                                <asp:BoundField DataField="REG_NO" HeaderText="Regi. No" />  
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" />  
                                <asp:BoundField DataField="SIP_NO" HeaderText="SIP No." />                                                                                                 
                                <asp:BoundField DataField="SCHEDULE_NO" HeaderText="Installment No. " /> 
                                <asp:BoundField DataField="DEBIT_DATE" HeaderText="Debit Date" /> 
                                <asp:BoundField DataField="AMOUNT" HeaderText=" Debit Amount" />                                                                                                                                                                                                                                                     
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
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
    <br />
    <br />
 
    <br />
</asp:Content>

