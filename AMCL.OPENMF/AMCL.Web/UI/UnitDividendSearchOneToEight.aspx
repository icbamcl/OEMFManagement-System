<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDividendSearchOneToEight.aspx.cs" Inherits="UI_UnitDividendSearchOneToEight" Title="Unit Dividend Search One To Eight Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(Confrm)
        {         
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
          
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";            
            
            document.getElementById("<%=holderAddress3TextBox.ClientID%>").value ="";
           
       
            return false;
        }
        else
        {
         return false;
        }
       
    }
    
    
     function fnResetAll()
    {
                                                       
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
                      
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";                                         
                   
            document.getElementById("<%=holderAddress3TextBox.ClientID%>").value ="";
           
             
             alert("No Data Found");
             return false;
      
    }
     function fnCheqInput()
        {
        
        
              //Input Text Checking
           if(document.getElementById("<%=BOTextBox.ClientID%>").value =="" && document.getElementById("<%=folioTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=BOTextBox.ClientID%>").focus();
               alert("Please Enter Only BO Number or Folio Number");
                return false;
                
            }
            if(document.getElementById("<%=BOTextBox.ClientID%>").value !="" && document.getElementById("<%=folioTextBox.ClientID%>").value !="")
            {
                document.getElementById("<%=BOTextBox.ClientID%>").focus();
                alert("Please Enter Only BO Number or Folio Number ");
                return false;
                
            }
            
    }
         
       
     
  

       
       
    function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		}
	}
	
</script>
 <style type="text/css">
      .style7
     {
         font-size: 12px;
         font-weight: bold;
         color:Red; 
        
     }
      .style9
     {
         text-align: center;
         text-decoration: underline;
         color: #6666FF;
         font-size: small;
     }
    
      .style11
     {}
     .style12
     {
         font-weight: bold;
         text-decoration: underline;
     }
      .style13
     {
         font-weight: bold;
         text-align: right;
     }
     .style14
     {
         text-align: left;
         font-weight: bold;
         font-size: medium;
         color: #CC0000;
         font-family: "Times New Roman";
     }
      .style15
     {
         text-align: left;
         font-size: medium;
         text-decoration: underline;
         color: #6666FF;
         font-family: Tahoma;
     }
     
      .style16
     {
         width: 52px;
         font-weight: bold;
     }
     .style17
     {
         font-size: 12px;
         font-weight: 700;
         color: Red;
         width: 42px;
     }
     .style18
     {
         width: 170px;
          font-size: 12px;
         font-weight: 700;
         color: Green;
     }
     
      .auto-style3 {
         font-family: Verdana, Arial, Helvetica, sans-serif;
         border: 1px #1B68B8 solid;
         BACKGROUND-COLOR: #FFFFDD;
         COLOR: #000000;
         FONT-SIZE: 12px;
         WIDTH: 280px;
         HEIGHT: 20px;
         padding-left: 2px;
         margin-left: 2px;
         font-weight: bold;
     }
     .auto-style4 {
         text-align: center;
         color: #6666FF;
         font-size: small;
     }
     
      .auto-style5 {
         width: 1203px;
     }
     .auto-style7 {
         width: 309px;
     }
     .auto-style11 {
         width: 298px;
     }
          
      </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Previous Year Dividend Information&nbsp;One to Eight&nbsp; Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      <div id="dvUpdatePannel" runat="server" style="text-align:left">
      
              <div id="dvContent" runat="server" style="width:1200px;  text-align:left;" >
              <table width="1200px" align="left" cellpadding="0" cellspacing="0" border="0" class="auto-style5" >
              <colgroup width="300"></colgroup>
              <colgroup width="150"></colgroup>
              <colgroup width="310"></colgroup>
                <tr>
                    <td align="right" class="auto-style4" colspan="3" >
                        <b>Fund Code:</b><asp:DropDownList ID="fundCodeDDL" runat="server" ></asp:DropDownList>
                        <b>/BO:</b> 
                                            <span class="star">
                        <asp:TextBox ID="BOTextBox" runat="server" CssClass="auto-style3" 
                            MaxLength="16" TabIndex="1" Width="170px"></asp:TextBox>*</span> &nbsp;<b>/Folio:<asp:TextBox ID="folioTextBox" runat="server" AutoPostBack="True" 
                            CssClass="TextInputStyleSmall" 
                            onkeypress="fncInputNumericValuesOnly()" 
                             TabIndex="2" Width="90px"></asp:TextBox>
                        </b>&nbsp;&nbsp;<asp:Button  ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                            onclick="findButton_Click" AccessKey="f" 
                            onclientclick="return fnCheqInput();" TabIndex="3" 
                            meta:resourcekey="findButtonResource1" /></td>
                            <td rowspan="5" align="center" class="auto-style11">
                              <div  id="dvImage" runat="server"  
                                    style=" border-style: solid; border-color: inherit; border-width: 1px; width: 217px; width:415px; height: 254px;">
                                <table align="center" width="164"  cellpadding="0" cellspacing="0" 
                                    style="height: 180px">
                                <tr style=" height:15px;">
                                   <td align="center" ><span  style="border:1px solid">Signature    and 
                                       Photo    </span></td>
                                    </tr>
                                   <tr style=" height:102px;">
                                    <td align="left">
                                        &nbsp;</td>    
                                </tr>
                                                               
                                </table>
                             </div>
                            </td>
                            
                </tr>
                <tr>
                    <td align="right" rowspan="5" class="auto-style7" >
                    <div  id="dvLockin" runat="server" style="visibility:hidden">
                        <table  width="252px" align="left" cellpadding="0" cellspacing="0" border="0">
                        <colgroup width="125"></colgroup>
                        <colgroup width="120"></colgroup>
                        <tr>
                                <td colspan="2" class="style15">
                                    <b style="text-align: center">Transaction Lock In Status</b></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    SALE LOCK: </td>
                                <td class="style14">
            
                                    &nbsp;
            
                                    <asp:Label ID="SaleLockLabel" runat="server" Text="NO"></asp:Label>
                                                        </td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    REPURCHASE LOCK:</td>
                                <td class="style14">
            
                                    &nbsp;
            
                                    <asp:Label ID="RepLockLabel" runat="server" Text="NO"></asp:Label>
                                                        </td>
                            </tr>
                              <tr>
                                <td class="style13">
                                    TRANSFER LOCK:</td>
                                <td class="style14"> &nbsp;
            
                                    <asp:Label ID="TransferLockLabel" runat="server" Text="NO"></asp:Label>
                                  </td>
                            </tr>
                            <tr>
                                <td class="style13"> &nbsp;
                                    LIEN LOCK:</td>
                                <td class="style14">
            
                                    &nbsp;
            
                                    <asp:Label ID="LienLockLabel" runat="server" Text="NO"></asp:Label>
                                </td>
                            </tr>
                              <tr>
                                <td class="style13"> &nbsp;
                                    RENEWAL LOCK:</td>
                                <td class="style14">
            
                                    &nbsp;
            
                                    <asp:Label ID="RenLockLabel" runat="server" Text="NO"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                             <tr>
                                <td class="style12" colspan="2" style="text-align: center">
                                    LOCK&nbsp; IN&nbsp; REMARKS</td>
                            </tr>
                             <tr>
                                <td class="style11" colspan="2">
                        <asp:TextBox ID="LockRemarksTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="63px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                            </tr>
                             <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                             <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                             <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    </td>
                    <td align="right" class="auto-style11">
                        <b>Name of Holder:</b></td>
                    <td align="left" class="auto-style11">
                        <asp:TextBox ID="holderNameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="3" 
                            meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
                    </td>
                   
                   
                </tr>
                 
                <tr>
                    <td align="right" class="auto-style11">
                        <b>Address1:</b></td>
                    <td align="left" class="auto-style11">
                        <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                            meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td align="right" class="auto-style11">
                        <b>Address2:</b></td>
                    <td align="left" class="auto-style11">
                        <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1"></asp:TextBox>
                                            </td>
                  
                    </tr>
                <tr>
                    <td align="right" class="auto-style11">
                        <b>Address3:</b></td>
                    <td align="left" class="auto-style11" >
                        <asp:TextBox ID="holderAddress3TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="6" 
                            meta:resourcekey="holderAddress2TextBoxResource1"></asp:TextBox>
                                            </td>
                    
                </tr>   
                <tr>
                    <td align="right" class="auto-style11">
                        &nbsp;</td>
                    <td align="left" class="auto-style11" >
                        &nbsp;</td>
                    <td class="auto-style11"></td>
                </tr>  
                <tr>
                    <td colspan="4">
                     <table>
                    <tr>
        <td align="left"> 
                &nbsp;</td>    
    </tr>
                     </table>
                    </td>
                    
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" 
                            style="width: 1203px">
                            <tr>
                                <td align="left" > 
                                     <div ID="dvLedger" runat="server" 
                                        style="text-align: left; display: block; overflow: auto; width:800; height:300px; padding-left:20px;">
                                       
                                        <asp:DataGrid ID="dgLedger" runat="server" AutoGenerateColumns="False" 
                                            BorderColor="Black" CellPadding="1" CellSpacing="1" 
                                            meta:resourcekey="dgLedgerResource1" Width="1117px">
                                            <SelectedItemStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="TableText" />
                                            <HeaderStyle CssClass="DataGridHeader" HorizontalAlign="Center" />
                                            <AlternatingItemStyle CssClass="AlternatColor" />
                                            <Columns>
                                             
                                             
                                                <asp:BoundColumn DataField="DIV_YEAR" HeaderText="FY" >
                                                    
                                                </asp:BoundColumn>
                                               
                                                <asp:BoundColumn DataField="WAR_NO" HeaderText="Warrant No">
                                                   
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BALANCE" HeaderText="No of Unit">
                                                   
                                                </asp:BoundColumn>
                                                
                                                <asp:BoundColumn DataField="TOT_DED" HeaderText="Tax Diduct">                                                   
                                                    
                                                </asp:BoundColumn>
                                                
                                                  <asp:BoundColumn DataField="DIV_AMT" HeaderText="Total Dividend">
                                                   
                                                </asp:BoundColumn>
                                               
                                                
                                                                                                                                                
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
              </table>
              </div>
            
    
   </div>
             
            
    
    <br />
    <br />
    <br />
    <br />
</asp:Content>

