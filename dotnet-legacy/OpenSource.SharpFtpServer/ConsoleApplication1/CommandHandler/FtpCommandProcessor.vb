Imports ConsoleApplication1.SharpFtpServer
Imports System.Collections.Generic

Public Class FtpCommandProcessor

  Private _Handlers As New List(Of IFtpCommandHandler)

  Public Sub New(ParamArray registerCommandHandlers() As IFtpCommandHandler)
    For Each handler As IFtpCommandHandler In registerCommandHandlers
      Me.RegisterCommandHandler(handler)
    Next
  End Sub

  Public Sub RegisterCommandHandler(handler As IFtpCommandHandler)
    _Handlers.Add(handler)
  End Sub

  Public Function ProcessFtpCommand(command As String, arguments As String, logEntry As LogEntry) As String
    Dim result As String = Nothing
    command = command.ToUpper()
    For Each handler As IFtpCommandHandler In _Handlers
      result = handler.HandleFtpCommand(command, arguments, logEntry)
      If (result IsNot Nothing) Then
        Return result
      End If
    Next
    Return FtpMessages.GetMessage(FtpCommunicationMessage.UnknownCommand)
  End Function

End Class

Public Interface IFtpCommandHandler

  Function HandleFtpCommand(command As String, arguments As String, logEntry As LogEntry) As String

End Interface
