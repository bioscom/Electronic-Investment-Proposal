function autoComplete (field, property, forcematch) 
	{
		var found = false;
		var xselect = document.getElementById("<%= all_proj.ClientID %>");
		for (var i = 0; i < xselect.options.length; i++) 
		{
		    if (xselect.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) 
		    {
			    found=true; 
			    break;
			}
		}
		
		if (found) { xselect.selectedIndex = i; }
		else { xselect.selectedIndex = 0; }
		
		if (field.createTextRange) 
		{
		    if (forcematch && !found) 
		    {
				field.value=field.value.substring(0,field.value.length-1); 
				return;
			}
			var cursorKeys ="8;46;37;38;39;40;33;34;35;36;45;";
			if (cursorKeys.indexOf(event.keyCode+";") == -1) 
			{
				var r1 = field.createTextRange();
				var oldValue = r1.text;
				var newValue = found ? xselect.options[i][property] : oldValue;
				if (newValue != field.value) 
				{
					field.value = newValue;
					var rNew = field.createTextRange();
					rNew.moveStart('character', oldValue.length) ;
					rNew.select();
				}
			}
		}
	}
	
	function getProjTitle()
    {
        var projTitle  = document.getElementById("<%=projTitleTextBox.ClientID %>");
        var allProj  = document.getElementById("<%= all_proj.ClientID %>");
        
        projTitle.value = allProj.options[allProj.selectedIndex].value;
        
        var projectitleHF = document.getElementById("<%= projectTitleHF.ClientID %>");
        projectitleHF.value = allProj.options[allProj.selectedIndex].value;
    }
    
    function getProjNumber(allProj)
    {
        var projNumber = document.getElementById("<%=projNumTextBox.ClientID %>");
        projNumber.value = allProj.options[allProj.selectedIndex].text;
        
    }
    
    function clearBOM()
    {
        var BOMBox = document.getElementById("<%=BOMTextBox.ClientID %>");
        BOMBox.value = "";
    }
			
    function trimReviewers()
    {
	    //dir	
	    boxlen=dir.options.length;
	    for(i=0;i<=boxlen;++i)
	    {
		    dir.options.remove(i);				
	    }
		
	    for(i=0;i<=dirers.options.length-1;++i)
	    {
        //		if(dirers[i].value==selid){
	        if(SS.value >= 2.0)
	        {
			    if(dirers[i+1].text=="MD")
			    {
				    var newElem = document.createElement("option");
					newElem.text = dirers[i].text;
					newElem.value = dirers[i+1].value;
					dir.options.add(newElem,i);					
				}
			}
			else
			{
			    var newElem1 = document.createElement("option");
				newElem1.text = dirers[i].text;
				newElem1.value = dirers[i+1].value;
				dir.options.add(newElem1,i);
			}			
        //	    }
	    }
	    //end
    }

    function getCurYear()
    {
	    var today = new Date();
	    x = today.getYear();
		
	    var Year = x % 100;
	    Year += (Year < 38) ? 2000 : 1900;
		
	    yearminus1.value=Year-1;
	    yearnow.value=Year;
	    yearplus1.value=Year+1;
	    yearplus2.value=Year+2;
	    yearplus3.value=Year+3;
	    yearplus4.value=Year+4;
	    yearplus5.value=Year+5;
    }

    function newWindow(msg) 
    {	
	    var newWindow =window.open("","","HEIGHT=400,WidTH=500,alwaysRaised")
	    var newContent = "<html><title>Investment Proposal Help</title><body bgcolor=#CCCCCC>"
	    newContent += msg
	    newContent += "<FORM><INPUT TYPE='button' VALUE='OK'"
	    newContent += "onClick='self.close()'></FORM></BODY></HTML>"
	    newWindow.document.write(newContent)
	    newWindow.document.close()    
    }
    
    function displaytable(chkbox, thetable)
    {
        if (thetable.style.display=='none') { thetable.style.display='block'; }
        else { thetable.style.display='none'; }
    }
    
    function disptr(thebutton, therow)
    {
        if (therow.style.display=='none') { therow.style.display='block'; }
    }
    
    function undisptr(unthebutton, untherow)
    {
        if (untherow.style.display=='block') { untherow.style.display='none'; }
    }
    
    function buzScope(scope, buzUnit, buz)
    {
        if (scope.value == 2)
        {
            if (buzUnit.style.display=='none' && buz.style.display=='none')
            {
                buzUnit.style.display='block'; 
                buz.style.display='block';
            }
        }
        else
        {
            buzUnit.style.display='none'; 
            buz.style.display='none';
        }
    }
	    
    //
    // *************** The following JavaScript functions are used to calculate
    // *************** the mathematics in this Summary Section 
    //
    
    //Amount Functions
    
    function rndnum(num)
	{
		var dec = 2;
		var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
		return result;
	}
    
    function shareFormular(share)
    {
        var shellshare = document.getElementById("<%= shellshareTextBox.ClientID %>");
        var sshp = document.getElementById("sshpCheckBox");
        var shellshareHF = document.getElementById("<%= shellshareHF.ClientID %>");
        
        if (share.value == "1") 
        { 
            shellshare.value = "30";
            shellshareHF.value = "30"; 
            sshp.disabled = true; 
        }
        else if (share.value == "2") 
        { 
            shellshare.value = "30";
            shellshareHF.value = "30"; 
            sshp.disabled = true; 
        }
        else if (share.value == "3") 
        { 
            shellshare.value = "0";
            shellshareHF.value = "0"; 
            sshp.disabled = false;
        }
    }
    
    function getShellShare()
    {
	    var percent1 = document.getElementById("<%=percent1TextBox.ClientID %>");
	    var percent2 = document.getElementById("<%=percent2TextBox.ClientID %>");
	    var percent3 = document.getElementById("<%=percent3TextBox.ClientID %>");
	    var percent4 = document.getElementById("<%=percent4TextBox.ClientID %>");
	    var percent5 = document.getElementById("<%=percent5TextBox.ClientID %>");
	    var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
	    var shellshareHF = document.getElementById("<%= shellshareHF.ClientID %>");
	    //
	    //Amount
	    //
	    var capexjv = document.getElementById("<%=capexJVTextBox.ClientID %>");
	    var ncapexjv = document.getElementById("<%=ncapexJVTextBox.ClientID %>");
	    var capexss = document.getElementById("<%=capexssTextBox.ClientID %>");
	    var ncapexss = document.getElementById("<%=ncapexssTextBox.ClientID %>");

	    var opexjv = document.getElementById("<%=opexJVTextBox.ClientID %>");
  	    var nopexjv = document.getElementById("<%=nopexJVTextBox.ClientID %>");
	    var opexss = document.getElementById("<%=opexssTextBox.ClientID %>");
	    var nopexss = document.getElementById("<%=nopexssTextBox.ClientID %>");

	    var totaljv = document.getElementById("<%=totalJVTextBox.ClientID %>");
	    var ntotaljv = document.getElementById("<%=ntotalJVTextBox.ClientID %>");
	    var totalss = document.getElementById("<%=totalSSTextBox.ClientID %>");
	    var ntotalss = document.getElementById("<%=ntotalSSTextBox.ClientID %>");
	    
	    var oapprovedjv = document.getElementById("<%=oapprovedJVTextBox.ClientID %>");
	    var noapprovedjv = document.getElementById("<%=noapprovedJVTextBox.ClientID %>");
	    var oapprovedss = document.getElementById("<%=oapprovedSSTextBox.ClientID %>");
	    var noapprovedss = document.getElementById("<%=noapprovedSSTextBox.ClientID %>");
	    
	    var JV = document.getElementById("<%=JVTextBox.ClientID %>");
	    var nJV = document.getElementById("<%=nJVTextBox.ClientID %>");
	    var SS = document.getElementById("<%=SSTextBox.ClientID %>");
	    var nSS = document.getElementById("<%=nSSTextBox.ClientID %>");
	    
	    //
	    //Main Commitment
	    //
	    var pass1 = document.getElementById("<%=pass1TextBox.ClientID %>");
	    var tpss1 = document.getElementById("<%=tpss1TextBox.ClientID %>");
	    var tss1 = document.getElementById("<%=tss1TextBox.ClientID %>");
	    
	    var pass2 = document.getElementById("<%=pass2TextBox.ClientID %>");
	    var tpss2 = document.getElementById("<%=tpss2TextBox.ClientID %>");
	    var tss2 = document.getElementById("<%=tss2TextBox.ClientID %>");
	    
	    var pass3 = document.getElementById("<%=pass3TextBox.ClientID %>");
	    var tpss3 = document.getElementById("<%=tpss3TextBox.ClientID %>");
	    var tss3 = document.getElementById("<%=tss3TextBox.ClientID %>");
	    
	    var pass4 = document.getElementById("<%=pass4TextBox.ClientID %>");
	    var tpss4 = document.getElementById("<%=tpss4TextBox.ClientID %>");
	    var tss4 = document.getElementById("<%=tss4TextBox.ClientID %>");
	    
	    var pass5 = document.getElementById("<%=pass5TextBox.ClientID %>");
	    var tpss5 = document.getElementById("<%=tpss5TextBox.ClientID %>");
	    var tss5 = document.getElementById("<%=tss5TextBox.ClientID %>");
	    
	    //Amont Hidden Fields
	    var capexssHF = document.getElementById("<%=capexssHF.ClientID %>");
	    var ncapexssHF = document.getElementById("<%=ncapexssHF.ClientID %>");
	    
	    var opexssHF = document.getElementById("<%=opexssHF.ClientID %>");
	    var nopexssHF = document.getElementById("<%=nopexssHF.ClientID %>");
	    
	    var totaljvHF = document.getElementById("<%=totalJVHF.ClientID %>");
	    var ntotaljvHF = document.getElementById("<%=ntotalJVHF.ClientID %>");
	    var totalssHF = document.getElementById("<%=totalSSHF.ClientID %>");
	    var ntotalssHF = document.getElementById("<%=ntotalSSHF.ClientID %>");
	    
	    var oapprovedssHF = document.getElementById("<%=oapprovedSSHF.ClientID %>");
   	    var noapprovedssHF = document.getElementById("<%=noapprovedSSHF.ClientID %>");

	    var JVHF = document.getElementById("<%=JVHF.ClientID %>");
   	    var nJVHF = document.getElementById("<%=nJVHF.ClientID %>");
	    var SSHF = document.getElementById("<%=SSHF.ClientID %>");
	    var nSSHF = document.getElementById("<%=nSSHF.ClientID %>");
	    
	    //Main Commitment Hidden Fields
	    var pass1HF = document.getElementById("<%=pass1HF.ClientID %>");
	    var tpss1HF = document.getElementById("<%= tpss1HF.ClientID %>");
	    var tjv1HF = document.getElementById("<%=tjv1HF.ClientID %>"); 
	    var tss1HF = document.getElementById("<%=tss1HF.ClientID %>");
	    
	    var pass2HF = document.getElementById("<%=pass2HF.ClientID %>");
	    var tpss2HF = document.getElementById("<%=tpss2HF.ClientID %>");
	    var tjv2HF = document.getElementById("<%=tjv2HF.ClientID %>"); 
	    var tss2HF = document.getElementById("<%=tss2HF.ClientID %>");
	    
	    var pass3HF = document.getElementById("<%=pass3HF.ClientID %>");
	    var tpss3HF = document.getElementById("<%=tpss3HF.ClientID %>");
	    var tjv3HF = document.getElementById("<%=tjv3HF.ClientID %>"); 
	    var tss3HF = document.getElementById("<%=tss3HF.ClientID %>");
	    
	    var pass4HF = document.getElementById("<%=pass4HF.ClientID %>");
	    var tpss4HF = document.getElementById("<%=tpss4HF.ClientID %>");
	    var tjv4HF = document.getElementById("<%=tjv4HF.ClientID %>"); 
	    var tss4HF = document.getElementById("<%=tss4HF.ClientID %>");
	    
	    var pass5HF = document.getElementById("<%=pass5HF.ClientID %>");
	    var tpss5HF = document.getElementById("<%=tpss5HF.ClientID %>");
	    var tjv5HF = document.getElementById("<%=tjv5HF.ClientID %>"); 
	    var tss5HF = document.getElementById("<%=tss5HF.ClientID %>");
        	    
        shell_share.value = rndnum(100-percent1.value-percent2.value-percent3.value-percent4.value-percent5.value);
        shellshareHF.value = rndnum(100-percent1.value-percent2.value-percent3.value-percent4.value-percent5.value);
		
	    if(shell_share.value <= 0)
	    {
		    percent1.value=0; percent2.value=0; percent3.value=0; percent4.value=0; percent5.value=0;
		    
		    shell_share.value=0;
	    }
		
	    capexjv.value=0; ncapexjv.value=0; capexss.value=0; ncapexss.value=0; 
	    opexjv.value=0; nopexjv.value=0; opexss.value=0; nopexss.value=0; 
	    totaljv.value=0; ntotaljv.value=0; totalss.value=0; ntotalss.value=0;
	    oapprovedjv.value=0; noapprovedjv.value=0; oapprovedss.value=0; noapprovedss.value=0;
	    JV.value=0; nJV.value=0; SS.value=0; nSS.value=0; 
	    
	    pass1.value=0; tpss1.value=0; tss1.value=0; 
	    pass2.value=0; tpss2.value=0; tss2.value=0; 
	    pass3.value=0; tpss3.value=0; tss3.value=0; 
	    pass4.value=0; tpss4.value=0; tss4.value=0; 
	    pass5.value=0; tpss5.value=0; tss5.value=0;	  
    }
    
    function getcapexjv()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
	    
	    var capexjv = document.getElementById("<%=capexJVTextBox.ClientID %>");
	    var capexss = document.getElementById("<%=capexssTextBox.ClientID %>");
	    var capexssHF = document.getElementById("<%=capexssHF.ClientID %>");

	    var opexjv = document.getElementById("<%=opexJVTextBox.ClientID %>");
	    var opexss = document.getElementById("<%=opexssTextBox.ClientID %>");
	    
	    var totaljv = document.getElementById("<%=totalJVTextBox.ClientID %>");
	    var totalss = document.getElementById("<%=totalSSTextBox.ClientID %>");
	    var totaljvHF = document.getElementById("<%=totalJVHF.ClientID %>");
	    var totalssHF = document.getElementById("<%=totalSSHF.ClientID %>");

	    var oapprovedjv = document.getElementById("<%=oapprovedJVTextBox.ClientID %>");
	    var oapprovedss = document.getElementById("<%=oapprovedSSTextBox.ClientID %>");
	    
	    var JV = document.getElementById("<%=JVTextBox.ClientID %>");
	    var SS = document.getElementById("<%=SSTextBox.ClientID %>");
	    var JVHF = document.getElementById("<%=JVHF.ClientID %>");
	    var SSHF = document.getElementById("<%=SSHF.ClientID %>");
        
        capexss.value = (rndnum(shell_share.value)*rndnum(capexjv.value))/100; capexssHF.value = (rndnum(shell_share.value)*rndnum(capexjv.value))/100;
		totaljv.value = rndnum(capexjv.value) + rndnum(opexjv.value); totaljvHF.value = rndnum(capexjv.value) + rndnum(opexjv.value);
		totalss.value = rndnum(capexss.value) + rndnum(opexss.value); totalssHF.value = rndnum(capexss.value) + rndnum(opexss.value);
		JV.value = rndnum(oapprovedjv.value) + rndnum(totaljv.value); JVHF.value = rndnum(oapprovedjv.value) + rndnum(totaljv.value);
		SS.value = rndnum(oapprovedss.value) + rndnum(totalss.value); SSHF.value = rndnum(oapprovedss.value) + rndnum(totalss.value);
    }
    
    function getncapexjv()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        
        var ncapexjv = document.getElementById("<%=ncapexJVTextBox.ClientID %>");
	    var ncapexss = document.getElementById("<%=ncapexssTextBox.ClientID %>");
	    var ncapexssHF = document.getElementById("<%=ncapexssHF.ClientID %>");
	    
	    var nopexjv = document.getElementById("<%=nopexJVTextBox.ClientID %>");
	    var nopexss = document.getElementById("<%=nopexssTextBox.ClientID %>");
	    
	    var ntotaljv = document.getElementById("<%=ntotalJVTextBox.ClientID %>");
	    var ntotalss = document.getElementById("<%=ntotalSSTextBox.ClientID %>");
	    var ntotaljvHF = document.getElementById("<%=ntotalJVHF.ClientID %>");
	    var ntotalssHF = document.getElementById("<%=ntotalSSHF.ClientID %>");
	    
	    var noapprovedjv = document.getElementById("<%=noapprovedJVTextBox.ClientID %>");
	    var noapprovedss = document.getElementById("<%=noapprovedSSTextBox.ClientID %>");
	    
	    var nJV = document.getElementById("<%=nJVTextBox.ClientID %>");
	    var nSS = document.getElementById("<%=nSSTextBox.ClientID %>");
	    var nJVHF = document.getElementById("<%=nJVHF.ClientID %>");
	    var nSSHF = document.getElementById("<%=nSSHF.ClientID %>");
        
        ncapexss.value = (rndnum(shell_share.value)*rndnum(ncapexjv.value))/100; ncapexssHF.value = (rndnum(shell_share.value)*rndnum(ncapexjv.value))/100;
		ntotaljv.value = rndnum(ncapexjv.value) + rndnum(nopexjv.value); ntotaljvHF.value = rndnum(ncapexjv.value) + rndnum(nopexjv.value);
		ntotalss.value = rndnum(ncapexss.value) + rndnum(nopexss.value); ntotalssHF.value = rndnum(ncapexss.value) + rndnum(nopexss.value);
		nJV.value = rndnum(noapprovedjv.value) + rndnum(ntotaljv.value); nJVHF.value = rndnum(noapprovedjv.value) + rndnum(ntotaljv.value);
		nSS.value = rndnum(noapprovedss.value) + rndnum(ntotalss.value); nSSHF.value = rndnum(noapprovedss.value) + rndnum(ntotalss.value);
    }
	    
    function getcapexss(capexss)
    {
        var capexssHF = document.getElementById("<%=capexssHF.ClientID %>");
        capexss.value=rndnum(capexss.value); capexssHF.value = rndnum(capexss.value);
    }
    
    function getncapexss(ncapexss)
    {
        var ncapexssHF = document.getElementById("<%=ncapexssHF.ClientID %>");
        ncapexss.value=rndnum(ncapexss.value); ncapexssHF.value = rndnum(ncapexss.value);
    }
    
    function getopexjv()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
	    var capexjv = document.getElementById("<%=capexJVTextBox.ClientID %>");
	    var capexss = document.getElementById("<%=capexssTextBox.ClientID %>");
	    var opexjv = document.getElementById("<%=opexJVTextBox.ClientID %>");
	    var opexss = document.getElementById("<%=opexssTextBox.ClientID %>");
	    var totaljv = document.getElementById("<%=totalJVTextBox.ClientID %>");
	    var totalss = document.getElementById("<%=totalSSTextBox.ClientID %>");
	    var oapprovedjv = document.getElementById("<%=oapprovedJVTextBox.ClientID %>");
	    var oapprovedss = document.getElementById("<%=oapprovedSSTextBox.ClientID %>");
	    var JV = document.getElementById("<%=JVTextBox.ClientID %>");
	    var SS = document.getElementById("<%=SSTextBox.ClientID %>");
	    
	    var opexssHF = document.getElementById("<%=opexssHF.ClientID %>");
	    var capexssHF = document.getElementById("<%=capexssHF.ClientID %>");
        var totaljvHF = document.getElementById("<%=totalJVHF.ClientID %>");
	    var totalssHF = document.getElementById("<%=totalSSHF.ClientID %>");
	    var JVHF = document.getElementById("<%=JVHF.ClientID %>");
	    var SSHF = document.getElementById("<%=SSHF.ClientID %>");
	    
        opexss.value= (rndnum(shell_share.value)*rndnum(opexjv.value))/100; opexssHF.value= (rndnum(shell_share.value)*rndnum(opexjv.value))/100;
		totaljv.value=rndnum(capexjv.value) + rndnum(opexjv.value); totaljvHF.value=rndnum(capexjv.value) + rndnum(opexjv.value);
		totalss.value=rndnum(capexss.value) + rndnum(opexss.value); totalssHF.value=rndnum(capexss.value) + rndnum(opexss.value);
		JV.value=rndnum(oapprovedjv.value) + rndnum(totaljv.value); JVHF.value=rndnum(oapprovedjv.value) + rndnum(totaljv.value);
		SS.value=rndnum(oapprovedss.value) + rndnum(totalss.value); SSHF.value=rndnum(oapprovedss.value) + rndnum(totalss.value);
    }
    
    function getnopexjv()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var ncapexjv = document.getElementById("<%=ncapexJVTextBox.ClientID %>");
	    var ncapexss = document.getElementById("<%=ncapexssTextBox.ClientID %>");
	    var nopexjv = document.getElementById("<%=nopexJVTextBox.ClientID %>");
	    var nopexss = document.getElementById("<%=nopexssTextBox.ClientID %>");
	    var ntotaljv = document.getElementById("<%=ntotalJVTextBox.ClientID %>");
	    var ntotalss = document.getElementById("<%=ntotalSSTextBox.ClientID %>");
	    var noapprovedjv = document.getElementById("<%=noapprovedJVTextBox.ClientID %>");
	    var noapprovedss = document.getElementById("<%=noapprovedSSTextBox.ClientID %>");
	    var nJV = document.getElementById("<%=nJVTextBox.ClientID %>");
	    var nSS = document.getElementById("<%=nSSTextBox.ClientID %>");
	    
	    var nopexssHF = document.getElementById("<%=nopexssHF.ClientID %>");
	    var ncapexssHF = document.getElementById("<%=ncapexssHF.ClientID %>");
	    var ntotaljvHF = document.getElementById("<%=ntotalJVHF.ClientID %>");
	    var ntotalssHF = document.getElementById("<%=ntotalSSHF.ClientID %>");
	    var nJVHF = document.getElementById("<%=nJVHF.ClientID %>");
	    var nSSHF = document.getElementById("<%=nSSHF.ClientID %>");
        
        nopexss.value= (rndnum(shell_share.value)*rndnum(nopexjv.value))/100; nopexssHF.value= (rndnum(shell_share.value)*rndnum(nopexjv.value))/100;
		ntotaljv.value=rndnum(ncapexjv.value) + rndnum(nopexjv.value); ntotaljvHF.value=rndnum(ncapexjv.value) + rndnum(nopexjv.value);
		ntotalss.value=rndnum(ncapexss.value) + rndnum(nopexss.value); ntotalssHF.value=rndnum(ncapexss.value) + rndnum(nopexss.value);
		nJV.value=rndnum(noapprovedjv.value) + rndnum(ntotaljv.value); nJVHF.value=rndnum(noapprovedjv.value) + rndnum(ntotaljv.value);
		nSS.value=rndnum(noapprovedss.value) + rndnum(ntotalss.value); nSSHF.value=rndnum(noapprovedss.value) + rndnum(ntotalss.value);
    }
    
    function getopexss(opexss)
    {
        var opexssHF = document.getElementById("<%=opexssHF.ClientID %>");
        opexss.value=rndnum(opexss.value); opexssHF.value=rndnum(opexss.value);
    }
    
    function getnopexss(nopexss)
    {
        var nopexssHF = document.getElementById("<%=nopexssHF.ClientID %>");
        nopexss.value=rndnum(nopexss.value); nopexssHF.value=rndnum(nopexss.value);
    }
    
    function gettotaljv(totaljv)
    {
        var totaljvHF = document.getElementById("<%=totalJVHF.ClientID %>");
        totaljv.value=rndnum(totaljv.value);totaljvHF.value=rndnum(totaljv.value);
    }
    
    function getntotaljv(ntotaljv)
    {
        var ntotaljvHF = document.getElementById("<%=ntotalJVHF.ClientID %>");
        ntotaljv.value=rndnum(ntotaljv.value);ntotaljvHF.value=rndnum(ntotaljv.value);
    }

    function gettotalss(totalss)
    {
    	var totalssHF = document.getElementById("<%=totalSSHF.ClientID %>");
        totalss.value=rndnum(totalss.value);
        totalssHF.value=rndnum(totalss.value);
    }

    function getntotalss(ntotalss)
    {
        var ntotalssHF = document.getElementById("<%=ntotalSSHF.ClientID %>");
        ntotalss.value=rndnum(ntotalss.value); ntotalssHF.value=rndnum(ntotalss.value);
    }
    
    function getoapprovedjv()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
	    var capexjv = document.getElementById("<%=capexJVTextBox.ClientID %>");
	    var capexss = document.getElementById("<%=capexssTextBox.ClientID %>");
	    var opexjv = document.getElementById("<%=opexJVTextBox.ClientID %>");
	    var opexss = document.getElementById("<%=opexssTextBox.ClientID %>");
	    var totaljv = document.getElementById("<%=totalJVTextBox.ClientID %>");
	    var totalss = document.getElementById("<%=totalSSTextBox.ClientID %>");
	    var oapprovedjv = document.getElementById("<%=oapprovedJVTextBox.ClientID %>");
	    var oapprovedss = document.getElementById("<%=oapprovedSSTextBox.ClientID %>");
	    var JV = document.getElementById("<%=JVTextBox.ClientID %>");
	    var SS = document.getElementById("<%=SSTextBox.ClientID %>");
        
   	    var oapprovedssHF = document.getElementById("<%=oapprovedSSHF.ClientID %>");
        var totaljvHF = document.getElementById("<%=totalJVHF.ClientID %>");
	    var totalssHF = document.getElementById("<%=totalSSHF.ClientID %>");
	    var JVHF = document.getElementById("<%=JVHF.ClientID %>");
	    var SSHF = document.getElementById("<%=SSHF.ClientID %>");

        
        oapprovedss.value = (rndnum(shell_share.value)*rndnum(oapprovedjv.value))/100; oapprovedssHF.value = (rndnum(shell_share.value)*rndnum(oapprovedjv.value))/100;
		totaljv.value = rndnum(capexjv.value) + rndnum(opexjv.value);totaljvHF.value = rndnum(capexjv.value) + rndnum(opexjv.value);
		totalss.value = rndnum(capexss.value) + rndnum(opexss.value);totalssHF.value = rndnum(capexss.value) + rndnum(opexss.value);
		JV.value = rndnum(oapprovedjv.value) + rndnum(totaljv.value);JVHF.value = rndnum(oapprovedjv.value) + rndnum(totaljv.value);
		SS.value = rndnum(oapprovedss.value) + rndnum(totalss.value);SSHF.value = rndnum(oapprovedss.value) + rndnum(totalss.value);
    }
    
    function getnoapprovedjv()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var ncapexjv = document.getElementById("<%=ncapexJVTextBox.ClientID %>");
	    var ncapexss = document.getElementById("<%=ncapexssTextBox.ClientID %>");
	    var nopexjv = document.getElementById("<%=nopexJVTextBox.ClientID %>");
	    var nopexss = document.getElementById("<%=nopexssTextBox.ClientID %>");
	    var ntotaljv = document.getElementById("<%=ntotalJVTextBox.ClientID %>");
	    var ntotalss = document.getElementById("<%=ntotalSSTextBox.ClientID %>");
	    var noapprovedjv = document.getElementById("<%=noapprovedJVTextBox.ClientID %>");
	    var noapprovedss = document.getElementById("<%=noapprovedSSTextBox.ClientID %>");
	    var nJV = document.getElementById("<%=nJVTextBox.ClientID %>");
	    var nSS = document.getElementById("<%=nSSTextBox.ClientID %>");
        
        var nopexssHF = document.getElementById("<%=nopexssHF.ClientID %>");
	    var ntotaljvHF = document.getElementById("<%=ntotalJVHF.ClientID %>");
	    var ntotalssHF = document.getElementById("<%=ntotalSSHF.ClientID %>");
	    var noapprovedssHF = document.getElementById("<%=noapprovedSSHF.ClientID %>");
	    var nJVHF = document.getElementById("<%=nJVHF.ClientID %>");
	    var nSSHF = document.getElementById("<%=nSSHF.ClientID %>");

        noapprovedss.value= (rndnum(shell_share.value)*rndnum(noapprovedjv.value))/100; noapprovedssHF.value= (rndnum(shell_share.value)*rndnum(noapprovedjv.value))/100;
		ntotaljv.value=rndnum(ncapexjv.value) + rndnum(nopexjv.value); ntotaljvHF.value=rndnum(ncapexjv.value) + rndnum(nopexjv.value);
		ntotalss.value=rndnum(ncapexss.value) + rndnum(nopexss.value); ntotalssHF.value=rndnum(ncapexss.value) + rndnum(nopexss.value);
		nJV.value=rndnum(noapprovedjv.value) + rndnum(ntotaljv.value); nJVHF.value=rndnum(noapprovedjv.value) + rndnum(ntotaljv.value);
		nSS.value=rndnum(noapprovedss.value) + rndnum(ntotalss.value); nSSHF.value=rndnum(noapprovedss.value) + rndnum(ntotalss.value);
    }
    
    function getoapprovedss(oapprovedss)
    {
    	var oapprovedssHF = document.getElementById("<%=oapprovedSSHF.ClientID %>");
        oapprovedss.value=rndnum(oapprovedss.value); oapprovedssHF.value=rndnum(oapprovedss.value);
    }
    
    function getnoapprovedss(noapprovedss)
    {
        var noapprovedssHF = document.getElementById("<%=noapprovedSSHF.ClientID %>");
        noapprovedss.value=rndnum(noapprovedss.value); noapprovedssHF.value=rndnum(noapprovedss.value);
    }
    
    function getJV(JV)
    {
    	var JVHF = document.getElementById("<%=JVHF.ClientID %>");
        JV.value=rndnum(JV.value); JVHF.value=rndnum(JV.value);
    }
    
    function getnJV(nJV)
    {
    	var nJVHF = document.getElementById("<%=nJVHF.ClientID %>");
        nJV.value=rndnum(nJV.value); nJVHF.value=rndnum(nJV.value);
    }
    
    function getSS(SS)
    {
    	var SSHF = document.getElementById("<%=SSHF.ClientID %>");
        SS.value=rndnum(SS.value); SSHF.value=rndnum(SS.value);
    }
    
    function getnSS(nSS) 
    { 
    	var nSSHF = document.getElementById("<%=nSSHF.ClientID %>");
        nSS.value=rndnum(nSS.value); nSSHF.value=rndnum(nSS.value); 
    }
    
    
    //Main Commitment Script
    	    
    function pajv1()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv1 = document.getElementById("<%=pajv1TextBox.ClientID %>"); 
        var pass1 = document.getElementById("<%=pass1TextBox.ClientID %>"); 
        var tpjv1 = document.getElementById("<%=tpjv1TextBox.ClientID %>"); 
        var tpss1 = document.getElementById("<%=tpss1TextBox.ClientID %>"); 
        var tjv1 = document.getElementById("<%=tjv1TextBox.ClientID %>"); 
        var tss1 = document.getElementById("<%=tss1TextBox.ClientID %>"); 

        var pass1HF = document.getElementById("<%=pass1HF.ClientID %>");
  	    var tjv1HF = document.getElementById("<%=tjv1HF.ClientID %>"); 
  	    var tss1HF = document.getElementById("<%=tss1HF.ClientID %>");
	    var tpss1HF = document.getElementById("<%= tpss1HF.ClientID %>");

        pass1.value = (rndnum(shell_share.value)*rndnum(pajv1.value))/100; pass1HF.value = (rndnum(shell_share.value)*rndnum(pajv1.value))/100;
        tjv1.value=rndnum(pajv1.value) + rndnum(tpjv1.value); tjv1HF.value=rndnum(pajv1.value) + rndnum(tpjv1.value);
        tss1.value=rndnum(pass1.value) + rndnum(tpss1.value); tss1HF.value=rndnum(pass1.value) + rndnum(tpss1.value);
    }

    function tpjv1()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv1 = document.getElementById("<%=pajv1TextBox.ClientID %>"); 
        var pass1 = document.getElementById("<%=pass1TextBox.ClientID %>"); 
        var tpjv1 = document.getElementById("<%=tpjv1TextBox.ClientID %>"); 
        var tpss1 = document.getElementById("<%=tpss1TextBox.ClientID %>"); 
        var tjv1 = document.getElementById("<%=tjv1TextBox.ClientID %>"); 
        var tss1 = document.getElementById("<%=tss1TextBox.ClientID %>"); 
        
        var tjv1HF = document.getElementById("<%=tjv1HF.ClientID %>"); 
  	    var tss1HF = document.getElementById("<%=tss1HF.ClientID %>");
	    var tpss1HF = document.getElementById("<%= tpss1HF.ClientID %>");
        
        tpss1.value= (rndnum(shell_share.value)*rndnum(tpjv1.value))/100; tpss1HF.value= (rndnum(shell_share.value)*rndnum(tpjv1.value))/100;
        tjv1.value=rndnum(pajv1.value) + rndnum(tpjv1.value); tjv1HF.value=rndnum(pajv1.value) + rndnum(tpjv1.value);
        tss1.value=rndnum(pass1.value) + rndnum(tpss1.value); tss1HF.value=rndnum(pass1.value) + rndnum(tpss1.value);
    }

    function pajv2()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv2 = document.getElementById("<%=pajv2TextBox.ClientID %>"); 
        var pass2 = document.getElementById("<%=pass2TextBox.ClientID %>"); 
        var tpjv2 = document.getElementById("<%=tpjv2TextBox.ClientID %>"); 
        var tpss2 = document.getElementById("<%=tpss2TextBox.ClientID %>"); 
        var tjv2 = document.getElementById("<%=tjv2TextBox.ClientID %>"); 
        var tss2 = document.getElementById("<%=tss2TextBox.ClientID %>"); 
        
        var pass2HF = document.getElementById("<%=pass2HF.ClientID %>");
	    var tjv2HF = document.getElementById("<%=tjv2HF.ClientID %>"); 
	    var tss2HF = document.getElementById("<%=tss2HF.ClientID %>");
        
        pass2.value= (rndnum(shell_share.value)*rndnum(pajv2.value))/100;
        pass2HF.value= (rndnum(shell_share.value)*rndnum(pajv2.value))/100;
        tjv2.value=rndnum(pajv2.value) + rndnum(tpjv2.value);
        tjv2HF.value=rndnum(pajv2.value) + rndnum(tpjv2.value);
        tss2.value=rndnum(pass2.value) + rndnum(tpss2.value);
        tss2HF.value=rndnum(pass2.value) + rndnum(tpss2.value);
    }

    function tpjv2()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv2 = document.getElementById("<%=pajv2TextBox.ClientID %>"); 
        var pass2 = document.getElementById("<%=pass2TextBox.ClientID %>"); 
        var tpjv2 = document.getElementById("<%=tpjv2TextBox.ClientID %>"); 
        var tpss2 = document.getElementById("<%=tpss2TextBox.ClientID %>"); 
        var tjv2 = document.getElementById("<%=tjv2TextBox.ClientID %>"); 
        var tss2 = document.getElementById("<%=tss2TextBox.ClientID %>"); 
        
	    var tjv2HF = document.getElementById("<%=tjv2HF.ClientID %>"); 
	    var tpss2HF = document.getElementById("<%=tpss2HF.ClientID %>");
	    var tss2HF = document.getElementById("<%=tss2HF.ClientID %>");
        
        tpss2.value= (rndnum(shell_share.value)*rndnum(tpjv2.value))/100;
        tpss2HF.value= (rndnum(shell_share.value)*rndnum(tpjv2.value))/100;
        tjv2.value=rndnum(pajv2.value) + rndnum(tpjv2.value);
        tjv2HF.value=rndnum(pajv2.value) + rndnum(tpjv2.value);
        tss2.value=rndnum(pass2.value) + rndnum(tpss2.value);
        tss2HF.value=rndnum(pass2.value) + rndnum(tpss2.value);
    }

    function pajv3()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv3 = document.getElementById("<%=pajv3TextBox.ClientID %>"); 
        var pass3 = document.getElementById("<%=pass3TextBox.ClientID %>"); 
        var tpjv3 = document.getElementById("<%=tpjv3TextBox.ClientID %>"); 
        var tpss3 = document.getElementById("<%=tpss3TextBox.ClientID %>"); 
        var tjv3 = document.getElementById("<%=tjv3TextBox.ClientID %>"); 
        var tss3 = document.getElementById("<%=tss3TextBox.ClientID %>"); 
        
   	    var pass3HF = document.getElementById("<%=pass3HF.ClientID %>");
	    var tjv3HF = document.getElementById("<%=tjv3HF.ClientID %>"); 
	    var tss3HF = document.getElementById("<%=tss3HF.ClientID %>");

        pass3.value= (rndnum(shell_share.value)*rndnum(pajv3.value))/100;
        pass3HF.value= (rndnum(shell_share.value)*rndnum(pajv3.value))/100;
        tjv3.value=rndnum(pajv3.value) + rndnum(tpjv3.value);
        tjv3HF.value=rndnum(pajv3.value) + rndnum(tpjv3.value);
        tss3.value=rndnum(pass3.value) + rndnum(tpss3.value);
        tss3HF.value=rndnum(pass3.value) + rndnum(tpss3.value);
    } 

    function tpjv3()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv3 = document.getElementById("<%=pajv3TextBox.ClientID %>"); 
        var pass3 = document.getElementById("<%=pass3TextBox.ClientID %>"); 
        var tpjv3 = document.getElementById("<%=tpjv3TextBox.ClientID %>"); 
        var tpss3 = document.getElementById("<%=tpss3TextBox.ClientID %>"); 
        var tjv3 = document.getElementById("<%=tjv3TextBox.ClientID %>"); 
        var tss3 = document.getElementById("<%=tss3TextBox.ClientID %>"); 
        
	    var tjv3HF = document.getElementById("<%=tjv3HF.ClientID %>"); 
	    var tpss3HF = document.getElementById("<%=tpss3HF.ClientID %>");
	    var tss3HF = document.getElementById("<%=tss3HF.ClientID %>");

        tpss3.value= (rndnum(shell_share.value)*rndnum(tpjv3.value))/100;
        tpss3HF.value= (rndnum(shell_share.value)*rndnum(tpjv3.value))/100;
        tjv3.value=rndnum(pajv3.value) + rndnum(tpjv3.value);
        tjv3HF.value=rndnum(pajv3.value) + rndnum(tpjv3.value);
        tss3.value=rndnum(pass3.value) + rndnum(tpss3.value);
        tss3HF.value=rndnum(pass3.value) + rndnum(tpss3.value);
    }

    function pajv4()
    {   
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv4 = document.getElementById("<%=pajv4TextBox.ClientID %>"); 
        var pass4 = document.getElementById("<%=pass4TextBox.ClientID %>"); 
        var tpjv4 = document.getElementById("<%=tpjv4TextBox.ClientID %>"); 
        var tpss4 = document.getElementById("<%=tpss4TextBox.ClientID %>"); 
        var tjv4 = document.getElementById("<%=tjv4TextBox.ClientID %>"); 
        var tss4 = document.getElementById("<%=tss4TextBox.ClientID %>"); 
        
        var pass4HF = document.getElementById("<%=pass4HF.ClientID %>");
	    var tjv4HF = document.getElementById("<%=tjv4HF.ClientID %>"); 
	    var tss4HF = document.getElementById("<%=tss4HF.ClientID %>");

        pass4.value= (rndnum(shell_share.value)*rndnum(pajv4.value))/100;
        pass4HF.value= (rndnum(shell_share.value)*rndnum(pajv4.value))/100;
        tjv4.value=rndnum(pajv4.value) + rndnum(tpjv4.value);
        tjv4HF.value=rndnum(pajv4.value) + rndnum(tpjv4.value);
        tss4.value=rndnum(pass4.value) + rndnum(tpss4.value);
        tss4HF.value=rndnum(pass4.value) + rndnum(tpss4.value);
    }

    function tpjv4()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv4 = document.getElementById("<%=pajv4TextBox.ClientID %>"); 
        var pass4 = document.getElementById("<%=pass4TextBox.ClientID %>"); 
        var tpjv4 = document.getElementById("<%=tpjv4TextBox.ClientID %>"); 
        var tpss4 = document.getElementById("<%=tpss4TextBox.ClientID %>"); 
        var tjv4 = document.getElementById("<%=tjv4TextBox.ClientID %>"); 
        var tss4 = document.getElementById("<%=tss4TextBox.ClientID %>"); 
            
	    var tjv4HF = document.getElementById("<%=tjv4HF.ClientID %>"); 
	    var tpss4HF = document.getElementById("<%=tpss4HF.ClientID %>");
	    var tss4HF = document.getElementById("<%=tss4HF.ClientID %>");
            
        tpss4.value= (rndnum(shell_share.value)*rndnum(tpjv4.value))/100;
        tpss4HF.value= (rndnum(shell_share.value)*rndnum(tpjv4.value))/100;
        tjv4.value=rndnum(pajv4.value) + rndnum(tpjv4.value);
        tjv4HF.value=rndnum(pajv4.value) + rndnum(tpjv4.value);
        tss4.value=rndnum(pass4.value) + rndnum(tpss4.value);
        tss4HF.value=rndnum(pass4.value) + rndnum(tpss4.value);
    }

    function pajv5()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv5 = document.getElementById("<%=pajv5TextBox.ClientID %>"); 
        var pass5 = document.getElementById("<%=pass5TextBox.ClientID %>"); 
        var tpjv5 = document.getElementById("<%=tpjv5TextBox.ClientID %>"); 
        var tpss5 = document.getElementById("<%=tpss5TextBox.ClientID %>"); 
        var tjv5 = document.getElementById("<%=tjv5TextBox.ClientID %>"); 
        var tss5 = document.getElementById("<%=tss5TextBox.ClientID %>"); 
        
        var pass5HF = document.getElementById("<%=pass5HF.ClientID %>");
	    var tjv5HF = document.getElementById("<%=tjv5HF.ClientID %>"); 
	    var tss5HF = document.getElementById("<%=tss5HF.ClientID %>");
        
        pass5.value= (rndnum(shell_share.value)*rndnum(pajv5.value))/100;
        pass5HF.value= (rndnum(shell_share.value)*rndnum(pajv5.value))/100;
        tjv5.value=rndnum(pajv5.value) + rndnum(tpjv5.value);
        tjv5HF.value=rndnum(pajv5.value) + rndnum(tpjv5.value);
        tss5.value=rndnum(pass5.value) + rndnum(tpss5.value);
        tss5HF.value=rndnum(pass5.value) + rndnum(tpss5.value);
    }

    function tpjv5()
    {
        var shell_share = document.getElementById("<%=shellshareTextBox.ClientID %>");
        var pajv5 = document.getElementById("<%=pajv5TextBox.ClientID %>"); 
        var pass5 = document.getElementById("<%=pass5TextBox.ClientID %>"); 
        var tpjv5 = document.getElementById("<%=tpjv5TextBox.ClientID %>"); 
        var tpss5 = document.getElementById("<%=tpss5TextBox.ClientID %>"); 
        var tjv5 = document.getElementById("<%=tjv5TextBox.ClientID %>"); 
        var tss5 = document.getElementById("<%=tss5TextBox.ClientID %>"); 
        
	    var tjv5HF = document.getElementById("<%=tjv5HF.ClientID %>"); 
	    var tpss5HF = document.getElementById("<%=tpss5HF.ClientID %>");
	    var tss5HF = document.getElementById("<%=tss5HF.ClientID %>");

        tpss5.value= (rndnum(shell_share.value)*rndnum(tpjv5.value))/100;
        tpss5HF.value= (rndnum(shell_share.value)*rndnum(tpjv5.value))/100;
        tjv5.value=rndnum(pajv5.value) + rndnum(tpjv5.value);
        tjv5HF.value=rndnum(pajv5.value) + rndnum(tpjv5.value);
        tss5.value=rndnum(pass5.value) + rndnum(tpss5.value);
        tss5HF.value=rndnum(pass5.value) + rndnum(tpss5.value);
    }
    
    //Section 1: JavaScripts
    
    function ppym()
    {
        //Section 1 Hidden Fields
        
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
    
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
    
        tpym.value = ppym.value- -cpym.value; tpymHF.value = ppym.value- -cpym.value; 
        ppjv.value = ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;
        ppjvHF.value = ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value; 
        tpjv.value = ppjv.value - -cpjv.value; tpjvHF.value = ppjv.value - -cpjv.value; 
        ppss.value = rndnum(shell_share.value*ppjv.value/100); ppssHF.value = rndnum(shell_share.value*ppjv.value/100);
        tpss.value = ppss.value - -cpss.value; tpssHF.value = ppss.value - -cpss.value;
    }

    function ppyn()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
        
        tpyn.value= ppyn.value - -cpyn.value; tpynHF.value= ppyn.value - -cpyn.value; 
        ppjv.value=ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;
        ppjvHF.value = ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;  
        tpjv.value= ppjv.value - -cpjv.value; tpjvHF.value = ppjv.value - -cpjv.value; 
        ppss.value= rndnum(shell_share.value*ppjv.value/100); ppssHF.value = rndnum(shell_share.value*ppjv.value/100);  
        tpss.value= ppss.value - -cpss.value; tpssHF.value = ppss.value - -cpss.value;
    }

    function ppyp1()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
        
        tpyp1.value= ppyp1.value - -cpyp1.value;
        tpy1HF.value= ppyp1.value - -cpyp1.value; 
        ppjv.value=ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;
        ppjvHF.value = ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;  
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value = ppjv.value - -cpjv.value; 
        ppss.value= rndnum(shell_share.value*ppjv.value/100);
        ppssHF.value = rndnum(shell_share.value*ppjv.value/100);  
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value = ppss.value - -cpss.value;
    }

    function ppyp2()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
        
        tpyp2.value= ppyp2.value - -cpyp2.value;
        tpy2HF.value= ppyp2.value - -cpyp2.value;  
        ppjv.value=ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;
        ppjvHF.value = ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;  
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value = ppjv.value - -cpjv.value; 
        ppss.value= rndnum(shell_share.value*ppjv.value/100);
        ppssHF.value = rndnum(shell_share.value*ppjv.value/100);  
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value = ppss.value - -cpss.value;
    }

    function ppyp3()
    {   
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
     
    
        tpyp3.value= ppyp3.value - -cpyp3.value; 
        tpy3HF.value= ppyp3.value - -cpyp3.value; 
        ppjv.value=ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;
        ppjvHF.value = ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;  
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value = ppjv.value - -cpjv.value; 
        ppss.value= rndnum(shell_share.value*ppjv.value/100);
        ppssHF.value = rndnum(shell_share.value*ppjv.value/100);  
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value = ppss.value - -cpss.value;
    }

    function ppyp4()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
    
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");

    
        tpyp4.value= ppyp4.value - -cpyp4.value;
        tpy4HF.value= ppyp4.value - -cpyp4.value;  
        ppjv.value=ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;
        ppjvHF.value = ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;  
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value = ppjv.value - -cpjv.value; 
        ppss.value= rndnum(shell_share.value*ppjv.value/100);
        ppssHF.value = rndnum(shell_share.value*ppjv.value/100);  
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value = ppss.value - -cpss.value;
    }

    function ppyp5()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
    
        tpyp5.value= ppyp5.value - -cpyp5.value;
        tpy5HF.value= ppyp5.value - -cpyp5.value; 
        ppjv.value=ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;
        ppjvHF.value = ppym.value- -ppyn.value- -ppyp1.value- -ppyp2.value- -ppyp3.value- -ppyp4.value- -ppyp5.value;  
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value = ppjv.value - -cpjv.value; 
        ppss.value= rndnum(shell_share.value*ppjv.value/100);
        ppssHF.value = rndnum(shell_share.value*ppjv.value/100);  
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value = ppss.value - -cpss.value;
    }

    function cpym()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
        
        tpym.value= ppym.value - -cpym.value;
        tpymHF.value= ppym.value - -cpym.value; 
        cpjv.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value;
        cpjvHF.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value; 
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value= ppjv.value - -cpjv.value; 
        cpss.value= rndnum(shell_share.value*cpjv.value/100);
        cpssHF.value= rndnum(shell_share.value*cpjv.value/100); 
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value= ppss.value - -cpss.value;
    }

    function cpyn()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
    
        tpyn.value= ppyn.value - -cpyn.value; 
        tpynHF.value= ppyn.value - -cpyn.value; 
        cpjv.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value;
        cpjvHF.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value; 
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value= ppjv.value - -cpjv.value; 
        cpss.value= rndnum(shell_share.value*cpjv.value/100);
        cpssHF.value= rndnum(shell_share.value*cpjv.value/100); 
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value= ppss.value - -cpss.value;
    }

    function cpyp1()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
    
        tpyp1.value= ppyp1.value - -cpyp1.value; 
        tpy1HF.value= ppyp1.value - -cpyp1.value;
        cpjv.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value;
        cpjvHF.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value; 
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value= ppjv.value - -cpjv.value; 
        cpss.value= rndnum(shell_share.value*cpjv.value/100);
        cpssHF.value= rndnum(shell_share.value*cpjv.value/100); 
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value= ppss.value - -cpss.value;
    }

    function cpyp2()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
    
        tpyp2.value= ppyp2.value - -cpyp2.value; 
        tpy2HF.value= ppyp2.value - -cpyp2.value; 
        cpjv.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value;
        cpjvHF.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value; 
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value= ppjv.value - -cpjv.value; 
        cpss.value= rndnum(shell_share.value*cpjv.value/100);
        cpssHF.value= rndnum(shell_share.value*cpjv.value/100); 
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value= ppss.value - -cpss.value;
    }

    function cpyp3()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
        
        tpyp3.value= ppyp3.value - -cpyp3.value;
        tpy3HF.value= ppyp3.value - -cpyp3.value; 
        cpjv.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value;
        cpjvHF.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value; 
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value= ppjv.value - -cpjv.value; 
        cpss.value= rndnum(shell_share.value*cpjv.value/100);
        cpssHF.value= rndnum(shell_share.value*cpjv.value/100); 
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value= ppss.value - -cpss.value;
    }

    function cpyp4()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
    
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
    
        tpyp4.value= ppyp4.value - -cpyp4.value;
        tpy4HF.value= ppyp4.value - -cpyp4.value; 
        cpjv.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value;
        cpjvHF.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value; 
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value= ppjv.value - -cpjv.value; 
        cpss.value= rndnum(shell_share.value*cpjv.value/100);
        cpssHF.value= rndnum(shell_share.value*cpjv.value/100); 
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value= ppss.value - -cpss.value;
    }

    function cpyp5()
    {
        var ppjvHF = document.getElementById("<%=ppjvHF.ClientID %>");
        var ppssHF = document.getElementById("<%=ppssHF.ClientID %>");
        var cpjvHF = document.getElementById("<%=cpjvHF.ClientID %>");
        var cpssHF = document.getElementById("<%=cpssHF.ClientID %>");
        var tpymHF = document.getElementById("<%=tpymHF.ClientID %>");
        var tpynHF = document.getElementById("<%=tpynHF.ClientID %>");
        var tpy1HF = document.getElementById("<%=tpy1HF.ClientID %>");
        var tpy2HF = document.getElementById("<%=tpy2HF.ClientID %>");
        var tpy3HF = document.getElementById("<%=tpy3HF.ClientID %>");
        var tpy4HF = document.getElementById("<%=tpy4HF.ClientID %>");
        var tpy5HF = document.getElementById("<%=tpy5HF.ClientID %>");
        var tpjvHF = document.getElementById("<%=tpjvHF.ClientID %>");
        var tpssHF = document.getElementById("<%=tpssHF.ClientID %>");
        
        var shell_share = document.getElementById("<%=ShellSharedTextBox.ClientID %>");
        var ppym = document.getElementById("<%=ppym.ClientID %>"); 
        var ppyn = document.getElementById("<%=ppyn.ClientID %>");
        var ppyp1 = document.getElementById("<%=ppy1.ClientID %>");
        var ppyp2 = document.getElementById("<%=ppy2.ClientID %>");
        var ppyp3 = document.getElementById("<%=ppy3.ClientID %>");
        var ppyp4 = document.getElementById("<%=ppy4.ClientID %>");
        var ppyp5 = document.getElementById("<%=ppy5.ClientID %>");
        var ppjv = document.getElementById("<%=ppjv.ClientID %>");
        var ppss = document.getElementById("<%=ppss.ClientID %>");

        var cpym = document.getElementById("<%=cpym.ClientID %>");
        var cpyn = document.getElementById("<%=cpyn.ClientID %>");
        var cpyp1 = document.getElementById("<%=cpy1.ClientID %>");
        var cpyp2 = document.getElementById("<%=cpy2.ClientID %>");
        var cpyp3 = document.getElementById("<%=cpy3.ClientID %>");
        var cpyp4 = document.getElementById("<%=cpy4.ClientID %>");
        var cpyp5 = document.getElementById("<%=cpy5.ClientID %>");
        var cpjv = document.getElementById("<%=cpjv.ClientID %>");
        var cpss = document.getElementById("<%=cpss.ClientID %>");

        var tpym = document.getElementById("<%=tpym.ClientID %>");
        var tpyn = document.getElementById("<%=tpyn.ClientID %>");
        var tpyp1 = document.getElementById("<%=tpy1.ClientID %>");
        var tpyp2 = document.getElementById("<%=tpy2.ClientID %>");
        var tpyp3 = document.getElementById("<%=tpy3.ClientID %>");
        var tpyp4 = document.getElementById("<%=tpy4.ClientID %>");
        var tpyp5 = document.getElementById("<%=tpy5.ClientID %>");
        var tpjv = document.getElementById("<%=tpjv.ClientID %>");
        var tpss = document.getElementById("<%=tpss.ClientID %>");
    
        tpyp5.value= ppyp5.value - -cpyp5.value;
        tpy5HF.value= ppyp5.value - -cpyp5.value; 
        cpjv.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value;
        cpjvHF.value=cpym.value- -cpyn.value- -cpyp1.value- -cpyp2.value- -cpyp3.value- -cpyp4.value- -cpyp5.value; 
        tpjv.value= ppjv.value - -cpjv.value; 
        tpjvHF.value= ppjv.value - -cpjv.value; 
        cpss.value= rndnum(shell_share.value*cpjv.value/100);
        cpssHF.value= rndnum(shell_share.value*cpjv.value/100); 
        tpss.value= ppss.value - -cpss.value;
        tpssHF.value= ppss.value - -cpss.value;
    }
    
    function showMyBrowser()
    {
        //navigator.
       
    }