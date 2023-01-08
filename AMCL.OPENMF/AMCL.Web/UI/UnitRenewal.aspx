<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRenewal.aspx.cs" Inherits="UI_Renewal" Title=" Unit Renwal Entry Form (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(Confrm)
        {
           
            document.getElementById("<%=renewalDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=totalUnitsTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value =""; 
            document.getElementById("<%=renewalNumberTextBox.ClientID%>").value =""; 
            document.getElementById("<%=renewalUnitsTextBox.ClientID%>").value ="";          
            document.getElementById("<%=RemarksTextBox.ClientID%>").value =""; 
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
            if(document.getElementById("<%=leftDataGrid.ClientID%>"))
             {
                document.getElementById("<%=leftDataGrid.ClientID%>").style.visibility='hidden';
             }  
          
       
            
         document.getElementById("<%=aTextBox.ClientID%>").value ="";
         document.getElementById("<%=bTextBox.ClientID%>").value ="";
         document.getElementById("<%=cTextBox.ClientID%>").value ="";
         document.getElementById("<%=dTextBox.ClientID%>").value ="";
         document.getElementById("<%=eTextBox.ClientID%>").value ="";
         document.getElementById("<%=fTextBox.ClientID%>").value ="";
         document.getElementById("<%=gTextBox.ClientID%>").value ="";
         document.getElementById("<%=hTextBox.ClientID%>").value ="";
         document.getElementById("<%=iTextBox.ClientID%>").value ="";
         document.getElementById("<%=jTextBox.ClientID%>").value ="";
         document.getElementById("<%=kTextBox.ClientID%>").value ="";
         document.getElementById("<%=lTextBox.ClientID%>").value = "";
         document.getElementById("<%=yTextBox.ClientID%>").value ="";
          document.getElementById("<%=yQTYTextBox.ClientID%>").value ="";
         document.getElementById("<%=tdCIP.ClientID%>").innerHTML ="";
         document.getElementById("<%=SignImage.ClientID%>").src ="";
        document.getElementById("<%=regNoTextBox.ClientID%>").focus();
            return false;
            
        }
        else
        {
         return false;
        }
       
    }
    
    
     function fnResetAll()
    {
       
              
            document.getElementById("<%=renewalDateTextBox.ClientID%>").value ="";
            
            
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=renewalNumberTextBox.ClientID%>").value ="";
            document.getElementById("<%=totalUnitsTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value ="";          
            document.getElementById("<%=RemarksTextBox.ClientID%>").value =""; 
            document.getElementById("<%=renewalDateTextBox.ClientID%>").value =""; 
            document.getElementById("<%=renewalUnitsTextBox.ClientID%>").value =""; 
            
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
             if(document.getElementById("<%=leftDataGrid.ClientID%>"))
             {
                document.getElementById("<%=leftDataGrid.ClientID%>").style.visibility='hidden';
             }  
          
            
             document.getElementById("<%=aTextBox.ClientID%>").value ="";
             document.getElementById("<%=bTextBox.ClientID%>").value ="";
             document.getElementById("<%=cTextBox.ClientID%>").value ="";
             document.getElementById("<%=dTextBox.ClientID%>").value ="";
             document.getElementById("<%=eTextBox.ClientID%>").value ="";
             document.getElementById("<%=fTextBox.ClientID%>").value ="";
             document.getElementById("<%=gTextBox.ClientID%>").value ="";
             document.getElementById("<%=hTextBox.ClientID%>").value ="";
             document.getElementById("<%=iTextBox.ClientID%>").value ="";
             document.getElementById("<%=jTextBox.ClientID%>").value ="";
              document.getElementById("<%=kTextBox.ClientID%>").value ="";
             document.getElementById("<%=tdCIP.ClientID%>").innerHTML ="";
             document.getElementById("<%=SignImage.ClientID%>").src ="";
             document.getElementById("<%=lTextBox.ClientID%>").value = "";
            document.getElementById("<%=yTextBox.ClientID%>").value ="";
              document.getElementById("<%=yQTYTextBox.ClientID%>").value ="";
             document.getElementById("<%=regNoTextBox.ClientID%>").focus();
             alert("No Data Found");
             return false;
      
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
                document.getElementById("<%=renewalUnitsTextBox.ClientID%>").value=parseInt(sum);
            }  
        }
     function fnCheqInput()
        {
        
        
              //Input Text Checking
              
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=regNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Valid Registration Number");
                    return false;
                }
            }
            
           if(document.getElementById("<%=renewalDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=renewalDateTextBox.ClientID%>").focus();
                alert("Please Enter Renewal Date");
                return false;
                
            }
            if(document.getElementById("<%=renewalDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=renewalDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=renewalDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
            if(document.getElementById("<%=renewalNumberTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=renewalNumberTextBox.ClientID%>").focus();
                alert("Please Enter Renewal Number");
                return false;
                
            }
            
          
         
           
          
            if(!document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               alert("Plese Add Certificate to The List");
                     return false;
            }
          
           
         //Data Grid Checking
         if (document.getElementById("ctl00_ContentPlaceHolder1_dinoGridView")) {
             var inputs = document.getElementById("ctl00_ContentPlaceHolder1_dinoGridView").getElementsByTagName("input");

             var wightNo = 0;
             wightNo = parseInt(wightNo);
             var dinotxt = "";
             var weighttxt = "";


             for (var i = 0; i < inputs.length; i++) {
                 if (inputs[i].type == "text") {
                     if (inputs[i].id.indexOf("weightTextBox") != -1) {


                         wightNo = wightNo + parseInt(inputs[i].value)
                     }
                 }
             }
             var unitQty = document.getElementById("ctl00_ContentPlaceHolder1_renewalUnitsTextBox").value
             if (wightNo != parseInt(unitQty)) {
                 document.getElementById("ctl00_ContentPlaceHolder1_renewalUnitsTextBox").focus();
                 alert(" Save Failed:Renewal Unit Quantity and List Quantity is not Equal");
                 return false;
             }
         }
        
        }
        
         function fnCheckRegNo()
            {
                if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Registration Number");
                    return false;
                    
                }
            }
        
        function fnCheqQty()// check Add to lsit validation
        {
            var SumQty=0;
            if(document.getElementById("<%=renewalUnitsTextBox.ClientID%>").value =="")
            {
                
                alert("Please Select Renewal Unit Quantity");
                return false;
                
            }
            if(document.getElementById("<%=aTextBox.ClientID%>").value != "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=aTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=aTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination A");
                    return false;
                }
                SumQty=parseInt(SumQty)+(1*parseInt(document.getElementById("<%=aTextBox.ClientID%>").value));
            }
           if(document.getElementById("<%=bTextBox.ClientID%>").value != "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=bTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=bTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination B");
                    return false;
                }
                 SumQty=parseInt(SumQty)+(5*parseInt(document.getElementById("<%=bTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=cTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=cTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=cTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination C");
                    return false;
                }
                SumQty=parseInt(SumQty)+(10*parseInt(document.getElementById("<%=cTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=dTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=dTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=dTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination D");
                    return false;
                }
                 SumQty=parseInt(SumQty)+(20*parseInt(document.getElementById("<%=dTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=eTextBox.ClientID%>").value != "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=eTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=eTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination E");
                    return false;
                }
                SumQty=parseInt(SumQty)+(50*parseInt(document.getElementById("<%=eTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=fTextBox.ClientID%>").value != "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=fTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=fTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination F");
                    return false;
                }
                SumQty=parseInt(SumQty)+(100*parseInt(document.getElementById("<%=fTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=gTextBox.ClientID%>").value !="")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=gTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=gTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination G");
                    return false;
                }
                SumQty=parseInt(SumQty)+(250*parseInt(document.getElementById("<%=gTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=hTextBox.ClientID%>").value!="")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=hTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=hTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination H");
                    return false;
                }
                 SumQty=parseInt(SumQty)+(500*parseInt(document.getElementById("<%=hTextBox.ClientID%>").value));
            }
             if(document.getElementById("<%=iTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=iTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=iTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination I");
                    return false;
                }
                 SumQty=parseInt(SumQty)+(1000*parseInt(document.getElementById("<%=iTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=jTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=jTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=jTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination J");
                    return false;
                }
               SumQty=parseInt(SumQty)+(5000*parseInt(document.getElementById("<%=jTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=kTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=kTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=kTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination J");
                    return false;
                }
               SumQty=parseInt(SumQty)+(10000*parseInt(document.getElementById("<%=kTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=lTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=lTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=lTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination J");
                    return false;
                }
               SumQty=parseInt(SumQty)+(20000*parseInt(document.getElementById("<%=lTextBox.ClientID%>").value));
            }
            if(document.getElementById("<%=yQTYTextBox.ClientID%>").value!= "")
            {
                 var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=yQTYTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=yQTYTextBox.ClientID%>").focus();
                    alert("Invalid Certtificate Quantity Number of Dinomination Y");
                    return false;
                }
               SumQty=parseInt(SumQty)+(parseInt(document.getElementById("<%=lTextBox.ClientID%>").value));
            }
        
        
         if(parseInt(SumQty)>0)
         {
            if(parseInt(SumQty)!=parseInt(document.getElementById("<%=renewalUnitsTextBox.ClientID%>").value))
            {
                alert("Renewal Unit Quantity and Dinomination Quantity is not Equal:"+parseInt(SumQty));
                return false;
            }
         }
        }
        function fnDinoReset()//Dinomination Reset
        {
             document.getElementById("<%=aTextBox.ClientID%>").value ="";
             document.getElementById("<%=bTextBox.ClientID%>").value ="";
             document.getElementById("<%=cTextBox.ClientID%>").value ="";
             document.getElementById("<%=dTextBox.ClientID%>").value ="";
             document.getElementById("<%=eTextBox.ClientID%>").value ="";
             document.getElementById("<%=fTextBox.ClientID%>").value ="";
             document.getElementById("<%=gTextBox.ClientID%>").value ="";
             document.getElementById("<%=hTextBox.ClientID%>").value ="";
             document.getElementById("<%=iTextBox.ClientID%>").value ="";
             document.getElementById("<%=jTextBox.ClientID%>").value ="";
             document.getElementById("<%=kTextBox.ClientID%>").value ="";
            document.getElementById("<%=lTextBox.ClientID%>").value = "";
            document.getElementById("<%=yTextBox.ClientID%>").value = "";
            document.getElementById("<%=yQTYTextBox.ClientID%>").value ="";
             return false;
         }
         
       function  fnFindChek()//check Find Button validation
       {
         if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=regNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Valid Registration Number");
                    return false;
                }
            }
            if(document.getElementById("<%=dinoGridView.ClientID%>"))
            {
               document.getElementById("<%=dinoGridView.ClientID%>").style.visibility='hidden';
            }
            if(document.getElementById("<%=leftDataGrid.ClientID%>"))
             {
                document.getElementById("<%=leftDataGrid.ClientID%>").style.visibility='hidden';
             }  
       }
       
       
       
       function fnValidateWeight(dinoObj)
       {
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
                var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
                var wightNo=0;
                var dinotxt = "";
                var weighttxt = "";
               
                
                for(var i=0;i<inputs.length;i++)
                {
                    if(inputs[i].type =="text")
                    {
                        if(inputs[i].id.indexOf("dinoTextBox") != -1)
                        {
                            dinotxt = inputs[i];
                            if(dinoObj.id==dinotxt.id)
                            { 
                             wightNo=i+2;
                            }
                            
                        }
                      }
                }
                if (dinoObj.value.indexOf("Y") == 0 || dinoObj.value.indexOf("y") == 0)
                {
                    return true;

                }
                else if(dinoObj.value.indexOf("L")==0 ||dinoObj.value.indexOf("l")==0)
                  {
                      var weight=inputs[wightNo].value;
                        if(weight!=20000)
                        {
                           inputs[wightNo].focus();
                           alert("Invalid Certificate Weight");
                           return false;
                        }                   
                       
                  }  
               else if(dinoObj.value.indexOf("K")==0 ||dinoObj.value.indexOf("k")==0)
                  {
                   
                        var weight=inputs[wightNo].value;
                        if(weight!=10000)
                        {
                           inputs[wightNo].focus();
                           alert("Invalid Certificate Weight");
                           return false;
                        }
                     
                  }  
                if(dinoObj.value.indexOf("J")==0 ||dinoObj.value.indexOf("j")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=5000)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
                else if(dinoObj.value.indexOf("i")==0 ||dinoObj.value.indexOf("I")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=1000)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("h")==0 ||dinoObj.value.indexOf("H")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=500)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
              else if(dinoObj.value.indexOf("g")==0 ||dinoObj.value.indexOf("G")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=250)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
              else if(dinoObj.value.indexOf("f")==0 ||dinoObj.value.indexOf("F")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=100)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("e")==0 ||dinoObj.value.indexOf("E")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=50)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("d")==0 ||dinoObj.value.indexOf("D")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=20)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("c")==0 ||dinoObj.value.indexOf("C")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=10)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("b")==0 ||dinoObj.value.indexOf("B")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=5)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
               else if(dinoObj.value.indexOf("a")==0 ||dinoObj.value.indexOf("A")==0)
                {
                    var weight=inputs[wightNo].value;
                    if(weight!=1)
                    {
                       inputs[wightNo].focus();
                       alert("Invalid Certificate Weight");
                       return false;
                    }
                }
                else
                {
                   alert("Invalid Certificate Dinomination");
                       return false;  
                }
            }
            
       }
      
       function fnValidateDino(weightObj)
       {
           if(document.getElementById("<%=dinoGridView.ClientID%>"))
           {
                var inputs = document.getElementById("<%=dinoGridView.ClientID%>").getElementsByTagName("input");
                
                var dinoNo=0;
                var dinotxt = "";
                var weighttxt = "";
                
                    for(var i=0;i<inputs.length;i++)
                    {
                        if(inputs[i].type =="text")
                        {
                            if(inputs[i].id.indexOf("weightTextBox") != -1)
                            {
                                weighttxt = inputs[i];
                                if(weightObj.id==weighttxt.id)
                                { 
                                 dinoNo=i-2;
                                }
                                
                            }
                          }
                    }
                     if(weightObj.value==20000)
                    {
                 
                        var dino=inputs[dinoNo].value;
                       
                       if(dino.toUpperCase()!="L")
                        {
                           inputs[dinoNo].focus();
                           alert("Invalid Dinomination");
                           return false;
                        }                     
                    }
                   else if(weightObj.value==10000)
                    {
                 
                        var dino=inputs[dinoNo].value;
                       
                       if(dino.toUpperCase()!="K")
                        {
                           inputs[dinoNo].focus();
                           alert("Invalid Dinomination");
                           return false;
                        }                     
                    }
                   else if(weightObj.value==5000)
                    {
                        var dino=inputs[dinoNo].value;
                       
                       if(dino.toUpperCase()!="J")
                        {
                           inputs[dinoNo].focus();
                           alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==1000)
                    {
                       var dino=inputs[dinoNo].value;
                         if(dino.toUpperCase()!="I")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==500)
                    {
                        var dino=inputs[dinoNo].value;
                         if(dino.toUpperCase()!="H")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==250)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="G")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==100)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="F")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                    else if(weightObj.value==50)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="E")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                  else if(weightObj.value==20)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="D")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                 else if(weightObj.value==10)
                    {
                        var dino=inputs[dinoNo].value;
                       if(dino.toUpperCase()!="C")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                   else if(weightObj.value==5)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="B")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                    }
                   else if(parseInt(weightObj.value)==1)
                    {
                        var dino=inputs[dinoNo].value;
                        if(dino.toUpperCase()!="A")
                        {
                           inputs[dinoNo].focus();
                            alert("Invalid Dinomination");
                           return false;
                        }
                   }
                  
                   else
                   {
                  
                               var dino = inputs[dinoNo].value;
                               if (dino.toUpperCase() == "Y")
                               {
                                  return true;
                               }
                               else
                               {
                                   alert("Invalid Weight");
                                   return false;
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
</script>
 <style type="text/css">
      .style6
        {
            font-size: smaller;
            font-weight: bold;
            color: #009933;
        }
     .style19
     {
         font-size: large;
         font-weight: bold;
         color: #75C8FF;
     }
     .style20
     {
         color: #FF3300;
     }
     .style21
     {
         width: 26px;
     }
     .auto-style1 {
         font-size: medium;
     }
     .auto-style2 {
         font-family: Verdana, Arial, Helvetica, sans-serif;
         border: 1px #1B68B8 solid;
         BACKGROUND-COLOR: #FFFFDD;
         COLOR: #000000;
         FONT-SIZE: 12px;
         padding-left: 2px;
     }
     .auto-style3 {
         text-align: center;
     }
     </style>



    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
              Unit Holder Renewal Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
      <br />
      <div id="dvContent" runat="server">
      
    
    <table width="1100" align="center" cellpadding="0" cellspacing="0" border="0" >
    <colgroup width="120"></colgroup>
    <colgroup width="320"></colgroup>
    <colgroup width="160"></colgroup>
    <colgroup width="160"></colgroup>
    <tr>
        <td align="right" rowspan="2"><b>Renewal No:</b></td>
        <td align="left" rowspan="2">
            <asp:TextBox ID="renewalNumberTextBox" runat="server" 
                CssClass="textInputStyleDate" MaxLength="6" 
                meta:resourcekey="saleNumberTextBoxResource1" 
                onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
            <span class="star">*</span></td>
        <td class="style19" align="center">Denomination</td>
        <td colspan="2" align="center"><span class="style6"  
                           style="border:1px solid">Signature    and 
                       Photo</span></td>
    </tr>
     <tr>
        <td rowspan="13">
            <table align="center" cellpadding="0" cellspacing="0" border="1px">
            <colgroup width="80px"></colgroup>
            <tr>
                <td align="right">
                    <b>A:1x</b></td>
                <td align="left">
                    <asp:TextBox ID="aTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="aTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>B:5x</b></td>
                <td align="left">
                    <asp:TextBox ID="bTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="bTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>C:10x&nbsp;</b></td>
                <td align="left">
                    <asp:TextBox ID="cTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="cTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>D:20x</b></td>
                <td align="left">
                    <asp:TextBox ID="dTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="dTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>E:50x</b></td>
                <td align="left">
                    <asp:TextBox ID="eTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="eTextBoxResource2" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                     <b>F:100x</b></td>
                <td align="left">
                    <asp:TextBox ID="fTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="fTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                   <b>G:250x</b></td>
                <td align="left">
                    <asp:TextBox ID="gTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="gTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>H:500x</b></td>
                <td align="left">
                    <asp:TextBox ID="hTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="hTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                <b>I:1000x</b></td>
                <td align="left">
                    <asp:TextBox ID="iTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="iTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <b>J:5000x</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="jTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        meta:resourcekey="jTextBoxResource1" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                <b>K:10000x</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="kTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td align="right">
                <b>L:20000x</b>
                </td>
                <td align="left">
                    <asp:TextBox ID="lTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td align="right" class="auto-style3">
                    <b>
                    <asp:TextBox ID="yQTYTextBox" runat="server" CssClass="auto-style2" 
                        onkeypress="fncInputNumericValuesOnly()" Width="40px"></asp:TextBox>
                &nbsp;<span class="auto-style1">Y:</span></b>
                </td>
                <td align="left">
                    <asp:TextBox ID="yTextBox" runat="server" CssClass="TextInputStyleDenomination" 
                        onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                </td>
            </tr>
                <%-- <tr>
                <td align="right">
                </td>
                <td align="left">
                </td>
            </tr>--%>
            </table>
        </td>
        <td colspan="2" rowspan="10" align="center">
            <asp:Image ID="SignImage" runat="server" Height="202px" 
                meta:resourcekey="SignImageResource1" Width="286px" />
         </td>
    </tr>
     <tr>
        <td align="right">Registration No:</td>
        <td align="left"> 
        
                    <asp:TextBox ID="fundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="fundCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="branchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="branchCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"   
                    CssClass= "TextInputStyleSmall" TabIndex="1" 
                    onkeypress= "fncInputNumericValuesOnly()"
                    meta:resourcekey="regNoTextBoxResource1" Width="95px" 
                       ></asp:TextBox>
                                    <span class="star">*</span>&nbsp;<asp:Button 
                    ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                    onclick="findButton_Click" AccessKey="f" 
                    onclientclick="return fnFindChek();" TabIndex="2" 
                    meta:resourcekey="findButtonResource1" /></td>
    </tr>
     <tr>
       <td align="right">Name of Holder:</td>
        <td align="left">
            <asp:TextBox ID="holderNameTextBox" runat="server" 
                CssClass="TextInputStyleLarge" MaxLength="55" 
                meta:resourcekey="holderNameTextBoxResource1" TabIndex="3"></asp:TextBox>
         </td>
    </tr>
     <tr>
      <td align="right">&nbsp;</td>
        <td align="left">
           
                                <asp:TextBox ID="jHolderTextBox" runat="server" CssClass="TextInputStyleLarge" 
                                    MaxLength="55" meta:resourcekey="jHolderTextBoxResource1" TabIndex="4"></asp:TextBox>
           
                                </td>
     </tr>
     <tr>
        <td align="right">Address1:</td>
        <td align="left">
            <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                CssClass="TextInputStyleLarge" MaxLength="55" 
                meta:resourcekey="holderAddress1TextBoxResource1" TabIndex="5"></asp:TextBox>
         </td>
    </tr>
     <tr>
      <td align="right">Address2:</td>
       <td align="left">
           <asp:TextBox ID="holderAddress2TextBox" runat="server" 
               CssClass="TextInputStyleLarge" MaxLength="55" 
               meta:resourcekey="holderAddress2TextBoxResource1" TabIndex="6"></asp:TextBox>
         </td>
    </tr>
     <tr>
        <td align="right">Telephone/Mobile:</td>
       <td align="left">
           <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
               CssClass="TextInputStyleLarge" MaxLength="20" 
               meta:resourcekey="holderTelphoneTextBoxResource1" TabIndex="7"></asp:TextBox>
         </td>
    </tr>
     <tr>
        <td align="right" class="style20"><b>CIP:</b></td>
       <td align="left" id="tdCIP" runat="server"  >
                &nbsp;</td>
     </tr>
   <tr>
        <td align="right">Renewal Date:</td>
       <td align="left">
           <asp:TextBox ID="renewalDateTextBox" runat="server" 
               CssClass="textInputStyleDate" meta:resourcekey="saleDateTextBoxResource1" 
               TabIndex="9"></asp:TextBox>
           <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
               Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
               TargetControlID="renewalDateTextBox" />
           <asp:ImageButton ID="RegDateImageButton" runat="server" 
               AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
               meta:resourcekey="RegDateImageButtonResource1" TabIndex="10" />
           <span class="star">* </span></td>
    </tr>
     <tr>
       <td align="right">Renewal Type:</td>
       <td align="left">
           <asp:DropDownList ID="renewalTypeDropDownList" runat="server" 
               CssClass="DropDownList" Font-Bold="True" 
               meta:resourcekey="saleTypeDropDownListResource1" TabIndex="11">
               <asp:ListItem meta:resourcekey="ListItemResource1" Selected="True" Value="S">Sub-Division</asp:ListItem>
               <asp:ListItem meta:resourcekey="ListItemResource2" Value="C">Consolidation</asp:ListItem>
               <asp:ListItem meta:resourcekey="ListItemResource2" Value="L">Lost</asp:ListItem>
           </asp:DropDownList>
         </td>
    </tr>
      <tr>
        <td align="right">Total Holding Units:</td>
        <td style="text-align: left">
            <asp:TextBox ID="totalUnitsTextBox" runat="server" 
                CssClass="textInputStyleDate" Enabled="False" Font-Bold="True" MaxLength="6" 
                meta:resourcekey="saleRateTextBoxResource1" TabIndex="12"></asp:TextBox>
          </td>
        <td style="text-align: right" class="style21" rowspan="5"></td>
        <td style="text-align: left; color:Red" id="td1" runat="server"></td>
    </tr>
     <tr>
        <td align="right">Renewal Units:</td>
        <td style="text-align: left">
            <asp:TextBox ID="renewalUnitsTextBox" runat="server" 
                CssClass="textInputStyleDate" Font-Bold="True" MaxLength="6" 
                meta:resourcekey="unitQtyTextBoxResource1" 
                onkeypress="fncInputNumericValuesOnly()" TabIndex="13"></asp:TextBox>
            <span class="star">*</span></td>
        <td align="left">                &nbsp;</td>
    </tr>
     <tr>
        <td align="right">Remarks:</td>
        <td style="text-align: left">
            <asp:TextBox ID="RemarksTextBox" runat="server" CssClass="TextInputStyleLarge" 
                MaxLength="55" meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="14"></asp:TextBox>
         </td>
    </tr>
     <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td> 
                <asp:Button ID="DinoResetButton" runat="server" Text="Reset" 
                CssClass="buttonmid"  OnClientClick="return fnDinoReset();" AccessKey="r" 
                    meta:resourcekey="DinoResetButtonResource1" /></td>
           <td align="left">
                &nbsp;</td>
    </tr>
     <tr>
        <td colspan="3" align="center">
                &nbsp;</td>
         <td align="left">
                &nbsp;</td>
     </tr>
     <tr>
        <td colspan="5">
           <table>
           <tr>
            <td>
                 <table align="center" style="width: 430">
         <tr>
         <td class="style13"> 
          <table align="center" style="width: 427px; height: 20px;">
        <tr class="style6">
            <td align="center" class="style16"><a href="#" onclick="fnCheckAll();">Select All</a> </td>
            <td><span class="style8">Sale No</span></td>
            <td><span class="style8">Certificate</span></td>
            <td><span class="style8">Weight</span></td>
        </tr>
    </table>
             <div style="text-align: center; display: block; overflow: auto; width:430px; height:200px;">

               <asp:DataGrid ID="leftDataGrid" runat="server" AutoGenerateColumns="False" 
                   Width="403px"  >
                   <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                    <ItemStyle CssClass="TableText"></ItemStyle>
                    <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                    <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                    <Columns>
                    <asp:TemplateColumn HeaderText="">
                    <ItemTemplate> 
                         <asp:CheckBox ID="leftCheckBox" runat="server" onclick="fnSelectedTotalUnit();" ></asp:CheckBox> 
                    </ItemTemplate>                    
                    </asp:TemplateColumn>                    
                    <asp:BoundColumn DataField="SL_NO"></asp:BoundColumn>                                                                                              
                    <asp:BoundColumn DataField="CERTIFICATE"></asp:BoundColumn>                                                                                              
                    <asp:BoundColumn  DataField="QTY"></asp:BoundColumn>                                       
                </Columns>
            </asp:DataGrid>              
                </div>
           <table align="center" style="width: 427px; height: 20px;">
        <tr class="style6">
            <td align="center" class="style17"><a href="#" onclick="fnUnCheckAll();">Deselect All</a></td>
            <td><span class="style8">Sale No</span></td>
            <td><span class="style8">Certificate</span></td>
            <td><span class="style8">Weight</span></td>
        </tr>
    </table>
           </td>
         </tr>
    </table>
            </td>
            <td>
               <asp:Button ID="addListButton" runat="server" 
                    Text="Add to List" CssClass="buttoncommon" onclick="addListButton_Click" 
                    OnClientClick="return fnCheqQty();" AccessKey="l" style="height: 20px" /> 
            </td>
            <td>
                <div style="text-align: center; display: block; overflow: auto; width:319px; height:241px;" id="dvContentBottom" runat="server">
                <asp:DataGrid ID="dinoGridView" runat="server" AutoGenerateColumns="False" 
                    meta:resourcekey="dinoGridViewResource1" >
                <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                <ItemStyle CssClass="TableText"></ItemStyle>
                <HeaderStyle CssClass="TableHeader2"></HeaderStyle>
                <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                <Columns>
                 <asp:TemplateColumn HeaderText="Dinomination" >
                        <ItemTemplate>
                            <asp:TextBox ID="dinoTextBox" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem,"dino") %>' Width="50px" 
                                onBlur="Javascript:fnValidateWeight(this);" 
                                meta:resourcekey="dinoTextBoxResource1"></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="False" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                 <asp:TemplateColumn HeaderText="Certificate" >
                        <ItemTemplate>
                            <asp:TextBox ID="certNoTextBox" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem,"cert_no") %>' Width="80px" onkeypress= "fncInputNumericValuesOnly()"  
                                meta:resourcekey="certNoTextBoxResource1"></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"  Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Weight"  >
                        <ItemTemplate>
                            <asp:TextBox ID="weightTextBox" runat="server"  
                                Text='<%# DataBinder.Eval(Container.DataItem,"cert_weight") %>' Width="70px" onkeypress= "fncInputNumericValuesOnly()"  
                                onBlur="Javascript:fnValidateDino(this);" 
                                meta:resourcekey="weightTextBoxResource1"></asp:TextBox>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Font-Bold="True" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
                <br />
                </div>
            </td>
           </tr>
           </table>
         </td>
    </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td class="style21">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
   
    </table>
   
       
     </div>
     <table width="1100" align="center" cellpadding="0" cellspacing="0" border="0" >
      <tr>
        <td colspan="5" align="center">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="SaveButton_Click" AccessKey="s" 
                meta:resourcekey="SaveButtonResource1"/>&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="a" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
        </td>
    </tr>
     </table>
    <br />
  </asp:Content> 
