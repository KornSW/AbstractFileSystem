Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Net.Security
Imports System.Net.Sockets
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Friend Enum TransferType
  Ascii
  Ebcdic
  Image
  Local
End Enum

Friend Enum FormatControlType
  NonPrint
  Telnet
  CarriageControl
End Enum

Friend Enum DataConnectionType
  Passive
  Active
End Enum

Friend Enum FileStructureType
  File
  Record
  Page
End Enum
