<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSMSGenerate.aspx.cs" Inherits="UI_UnitSMSSend" Title="Unit SMS Send (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
        function fnCheckAll(obj)
        {
            for (Looper = 0; Looper < document.forms[0].length ; Looper++)
            {
                var strType = document.forms[0].elements[Looper].type;
                if (strType == "checkbox")
                {

                    document.forms[0].elements[Looper].checked = obj;
                }
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
           Unit Holder&#39;s SMS Generate Form&nbsp;
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" colspan="5" class="style1">
            </td>
      </tr>
     
        <tr>
        <td   align="right" ><b>SMS Type</b>:</td>
        
        <td align="left" >         
            <asp:DropDownList ID="SMSType" runat="server">
            <asp:ListItem Value="0">Select Type</asp:ListItem>
            <asp:ListItem Value="CHQ">Repurchase Cheque </asp:ListItem>
            <%--<asp:ListItem Value="EFT">Repurchase BEFTN </asp:ListItem>
            <asp:ListItem Value="SL">Sale Certificate</asp:ListItem>
            <asp:ListItem Value="CIP">CIP Certificate</asp:ListItem>  --%>        
            </asp:DropDownList>
            </td>
        
        <td align="right" colspan="2" >  <b>&nbsp;Fund Name :</b>     
           </td>
        
        <td align="left" >  
            <asp:DropDownList ID="fundNameDropDownList" runat="server"  AutoPostBack="true"
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" ></asp:DropDownList>
            </td>
      </tr>
    
          <tr>
            <td align="left" colspan="5" class="style1">
            </td>
      </tr>
          <tr>
         <td   align="right" >&nbsp;</td>
        
       <td align="left" colspan="2" >         
            &nbsp;</td>
        
        <td align="left" class="style2" colspan="2" >         
        <asp:Button ID="SMSGenerateButton" runat="server" Text="Generate SMS" CssClass="buttoncommon" 
               Width="150px" Height="24px" OnClick="SMSGenerateButton_Click"/></td>
        
       
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
                         
                                <asp:GridView ID="SMSListGridView" runat="server" AutoGenerateColumns="False" 
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
                                <HeaderTemplate>                              
                                <input id="chkAllSMS" type="checkbox"   onclick="fnCheckAll(this.checked)"/>
                                </HeaderTemplate>                                 
                                <ItemTemplate>
                                <asp:CheckBox ID="leftCheckBox" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                                                                                                            
                                <asp:BoundField DataField="REG_BK" HeaderText="Fund Code" />
                                <asp:BoundField DataField="REG_BR" HeaderText="Branch Code" />
                                <asp:BoundField DataField="REG_NO" HeaderText="Regi. No" />                                 
                                <asp:BoundField DataField="TRAN_NO" HeaderText="Trans. No" />                                
                                <asp:BoundField DataField="HNAME" HeaderText="Name of Holder" /> 
                                <asp:BoundField DataField="MOBILE1" HeaderText="Mobile Number" />  
                                <asp:BoundField DataField="SMS" HeaderText="SMS Text" />  
                                <asp:BoundField DataField="SMS_CREATE_DATE" HeaderText="Create Date" />                             
                                <asp:BoundField DataField="SMS_TYPE" HeaderText="SMS Type" />

                               
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

