<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRepurchaseBEFTNAdvicePrint.aspx.cs" Inherits="UI_UnitRepurchaseBEFTNPostingAccount" Title="Unit UnitRepurchase BEFTN Posting Account (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 
 
   
       function fnCheckInput()
        {
    
            if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name ");
                return false;
               }
               
          if(document.getElementById("<%=BEFTNDateDropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=BEFTNDateDropDownList.ClientID%>").focus();
                alert("Please Select BEFTN Date ");
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
        .style1
        {
            height: 13px;
        }
        .style2
        {
            height: 20px;
        }
    </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Repurchase BEFTN Advice Form&nbsp;
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="1200" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" colspan="5" class="style1">
            </td>
      </tr>
      <tr>
     <td   align="right" ><b>Fund Name :</b></td>
        
        <td   align="right" style="text-align: left" ><asp:DropDownList ID="fundNameDropDownList" runat="server"  AutoPostBack="true"
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" ></asp:DropDownList>
            <span class="star">* </span>
            </td>
        <td   align="right" ><b>BEFTN Date :</b></td>
        
        <td align="left" >         
            <asp:DropDownList ID="BEFTNDateDropDownList" runat="server"  
                >
            </asp:DropDownList>
            <span class="star">* </span></td>
        
        
        
        <td align="left" >         
        <asp:Button ID="AdviceButton" runat="server" Text="Print Advice Letter" OnClientClick="return fnCheckInput();"
                CssClass="buttoncommon"  Width="150px" Height="30px" 
                onclick="AdviceButton_Click"/> 
                                            </td>
      </tr>
        <tr>
            <td align="left" colspan="5">
           &nbsp;</td>
      </tr>
       <tr>
            <td align="left" colspan="5">
            
                <table align="left">
                    <tr>
                        <td>
                         <div id="dvGridSurrender" runat="server"  >
                         
                                <asp:GridView ID="SurrenderListGridView" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                               
                                <asp:BoundField DataField="FUND_NM" HeaderText="Name of The Fund" />                               
                                <asp:BoundField DataField="REG_BR" HeaderText="Branch Code" />
                                <asp:BoundField DataField="REG_NO" HeaderText="Regi. No" />  
                                <asp:BoundField DataField="REP_NO" HeaderText="Rep No" />  
                                <asp:BoundField DataField="BEFTN_DATE" HeaderText="BEFTN_Date" /> 
                                <asp:BoundField DataField="HNAME" HeaderText="Name " />  
                                <asp:BoundField DataField="HOLDER_AC_NO" HeaderText="Account No." />    
                                <asp:BoundField DataField="HOLDER_ROUTING_NO" HeaderText="Routing No" />  
                                <asp:BoundField DataField="TOTAL_AMOUNT" HeaderText="Total Amount" /> 
                                <asp:BoundField DataField="BANK_AC_NO" HeaderText=" Fund Acc No." />    
                                <asp:BoundField DataField="BANK_ROUTING_NO" HeaderText=" Fund Routing No" />  
                                <asp:BoundField DataField="VOUCHER_NO" HeaderText=" Voucher No." /> 
                                                                                                                        
                                                             
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
            </td>   
      </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" >&nbsp;</td>  
                                
      </tr>    
</table>
            </td>
        </tr>
       
      </table>

    
</asp:Content>

