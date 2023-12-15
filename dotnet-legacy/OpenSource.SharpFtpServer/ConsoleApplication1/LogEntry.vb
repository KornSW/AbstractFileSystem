Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

' Fields: date time c-ip c-port cs-username cs-method cs-uri-stem sc-status sc-bytes cs-bytes s-name s-port

Public Class LogEntry
  Public Property [Date]() As DateTime
    Get
      Return m_Date
    End Get
    Set(value As DateTime)
      m_Date = value
    End Set
  End Property
  Private m_Date As DateTime
  Public Property CIP() As String
    Get
      Return m_CIP
    End Get
    Set(value As String)
      m_CIP = value
    End Set
  End Property
  Private m_CIP As String
  Public Property CPort() As String
    Get
      Return m_CPort
    End Get
    Set(value As String)
      m_CPort = value
    End Set
  End Property
  Private m_CPort As String
  Public Property CSUsername() As String
    Get
      Return m_CSUsername
    End Get
    Set(value As String)
      m_CSUsername = value
    End Set
  End Property
  Private m_CSUsername As String
  Public Property CSMethod() As String
    Get
      Return m_CSMethod
    End Get
    Set(value As String)
      m_CSMethod = value
    End Set
  End Property
  Private m_CSMethod As String
  Public Property CSUriStem() As String
    Get
      Return m_CSUriStem
    End Get
    Set(value As String)
      m_CSUriStem = value
    End Set
  End Property
  Private m_CSUriStem As String
  Public Property SCStatus() As String
    Get
      Return m_SCStatus
    End Get
    Set(value As String)
      m_SCStatus = value
    End Set
  End Property
  Private m_SCStatus As String
  Public Property SCBytes() As String
    Get
      Return m_SCBytes
    End Get
    Set(value As String)
      m_SCBytes = value
    End Set
  End Property
  Private m_SCBytes As String
  Public Property CSBytes() As String
    Get
      Return m_CSBytes
    End Get
    Set(value As String)
      m_CSBytes = value
    End Set
  End Property
  Private m_CSBytes As String
  Public Property SName() As String
    Get
      Return m_SName
    End Get
    Set(value As String)
      m_SName = value
    End Set
  End Property
  Private m_SName As String
  Public Property SPort() As String
    Get
      Return m_SPort
    End Get
    Set(value As String)
      m_SPort = value
    End Set
  End Property
  Private m_SPort As String

  Public Overrides Function ToString() As String
    Return String.Format("{0:yyyy-MM-dd HH:mm:ss} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}", [Date], CIP, If(CPort, "-"), CSUsername, CSMethod, _
     If(CSUriStem, "-"), SCStatus, If(SCBytes, "-"), If(CSBytes, "-"), If(SName, "-"), If(SPort, "-"))
  End Function
End Class
