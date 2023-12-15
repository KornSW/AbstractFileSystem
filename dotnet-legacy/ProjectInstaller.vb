Imports System.ComponentModel
Imports System.Configuration.Install

Public Class ProjectInstaller

  Public Sub New()
    MyBase.New()
    InitializeComponent()
    Application.ConfigureInstallers(Me.ServiceProcessInstaller, Me.ServiceInstaller)
  End Sub

End Class
