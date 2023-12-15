Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text
Imports KSW.Base
Imports System.Windows.Forms
Imports System.Security

Public Class FormMain

  '"http://localhost:1810"
  'ac4f084064879115756281290bd84c84

  Private _ClientProvider As AFS.WebBridge.WebbridgeClient = Nothing

  Public Property Application As Application

  Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.InitializeClientProvider()
  End Sub

  Private Sub InitializeClientProvider()
    gDirectories.Root = Nothing
    Dim key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\KSW\AfsTrayBrowser", False)
    If (key Is Nothing) Then
      Exit Sub
    End If

    Dim server As String = key.GetValue("Url", "").ToString()
    Dim user As String = key.GetValue("UserName", "").ToString()
    Dim pass As String = key.GetValue("Password", "").ToString()
    key.Close()

    If (server = "" OrElse user = "" OrElse pass = "") Then
      Exit Sub
    End If
    Dim pwd As New SecureString

    pwd.DecryptFrom(pass)

    gDirectories.Root = New AFS.WebBridge.WebbridgeClient(server, user, pwd)

  End Sub

  Private Sub gBtnEditSettings_Click(sender As Object, e As EventArgs) Handles gBtnEditSettings.Click
    Using dlg As New FormSettings
      dlg.gTxtPassword.Tag = ""
      Dim key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\KSW\AfsTrayBrowser", False)
      If (key IsNot Nothing) Then
        dlg.gTxtUrl.Text = key.GetValue("Url", "").ToString()
        dlg.gTxtUser.Text = key.GetValue("UserName", "").ToString()
        Dim pwd As New SecureString
        Dim pass As String = key.GetValue("Password", "").ToString()
        Try
          pwd.DecryptFrom(pass)
        Catch
        End Try
        dlg.gTxtPassword.Text = New String("*"c, pwd.Length)
        dlg.gTxtPassword.Tag = dlg.gTxtPassword.Text
        key.Close()
      End If
      If (dlg.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
        key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\KSW\AfsTrayBrowser")
        key.SetValue("Url", dlg.gTxtUrl.Text)
        key.SetValue("UserName", dlg.gTxtUser.Text)
        If (dlg.gTxtPassword.Tag Is Nothing) Then
          Dim ss As New SecureString
          ss.AppendString(dlg.gTxtPassword.Text)
          key.SetValue("Password", ss.GetEncryptedString())
        End If
        key.Close()
      End If
    End Using
    Me.InitializeClientProvider()
  End Sub

#Region " Tray Application "

  Private Sub FormMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    e.Cancel = True
    Me.WindowState = FormWindowState.Minimized
  End Sub

  Private Sub FormMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    If (Me.WindowState = FormWindowState.Minimized) Then
      Me.ShowInTaskbar = False
    Else
      Me.ShowInTaskbar = True
    End If
  End Sub

  Private Sub gTrayIcon_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles gTrayIcon.MouseDoubleClick
    If (Me.WindowState = FormWindowState.Minimized) Then
      Me.WindowState = FormWindowState.Normal
    End If
  End Sub

  Private Sub gBtnExit_Click(sender As Object, e As EventArgs) Handles gBtnExit.Click
    Me.Application.Shutdown()
  End Sub

#End Region

End Class
