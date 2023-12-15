Imports System

Public Interface ILog
  Sub Info(newEnty As Object)
  Sub [Error](newEnty As Object)
End Interface

Public Class MyLog
  Implements ILog

  Public Sub [Error](newEntry As Object) Implements ILog.Error
    Console.WriteLine(newEntry.ToString())
  End Sub

  Public Sub Info(newEntry As Object) Implements ILog.Info
    Console.WriteLine(newEntry.ToString())
  End Sub

End Class

Public Class LogManager
  Public Shared Function GetLogger(targetType As System.Type) As ILog
    Return New MyLog()
  End Function
End Class
