Imports KSW.FtpServerEngine
Imports System.Net
Imports System.IO
Imports System.Net.Sockets

Public Class FtpBaseCommandHandler
  Implements IFtpCommandHandler

#Region " Constants (Command Strings) "

  Public Const _QUIT As String = "QUIT"
  Public Const _USER As String = "USER"
  Public Const _PASS As String = "PASS"
  Public Const _MDTM As String = "MDTM"
  Public Const _SIZE As String = "SIZE"
  Public Const _SYST As String = "SYST"
  Public Const _REIN As String = "REIN"
  Public Const _PORT As String = "PORT"
  Public Const _PASV As String = "PASV"
  Public Const _TYPE As String = "TYPE"
  Public Const _STRU As String = "STRU"
  Public Const _MODE As String = "MODE"
  Public Const _NOOP As String = "NOOP"
  Public Const _ACCT As String = "ACCT"
  Public Const _ALLO As String = "ALLO"
  Public Const _NLST As String = "NLST"
  Public Const _SITE As String = "SITE"
  Public Const _STAT As String = "STAT"
  Public Const _HELP As String = "HELP"
  Public Const _SMNT As String = "SMNT"
  Public Const _REST As String = "REST"
  Public Const _ABOR As String = "ABOR"
  Public Const _AUTH As String = "AUTH"
  Public Const _FEAT As String = "FEAT"
  Public Const _OPTS As String = "OPTS"
  Public Const _EPRT As String = "EPRT"
  Public Const _EPSV As String = "EPSV"

  Private Const _HiddenPwd As String = "******"

#End Region

  Private _Connection As ClientConnection
  Private _UserName As String = Nothing

  Public Sub New(connection As ClientConnection)
    _Connection = connection
  End Sub

  Public Function HandleFtpCommand(command As String, arguments As String, logEntry As LogEntry) As String Implements IFtpCommandHandler.HandleFtpCommand
    Dim response As String = Nothing

    If (Not command = _PASS) Then
      _UserName = Nothing
    End If

    Select Case command

      Case _USER
        _UserName = arguments
        response = FtpMessages.GetMessage(FtpCommunicationMessage.UsernameOkNeedPass)

      Case _PASS
        Dim newUser As User = UserStore.Validate(_UserName, arguments)
        If (newUser Is Nothing) Then
          response = FtpMessages.GetMessage(FtpCommunicationMessage.NotLoggedIn)
        Else
          _Connection.ChangeIdentity(newUser)
          response = FtpMessages.GetMessage(FtpCommunicationMessage.LoggedIn)
        End If
        logEntry.CSUriStem = _HiddenPwd

      Case _QUIT
        response = FtpMessages.GetMessage(FtpCommunicationMessage.ServiceClosingControlConnection)

      Case _REIN
        _Connection.ChangeIdentity(Nothing)

      Case _PORT
        _Connection.ActiveModePort(arguments, logEntry.CPort)
        response = FtpMessages.GetMessage(FtpCommunicationMessage.DataConnectionEstablished)

      Case _PASV
        Dim passiveEndpointInfo As Byte() = _Connection.EnterPassiveMode(logEntry.SPort)
        response = String.Format(FtpMessages.GetMessage(FtpCommunicationMessage.EnteringPassiveMode), passiveEndpointInfo(0), passiveEndpointInfo(1), passiveEndpointInfo(2), passiveEndpointInfo(3), passiveEndpointInfo(4), passiveEndpointInfo(5))

      Case _TYPE
        Dim transferTypeString As String = String.Empty
        Dim identifierChar As Char = command(1)
        If (_Connection.SetTransferType(identifierChar, If(command.Length = 3, command(2), Nothing), transferTypeString)) Then
          response = String.Format("200 Type set to {0}", transferTypeString)
        Else
          response = FtpMessages.GetMessage(FtpCommunicationMessage.CommandNotImplementedForThatParameter)
        End If
        logEntry.CSUriStem = identifierChar

      Case _STRU
        Dim notImplemented As Boolean = False
        Dim structureIdentifier As Char = arguments(0)
        If (_Connection.SetFileStructureType(structureIdentifier, notImplemented)) Then
          response = "200 Command OK"
        Else
          If (notImplemented) Then
            response = String.Format("504 STRU not implemented for ""{0}""", structureIdentifier)
          Else
            response = String.Format("501 Parameter {0} not recognized", structureIdentifier)
          End If
        End If

      Case _MODE
        If (_Connection.SetMode(arguments)) Then
          response = "200 OK"
        Else
          response = "504 Command not implemented for that parameter"
        End If

      Case _SYST
        response = FtpMessages.GetMessage(FtpCommunicationMessage.SystemType)

      Case _NOOP, _ACCT, _ALLO
        response = FtpMessages.GetMessage(FtpCommunicationMessage.OK)

      Case _NLST, _SITE, _STAT, _HELP, _SMNT, _REST, _ABOR
        response = FtpMessages.GetMessage(FtpCommunicationMessage.UnknownCommand)

      Case _AUTH '(extension defined by RFC 2228)
        If (arguments = "TLS") Then
          response = "234 Enabling TLS Connection"
        Else
          response = "504 Unrecognized AUTH mode"
        End If

      Case _FEAT '(extension defined by RFC 2389)
        _Connection.ControlWriter.WriteLine(FtpMessages.GetMessage(FtpCommunicationMessage.ExtensionsSupportedBegin))
        _Connection.ControlWriter.WriteLine(" " & _MDTM)
        _Connection.ControlWriter.WriteLine(" " & _SIZE)
        response = FtpMessages.GetMessage(FtpCommunicationMessage.ExtensionsSupportedEnd)

      Case _OPTS '(extension defined by RFC 2389)
        response = FtpMessages.GetMessage(FtpCommunicationMessage.OK)

      Case _EPRT '(extension defined by RFC 2428)
        _Connection.ActiveModePortEx(arguments, logEntry.CPort)
        response = FtpMessages.GetMessage(FtpCommunicationMessage.DataConnectionEstablished)

      Case _EPSV '(extension defined by RFC 2428)
        Dim passiveEndpointInfo As Integer = _Connection.EnterPassiveModeEx(logEntry.SPort)
        response = String.Format(FtpMessages.GetMessage(FtpCommunicationMessage.EnteringExtendedPassiveMode), passiveEndpointInfo)

    End Select

    Return response
  End Function

End Class
