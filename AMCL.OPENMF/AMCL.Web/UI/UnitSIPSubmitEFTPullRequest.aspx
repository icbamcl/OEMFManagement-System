<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSIPSubmitEFTPullRequest.aspx.cs" Inherits="UI_UnitSIPSubmitEFTPullRequest" Title="Submit EFT Pull Request Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
   
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Save");
        if(confirm)
        {                        
            document.getElementById("<%=fundAccNameLabel.ClientID%>").value = "";
            document.getElementById("<%=fundAccNoLabel.ClientID%>").value ="";
            document.getElementById("<%=BankNameLabel.ClientID%>").value = "";
            document.getElementById("<%=BranchNameLabel.ClientID%>").value = "";
                                 
            return false;
        }
        else
        {
            return true;
            
        }
    }
    
  
	 
  	  function fnCheqInput()
        {
            if(document.getElementById("<%=sipDayDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=sipDayDropDownList.ClientID%>").focus();
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
        </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />        
        <table align="center" runat="server" id="tableFundName">
        <tr>
            <td class="FormTitle" align="center">
        Unit Holder SIP EFT Pull Request Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
           <tr>
               <td></td>
           </tr>
      </table>
      
      
 <table width="1100px" align="left" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="160"></colgroup>
<colgroup width="310"></colgroup>
<colgroup width="200"></colgroup>
     <tr>
        <td align="right"   ><b>Select SIP Date:</b></td>
        <td align="left" colspan="6">
        
            <asp:DropDownList ID="sipDayDropDownList" runat="server" 
                CssClass="DropDownList"       
                AutoPostBack="True" OnSelectedIndexChanged="sipDayDropDownList_SelectedIndexChanged"  >
                                  
            </asp:DropDownList>                                                                                               
                                                   
                              <%--  <span class="star">&nbsp;<asp:Button ID="addListButton" 
                    runat="server"  CssClass="auto-style3"
                    
                   Text="Add To List" Width="97px" OnClick="addListButton_Click" />
                      </span>--%>
                                                   
                                            
                                                   
                                </td>
         <%--<td align="right" ><b>SIP&nbsp;EFT Pull Sequence No:</b></td>
                  <td align="left"  colspan="3">
        
            <asp:TextBox ID="fundCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" 
                meta:resourcekey="fundCodeTextBoxResource1" Width="52px"></asp:TextBox>
            <b>/</b><asp:TextBox ID="eftPullSeqNoTextBox" runat="server"
                CssClass= "TextInputStyleSmall" Width="89px"              
                 ></asp:TextBox>
                                <span class="star">&nbsp;</span></td>
       --%>
       
    </tr>
     <tr>              
        <td align="right" class="auto-style4" >
            <b>SIP Fund Account Name:</b></td>
       <td align="left" class="auto-style4">
        
            <asp:Label ID="fundAccNameLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                   
                                            
                                                   
                                </td>
        <td align="right" class="auto-style4" ><b>SIP Account No.:</b></td>
        <td align="left" class="auto-style4" colspan="3">
            <asp:Label ID="fundAccNoLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>

           
            </tr>
            
    
     <tr>
         <td align="right" ><b>SIP&nbsp;Bank Name:</b></td>
        <td align="left">
            <asp:Label ID="BankNameLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>
          <td align="right" ><b>&nbsp;SIP Branch Name:</b></td>
        <td align="left" colspan="">
            <asp:Label ID="BranchNameLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>
         
    </tr>
   
    
    <tr>
       <td align="right" class="auto-style2" ><b>&nbsp;Total No. of Holder:</b></td>
        <td align="left" class="auto-style2">
            <asp:Label ID="totalHolderLabel" runat="server" 
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>
        <td align="right" ><b>Total Debit Amount:</b></td>
        <td align="left" colspan="3">
            <asp:Label ID="TotalAmountLabel" runat="server"  
                    style="font-weight: 700; font-size: medium; color: #339933;" CssClass="auto-style7"></asp:Label>
                                                    </td>
           
     
    </tr>
   <tr>
    <td colspan="6">

        &nbsp;</td>
</tr>     
<tr>
    <td align="center" colspan="6">

        <asp:Button ID="submitEFTPullButton" runat="server" Text="Submit EFT Pull Request" CssClass="auto-style3" 
                  Width="154px" Height="22px" OnClick="submitEFTPullButton_Click"  OnClientClick="return fnCheqInput();"
                />
        &nbsp;
        &nbsp;
        
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
                                <asp:BoundField DataField="AMOUNT" HeaderText=" Debit Amount" />                                                                                                    
                                <asp:BoundField DataField="SIP_ACC_NO" HeaderText="Debit Acc. No." /> 
                                <asp:BoundField DataField="SIP_ACC_ROUTING_NO" HeaderText="Debit Acc. Routing No." />                                                                                                                                                                                                                                                                                       
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

