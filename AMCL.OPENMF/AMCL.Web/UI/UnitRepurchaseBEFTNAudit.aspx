<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRepurchaseBEFTNAudit.aspx.cs" Inherits="UI_UnitRepurchaseBEFTNAudit" Title="Unit UnitRepurchase BEFTN Audit (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 
 
   
       
      
    
  
 
        
 
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
            text-align: left;
        }
    </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Repurchase BEFTN Audit Form&nbsp;
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" colspan="4" class="style1">
            </td>
      </tr>
      <tr>
        <td   align="right" ><b>Fund Name :</b></td>
        
        <td   align="right" style="text-align: left" ><asp:DropDownList ID="fundNameDropDownList" runat="server"  AutoPostBack="true"
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" ></asp:DropDownList>
            </td>
        
        <td align="right" style="text-align: left" >           &nbsp;&nbsp;&nbsp;&nbsp;           &nbsp;</td>
        
        <td align="right" style="text-align: left" >                    
        <asp:Button ID="AuditButton" runat="server" Text="Submit Audit" 
                CssClass="buttoncommon"  Width="133px" Height="30px" 
                onclick="AuditButton_Click"/> 
           </td>
        
      </tr>
        <tr>
            <td align="left" colspan="4">
           &nbsp;</td>
      </tr>
       <tr>
            <td align="left" colspan="4">
            
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
                                <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="leftCheckBox" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FUND_NM" HeaderText="Name of The Fund" />
                                <asp:BoundField DataField="REG_BK" HeaderText="Fund Code" />
                                <asp:BoundField DataField="REG_BR" HeaderText="Branch Code" />
                                <asp:BoundField DataField="REG_NO" HeaderText="Regi. No" />  
                                <asp:BoundField DataField="REP_NO" HeaderText="Repurchase No" />  
                                <asp:BoundField DataField="REP_DT" HeaderText="Repurchase Date" /> 
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" />  
                                <asp:BoundField DataField="QTY" HeaderText="No of Units" />    
                                <asp:BoundField DataField="REP_PRICE" HeaderText="Rate" />  
                                <asp:BoundField DataField="TOTAL" HeaderText="Total Amount" /> 
                                                                                                                        
                                                             
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
            </td>   
      </tr>
       <tr>
              <td align="right" colspan="2" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="2" >&nbsp;</td>  
                
      </tr>
       <tr>
              <td align="right" colspan="2" >&nbsp;</td>  
                                
      </tr>    
</table>
            </td>
        </tr>
       
      </table>

    
</asp:Content>

