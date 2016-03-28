
<%
 Dim BEO_NAME
 set conn=Server.CreateObject("ADODB.Connection")
 conn.Open "Driver={SQL Server};Server=PHOEBE;Database=NSS_DATABASE;Uid=sa;Pwd=2BtheDBA;" 
 set rs=Server.CreateObject("ADODB.recordset")
 sql="SELECT B.POST_AS, CASE substring([EVTYPETXT],1,9) WHEN 'Reception' THEN 'Reception' ELSE [EVTYPETXT] END EVENTTYPE, R.ROOM_NAME, right(CONVERT(varchar,dateadd(SECOND,E.EV_STIME,EV_SDATE), 100),7) STARTTIME, right(CONVERT(varchar,dateadd(SECOND,E.EV_CTIME,EV_SDATE), 100),7) ENDTIME " & _
  "FROM EVENT E, BUSINESS B, EVTYPE ET, ROOMDEF R " & _
  "where E.BUS_ID = B.BUS_ID " & _
  "and E.EVTYPE_ID = ET.EVTYPE_ID " & _
  "and E.ROOMDEF_ID = R.ROOMDEF_ID " & _
  "and CONVERT(DATE,GETDATE(), 106) = EV_SDATE " & _
  "and DATEPART(HH,GETDATE()) < (E.EV_CTIME/3600) " & _
  "and E.EVTYPE_ID in (8,9,39,40,42)" & _
  "order by B.POST_AS, E.EV_STIME "
 rs.Open sql, conn
 If Rs.EOF Then
 Response.write "<h2>NO&nbsp;&nbsp;&nbsp;EVENTS&nbsp;&nbsp;&nbsp;SCHEDULED&nbsp;&nbsp;&nbsp;AT&nbsp;&nbsp;&nbsp;THIS&nbsp;&nbsp;&nbsp;TIME<h2>" 
 Else
 Response.write "<table>"
 Do Until Rs.EOF
 	If BEO_NAME <> Rs.Fields("POST_AS") Then
		Response.write "<tr><td align='left' colspan='3'><br></TD></tr>" & vbCrLf
 		Response.write "<tr><td align='left' colspan='3'><big><B><U>" & Rs.Fields("POST_AS").Value & "</U></B></big></TD></tr>" & vbCrLf
		BEO_NAME = Rs.Fields("POST_AS")
	End If
	Response.write "<tr><td>" & Rs.Fields("STARTTIME").Value & " - " & Rs.Fields("ENDTIME").Value & "</TD></td><td align=right>" & Rs.Fields("ROOM_NAME").Value & "</TD></td></tr>"
 Rs.MoveNext
 Loop
 Response.write "</table>"
 End If
 rs.Close
 Set rs = Nothing
 Set Conn = Nothing
%>
