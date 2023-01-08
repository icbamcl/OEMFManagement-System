<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitLienMark.aspx.cs" Inherits="UI_UnitLienMark" Title=" Unit Lien Mark Entry (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
 function fnReset()
  {
      var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
             document.getElementById("<%=LienMarkNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=LienReqDateTextBox.ClientID%>").value ="";
             document.getElementById("<%=LienbankNameDropDownList.ClientID%>").value ="0";
             document.getElementById("<%=LienbranchNameDropDownList.ClientID%>").value ="0";
             document.getElementById("<%=LienReqRefTextBox.ClientID%>").value ="";
             document.getElementById("<%=RegNoTextBox.ClientID%>").value ="";
             document.getElementById("<%=HolderNameTextBox.ClientID%>").value ="";             
             document.getElementById("<%=HolderJNameTextBox.ClientID%>").value ="";
             document.getElementById("<%=TotalUnitHoldingTextBox.ClientID%>").value ="";
             document.getElementById("<%=TotalUnitLienTextBox.ClientID%>").value ="";
             document.getElementById("<%=tdSign.ClientID%>").innerHTML ="";
            
             if(document.getElementById("<%=leftDataGrid.ClientID%>"))
             {
                document.getElementById("<%=divLeftgrid.ClientID%>").innerHTML ="";
             }  
             return false;
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
          if(document.getElementById("<%=LienMarkNoTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=LienMarkNoTextBox.ClientID%>").focus();
                alert("Please Enter Lien Number ");
                return false;
               }
               
           if(document.getElementById("<%=LienbankNameDropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=LienbankNameDropDownList.ClientID%>").focus();
                alert("Please Select Lien Institution  ");
                return false;
               }
                
          if(document.getElementById("<%=LienbranchNameDropDownList.ClientID%>").value =="0")
               {
                document.getElementById("<%=LienbranchNameDropDownList.ClientID%>").focus();
                alert("Please Select Lien Institution Branch  ");
                return false;
               }
        
          if(document.getElementById("<%=LienReqDateTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=LienReqDateTextBox.ClientID%>").focus();
            alert("Please Enter Lien Request Date  ");
            return false;
           }
            if(document.getElementById("<%=LienReqRefTextBox.ClientID%>").value =="")
           {
            document.getElementById("<%=LienReqRefTextBox.ClientID%>").focus();
            alert("Please Enter Lien Request Reference  ");
            return false;
           }

            if(document.getElementById("<%=LienMarkNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=LienMarkNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=LienMarkNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Lien Mark   Number");
                    return false;
                }
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
       
      
      
        if(document.getElementById("<%=LienReqDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=LienReqDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=LienReqDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
      
      
        if(document.getElementById("<%=leftDataGrid.ClientID%>"))
        {
        
            var leftDatagrid=document.getElementById("<%=leftDataGrid.ClientID%>")
            var saleCert="";    
            var chek=0;                         
            for( var rowCount = 1; rowCount < leftDatagrid.rows.length; rowCount++)
            {
              var tr = leftDatagrid.rows[rowCount];
              var td= tr.childNodes[0]; 
              var item = td.firstChild; 
              var strType=item.type;
              if(strType=="checkbox")
              {
                if(item.checked)
                {
                 chek=chek+1;
                    if(saleCert=="")
                    {                       
                        saleCert=leftDatagrid.rows[rowCount].cells[1].innerHTML+"::"+leftDatagrid.rows[rowCount].cells[2].innerHTML;
                     }
                     else
                     {
                        if(leftDatagrid.rows[rowCount].cells[1].innerHTML==leftDatagrid.rows[rowCount-1].cells[1].innerHTML)
                        {
                            saleCert=saleCert+","+leftDatagrid.rows[rowCount].cells[2].innerHTML;
                         }
                         else
                         {
                            saleCert=saleCert+"\n"+leftDatagrid.rows[rowCount].cells[1].innerHTML+"::"+leftDatagrid.rows[rowCount].cells[2].innerHTML;
                         }
                     }
                }
              }
            } 
            
           if(chek>0)
            {
                var msg="Are You Sure to Lien Mark The Following Sale Certificate?"+'\n';
                msg=msg+" Sale Certificates: "+'\n'+saleCert;  
                var  conformMsg=confirm(msg);       
                if(conformMsg)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                alert("Please Select Any Sale Number To Lein");
                return false;
            }
  
        }
        else
        {
          alert("No Units to Lein Mark");
          return false;
        }  
     }
      
      
    
     
     
  
   function fnSelectedTotalUnit()
  {
    if(document.getElementById("<%=leftDataGrid.ClientID%>"))
            {      
                var datagrid=document.getElementById("<%=leftDataGrid.ClientID%>")
                var sum = 0;    
                var check = 0;                
                
                for( var rowCount = 0; rowCount < datagrid.rows.length; rowCount++)
                {
                  var tr = datagrid.rows[rowCount];
                  var td= tr.childNodes[0]; 
                  var item = td.firstChild; 
                  var strType=item.type;
                  if(strType=="checkbox")
                  {
                    if(item.checked)
                    {
                    datagrid.rows[rowCount].style.backgroundColor='#DDAAFF';
                     check = check +1;
                        
                     sum = sum + parseInt(datagrid.rows[rowCount].cells[3].innerHTML);
                       
                    }
                    else
                    {
                        if(rowCount%2==0)
                        {
                            datagrid.rows[rowCount].style.backgroundColor='#D5E0E6';
                        }
                        else
                        {
                            datagrid.rows[rowCount].style.backgroundColor='#DBEAF5';
                        }
                    }
                  }
                }
                document.getElementById("<%=TotalUnitLienTextBox.ClientID%>").value=parseInt(sum);
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
        .style6
        {
            font-family: "Times New Roman";
            font-weight: bold;
        }
        .style11
        {
            height: 20px;            
        }
         .style8
        {
            font-size: 14px;
        }
        .style12
        {
            font-weight: normal;
        }
        .style13
        {
            width: 60px;
        }
        .style14
        {
            font-weight: normal;
            width: 77px;
        }
        .style15
        {
            width: 81px;
        }
        .style16
        {
            width: 84px;
        }
        </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
       <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Lien Mark Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>

      <br />
    
      <div id="dvContent" runat="server" 
        style="width:924px; padding-right:100px; padding-bottom:10px;  text-align:left" >
          <div  id="dvLeftContent" style=" float:left; width:572px;">
          <table align="center" cellpadding="0" cellspacing="0" border="0" 
                  style="width: 560px" >
    <colgroup width="150"></colgroup>
    <colgroup width="300"></colgroup>
        <tr>
            <td colspan="2" align="left">&nbsp;</td>
            
        </tr>
        <tr>
            <td align="left" >Lien Mark No:</td>
            <td align="left">
                <asp:TextBox ID="LienMarkNoTextBox" runat="server"  MaxLength="6" 
                    CssClass= "TextInputStyleSmall" TabIndex="2" 
                    onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                    <span class="star">*</span></td>
                       
        </tr>
        <tr>
            <td align="left" >Lien Request Date:</td>
            <td align="left">
                <span class="star">
                <asp:TextBox ID="LienReqDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="4"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                    TargetControlID="LienReqDateTextBox" PopupButtonID="RegDateImageButton" 
                    Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="RegDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" />
                * </span></td>
            
           
        </tr>
        <tr>
            <td align="left" >Lien Institution :</td>
            <td align="left">
               
                <asp:DropDownList ID="LienbankNameDropDownList" runat="server" 
                    AutoPostBack="true" 
                    onselectedindexchanged="LienbankNameDropDownList_SelectedIndexChanged" 
                    TabIndex="5">
                </asp:DropDownList>
               
            </td>
            
           
        </tr>
        <tr>
            <td align="left" >Lien Institution Branch:</td>
            <td align="left">
               
                <asp:DropDownList ID="LienbranchNameDropDownList" runat="server"   TabIndex="5">
                </asp:DropDownList>
               
            </td>
           
        </tr>
        <tr>
             <td align="left" class="style11"  >Lien Request Reference:</td>
            <td align="left" class="style11" >
                <asp:TextBox ID="LienReqRefTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" TabIndex="7"></asp:TextBox>
             </td>
             
        </tr>
        <tr>
            <td align="left" >Registration No :</td>
            <td align="left">
                <asp:TextBox ID="FundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" TabIndex="3"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="BranchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="4"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="RegNoTextBox" runat="server"  MaxLength="8" onkeypress= "fncInputNumericValuesOnly()"
                    CssClass= "TextInputStyleSmall" TabIndex="1" 
                    ontextchanged="RegNoTextBox_TextChanged" AutoPostBack="True" 
                    Width="86px"></asp:TextBox>
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
            <td align="left">&nbsp;</td>
            <td align="left">
                 <asp:TextBox ID="HolderJNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="7"></asp:TextBox></td>
                                     
        </tr>

        </table>
        </div>
        <div style="border-style: solid; border-color: inherit; border-width: 1px; width:229px; float:left; height:172px;">
                <table align="center" width="164" cellpadding="0" cellspacing="0" 
                    style="height: 98px">
                <tr style=" height:15px;">
                   <td align="center" ><span   style="border:1px solid"><b>Signature and Photo</b>&nbsp; &nbsp;    </span></td></tr>
                   <tr>
                    <td align="left" id="tdSign" runat="server">
                  <asp:Image ID="SignImage" runat="server" Height="156px" Width="222px"  />
                    </td>    
                </tr>
              
                </table>
       </div>
    </div>
    
    
    <div id="dvTotalUnits" runat="server" 
              style="text-align:left;  width:459px; height: 43px;" >
     <table align="left" cellpadding="0" cellspacing="0" style="width: 455px">
     <colgroup width="250"></colgroup>
    
        <tr >
           <td align="left" ><b>Total Holding&nbsp; Units:            <td align="left">
                <asp:TextBox ID="TotalUnitHoldingTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#009933"></asp:TextBox>
            </td>           
        </tr>
         <tr >
           <td align="left" ><b>Selected Total Units for Lien Mark:</b></td>
            <td align="left">
                <asp:TextBox ID="TotalUnitLienTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall"   Enabled= "false" Width="100px" 
                    Font-Bold="True" ForeColor="#990033"></asp:TextBox>
            &nbsp;<asp:Button ID="AddTotalButton" runat="server" AccessKey="l" 
                   CssClass="buttoncommon" EnableTheming="True" 
                   Text="Add Total" 
                   Height="18px" Width="87px" onclick="AddTotalButton_Click" />
            </td>           
        </tr>                      
      </table>
      
    </div>
    <br>
    
          <div ID="dvContentBottom" runat="server" style="width:559px; height: 266px;">
              <div ID="divLeftgrid" runat="server" 
                  style="width:494px; float:left; text-align:center;">
                  <table align="center" style="width: 427px; height: 20px;">
                      <tr class="style6">
                          <td align="center" class="style13">
                              <span class="style12"><a href="#" onclick="fnCheckAll();">Select All</a></span>
                          </td>
                          <td class="style16">
                              <span class="style8">Sale No</span></td>
                          <td>
                              <span class="style8">Certificate</span></td>
                          <td>
                              <span class="style8">Weight</span></td>
                      </tr>
                  </table>
                  <table align="center" style="width: 430">
                      <tr>
                          <td>
                              <div style="text-align: center; display: block; overflow: auto; width:430px; height:200px;">
                                  <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" 
                                      Width="403px">
                                      <SelectedItemStyle HorizontalAlign="Center" />
                                      <ItemStyle CssClass="TableText" />
                                      <HeaderStyle CssClass="DataGridHeader" />
                                      <AlternatingItemStyle CssClass="AlternatColor" />
                                      <Columns>
                                          <asp:TemplateColumn HeaderText="">
                                              <ItemTemplate>
                                                  <asp:CheckBox ID="leftCheckBox" runat="server" 
                                                      onclick="fnSelectedTotalUnit();" />
                                              </ItemTemplate>
                                          </asp:TemplateColumn>
                                          <asp:BoundColumn DataField="SL_NO"></asp:BoundColumn>
                                          <asp:BoundColumn DataField="CERTIFICATE"></asp:BoundColumn>
                                          <asp:BoundColumn DataField="QTY"></asp:BoundColumn>
                                      </Columns>
                                  </asp:DataGrid>
                              </div>
                              <table align="left" style="width: 427px; height: 20px;">
                                  <tr class="style6">
                                      <td align="center" class="style14">
                                          <a href="#" onclick="fnUnCheckAll();">Deselect All</a></td>
                                      <td class="style15">
                                          <span class="style8">Sale No</span></td>
                                      <td>
                                          <span class="style8">Certificate</span></td>
                                      <td>
                                          <span class="style8">Weight</span></td>
                                  </tr>
                              </table>
                          </td>
                      </tr>
                  </table>
              </div>
          </div>
          
    
    <br />
    
   
    <table align="left" cellpadding="0" cellspacing="0" style="width: 1230px">
     <tr>
        <td align="right">
        
        </td>
        <td align="center">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheckInput();"
                 AccessKey="s" onclick="SaveButton_Click"/>&nbsp;
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
   
    <br />
    <br />
    <br />
    <br />
</asp:Content>

