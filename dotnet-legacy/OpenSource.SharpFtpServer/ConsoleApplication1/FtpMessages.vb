
Public Enum FtpCommunicationMessage As Integer
  OK = 200
  UnknownCommand = 502
  NotLoggedIn = 530
  ServiceClosingControlConnection = 221
  ExtensionsSupportedBegin = 211
  ExtensionsSupportedEnd = 2110
  SystemType = 215
  ServiceReady = 220
  TransferCompleted = 226
  UsernameOkNeedPass = 331
  LoggedIn = 230
  EnteringExtendedPassiveMode = 229
  EnteringPassiveMode = 227
  DataConnectionEstablished = 2000
  CommandNotImplementedForThatParameter = 504
End Enum

Public Class FtpMessages

  Public Shared Function GetMessage(ftpMessage As FtpCommunicationMessage) As String

    Select Case ftpMessage

      Case FtpCommunicationMessage.UnknownCommand : Return "502 Command not implemented"

        'Case FtpCommunicationMessage.ddd : Return " MDTM"
        'Case FtpCommunicationMessage.ddd : Return " SIZE"

        'Case FtpCommunicationMessage.ddd : Return "150 Opening {0} mode data transfer for APPE"
        'Case FtpCommunicationMessage.ddd : Return "150 Opening {0} mode data transfer for LIST"
        'Case FtpCommunicationMessage.ddd : Return "150 Opening {0} mode data transfer for RETR"
        'Case FtpCommunicationMessage.ddd : Return "150 Opening {0} mode data transfer for STOU"

      Case FtpCommunicationMessage.OK : Return "200 OK"
        'Case FtpCommunicationMessage.ddd : Return "200 Looks good to me..."
        'Case FtpCommunicationMessage.ddd : Return "200 Command OK"
      Case FtpCommunicationMessage.DataConnectionEstablished : Return "200 Data Connection Established"
        'Case FtpCommunicationMessage.ddd : Return "200 Type set to {0}"

      Case FtpCommunicationMessage.ExtensionsSupportedBegin : Return "211- Extensions supported:"
      Case FtpCommunicationMessage.ExtensionsSupportedEnd : Return "211 End"

      Case FtpCommunicationMessage.SystemType : Return "215 UNIX Type: L8"

      Case FtpCommunicationMessage.ServiceReady : Return "220 Service Ready."


      Case FtpCommunicationMessage.ServiceClosingControlConnection : Return "221 Service closing control connection"

      Case FtpCommunicationMessage.TransferCompleted : Return "226 Closing data connection, file transfer successful"


      Case FtpCommunicationMessage.EnteringPassiveMode : Return "227 Entering Passive Mode ({0},{1},{2},{3},{4},{5})"
      Case FtpCommunicationMessage.EnteringExtendedPassiveMode : Return "229 Entering Extended Passive Mode (|||{0}|)"

      Case FtpCommunicationMessage.LoggedIn : Return "230 User logged in"



        'Case FtpCommunicationMessage.ddd : Return "234 Enabling TLS Connection"

      Case FtpCommunicationMessage.UsernameOkNeedPass : Return "331 Username ok, need password"

        'Case FtpCommunicationMessage.ddd : Return "350 Requested file action pending further information"

        'Case FtpCommunicationMessage.ddd : Return "501 Parameter {0} not recognized"
        'Case FtpCommunicationMessage.ddd : Return "502 Command not implemented"

        'Case FtpCommunicationMessage.ddd : Return "504 STRU not implemented for ""{0}"""
      Case FtpCommunicationMessage.CommandNotImplementedForThatParameter : Return "504 Command not implemented for that parameter"
        'Case FtpCommunicationMessage.ddd : Return "504 Unrecognized AUTH mode"

      Case FtpCommunicationMessage.NotLoggedIn : Return "530 Not logged in"

      Case Else : Return String.Empty
    End Select

  End Function

End Class
