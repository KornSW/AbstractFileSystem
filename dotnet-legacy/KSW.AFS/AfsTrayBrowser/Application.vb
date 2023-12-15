Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports KSW.AFS
Imports KSW.Base
Imports KSW.Base.Shell.WinForms

#Region " (Process Entry Point) "

Friend Module Main

  Public Function Main(arguments() As String) As Integer
    Using applicationInstance As New Application
      Return applicationInstance.Run(arguments)
    End Using
  End Function

End Module

#End Region

Public Class Application
#Region "..."
  Inherits WinFormsApplication

  Public Sub New()
    MyBase.New(My.Application, True)
  End Sub

  Protected Overrides Sub PrepareStartup(ByRef cancel As Boolean)
    MyBase.PrepareStartup(cancel)
    InjectedTypes.InjectAssembliesFromWorkdir(True)
    Me.InitServices()
  End Sub

  Protected Overrides Function CreateMainForm() As Form
    Dim newInstance As New FormMain
    newInstance.Application = Me
    Return newInstance
  End Function

  Protected Overrides Sub PrepareShutdown()
    Me.ShutdownServices()
    MyBase.PrepareShutdown()
  End Sub

#End Region

  'Public Property Config As ToolConfig

  'Public Property Service1 As Service1

  Private Sub InitServices()

    'Me.Config = AppConfig.Load(Of ToolConfig)("ToolConfig.xml")
    'If (Me.Config Is Nothing) Then
    '  Me.Config = New ToolConfig
    'End If

    'Me.Service1 = New Service1
  End Sub

  Private Sub ShutdownServices()
    'Me.Service1.TryDispose(True)
    'Me.Service1 = Nothing
  End Sub

End Class
