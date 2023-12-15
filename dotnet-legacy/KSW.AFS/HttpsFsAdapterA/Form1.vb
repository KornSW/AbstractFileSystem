
Imports System.Security
Imports System

Public Class Form1


  Dim con As New HfsConection

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    con.Url = "http://file.familiekorn.net"
    'con.TransmissionDiag = Sub(url, response)



    '                         resultXml = response.ExtractRegions("<!--{", "}-->")

    '                       End Sub













  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    If Not con.Authenticate("tobias", "1#Aaaaaddd$Remote") Then
      TextBox1.Text = "Auth Error"
      Exit Sub
    End If

    con.Test2()

    'Me.Icon = con.Test().ToIcon()

  End Sub
End Class
