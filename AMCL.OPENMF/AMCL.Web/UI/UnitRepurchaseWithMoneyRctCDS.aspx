<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRepurchaseWithMoneyRctCDS.aspx.cs" Inherits="UI_UnitRepurchaseWithMoneyRctCDS" Title="Unit Repurchase Entry (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 function fnReset()
  {
      var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
              document.getElementById("<%=moneyReceipDropDownList.ClientID%>").value ="0";
             document.getElementById("<%=RepRateTextBox.ClientID%>").value ="";
             document.getElementById("<%=RepDateTextBox.ClientID%>").value ="";
             document.getElementById("<%=RegNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=HolderNameTextBox.ClientID%>").value ="";             
             document.getElementById("<%=HolderJNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=TotalUnitHoldingTextBox.ClientID%>").value ="";
             document.getElementById("<%=TotalUnitRepurchaseTextBox.ClientID%>").value ="";
             document.getElementById("<%=tdSign.ClientID%>").innerHTML ="";
            
           
            
         return false;
        }
    }
    function CheckAllData(checkVal) {
        if (document.getElementById("<%=leftDataGrid.ClientID%>")) {
         

            var datagrid = document.getElementById("<%=leftDataGrid.ClientID%>")

            var check = 0;

            for (var rowCount = 1; rowCount < datagrid.rows.length; rowCount++) {
                
                var tr = datagrid.rows[rowCount];
                var td = tr.childNodes[0];
                
                var item = td.firstChild;              
                var strType = item.type;

                if (strType == "checkbox") 
                {
                    item.checked = checkVal;
                }
            }
        }
    }
 function fnCheckAll()
    {
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
                document.forms[0].elements[Looper].checked=true;
            }   
        }
        fnSelectedTotalUnit();
    }
    function fnUnCheckAll()
    {
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
                document.forms[0].elements[Looper].checked=false;
          }   
        }
        fnSelectedTotalUnit();
    }
    
    
    function fnCheckInput()
    {
             
           if( document.getElementById("<%=moneyReceipDropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=moneyReceipDropDownList.ClientID%>").focus();
                alert("Please Select Repurchase Number ");
                return false;
               }
               
               
           if(document.getElementById("<%=RepRateTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=RepRateTextBox.ClientID%>").focus();
                alert("Please Enter Repurchase Rate ");
                return false;
               }
        
          if(document.getElementById("<%=RepDateTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=RepDateTextBox.ClientID%>").focus();
            alert("Please Enter Date of Repurchase  ");
            return false;
           }

           
          if(document.getElementById("<%=RegNoTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=RegNoTextBox.ClientID%>").focus();
            alert("Please Enter Registration Number ");
            return false;
           }
         if(document.getElementById("<%=RegNoTextBox.ClientID%>").value !="")
        {
            var digitCheck = /^\d+$/;
            if(!digitCheck.test(document.getElementById("<%=RegNoTextBox.ClientID%>").value))
            {
                document.getElementById("<%=RegNoTextBox.ClientID%>").focus();
                alert("Please Enter Numaric value for Registration Number");
                return false;
            }
        }
       
      
      
        if(document.getElementById("<%=RepDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=RepDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=RepDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
      
      
      
     }

     function fnSelectedTotalUnit() {
         if (document.getElementById("<%=leftDataGrid.ClientID%>")) {
             var datagrid = document.getElementById("<%=leftDataGrid.ClientID%>");
              var sum = 0;
              var check = 0;  
             for (Looper = 0; Looper < document.forms[0].length; Looper++) {
                 var strType = document.forms[0].elements[Looper].type;
                 if (strType == "checkbox"  && document.forms[0].elements[Looper].checked == true)
                  {

                     sum = sum + parseInt(datagrid.rows[rowCount].cells[2].innerHTML);
                 }
             }
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
        .style4
        {
            text-align: center;
        }
        .style5
        {
            text-align: left;
        }
        .auto-style1 {
            width: 815px;
            height: 213px;
        }
    </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Repurchase Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>

    
      
    
      <div id="dvContent" runat="server" class="auto-style1" >
          <div  id="dvLeftContent" style=" float:left; width:430px;">
          <table align="center" cellpadding="0" cellspacing="0" border="0" 
                  style="width: 488px" >
    <colgroup width="130"></colgroup>
    <colgroup width="350"></colgroup>
        <tr>
            <td colspan="2" align="left">&nbsp;</td>
            
        </tr>
        <tr>
            <td align="left" >Repurchase No:</td>
            <td align="left">
                                    <span class="star">
            <asp:DropDownList ID="moneyReceipDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="moneyReceipDropDownList_SelectedIndexChanged" >
            </asp:DropDownList>
                                    *</span></td>
                       
        </tr>
        <tr>
            <td align="left" >Repurchase Date</td>
            <td align="left">
                <asp:TextBox ID="RepDateTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True"></asp:TextBox>
            </td>
            
           
        </tr>
        <tr>
             <td align="left"  >Repurchase Rate:</td>
            <td align="left" >
                <asp:TextBox ID="RepRateTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ></asp:TextBox>
                                    </td>
             
        </tr>
        <tr>
             <td align="left"  >No of Units :</td>
            <td align="left" >
                <asp:TextBox ID="QtyTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ></asp:TextBox>
                                    </td>
             
        </tr>
        <tr>
            <td align="left" >&nbsp;Reg No :</td>
            <td align="left">
                <asp:TextBox ID="FundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" TabIndex="3"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="BranchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="4"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="RegNoTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;</td>
            
           
           
        </tr>
         
        <tr>
            <td align="left" >Name of Holder:</td>
            <td align="left">
                <asp:TextBox ID="HolderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="6"></asp:TextBox>
            </td>
        </tr>
        
       
            
        <tr>
            <td align="left">Name of Joint Holder:</td>
            <td align="left">
                 <asp:TextBox ID="HolderJNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="7"></asp:TextBox></td>
                                     
        </tr>
        <tr>
            <td align="left">Payment Type:</td>
            <td align="left">
                  <table>
                    <tr>
                       
                        <td>
                            &nbsp;</td>
                         <td>
                             &nbsp;</td>
                        <td id="Td1" runat="server">
                        
                            <asp:Label ID="payTypeLabel" runat="server" Font-Bold="True" Font-Size="Small" Text="payType"></asp:Label>
                        
                        </td>
                    </tr>
                </table></td>
                                     
        </tr>
        </table>
      </div>
          <div style="border-style: solid; border-color: inherit; border-width: 1px; width:229px; float:left; height:153px;">
                <table align="center" width="164" cellpadding="0" cellspacing="0" 
                    style="height: 98px">
                <tr style=" height:15px;">
                   <td align="center" ><span   style="border:1px solid"><b>Signature and Photo</b>&nbsp;    &nbsp;    </span></td></tr>
                   <tr>
                    <td align="left" id="tdSign" runat="server">
                  <asp:Image ID="SignImage" runat="server" Height="136px" Width="222px"  />
                    </td>    
                </tr>
              
                </table>
           </div>
    </div>
            
  
     <table align="center" cellpadding="0" cellspacing="0" style="width: 467px">
    
    
        <tr >
           <td align="left" ><b>Total Holding&nbsp; Units: </b></td>
           <td align="left">
                <asp:TextBox ID="TotalUnitHoldingTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#009933"></asp:TextBox>
            </td>           
            </tr>
         <tr >
           <td align="left" ><b>Selected Total Units for Repurchase:</b></td>
            <td align="left">
                <asp:TextBox ID="TotalUnitRepurchaseTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#990033"></asp:TextBox>
        <asp:Button ID="addTotalSum" runat="server" Text="Add Total" CssClass="buttoncommon" 
                 AccessKey="s" onclick="addTotalSum_Click" />
            </td>           
        </tr>
        <tr>
             <td colspan="2">&nbsp;        </td>
        </tr>
              <tr>
              <td colspan="2">
        <table align="center" style="width: 400px; height: 20px;" cellpadding="1px" cellspacing="1px">
                <colgroup width="120" style="text-align: right"></colgroup>
                 <colgroup width="120"></colgroup>
                
         <tr>
         
         <td align="right" ><b>SL_TR_NO.</b></td>
         <td class="style4"><b style="text-align: right">Surrender Units</b></td>
         <td class="style5"><b style="text-align: left">Exist Unit</b></td>
         </tr>
         <tr>
         <td colspan="3">
          <div style="text-align: center; display: block; overflow: auto; width:400px; height:195px;">
                <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" 
                        ShowHeader="False">
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                <ItemStyle CssClass="TableText"></ItemStyle>
                <HeaderStyle CssClass="TableHeader2"></HeaderStyle>
                <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                <Columns>
                
                <asp:TemplateColumn>
                
                    <ItemTemplate> 
                         <asp:CheckBox ID="leftCheckBox" runat="server" /> 
                    </ItemTemplate>                    
                    </asp:TemplateColumn>       
                 <asp:TemplateColumn HeaderText="SL_TR_No." >
                        <ItemTemplate>
                            <asp:TextBox ID="SL_TR_NoTextBox" runat="server"  Enabled="false" Width="100px"
                                Text='<%# DataBinder.Eval(Container.DataItem,"SL_TR_NO") %>'
                               
                                ></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="False" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="Surrender_Units" >
                        <ItemTemplate>
                            <asp:TextBox ID="Sale_UnitsTextBox" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem,"SURRENDER_UNITS") %>' Width="100px"
                                onkeypress= "fncInputNumericValuesOnly()"   
                                meta:resourcekey="certNoTextBoxResource1"></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"  Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                              
                 <asp:TemplateColumn HeaderText="Exist_Units">
                        <ItemTemplate>
                            <asp:TextBox ID="Exist_UnitsTextBox" runat="server"   
                                Text='<%# DataBinder.Eval(Container.DataItem,"EXIST_UNITS") %>'  
                                Width="100px" Enabled="false" ></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                
                  
                  
                </Columns>
            </asp:DataGrid>
                   </div>
           </td>
            </tr>
            </table>        
         </td>
         </tr>
     
         <tr>
         <td colspan="2">
             <table align="center" cellpadding="0" cellspacing="0" style="width: 313px">
     <tr>
        <td align="right">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheckInput();"
                 AccessKey="s" onclick="SaveButton_Click"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" 
                  />
        
        </td>
        <td>
        &nbsp;
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
</asp:Content>

