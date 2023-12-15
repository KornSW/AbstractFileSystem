Imports System
Imports System.IO
Imports System.Security

Public Class AfsFtpServiceConfig

  Public Property ConfigValue1 As String = ""

  Public Property LoopbackOnly As Boolean = True

  Public Property Users As FtpUserConfig()

End Class

Public Class FtpUserConfig

  Public Property Disabled As Boolean = False
  Public Property FtpLogonAlias As String = Nothing
  Public Property FtpLogonPassword As SecureString = Nothing

  Public Property ImpersonationAlias As String = Nothing
  Public Property ImpersonationPassword As SecureString = Nothing

  Public Property AfsConfigurationFile As FileInfo = Nothing

End Class