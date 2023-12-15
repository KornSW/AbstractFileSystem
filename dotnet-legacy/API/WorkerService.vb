Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Text
Imports KSW.Base
Imports KSW.AFS

Public Class WorkerService

  Public Property FileName As String

  Public Sub Run(continueRunning As Func(Of Boolean), logging As LoggingChannel)
    While (continueRunning.Invoke() = True)

      logging.WriteLine(LogMessageType.Information, Guid.NewGuid.ToString())
      Trace.WriteLine("HALLO")

      Threading.Thread.Sleep(2000)

    End While
  End Sub

End Class
