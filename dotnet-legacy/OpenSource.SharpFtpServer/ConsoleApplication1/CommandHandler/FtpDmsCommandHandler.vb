
Public Class FtpDmsCommandHandler
  Implements IFtpCommandHandler

  Private _Connection As ClientConnection

  Public Sub New(connection As ClientConnection)
    _Connection = connection
  End Sub

  Public Function HandleFtpCommand(command As String, arguments As String, logEntry As LogEntry) As String Implements IFtpCommandHandler.HandleFtpCommand
    Select Case command
      Case Else
    End Select

    Return Nothing
  End Function

End Class
