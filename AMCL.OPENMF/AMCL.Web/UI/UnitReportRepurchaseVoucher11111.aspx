<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportRepurchaseVoucher11111.aspx.cs" Inherits="UI_UnitReportRepurchaseVoucher" Title=" Repurchase Voucher Report(Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
   function fnReset()
   {
        document.getElementById("<%=fromRegNoTextBox.ClientID%>").value ="";
        
        document.getElementById("<%=fromRepDateTextBox.ClientID%>").value ="";
        
        document.getElementById("<%=fromRepNoTextBox.ClientID%>").value ="";
      
   }
  
 
 function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		}
	}
	function fnEnable(Action)
        {
       
            if(Action.indexOf("DeathRadioButton")!=-1)
            {   
                if( document.getElementById("<%=DeathRadioButton.ClientID%>").checked)                
                {       
                         
                               
                      if(document.getElementById("<%=dvChequeIssue.ClientID%>").style.visibility == "hidden")
                      {
                        
                         document.getElementById("<%=dvChequeIssue.ClientID%>").style.visibility = "visible";
                      }
                     if(document.getElementById("<%=divDeath.ClientID%>").style.visibility == "hidden")
                      {
                        
                         document.getElementById("<%=divDeath.ClientID%>").style.visibility = "visible";
                      }
                      
                }
                
             }
           if(Action.indexOf("NormalRadioButton")!=-1)
            {   
                if( document.getElementById("<%=NormalRadioButton.ClientID%>").checked)                
                {       
                         
                               
                      if(document.getElementById("<%=dvChequeIssue.ClientID%>").style.visibility == "visible")
                      {
                    
                        document.getElementById("<%=dvChequeIssue.ClientID%>").style.visibility = "hidden";
                      }
                      if(document.getElementById("<%=divDeath.ClientID%>").style.visibility == "visible")
                      {
                        
                         document.getElementById("<%=divDeath.ClientID%>").style.visibility = "hidden";
                      }
                    
                }
                
             }
           if(Action.indexOf("YesRadioButton")!=-1)
            {   
                if( document.getElementById("<%=YesRadioButton.ClientID%>").checked)                
                {       
                         
                               
                      if(document.getElementById("<%=divLienMark.ClientID%>").style.visibility == "hidden")
                      {
                        
                         document.getElementById("<%=divLienMark.ClientID%>").style.visibility = "visible";
                      }
                  
                      
                }
                
             }
           if(Action.indexOf("NoRadioButton")!=-1)
            {   
                if( document.getElementById("<%=NoRadioButton.ClientID%>").checked)                
                {       
                         
                               
                      if(document.getElementById("<%=divLienMark.ClientID%>").style.visibility == "visible")
                      {
                    
                        document.getElementById("<%=divLienMark.ClientID%>").style.visibility = "hidden";
                      }
                    
                    
                }
                
             }
         }
     function CheckAllDataGridWarrantNo(checkVal)
     {
            if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {  
                
                var datagrid=document.getElementById("<%=dinoGridView.ClientID%>")
                   
                var check = 0;                
                
                for( var rowCount = 0; rowCount < datagrid.rows.length; rowCount++)
                {
                  var tr = datagrid.rows[rowCount];
                  var td= tr.childNodes[0]; 
                  var item = td.firstChild; 
                  var strType=item.type;
                  if(strType=="checkbox")
                  {
                        item.checked = checkVal; 
                  }
                  
                }
            }
     }
</script>



    <style type="text/css">
        .style7
        {
            height: 20px;
        }
        .style10
        {
            color: #FF3300;
            background-color: #FFFFFF;
        }
        #divDeath
        {
            height: 109px;
        }
        .style11
        {
            width: 184px;
        }
        .style12
        {
            width: 507px;
        }
        .style13
        {
            color: #FF3300;
        }
        .style14
        {
            height: 18px;
        }
    </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
     
      
      <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>  
      <table align="center" cellpadding="0" cellspacing="0" width="1200px">
      
        <tr>
            <td align="center" colspan="2" class="FormTitle" >
                Unit Repurchase Voucher Report Form (<span id="spanFundName" runat="server"></span>)
            </td>
      </tr>
        <tr>
            <td align="left" colspan="2">
            &nbsp;
            </td>
      </tr>
      <tr>
        <td align="right" class="style14" >
            Registration:
        </td>
        <td align="left" class="style14">
        <asp:TextBox ID="fundCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" Width="52px" TabIndex="1"></asp:TextBox>
            <asp:TextBox ID="branchCodeTextBox" runat="server" 
                CssClass="TextInputStyleSmall" Enabled="False" TabIndex="2" Width="50px"></asp:TextBox>
            <asp:TextBox ID="fromRegNoTextBox" runat="server" 
                CssClass="TextInputStyleSmall" onkeypress="fncInputNumericValuesOnly()" 
                TabIndex="5" Width="69px"></asp:TextBox>
        </td>
      </tr>
     
       <tr>
        <td align="right"  >
            Repurchase No:</td>
        <td align="left" >
        <asp:TextBox ID="fromRepNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="72px" TabIndex="3" 
                onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> 
            <span class="style13">*</span>&nbsp; &nbsp;</td>
      </tr>
      
      
   <tr>
        <td align="right" >
            Repurchase&nbsp; Date:</td>
        <td align="left" class="style7">
        <asp:TextBox ID="fromRepDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="6"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="fromRepDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="7" />
            </td>
      </tr>
       <tr>
                      <td align="right" class="style12">
                          Include Lien Amount <asp:RadioButton ID="NoRadioButton" 
                              runat="server" Checked="True"  GroupName="Lien" onclick="fnEnable('NoRadioButton');"
                               TabIndex="15" Text="No" 
                              style="font-weight: 700" />
                          &nbsp;<asp:RadioButton ID="YesRadioButton" runat="server"  onclick="fnEnable('YesRadioButton');"
                              GroupName="Lien"  TabIndex="15" 
                              Text="Yes" style="font-weight: 700" />
                          &nbsp; <b>
                      </td>
                       <td align="right">
                       
                  <table>
                    <tr>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Repurchase Type:</td>
                        <td>
                            <asp:RadioButton ID="NormalRadioButton" runat="server" Font-Bold="True"  onclick="fnEnable('NormalRadioButton');"
                                GroupName="RepType" Text="Normal" Checked="True" />
                        </td>
                        <td>
                            <asp:RadioButton ID="DeathRadioButton" runat="server" Font-Bold="True" onclick="fnEnable('DeathRadioButton');"
                                GroupName="RepType" Text="Death" />
                        </td>
                        <td runat="server" id="tdChequeIssue">
                        <div id="dvChequeIssue" runat="server" style=" visibility: visible;">
                                <table>
                                    <tr>
                                        <td>Cheque Issue To:</td>
                                        <td>
                                            <asp:DropDownList ID="ChequeIssueToDropDownList" runat="server" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ChequeIssueToDropDownList_SelectedIndexChanged">
                                            <asp:ListItem Value="S">Successor</asp:ListItem>
                                            <asp:ListItem Value="N">Nominee</asp:ListItem>
                                            <asp:ListItem Value="J">Joint Holder</asp:ListItem>                                            
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                      </td>
                  </tr>
    <tr>
            <td class="style12">   
                <div id="divLienMark" runat="server" style="text-align: center; visibility:visible; height: 106px;">
                    <table align="left" cellpadding="0" cellspacing="0" style="width: 541px">
                   
                   <tr>
                      <td align="right" class="style11">
                        Amount (in Tk.): 
                        </td>
                        <td align="left">
                          <asp:TextBox ID="LienAmountTextBox" runat="server" 
                              CssClass="TextInputStyleSmall" Width="117px"></asp:TextBox>
                         
                            <span class="style10">*</span></td>
                  </tr>
                  <tr>
              <td align="right" class="style11">
                  Lien Institution:</td>
              <td align="left">
                  <asp:DropDownList ID="LienbankNameDropDownList" runat="server" 
                      AutoPostBack="true" 
                      onselectedindexchanged="LienbankNameDropDownList_SelectedIndexChanged" 
                      TabIndex="16">
                  </asp:DropDownList>
                  <span class="star">* </span>
              </td>
          </tr>
          <tr>
              <td align="right" class="style11">
                  Lien Institution Branch:</td>
              <td align="left">
                  <asp:DropDownList ID="LienbranchNameDropDownList" runat="server" 
                     TabIndex="16">
                  </asp:DropDownList>
                  <span class="star">* </span>
              </td>
          </tr>
          <tr>
              <td align="right" class="style11">
                  Surrender Letter Reference No:</td>
              <td align="left">
                  <asp:TextBox ID="LienReqRefTextBox" runat="server" 
                      CssClass="TextInputStyleLarge" MaxLength="55" TabIndex="7"></asp:TextBox>
                  <span class="star">* </span>
              </td>
          </tr>
          <tr>
              <td align="right" class="style11">
                  Letter Reference Date:</td>
              <td align="left">
                  <span class="star">
                  <asp:TextBox ID="LienReqDateTextBox" runat="server" 
                      CssClass="textInputStyleDate" TabIndex="4"></asp:TextBox>
                  <ajaxToolkit:CalendarExtender ID="LienReqDatecalendarButtonExtender" 
                      runat="server" Format="dd-MMM-yyyy" PopupButtonID="LienReqDateImageButton" 
                      TargetControlID="LienReqDateTextBox" />
                  <asp:ImageButton ID="LienReqDateImageButton" runat="server" 
                      AlternateText="Click Here" 
                      ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="10" />
                  * </span>
              </td>
          </tr>
                    </table>
                    
                </div>                                        
            </td>
            <td align="left">  
              <div id="divDeath" runat="server" style="text-align: right; display: block; overflow: auto; width:451px; height:109px; visibility:visible;" >
                <asp:DataGrid ID="dinoGridView" runat="server" AutoGenerateColumns="False" 
                    meta:resourcekey="dinoGridViewResource1"  >
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                <ItemStyle CssClass="TableText"></ItemStyle>
                <HeaderStyle CssClass="TableHeader2"></HeaderStyle>
                <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                <Columns>
                
                <asp:TemplateColumn>
                 <HeaderTemplate>
                                    <input id="chkAllWarrant" type="checkbox" onclick="CheckAllDataGridWarrantNo(this.checked)"> 
                   </HeaderTemplate>
                    <ItemTemplate> 
                         <asp:CheckBox ID="leftCheckBox" runat="server" ></asp:CheckBox> 
                    </ItemTemplate>                    
                    </asp:TemplateColumn>       
                 <asp:TemplateColumn HeaderText="Name" >
                        <ItemTemplate>
                            <asp:TextBox ID="dinoTextBox" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem,"chqName") %>'></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="False" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                                                                          
                </Columns>
            </asp:DataGrid>         
              </div>                            
            </td>
      </tr>
      
          
          
          <tr>
              <td align="left" colspan="2">
                  &nbsp;
                
                  <br />
                  <br />
                  <br />
              </td>
          </tr>
          <tr>
              <td colspan="2">
                  <table align="center" cellpadding="0" cellspacing="0" width="500">
                      <tr>
                          <td align="right">
                              <asp:Button ID="ShowReportButton" runat="server" AccessKey="V" 
                                  CssClass="buttoncommon" onclick="ShowReportButton_Click" Text="View Repoert" />
                              &nbsp;
                          </td>
                          <td align="left">
                              &nbsp;&nbsp;&nbsp;
                              <asp:Button ID="regResetButton" runat="server" AccessKey="r" 
                                  CssClass="buttoncommon" OnClientClick="return fnReset();" Text="Reset" />
                              &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                              <asp:Button ID="regCloseButton" runat="server" CssClass="buttoncommon" 
                                  onclick="regCloseButton_Click" Text="Close" />
                          </td>
                      </tr>
                  </table>
              </td>
          </tr>
        <tr>
            <td align="left" colspan="2">
            &nbsp;
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
      </tr>
      
</table>
      </ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="LienbankNameDropDownList" EventName="SelectedIndexChanged" />
    <asp:AsyncPostBackTrigger ControlID="YesRadioButton" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="NoRadioButton" EventName="CheckedChanged" />
    
</Triggers>
</asp:UpdatePanel> 
    
    </asp:Content>

